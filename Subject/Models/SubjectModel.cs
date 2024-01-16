using System.ComponentModel.DataAnnotations;

namespace Subject.Models
{
    public class SubjectModel
    {
        [Key]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<GradeSubject> Grades { get; set; }
    }
}
