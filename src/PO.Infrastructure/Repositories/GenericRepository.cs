// FILE LOCATION: src/PO.Infrastructure/Repositories/GenericRepository.cs
// DESCRIPTION: Generic repository implementation with EF Core providing common CRUD operations

using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PO.Domain.Common;
using PO.Infrastructure.Data;
using PO.Infrastructure.Repositories.Interfaces;
using PO.Shared.Common;

namespace PO.Infrastructure.Repositories;

/// <summary>
/// Generic repository implementation using Entity Framework Core
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public class GenericRepository<T> : IGenericRepository<T> where T : class
{
    protected readonly PODbContext _context;
    protected readonly DbSet<T> _dbSet;

    public GenericRepository(PODbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet.Where(e => !((BaseEntity)(object)e).IsDeleted).ToListAsync();
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(PagedRequest request)
    {
        request.Normalize();

        var query = _dbSet.AsQueryable();

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = ApplySearchFilter(query, request.SearchTerm);
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = ApplySorting(query, request.SortBy, request.SortDirection);
        }
        else if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            // Default sort by CreatedAt descending
            query = query.OrderByDescending(e => ((BaseEntity)(object)e).CreatedAt);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return PagedResult<T>.Create(items, request.PageNumber, request.PageSize, totalCount);
    }

    public virtual async Task<PagedResult<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? orderBy = null)
    {
        var query = _dbSet.AsQueryable();

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        // Apply custom filter if provided
        if (filter != null)
        {
            query = query.Where(filter);
        }

        var totalCount = await query.CountAsync();

        // Apply ordering if provided
        if (orderBy != null)
        {
            query = query.OrderBy(orderBy);
        }

        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return PagedResult<T>.Create(items, pageNumber, pageSize, totalCount);
    }

    public virtual async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        return await query.FirstOrDefaultAsync();
    }

    public virtual async Task<bool> AnyAsync(Expression<Func<T, bool>> predicate)
    {
        var query = _dbSet.Where(predicate);

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        return await query.AnyAsync();
    }

    public virtual async Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null)
    {
        var query = _dbSet.AsQueryable();

        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        return await query.CountAsync();
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        var result = await _dbSet.AddAsync(entity);
        return result.Entity;
    }

    public virtual async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }

    public virtual async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Attach(entity);
        _context.Entry(entity).State = EntityState.Modified;
        return await Task.FromResult(entity);
    }

    public virtual async Task<bool> DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        return await DeleteAsync(entity);
    }

    public virtual async Task<bool> DeleteAsync(T entity)
    {
        if (_context.Entry(entity).State == EntityState.Detached)
        {
            _dbSet.Attach(entity);
        }

        _dbSet.Remove(entity);
        return await Task.FromResult(true);
    }

    public virtual async Task<bool> SoftDeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity == null)
            return false;

        // Check if entity supports soft delete
        if (entity is BaseEntity baseEntity)
        {
            baseEntity.IsDeleted = true;
            baseEntity.DeletedAt = DateTime.UtcNow;
            await UpdateAsync(entity);
            return true;
        }

        // Fallback to hard delete if soft delete not supported
        return await DeleteAsync(entity);
    }

    public virtual async Task<IEnumerable<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes)
    {
        var query = _dbSet.AsQueryable();

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        // Include related entities
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        return await query.ToListAsync();
    }

    public virtual async Task<PagedResult<T>> GetPagedWithIncludesAsync(
        PagedRequest request, 
        params Expression<Func<T, object>>[] includes)
    {
        request.Normalize();

        var query = _dbSet.AsQueryable();

        // Apply soft delete filter if entity supports it
        if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.Where(e => !((BaseEntity)(object)e).IsDeleted);
        }

        // Include related entities
        foreach (var include in includes)
        {
            query = query.Include(include);
        }

        // Apply search filter if provided
        if (!string.IsNullOrWhiteSpace(request.SearchTerm))
        {
            query = ApplySearchFilter(query, request.SearchTerm);
        }

        // Apply sorting
        if (!string.IsNullOrWhiteSpace(request.SortBy))
        {
            query = ApplySorting(query, request.SortBy, request.SortDirection);
        }
        else if (typeof(BaseEntity).IsAssignableFrom(typeof(T)))
        {
            query = query.OrderByDescending(e => ((BaseEntity)(object)e).CreatedAt);
        }

        var totalCount = await query.CountAsync();

        var items = await query
            .Skip((request.PageNumber - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync();

        return PagedResult<T>.Create(items, request.PageNumber, request.PageSize, totalCount);
    }

    public virtual async Task<IEnumerable<T>> ExecuteRawSqlAsync(string sql, params object[] parameters)
    {
        return await _dbSet.FromSqlRaw(sql, parameters).ToListAsync();
    }

    public virtual async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }

    protected virtual IQueryable<T> ApplySearchFilter(IQueryable<T> query, string searchTerm)
    {
        // Override in derived repositories for entity-specific search
        return query;
    }

    protected virtual IQueryable<T> ApplySorting(IQueryable<T> query, string sortBy, string sortDirection)
    {
        // Basic reflection-based sorting
        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.Property(parameter, sortBy);
        var lambda = Expression.Lambda(property, parameter);

        var methodName = sortDirection.ToLower() == "desc" ? "OrderByDescending" : "OrderBy";
        var resultExpression = Expression.Call(
            typeof(Queryable),
            methodName,
            new Type[] { typeof(T), property.Type },
            query.Expression,
            Expression.Quote(lambda));

        return query.Provider.CreateQuery<T>(resultExpression);
    }
}