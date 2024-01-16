namespace Subject.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }

        // Lista de docentes que enseñan en este grado
        public virtual ICollection<GradeSubject> Subjects { get; set; }
    }
}
