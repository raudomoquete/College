using Microsoft.EntityFrameworkCore;
using Subject.Data;
using Subject.Models;

namespace Subject.Services
{
    public class SubjectService : ISubject
    {
        private readonly ILogger<SubjectService> _logger;
        private readonly Context _context;

        public SubjectService(ILogger<SubjectService> logger, Context context) 
        { 
            _logger = logger;
        }

        public async Task AddSubject(SubjectModel subject)
        {
            try
            {
                subject.Id = Guid.NewGuid();
                _context.Subjects.Add(subject);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding subject");
            }
        }

        public async Task<bool> DeleteSubject(Guid id)
        {
            try
            {
                var subject = await _context.Subjects.FindAsync(id);

                if (subject != null)
                {
                    _context.Subjects.Remove(subject);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("subject not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting subject");
                throw;
            }
        }

        public async Task<IEnumerable<SubjectModel>> GetSubjects()
        {
            try
            {
                var subjects = await _context
                                    .Subjects
                                    .ToListAsync();
                return subjects;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teachers");
                throw;
            }
        }

        public async Task<SubjectModel> GetSubjectByName(string name)
        {
            try
            {
                var subject = await _context.Subjects
                                        .FirstOrDefaultAsync(s => s.Name == name);
                return subject;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subject by name");
                throw;
            }
        }

        public async Task<bool> UpdateSubject(SubjectModel subject)
        {
            try
            {
                // Buscar la asignatura existente por su ID
                var subjectToUpdate = await _context.Subjects
                                                    .Include(s => s.Grades)
                                                    .ThenInclude(gs => gs.Grade)
                                                    .FirstOrDefaultAsync(s => s.Id == subject.Id);

                if (subjectToUpdate != null)
                {
                    // Actualizar las propiedades escalares
                    _context.Entry(subjectToUpdate).CurrentValues.SetValues(subject);

                    
                    // aqui se eliminan las relaciones que ya no están presentes
                    foreach (var gradeSubject in subjectToUpdate.Grades.ToList())
                    {
                        if (!subject.Grades.Any(gs => gs.GradeId == gradeSubject.GradeId))
                        {
                            _context.GradeSubjects.Remove(gradeSubject);
                        }
                    }

                    // aqui se agregan nuevas relaciones
                    foreach (var gradeSubject in subject.Grades)
                    {
                        if (!subjectToUpdate.Grades.Any(gs => gs.GradeId == gradeSubject.GradeId))
                        {
                            _context.GradeSubjects.Add(new GradeSubject
                            {
                                SubjectId = subjectToUpdate.Id,
                                GradeId = gradeSubject.GradeId
                            });
                        }
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning($"Subject with ID {subject.Id} not found.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subject");
                throw;
            }
        }
    }
}
