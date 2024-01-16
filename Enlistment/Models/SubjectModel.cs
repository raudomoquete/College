using System.ComponentModel.DataAnnotations;

namespace Enlistment.Models
{
    public class SubjectModel
    {
        public Guid SubjectId { get; set; }

        public string Name { get; set; } = string.Empty;
    }
}
