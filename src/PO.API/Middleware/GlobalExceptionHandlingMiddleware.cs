// FILE LOCATION: src/PO.API/Middleware/GlobalExceptionHandlingMiddleware.cs
// DESCRIPTION: Global exception handling middleware for consistent error responses

using PO.Shared.Common;
using System.Net;
using System.Text.Json;

namespace PO.API.Middleware;

/// <summary>
/// Global exception handling middleware to catch and format all unhandled exceptions
/// </summary>
public class GlobalExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;

    public GlobalExceptionHandlingMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "An unhandled exception occurred. Request: {Method} {Path}", 
                context.Request.Method, context.Request.Path);

            await HandleExceptionAsync(context, exception);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var result = exception switch
        {
            ArgumentNullException => ApiResult.ErrorResult(
                "A required parameter was not provided.", 
                (int)HttpStatusCode.BadRequest),
            
            ArgumentException => ApiResult.ErrorResult(
                "Invalid parameter provided.", 
                (int)HttpStatusCode.BadRequest),
            
            UnauthorizedAccessException => ApiResult.ErrorResult(
                "Unauthorized access.", 
                (int)HttpStatusCode.Unauthorized),
            
            KeyNotFoundException => ApiResult.ErrorResult(
                "The requested resource was not found.", 
                (int)HttpStatusCode.NotFound),
            
            InvalidOperationException => ApiResult.ErrorResult(
                "The operation is not valid in the current state.", 
                (int)HttpStatusCode.BadRequest),
            
            TimeoutException => ApiResult.ErrorResult(
                "The operation timed out.", 
                (int)HttpStatusCode.RequestTimeout),
            
            NotSupportedException => ApiResult.ErrorResult(
                "The operation is not supported.", 
                (int)HttpStatusCode.BadRequest),
            
            _ => ApiResult.ErrorResult(
                "An internal server error occurred.", 
                (int)HttpStatusCode.InternalServerError)
        };

        context.Response.StatusCode = result.StatusCode;

        var jsonResponse = JsonSerializer.Serialize(result, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            WriteIndented = true
        });

        await context.Response.WriteAsync(jsonResponse);
    }
}