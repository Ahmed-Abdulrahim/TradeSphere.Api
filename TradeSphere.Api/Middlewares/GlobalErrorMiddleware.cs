namespace TradeSphere.Api.Middlewares
{
    public class GlobalErrorMiddleware(RequestDelegate next, ILogger<GlobalErrorMiddleware> logger, IWebHostEnvironment env)
    {
        public async Task Invoke(HttpContext context)
        {
            try
            {
                await next.Invoke(context);
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);

                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "application/json";
                var response = env.IsDevelopment()
                   ? new ApiExceptionHandler(StatusCodes.Status500InternalServerError, ex.Message, ex.StackTrace!.ToString())
                   : new ApiExceptionHandler(StatusCodes.Status500InternalServerError, "Internal Server Error", "An unexpected error occurred. Please try again later.");
                var options = new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };
                var json = JsonSerializer.Serialize(response, options);
                await context.Response.WriteAsync(json);
            }
        }

    }
}

