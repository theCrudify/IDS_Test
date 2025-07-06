// FILE LOCATION: src/PO.Infrastructure/Repositories/Interfaces/IGenericRepository.cs
// DESCRIPTION: Generic repository interface defining common CRUD operations for all entities

using System.Linq.Expressions;
using PO.Shared.Common;

namespace PO.Infrastructure.Repositories.Interfaces;

/// <summary>
/// Generic repository interface providing common CRUD operations
/// </summary>
/// <typeparam name="T">Entity type</typeparam>
public interface IGenericRepository<T> where T : class
{
    /// <summary>
    /// Get entity by ID
    /// </summary>
    Task<T?> GetByIdAsync(int id);

    /// <summary>
    /// Get all entities
    /// </summary>
    Task<IEnumerable<T>> GetAllAsync();

    /// <summary>
    /// Get entities with pagination
    /// </summary>
    Task<PagedResult<T>> GetPagedAsync(PagedRequest request);

    /// <summary>
    /// Get entities with pagination and custom filtering
    /// </summary>
    Task<PagedResult<T>> GetPagedAsync(
        int pageNumber,
        int pageSize,
        Expression<Func<T, bool>>? filter = null,
        Expression<Func<T, object>>? orderBy = null);

    /// <summary>
    /// Find entities matching the predicate
    /// </summary>
    Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Get single entity matching the predicate
    /// </summary>
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Check if any entity matches the predicate
    /// </summary>
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);

    /// <summary>
    /// Count entities matching the predicate
    /// </summary>
    Task<int> CountAsync(Expression<Func<T, bool>>? predicate = null);

    /// <summary>
    /// Add new entity
    /// </summary>
    Task<T> AddAsync(T entity);

    /// <summary>
    /// Add multiple entities
    /// </summary>
    Task AddRangeAsync(IEnumerable<T> entities);

    /// <summary>
    /// Update existing entity
    /// </summary>
    Task<T> UpdateAsync(T entity);

    /// <summary>
    /// Delete entity by ID
    /// </summary>
    Task<bool> DeleteAsync(int id);

    /// <summary>
    /// Delete entity
    /// </summary>
    Task<bool> DeleteAsync(T entity);

    /// <summary>
    /// Soft delete entity by ID (if entity supports soft delete)
    /// </summary>
    Task<bool> SoftDeleteAsync(int id);

    /// <summary>
    /// Get entities with included related data
    /// </summary>
    Task<IEnumerable<T>> GetWithIncludesAsync(params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Get paged entities with included related data
    /// </summary>
    Task<PagedResult<T>> GetPagedWithIncludesAsync(
        PagedRequest request, 
        params Expression<Func<T, object>>[] includes);

    /// <summary>
    /// Execute raw SQL query
    /// </summary>
    Task<IEnumerable<T>> ExecuteRawSqlAsync(string sql, params object[] parameters);

    /// <summary>
    /// Save changes to database
    /// </summary>
    Task<int> SaveChangesAsync();
}