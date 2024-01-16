using Subject.Models;

namespace Subject.Services
{
    public interface ISubject
    {
        Task AddSubject(SubjectModel subject);

        Task<SubjectModel> GetSubjectByName(string name);

        Task<bool> UpdateSubject(SubjectModel subject);

        Task<bool> DeleteSubject(Guid id);

        Task<IEnumerable<SubjectModel>> GetSubjects();
    }
}
