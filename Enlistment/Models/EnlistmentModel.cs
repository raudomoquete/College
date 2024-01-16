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

        // ID del estudiante que se está inscribiendo
        public Guid StudentId { get; set; }
        // Referencia al estudiante
        public virtual StudentModel? Student { get; set; }

        // ID de la asignatura en la que se está inscribiendo
        public Guid SubjectId { get; set; }
        // Referencia a la asignatura
        public virtual SubjectModel? Subject { get; set; }

        
        public Guid GradeId { get; set; }

        public virtual Grade? Grade { get; set; }

        public DateTime EnrollmentDate { get; set; }

        public StatusType Status { get; set; }
    }
}
