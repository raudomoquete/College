using System.ComponentModel.DataAnnotations;
using static Teacher.Data.Context;

namespace Teacher.Models
{
    public enum IdentificationType
    {
        Cedula,
        DNI,
        IdentificationCard,
        Passport,
    }

    public class TeacherModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Identification { get; set; } = string.Empty;

        public IdentificationType IdentificationType { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;

        // Lista de grados que enseña el docente
        public virtual ICollection<TeacherGrade> Grades { get; set; }

    }
}
