using Enlistment.Models;

namespace Enlistment.Services
{
    public interface IEnlistment
    {
        Task AddEnlistment(EnlistmentModel enlistment);

        Task<EnlistmentModel> GetEnlistmentById(Guid id);

        Task<bool> UpdateEnlistment(EnlistmentModel enlistment);

        Task<bool> DeleteEnlistment(Guid id);

        Task<IEnumerable<EnlistmentModel>> GetAllEnlistments();
    }
}
