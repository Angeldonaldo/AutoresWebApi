namespace WebAPIAutores.Servicios
{
    public class EscribirEnArchivo : IHostedService
    {
        private readonly ILogger<EscribirEnArchivo> logger;
        private readonly IWebHostEnvironment env;
        private readonly string nombreArchivo = "1.txt";
        private Timer timer;
        public EscribirEnArchivo(ILogger<EscribirEnArchivo> logger,IWebHostEnvironment env)
        {
            this.logger = logger;
            this.env = env;
        }
        Task IHostedService.StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork,null,TimeSpan.Zero,TimeSpan.FromSeconds(5));
            Escribir("Proceso Iniciado");
            return Task.CompletedTask;
        }

        Task IHostedService.StopAsync(CancellationToken cancellationToken)
        {
            timer.Dispose();
            Escribir("Proceso finializado");
            return Task.CompletedTask;
        }
        private void DoWork(Object state)
        {
            Escribir("Proceso en ejecucion"+DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss"));
        }
        private void Escribir(String mensaje)
        {
            var ruta = $@"{env.ContentRootPath}\wwwroot\{nombreArchivo}";
            using (StreamWriter writer = new StreamWriter(ruta,append:true))
            {
                writer.WriteLine(mensaje);
            }
        }
    }
}
