namespace WebAPIAutores.Servicios
{
    public interface IServicio
    {
        void RealizarTarea();
    }
    public class ServicioA : IServicio
    {
        private readonly ILogger<ServicioA> logger;
        public ServicioA(ILogger<ServicioA> logger)
        {
            this.logger = logger;
        }
        public void RealizarTarea()
        {

        }
  
    }   
    public class ServicioB : IServicio
    {
        public void RealizarTarea()
        {

        }
    }
}
