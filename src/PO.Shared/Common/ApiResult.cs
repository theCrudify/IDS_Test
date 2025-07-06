// FILE LOCATION: src/PO.Shared/Common/ApiResult.cs
// DESCRIPTION: Common API response wrapper for consistent response format across all endpoints

namespace PO.Shared.Common;

/// <summary>
/// Generic API result wrapper for consistent response format
/// </summary>
/// <typeparam name="T">Type of data being returned</typeparam>
public class ApiResult<T>
{
    /// <summary>
    /// Indicates if the operation was successful
    /// </summary>
    public bool Success { get; set; }

    /// <summary>
    /// The actual data payload
    /// </summary>
    public T? Data { get; set; }

    /// <summary>
    /// Error message if operation failed
    /// </summary>
    public string? Message { get; set; }

    /// <summary>
    /// List of error details (for validation errors)
    /// </summary>
    public List<string>? Errors { get; set; }

    /// <summary>
    /// HTTP status code
    /// </summary>
    public int StatusCode { get; set; }

    /// <summary>
    /// Timestamp of the response
    /// </summary>
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Creates a successful result with data
    /// </summary>
    public static ApiResult<T> SuccessResult(T data, string? message = null)
    {
        return new ApiResult<T>
        {
            Success = true,
            Data = data,
            Message = message ?? "Operation completed successfully",
            StatusCode = 200
        };
    }

    /// <summary>
    /// Creates a successful result without data
    /// </summary>
    public static ApiResult<T> SuccessResult(string? message = null)
    {
        return new ApiResult<T>
        {
            Success = true,
            Message = message ?? "Operation completed successfully",
            StatusCode = 200
        };
    }

    /// <summary>
    /// Creates an error result
    /// </summary>
    public static ApiResult<T> ErrorResult(string message, int statusCode = 400, List<string>? errors = null)
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }

    /// <summary>
    /// Creates a validation error result
    /// </summary>
    public static ApiResult<T> ValidationError(List<string> errors)
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = "Validation failed",
            Errors = errors,
            StatusCode = 400
        };
    }

    /// <summary>
    /// Creates a not found result
    /// </summary>
    public static ApiResult<T> NotFoundResult(string? message = null)
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message ?? "Resource not found",
            StatusCode = 404
        };
    }

    /// <summary>
    /// Creates an unauthorized result
    /// </summary>
    public static ApiResult<T> UnauthorizedResult(string? message = null)
    {
        return new ApiResult<T>
        {
            Success = false,
            Message = message ?? "Unauthorized access",
            StatusCode = 401
        };
    }
}

/// <summary>
/// Non-generic API result for operations that don't return data
/// </summary>
public class ApiResult : ApiResult<object>
{
    /// <summary>
    /// Creates a successful result without data
    /// </summary>
    public static new ApiResult SuccessResult(string? message = null)
    {
        return new ApiResult
        {
            Success = true,
            Message = message ?? "Operation completed successfully",
            StatusCode = 200
        };
    }

    /// <summary>
    /// Creates an error result
    /// </summary>
    public static new ApiResult ErrorResult(string message, int statusCode = 400, List<string>? errors = null)
    {
        return new ApiResult
        {
            Success = false,
            Message = message,
            Errors = errors,
            StatusCode = statusCode
        };
    }
}