using System.ComponentModel.DataAnnotations;

namespace Student.Models
{
    public enum IdentificationType
    {
        Cedula,
        DNI,
        IdentificationCard,
        Passport,
    }

    public class StudentModel
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public string Identification { get; set; } = string.Empty;

        public IdentificationType IdentificationType { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        [Required]
        public string LastName  { get; set; } = string.Empty;

        [Required]
        public int Age { get; set; }
      
        public string Email { get; set; } = string.Empty;

        public Guid GradeId { get; set; }

        public virtual Grade? Grade { get; set; }
    }
}
