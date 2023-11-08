using Microsoft.AspNetCore.Mvc.Filters;

namespace WebAPIAutores.Filtros
{
    public class FiltroDeExcepcion :ExceptionFilterAttribute
    {
        private readonly ILogger logger;

        public FiltroDeExcepcion(ILogger<FiltroDeExcepcion> logger)
        {
            this.logger = logger;
        }
        public override void OnException(ExceptionContext ex)
        {
            logger.LogError(ex.Exception, ex.Exception.Message);
            base.OnException(ex);

        }

    }
}
