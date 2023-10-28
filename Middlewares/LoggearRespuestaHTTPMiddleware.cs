using Microsoft.Extensions.Logging;

namespace WebAPIAutores.Middlewares
{
    public static class LoggearRespuestaHTTPMiddlewareExtensions
    {
        public static IApplicationBuilder UseLoggearRespuestaHTTP(this IApplicationBuilder app)
        {
            return app.UseMiddleware<LoggearRespuestaHTTPMiddleware>();
        }
    }
    public class LoggearRespuestaHTTPMiddleware
    {
        private readonly RequestDelegate siguiente;
        private readonly ILogger<LoggearRespuestaHTTPMiddleware> logger;

        public LoggearRespuestaHTTPMiddleware(RequestDelegate siguiente,ILogger<LoggearRespuestaHTTPMiddleware> logger)
        {
            this.siguiente = siguiente;
            this.logger = logger;
        }
        //Invoke o InvokeAsync

        public async Task InvokeAsync(HttpContext contexto)
        {
            using (var ms = new MemoryStream())
            {
                var cueroOriginalResouesta = contexto.Response.Body;
                contexto.Response.Body = ms;
                await siguiente(contexto);
                ms.Seek(0, SeekOrigin.Begin);
                string resouesta = new StreamReader(ms).ReadToEnd();
                ms.Seek(0, SeekOrigin.Begin);
                await ms.CopyToAsync(cueroOriginalResouesta);
                contexto.Response.Body = cueroOriginalResouesta;
                logger.LogInformation(resouesta);
            }
        }
    }
}
