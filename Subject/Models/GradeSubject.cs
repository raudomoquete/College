namespace Subject.Models
{
    public class GradeSubject
    {
        public Guid SubjectId { get; set; }
        public SubjectModel Subject { get; set; }
        public Guid GradeId { get; set; }
        public Grade Grade { get; set; }
    }
}
