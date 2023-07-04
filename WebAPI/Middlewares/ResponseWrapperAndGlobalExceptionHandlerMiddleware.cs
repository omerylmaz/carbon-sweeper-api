using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;

namespace WebAPI.Middlewares
{
    public class ResponseWrapperAndGlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDictionary<Type, HttpStatusCode> exceptionHttpStatusCodeMap = new Dictionary<Type, HttpStatusCode>
    {
        { typeof(ValidationException), HttpStatusCode.BadRequest },

    };
        public ResponseWrapperAndGlobalExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;
            var jsonSerializerSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };
            try
            {
                using var responseBody = new MemoryStream();
                context.Response.Body = responseBody;
                await _next(context);
                if (context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
                {
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var responseData = await new StreamReader(responseBody).ReadToEndAsync();
                    responseBody.Seek(0, SeekOrigin.Begin);
                    var response = new ApiResponseWrapper
                    {
                        Data = JsonConvert.DeserializeObject(responseData), //todo data null ise boş yere null gelmesin
                        MetaData = new { version = "1.0" }
                    };
                    context.Response.ContentType = "application/json";
                    var json = JsonConvert.SerializeObject(response, jsonSerializerSettings);
                    await context.Response.WriteAsync(json);
                }
                responseBody.Seek(0, SeekOrigin.Begin);
                await responseBody.CopyToAsync(originalBodyStream);
            }
            //todo burada hata alıyorum arada : too many bytes are written 72 of 0 -> response null olduğunda hata veriyor
            //todo 400 hata koşulu için if verilmemiş.
            catch (Exception ex)
            {
                HttpStatusCode code = GetHttpStatusCode(ex) ?? HttpStatusCode.InternalServerError;

                context.Response.StatusCode = (int)code;
                context.Response.ContentType = "application/json";
                var error = new ApiResponseWrapper { Error = ex.Message, MetaData = new { version = "1.0" }, HasError = true, StackTrace = ex.StackTrace };
                var json = JsonConvert.SerializeObject(error, jsonSerializerSettings);
                using var errorResponseBody = new MemoryStream();
                await errorResponseBody.WriteAsync(Encoding.UTF8.GetBytes(json));
                errorResponseBody.Seek(0, SeekOrigin.Begin);
                await errorResponseBody.CopyToAsync(originalBodyStream);
            }
        }
        private HttpStatusCode? GetHttpStatusCode<TException>(TException exception)
        {
            if (exceptionHttpStatusCodeMap.TryGetValue(exception.GetType(), out HttpStatusCode code))
            {
                return code;
            }
            return null;
        }
    }
    public class ApiResponseWrapper
    {
        public object? Data { get; set; }
        public bool HasError { get; set; } = false;
        public string? Error { get; set; }
        public string? StackTrace { get; set; }
        public object? MetaData { get; set; } //todo bunun ne olduğunu öğren intten
    }
}
