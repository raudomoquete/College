using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Student.Data;
using Student.Models;
using Student.Services;
using System.Net.WebSockets;

namespace Student.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILogger<StudentsController> _logger;
        private readonly Context _context;
        private readonly IStudent _student;

        public StudentsController(ILogger<StudentsController> logger, Context context, IStudent student)
        {
            _logger = logger;
            _context = context;
            _student = student;
        }

        [HttpPost]
        [Route("CreateStudent")]
        public async Task<IActionResult> CreateStudent(StudentModel student)
        {
            try
            {
                await _student.AddStudent(student);
                return Ok("Studen created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetStudentByIdentification/{identification}")]
        public async Task<IActionResult> GetStudentByIdentification(string identification)
        {
            return Ok(await _student.GetStudenByIdentification(identification));
        }

        [HttpPut]
        [Route("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(StudentModel student)
        {
            try
            {
                var updateResult = await _student
                                            .UpdateStudent(student);
                if(updateResult)
                {
                    return Ok("Student updated successfully");
                }
                else
                {
                    return NotFound("Student not found");
                }
            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(Guid id)
        {
            try
            {
                if(await _student.DeleteStudent(id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("student could not be properly deleted");
                }
            }
            catch(Exception ex)
            {
                return BadRequest($"Error deleting student: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllStudents")]
        public async Task<IActionResult> GetAllStudents()
        {
            return Ok(await _student.GetStudents());
        }
    }
}
