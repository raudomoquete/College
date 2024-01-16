using Microsoft.AspNetCore.Mvc;
using Teacher.Data;
using Teacher.Models;
using Teacher.Services;

namespace Teacher.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class TeacherController : ControllerBase
        {
            private readonly ILogger<TeacherController> _logger;
            private readonly Context _context;
            private readonly ITeacher _teacher;

            public TeacherController(ILogger<TeacherController> logger, Context context, ITeacher teacher)
            {
                    _logger = logger;
                    _context = context;
                    _teacher = teacher;
            }

            [HttpPost]
            [Route("CreateTeacher")]
            public async Task<IActionResult> CreateTeacher(TeacherModel teacher)
            {
                try
                {
                    await _teacher.AddTeacher(teacher);
                    return Ok("Teacher created successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex.Message);
                    return BadRequest(ex.Message);
                }
            }

            [HttpGet]
            [Route("GetTeacherByIdentification/{identification}")]
            public async Task<IActionResult> GetTeacherByIdentification(string identification)
            {
                return Ok(await _teacher.GetTeacherByIdentification(identification));
            }

            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateTeacher(Guid id, [FromBody] TeacherModel teacher)
            {
                if (id != teacher.Id)
                {
                    return BadRequest("Teacher ID mismatch");
                }

                try
                {
                    var result = await _teacher.UpdateTeacher(teacher);
                    if (result)
                    {
                        return Ok("Teacher updated successfully");
                    }
                    else
                    {
                        return NotFound("Teacher not found");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating teacher");
                    return StatusCode(500, "Internal server error");
                }
            }

            [HttpDelete]
            [Route("DeleteTeacher/{id}")]
            public async Task<IActionResult> DeleteTeacher(Guid id)
            {
                try
                {
                    if (await _teacher.DeleteTeacher(id))
                    {
                        return Ok();
                    }
                    else
                    {
                        return BadRequest("teacher could not be properly deleted");
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest($"Error deleting teacher: {ex.Message}");
                }
            }

            [HttpGet]
            [Route("GetAllTeachers")]
            public async Task<IActionResult> GetAllStudents()
            {
                return Ok(await _teacher.GetTeachers());
            }

    }
}
