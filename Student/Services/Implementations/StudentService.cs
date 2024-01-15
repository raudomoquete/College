using Microsoft.EntityFrameworkCore;
using Student.Data;
using Student.Models;

namespace Student.Services
{
    public class StudentService : IStudent
    {
        private readonly ILogger<StudentService> _logger;
        private readonly Context _context;

        public StudentService(ILogger<StudentService> logger, Context context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task AddStudent(StudentModel student)
        {
            try
            {
                student.Id = Guid.NewGuid();
                _context.Students.Add(student);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding student");
            }
        }

        public async Task<bool> DeleteStudent(Guid id)
        {
            try
            {
                var student = await _context.Students.FindAsync(id);

                if (student != null)
                {
                    _context.Students.Remove(student);
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Studen not found");
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error deleting student");
                throw;
            }
        }

        public async Task<StudentModel> GetStudenByIdentification(string identification)
        {
            try
            {
                var student = await _context.Students
                    .FindAsync(identification);
                return student;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting student by identification");
                throw;
            }
        }

        public async Task<IEnumerable<StudentModel>> GetStudents()
        {
            try
            {
                var students = await _context
                                    .Students
                                    .ToListAsync();
                return students;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting students");
                throw;
            }
        }

        public async Task<bool> UpdateStudent(StudentModel student)
        {
            try
            {
                var studentToUpdate = await _context
                                                .Students
                                                .FindAsync(student.Id);
                if(studentToUpdate != null)
                {
                    studentToUpdate.Name = student.Name;
                    studentToUpdate.LastName = student.LastName;
                    studentToUpdate.Identification = student.Identification;
                    studentToUpdate.Email = student.Email;
                    studentToUpdate.Age = student.Age;
                    studentToUpdate.GradeId = student.GradeId;
                    studentToUpdate.Grade = student.Grade;
                    studentToUpdate.IdentificationType = student.IdentificationType;
              
                    await _context.SaveChangesAsync();
                    return true;
                }
                else
                {
                    _logger.LogWarning("Student not found");                    
                    return false;
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Error updating student");
                throw;
            }
        }

       
    }
}
