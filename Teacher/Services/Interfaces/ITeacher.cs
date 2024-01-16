using Teacher.Models;

namespace Teacher.Services
{
    public interface ITeacher
    {
        Task AddTeacher(TeacherModel teacher);

        Task<TeacherModel> GetTeacherByIdentification(string identification);

        Task<bool> UpdateTeacher(TeacherModel teacher);

        Task<bool> DeleteTeacher(Guid id);

        Task<IEnumerable<TeacherModel>> GetTeachers();
    }
}
