namespace TradeSphere.Api.Errors
{
    public class ApiExceptionHandler : ApiResponse
    {
        public string Description { get; set; }

        public ApiExceptionHandler(int statusCode, string message = null, string description = null) : base(statusCode, message)
        {
            Description = description;
        }

    }
}
