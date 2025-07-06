using Microsoft.AspNetCore.Mvc;
using PO.Shared.Common;

namespace PO.API.Controllers;

/// <summary>
/// Test controller for API functionality
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Tags("Test")]
public class TestController : BaseController
{
    private readonly ILogger<TestController> _logger;

    public TestController(ILogger<TestController> logger)
    {
        _logger = logger;
    }

    /// <summary>
    /// Test endpoint to verify API is working
    /// </summary>
    /// <returns>Test response</returns>
    [HttpGet]
    [ProducesResponseType(typeof(ApiResult<string>), 200)]
    public ActionResult<ApiResult<string>> Get()
    {
        _logger.LogInformation("Test endpoint called");
        return CreateResponse(ApiResult<string>.SuccessResult("Purchase Order API is running successfully!"));
    }

    /// <summary>
    /// Health check endpoint
    /// </summary>
    /// <returns>Health status</returns>
    [HttpGet("health")]
    [ProducesResponseType(typeof(ApiResult<object>), 200)]
    public ActionResult<ApiResult<object>> Health()
    {
        _logger.LogInformation("Health check endpoint called");
        return CreateResponse(ApiResult<object>.SuccessResult(new { 
            Status = "Healthy",
            Timestamp = DateTime.UtcNow,
            Environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")
        }));
    }
}