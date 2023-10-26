using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPIAutores.Validaciones;

namespace WebAPIAutores.Entidades
{
    public class Autor:IValidatableObject {
        public int Id { get; set; }
        [Required(ErrorMessage = "El {0} es requerido")]
        //[PrimeraLetraMayuscula]
        [StringLength(maximumLength: 120, ErrorMessage = "El campo {0} no debe de tener mas de {1} carecteres")]
        public String Nombre { get; set; }
        /*
        [Range(18,120)]
        [NotMapped]
        public int Edad { get; set; }
        [CreditCard]
        [NotMapped]
        public String  TarjetaDeCredito { get; set; }
        [Url]
        [NotMapped]
        public String URL { get; set; }
                */
        public List<Libro> Libros { get; set; }

        //public int Menor { get; set; }
       // public int Mayor { get; set; }
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrEmpty(Nombre))
            {
                var primeraLetra = Nombre[0].ToString();
                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera netra debe de ser Mayus.", new string[] {nameof(Nombre)});
                }
            }
            /*
            if(Menor>Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser mas grande que el campo Mayor",
                    new string[] {nameof(Menor)});
            }
            */
        }   
    }
}