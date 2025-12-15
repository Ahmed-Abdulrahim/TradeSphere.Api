

namespace TradeSphere.Api.Extensions
{
    public static class ErrorValidationExtension
    {
        public static IServiceCollection ValidationResponse(this IServiceCollection service)
        {
            service.Configure<ApiBehaviorOptions>(config =>
            {
                config.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value!.Errors.Count() > 0)
                    .SelectMany(x => x.Value!.Errors)
                    .Select(e => e.ErrorMessage);
                    var response = new ApiValidationErrorResponse
                    {
                        Errors = errors.ToArray()
                    };
                    return new BadRequestObjectResult(response);
                };

            });
            return service;
        }
    }
}
