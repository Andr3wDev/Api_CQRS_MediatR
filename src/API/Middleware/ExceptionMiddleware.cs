using Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IHostEnvironment _env;
        public ILogger<ExceptionMiddleware> _logger { get; }

        public ExceptionMiddleware(
            RequestDelegate next,
            IHostEnvironment env,
            ILogger<ExceptionMiddleware> logger)
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
            catch (Exception exception)
            {
                string errorId = Guid.NewGuid().ToString();

                var errorResult = new ErrorResult
                {
                    Source = exception.TargetSite?.DeclaringType?.FullName,
                    Exception = exception.Message.Trim(),
                    ErrorId = errorId,
                    SupportMessage = $"Provide the ErrorId {errorId} to the support team for further analysis."
                };

                if (exception is not CustomException && exception.InnerException != null)
                {
                    while (exception.InnerException != null)
                    {
                        exception = exception.InnerException;
                    }
                }

                if (exception is FluentValidation.ValidationException fluentException)
                {
                    errorResult.Exception = "One or More Validations failed.";
                    foreach (var error in fluentException.Errors)
                    {
                        errorResult.Messages.Add(error.ErrorMessage);
                    }
                }

                switch (exception)
                {
                    case CustomException e:
                        errorResult.StatusCode = (int)e.StatusCode;
                        if (e.ErrorMessages is not null)
                        {
                            errorResult.Messages = e.ErrorMessages;
                        }
                        break;
                    case FluentValidation.ValidationException:
                        errorResult.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    default:
                        errorResult.StatusCode = (int)HttpStatusCode.InternalServerError;
                        break;
                }

                _logger.LogError($"{errorResult.Exception} Request failed with Status Code {errorResult.StatusCode} and Error Id {errorId}.");
                var response = context.Response;
                if (!response.HasStarted)
                {
                    response.ContentType = "application/json";
                    response.StatusCode = errorResult.StatusCode;
                    await response.WriteAsync(JsonSerializer.Serialize(errorResult));
                }
                else
                {
                    _logger.LogWarning("Can't write error response. Response has already started.");
                }
            }
        }
    }
}