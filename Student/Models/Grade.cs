namespace Student.Models
{
    public class Grade
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Number { get; set; }
        public virtual StudentModel? Student { get; set; }
    }
}
