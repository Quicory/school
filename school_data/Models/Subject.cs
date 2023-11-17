using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace School_Data.Models
{
    public class Subject
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la asignatura es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener al menos {2} de longitud.", MinimumLength = 2)]
        public string Name { get; set; }
        public DateTime? create_at { get; set; } = DateTime.Now;
        public DateTime? update_at { get; set; } = DateTime.Now;
        public List<Teacher> Teachers { get; set; } = new();
    }
}

