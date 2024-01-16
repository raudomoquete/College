using Microsoft.EntityFrameworkCore;
using Teacher.Data;
using Teacher.Models;
using static Teacher.Data.Context;

namespace Teacher.Services
{
    public class TeacherService : ITeacher
    {
        private readonly ILogger<TeacherService> _logger;
        private readonly Context _context;

        public TeacherService(ILogger<TeacherService> _logger, Context context)
        {
            this._logger = _logger;
            this._context = context;
        }

        public async Task AddTeacher(TeacherModel teacher)
        {
            try
            {
                teacher.Id = Guid.NewGuid();
                _context.Teachers.Add(teacher);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding teacher");
            }
        }

        public async Task<bool> DeleteTeacher(Guid id)
        {
            try
            {
                var teacher = await _context.Teachers.FindAsync(id);

                if (teacher != null)
                {
                    _context.Teachers.Remove(teacher);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("teacher not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting teacher");
                throw;
            }
        }

        public async Task<TeacherModel> GetTeacherByIdentification(string identification)
        {
            try
            {
                var teacher = await _context.Teachers
                                        .FindAsync(identification);
                return teacher;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teacher by identification");
                throw;
            }
        }

        public async Task<IEnumerable<TeacherModel>> GetTeachers()
        {
            try
            {
                var teachers = await _context
                                    .Teachers
                                    .ToListAsync();
                return teachers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting teachers");
                throw;
            }
        }

        public async Task<bool> UpdateTeacher(TeacherModel teacher)
        {
            try
            {
                var teacherToUpdate = await _context
                                                .Teachers
                                                .Include(t => t.Grades)
                                                .FirstOrDefaultAsync(t => t.Id == teacher.Id);
                if (teacherToUpdate != null)
                {
                    _context.Entry(teacherToUpdate).CurrentValues.SetValues(teacher);
                    teacherToUpdate.Name = teacher.Name;
                    teacherToUpdate.LastName = teacher.LastName;
                    teacherToUpdate.Identification = teacher.Identification;
                    teacherToUpdate.Email = teacher.Email;
                    teacherToUpdate.Age = teacher.Age;
                    teacherToUpdate.Grades = teacher.Grades;
                    teacherToUpdate.IdentificationType = teacher.IdentificationType;

                    var currentGrades = teacherToUpdate.Grades
                                                  .Select(tg => tg.Grade)
                                                  .ToList();

                    foreach (var grade in currentGrades)
                    {
                        if (!teacher.Grades.Any(g => g.GradeId == grade.Id))
                        {
                            _context.Grades.Remove(grade);
                        }
                    }

                    foreach (var gradeId in teacher.Grades.Select(g => g.GradeId))
                    {
                        if (!currentGrades.Any(g => g.Id == gradeId))
                        {
                            var gradeToAdd = new Grade { Id = gradeId };
                            _context.Grades.Attach(gradeToAdd);
                            teacherToUpdate.Grades.Add(new TeacherGrade { Teacher = teacherToUpdate, Grade = gradeToAdd });
                        }
                    }

                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Teacher not found");
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating teacher");
                throw;
            }
        }
    }
}
