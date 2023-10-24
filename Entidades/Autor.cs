using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Entidades
{
    public class Autor {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        [PrimeraLetraMayuscula]
        [StringLength(maximumLength: 5, ErrorMessage = "El campo {0} no debe de tener mas de {1} carecteres")]
        public String Nombre { get; set; }
        [Range(18,120)]
        [NotMapped]
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public String  TarjetaDeCredito { get; set; }
        [Url]
        [NotMapped]
        public String URL { get; set; }
        public List<Libro> Libros { get; set; }
    }
}