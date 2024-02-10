using Api.Middlewares;
using FluentValidation;

namespace WebApi.Helpers;

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class ErrorHandlerMiddleware(RequestDelegate next, ILogger<ErrorHandlerMiddleware> logger)
{
    private readonly ILogger _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            string result;
            switch (ex)
            {
                case CustomException e:
                    // custom exception
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;
                case KeyNotFoundException e:
                    // not found exception
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;
                case ValidationException e:
                    // Fluent Validation exception
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    var errors = new List<string>();
                    foreach (var error in e.Errors)
                    {
                        errors.Add(error.ErrorMessage);
                    }
                    result = JsonSerializer.Serialize(new { message = "Validation failed", errors });
                    await response.WriteAsync(result);
                    return;
                }
                default:
                    // unhandled exception
                    _logger.LogError(ex, ex.Message);
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

             result = JsonSerializer.Serialize(new { message = ex.Message });
            await response.WriteAsync(result);
        }
    }
}