using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi_emergencias.Models
{
    public class Emergencia
    {
        [Key]
        public int IdEmergencia { get; set; }
        
        [Required(ErrorMessage = "La descripción es obligatoria")]
        [StringLength(500, ErrorMessage = "La descripción no puede exceder los 500 caracteres")]
        [MinLength(10, ErrorMessage = "La descripción debe tener al menos 10 caracteres")]
        public string Descripcion { get; set; }

        [StringLength(100, ErrorMessage = "El tipo no puede exceder los 50 caracteres")]
        [Required(ErrorMessage = "El tipo es obligatorio")]
        public string Tipo { get; set; }

        [StringLength(200, ErrorMessage = "La gravedad no puede exceder los 50 caracteres")]
        [Required(ErrorMessage = "La gravedad es obligatorio")]
        public string Gravedad { get; set; }

        [Range(0, 1, ErrorMessage = "La urgencia debe estar entre 0 y 1")]
        [Required(ErrorMessage = "La urgencia es obligatoria")]
        public int Urgencia { get; set; }
        [StringLength(100)]
        public String Foto { get; set; }
        [StringLength(200)]
        public String Ruta { get; set; }
        [NotMapped]
        public IFormFile Archivo { get; set; }


    }
}
