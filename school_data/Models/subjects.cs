﻿

using System.ComponentModel.DataAnnotations;

namespace School_Data.Models
{
    public class Subjects
    {
        [Key]
        
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de la asignatura es requerido")]
        [StringLength(150, ErrorMessage = "El nombre debe tener al menos {2} de longitud.", MinimumLength = 2)]
        public string Name { get; set; }
        public DateTime create_at { get; set; } = DateTime.Now;
        public DateTime update_at { get; set; } = DateTime.Now;
    }
}
