// FILE LOCATION: src/PO.Shared/Common/PagedResult.cs
// DESCRIPTION: Paged result wrapper for handling paginated data responses

namespace PO.Shared.Common;

/// <summary>
/// Represents a paged result set with metadata
/// </summary>
/// <typeparam name="T">Type of items in the result set</typeparam>
public class PagedResult<T>
{
    /// <summary>
    /// The actual data items for the current page
    /// </summary>
    public IEnumerable<T> Items { get; set; } = new List<T>();

    /// <summary>
    /// Current page number (1-based)
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Number of items per page
    /// </summary>
    public int PageSize { get; set; }

    /// <summary>
    /// Total number of items across all pages
    /// </summary>
    public int TotalCount { get; set; }

    /// <summary>
    /// Total number of pages
    /// </summary>
    public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);

    /// <summary>
    /// Whether there is a previous page
    /// </summary>
    public bool HasPreviousPage => PageNumber > 1;

    /// <summary>
    /// Whether there is a next page
    /// </summary>
    public bool HasNextPage => PageNumber < TotalPages;

    /// <summary>
    /// Number of items in the current page
    /// </summary>
    public int CurrentPageSize => Items.Count();

    /// <summary>
    /// Creates a new paged result
    /// </summary>
    public static PagedResult<T> Create(IEnumerable<T> items, int pageNumber, int pageSize, int totalCount)
    {
        return new PagedResult<T>
        {
            Items = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount
        };
    }
}

/// <summary>
/// Parameters for requesting paged data
/// </summary>
public class PagedRequest
{
    /// <summary>
    /// Page number (1-based, default = 1)
    /// </summary>
    public int PageNumber { get; set; } = 1;

    /// <summary>
    /// Number of items per page (default = 10, max = 100)
    /// </summary>
    public int PageSize { get; set; } = 10;

    /// <summary>
    /// Search term for filtering
    /// </summary>
    public string? SearchTerm { get; set; }

    /// <summary>
    /// Sort field name
    /// </summary>
    public string? SortBy { get; set; }

    /// <summary>
    /// Sort direction (asc/desc)
    /// </summary>
    public string SortDirection { get; set; } = "asc";

    /// <summary>
    /// Validates and normalizes the paging parameters
    /// </summary>
    public void Normalize()
    {
        if (PageNumber < 1) PageNumber = 1;
        if (PageSize < 1) PageSize = 10;
        if (PageSize > 100) PageSize = 100;
        
        SortDirection = SortDirection?.ToLowerInvariant() == "desc" ? "desc" : "asc";
    }
}