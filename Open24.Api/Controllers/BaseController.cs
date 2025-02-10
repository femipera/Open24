using Microsoft.AspNetCore.Mvc;
using Open24.Shared.DTOs.Responses;

namespace Open24.Api.Controllers;

[ApiController]
public abstract class BaseController : ControllerBase
{
    protected ApiResponse<T> CreateErrorResponse<T>(int statusCode, string errorMessage)
    {
        return new ApiResponse<T>
        {
            Success = false,
            StatusCode = statusCode,
            ErrorMessage = errorMessage
        };
    }

    protected ApiResponse<T> CreateSuccessResponse<T>(T data, int statusCode)
    {
        return new ApiResponse<T>
        {
            Success = true,
            StatusCode = statusCode,
            Data = data
        };
    }

    protected async Task<IActionResult> ExecuteAsync<T>(Func<Task<ApiResponse<T>>> action)
    {
        try
        {
            var response = await action();
            return StatusCode(response.StatusCode, response);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status400BadRequest, new ApiResponse<T>
            {
                Success = false,
                StatusCode = StatusCodes.Status400BadRequest,
                ErrorMessage = ex.Message
            });
        }
    }
}

