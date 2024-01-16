using System.ComponentModel.DataAnnotations;

namespace Enlistment.Models
{
    public class StudentModel
    {
        public Guid StudentId { get;set; }

        public string Identification { get; set; } = string.Empty;

        public string Name { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public int Age { get; set; }

        public string Email { get; set; } = string.Empty;

        public virtual EnlistmentModel? Enlistment { get; set; }
    }
}
