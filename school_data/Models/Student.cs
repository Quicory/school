﻿
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace School_Data.Models
{
    [Index(nameof(IDNumber), IsUnique = true, Name = "Unique_IDNumber")]
    public class Student
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        [StringLength(100, ErrorMessage = "El nombre debe tener al menos {0} de longitud.")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "El apellido es requerido")]
        [StringLength(100, ErrorMessage = "El apellido debe tener al menos {0} de longitud.")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "La Fecha de Nacimiento es requerida")]
        public DateOnly? Birthdate { get; set; }
        [Required(ErrorMessage = "La Matrícula es requerida")]
        [StringLength(12, ErrorMessage = "La matrícula debe tener al menos {0} de longitud.")] 
        public string IDNumber { get; set; }
        [Required(ErrorMessage = "El Nombre del Padre es requerido")]
        public string FatherName { get; set; }
        [Required(ErrorMessage = "El Nombre de la Madre es requerida")]
        public string MotherName { get; set; }
        [Required(ErrorMessage = "La Dirección es requerida")]
        public string Address { get; set; }
        public DateTime? create_at { get; set; } = DateTime.Now;
        public DateTime? update_at { get; set; } = DateTime.Now;
    }
}
