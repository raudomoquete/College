using System.ComponentModel.DataAnnotations;
using System.Diagnostics;

namespace Enlistment.Models
{
    public enum StatusType
    {
        activa,
        completada, 
        cancelada
    }

    public class EnlistmentModel
    {
        [Key]
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public virtual StudentModel? Student { get; set; }

        public Guid SubjectId { get; set; }

        public virtual SubjectModel? Subject { get; set; }
        
        public Guid GradeId { get; set; }

        public virtual Grade? Grade { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public StatusType Status { get; set; }
    }
}
