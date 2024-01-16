using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Subject.Data;
using Subject.Models;
using Subject.Services;

namespace Subject.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController : ControllerBase
    {

        private readonly ILogger<SubjectsController> _logger;
        private readonly Context _context;
        private readonly ISubject _subject;

        public SubjectsController(ILogger<SubjectsController> logger, Context context, ISubject subject) 
        {
            _logger = logger;
            _context = context;
            _subject = subject;
        }

        [HttpPost]
        [Route("CreateSubject")]
        public async Task<IActionResult> CreateSubject(SubjectModel subject)
        {
            try
            {
                await _subject.AddSubject(subject);
                return Ok("Subject created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("subject-by-name/{name}")]
        public async Task<IActionResult> GetSubjectByName(string name)
        {
            try
            {
                var subject = await _subject.GetSubjectByName(name);
                if (subject != null)
                {
                    return Ok(subject);
                }
                else
                {
                    return NotFound($"Subject with name {name} not found.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting subject by name");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateSubject(Guid id, [FromBody] SubjectModel subject)
        {
            if (id != subject.Id)
            {
                return BadRequest("Subject ID mismatch");
            }

            try
            {
                var result = await _subject.UpdateSubject(subject);
                if (result)
                {
                    return Ok("Subject updated successfully");
                }
                else
                {
                    return NotFound("Subject not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating subject");
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete]
        [Route("DeleteSubject/{id}")]
        public async Task<IActionResult> DeleteSubject(Guid id)
        {
            try
            {
                if (await _subject.DeleteSubject(id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("Subject could not be properly deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting subject: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllSubjects")]
        public async Task<IActionResult> GetAllSubjects()
        {
            return Ok(await _subject.GetSubjects());
        }
    }
}
