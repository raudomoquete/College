using Enlistment.Data;
using Enlistment.Models;
using Microsoft.EntityFrameworkCore;

namespace Enlistment.Services
{
    public class EnlistmentService : IEnlistment
    {
        private readonly ILogger<EnlistmentService> _logger;
        private readonly Context _context;

        public EnlistmentService(ILogger<EnlistmentService> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddEnlistment(EnlistmentModel enlistment)
        {
            try
            {
                enlistment.Id = Guid.NewGuid();
                _context.Enlistments.Add(enlistment);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error crearting enlisment");
            }
        }

        public async Task<bool> DeleteEnlistment(Guid id)
        {
            try
            {
                var enlistment = await _context.Enlistments.FindAsync(id);

                if (enlistment != null)
                {
                    _context.Enlistments.Remove(enlistment);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Enlistment not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting enlistment");
                throw;
            }
        }

        public async Task<IEnumerable<EnlistmentModel>> GetAllEnlistments()
        {
            try
            {
                var enlistments = await _context
                                    .Enlistments
                                    .ToListAsync();
                return enlistments;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting rnlisments");
                throw;
            }
        }

        public async Task<EnlistmentModel> GetEnlistmentById(Guid id)
        {
            try
            {
                var enlistment = await _context.Enlistments.FindAsync(id); 

                return enlistment;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting enlistment by identification");
                throw;
            }
        }

        public async Task<bool> UpdateEnlistment(EnlistmentModel enlistment)
        {
            try
            {
                var existingEnlistment = await _context
                                            .Enlistments
                                            .FirstOrDefaultAsync(e => e.Id == enlistment.Id);

                if (existingEnlistment != null)
                {
                    existingEnlistment.StudentId = enlistment.StudentId;
                    existingEnlistment.SubjectId = enlistment.SubjectId;
                    existingEnlistment.GradeId = enlistment.GradeId;
                    existingEnlistment.EnrollmentDate = enlistment.EnrollmentDate;
                    existingEnlistment.Status = enlistment.Status;

                    _context.Entry(existingEnlistment).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating enlistment");
                throw;
            }
        }
    }
}
