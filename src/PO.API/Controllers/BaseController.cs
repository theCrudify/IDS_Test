// FILE LOCATION: src/PO.API/Controllers/BaseController.cs
// DESCRIPTION: Base controller with common functionality for all API controllers

using Microsoft.AspNetCore.Mvc;
using PO.Shared.Common;

namespace PO.API.Controllers;

/// <summary>
/// Base controller providing common functionality for all API controllers
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public abstract class BaseController : ControllerBase
{
    /// <summary>
    /// Creates an HTTP response based on the API result
    /// </summary>
    /// <typeparam name="T">Type of data in the result</typeparam>
    /// <param name="result">The API result</param>
    /// <returns>HTTP action result</returns>
    protected ActionResult<ApiResult<T>> CreateResponse<T>(ApiResult<T> result)
    {
        return result.StatusCode switch
        {
            200 => Ok(result),
            201 => Created(string.Empty, result),
            400 => BadRequest(result),
            401 => Unauthorized(result),
            403 => StatusCode(403, result),
            404 => NotFound(result),
            409 => Conflict(result),
            422 => UnprocessableEntity(result),
            500 => StatusCode(500, result),
            _ => StatusCode(result.StatusCode, result)
        };
    }

    /// <summary>
    /// Creates an HTTP response for non-generic API result
    /// </summary>
    /// <param name="result">The API result</param>
    /// <returns>HTTP action result</returns>
    protected ActionResult<ApiResult> CreateResponse(ApiResult result)
    {
        return result.StatusCode switch
        {
            200 => Ok(result),
            201 => Created(string.Empty, result),
            204 => NoContent(),
            400 => BadRequest(result),
            401 => Unauthorized(result),
            403 => StatusCode(403, result),
            404 => NotFound(result),
            409 => Conflict(result),
            422 => UnprocessableEntity(result),
            500 => StatusCode(500, result),
            _ => StatusCode(result.StatusCode, result)
        };
    }

    /// <summary>
    /// Gets the current user ID from the JWT token (when authentication is implemented)
    /// </summary>
    /// <returns>Current user ID</returns>
    protected int GetCurrentUserId()
    {
        // TODO: Implement when JWT authentication is added
        // For now, return a default user ID for development
        return 1;
    }

    /// <summary>
    /// Gets the current user's role from the JWT token (when authentication is implemented)
    /// </summary>
    /// <returns>Current user's role</returns>
    protected string GetCurrentUserRole()
    {
        // TODO: Implement when JWT authentication is added
        // For now, return a default role for development
        return "Admin";
    }

    /// <summary>
    /// Validates the model state and returns validation errors if any
    /// </summary>
    /// <returns>API result with validation errors or null if valid</returns>
    protected ApiResult? ValidateModelState()
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState
                .SelectMany(x => x.Value?.Errors ?? new Microsoft.AspNetCore.Mvc.ModelBinding.ModelErrorCollection())
                .Select(x => x.ErrorMessage)
                .ToList();

            return new ApiResult
            {
                Success = false,
                Message = "Validation failed",
                Errors = errors,
                StatusCode = 400
            };
        }

        return null;
    }

    /// <summary>
    /// Creates a success response with data
    /// </summary>
    /// <typeparam name="T">Type of data</typeparam>
    /// <param name="data">The data to return</param>
    /// <param name="message">Success message</param>
    /// <returns>Success API result</returns>
    protected ApiResult<T> Success<T>(T data, string? message = null)
    {
        return ApiResult<T>.SuccessResult(data, message);
    }

    /// <summary>
    /// Creates a success response without data
    /// </summary>
    /// <param name="message">Success message</param>
    /// <returns>Success API result</returns>
    protected ApiResult Success(string? message = null)
    {
        return ApiResult.SuccessResult(message);
    }

    /// <summary>
    /// Creates an error response
    /// </summary>
    /// <param name="message">Error message</param>
    /// <param name="statusCode">HTTP status code</param>
    /// <returns>Error API result</returns>
    protected ApiResult Error(string message, int statusCode = 400)
    {
        return ApiResult.ErrorResult(message, statusCode);
    }

    /// <summary>
    /// Handle result and return simple IActionResult
    /// </summary>
    protected IActionResult HandleResult<T>(ApiResult<T> result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        return result.StatusCode switch
        {
            400 => BadRequest(result),
            401 => Unauthorized(result),
            404 => NotFound(result),
            _ => StatusCode(result.StatusCode, result)
        };
    }

    /// <summary>
    /// Handle result and return simple IActionResult for non-generic result
    /// </summary>
    protected IActionResult HandleResult(ApiResult result)
    {
        if (result.Success)
        {
            return Ok(result);
        }

        return result.StatusCode switch
        {
            400 => BadRequest(result),
            401 => Unauthorized(result),
            404 => NotFound(result),
            _ => StatusCode(result.StatusCode, result)
        };
    }
}