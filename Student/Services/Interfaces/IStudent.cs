using Student.Models;

namespace Student.Services
{
    public interface IStudent
    {

        Task AddStudent(StudentModel student);

        Task<StudentModel> GetStudenByIdentification(string identification);

        Task<bool> UpdateStudent(StudentModel student);

        Task<bool> DeleteStudent(Guid id);

        Task <IEnumerable<StudentModel>> GetStudents();

    }
}
