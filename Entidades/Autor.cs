namespace WebAPIAutores.Entidades
{
    public class Autor{
        public int Id {get; set;}
        public String Nombre { get; set; }
        public List<Libro> Libros { get; set; }
    }
}