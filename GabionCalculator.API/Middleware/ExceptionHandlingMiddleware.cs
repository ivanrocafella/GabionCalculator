using GabionCalculator.BAL.Models;
using GabionCalculator.DAL.Exceptions;
using Newtonsoft.Json;
using UnauthorizedAccessException = GabionCalculator.DAL.Exceptions.UnauthorizedAccessException;

namespace GabionCalculator.API.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly RequestDelegate _next;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleException(context, ex);
            }
        }

        private Task HandleException(HttpContext context, Exception ex)
        {
            _logger.LogError(ex.Message);
            using (StreamWriter writer = new StreamWriter(Path.Combine(_env.ContentRootPath, "MyStaticFiles"), true))
            {
                writer.WriteLine(ex.Message);
            }

            var code = StatusCodes.Status500InternalServerError;
            var errors = new List<string> { ex.Message };

            code = ex switch
            {
                ResourceNotFoundException => StatusCodes.Status404NotFound,
                UnauthorizedAccessException => StatusCodes.Status401Unauthorized,
                ForbiddenException => StatusCodes.Status403Forbidden,
                _ => code
            };

            var result = JsonConvert.SerializeObject(ApiResult<string>.Failure(errors));

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = code;

            return context.Response.WriteAsync(result);
        }
    }
}
