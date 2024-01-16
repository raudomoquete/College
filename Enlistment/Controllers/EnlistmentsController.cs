using Enlistment.Data;
using Enlistment.Models;
using Enlistment.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Enlistment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnlistmentsController : ControllerBase
    {
        private readonly ILogger<EnlistmentsController> _logger;
        private readonly Context _context;
        private readonly IEnlistment _enlistment;

        public EnlistmentsController(ILogger<EnlistmentsController> logger, Context context, IEnlistment enlistment)
        {
            _logger = logger;
            _context = context;
            _enlistment = enlistment;
        }

        [HttpPost]
        [Route("CreateEnlistment")]
        public async Task<IActionResult> CreateEnlistment(EnlistmentModel enlistment)
        {
            try
            {
                await _enlistment.AddEnlistment(enlistment);
                return Ok("Enlistment created successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("GetEnlistmentById/{id}")]
        public async Task<IActionResult> GetEnlistmentById(Guid Id)
        {
            return Ok(await _enlistment.GetEnlistmentById(Id));
        }

        [HttpPut]
        [Route("UpdateEnlistment")]
        public async Task<IActionResult> UpdateEnlistment(EnlistmentModel enlistment)
        {
            try
            {
                var updateResult = await _enlistment
                                            .UpdateEnlistment(enlistment);
                if (updateResult)
                {
                    return Ok("Enlistment updated successfully");
                }
                else
                {
                    return NotFound("Enlistment not found");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteEnlistment/{id}")]
        public async Task<IActionResult> DeleteEnlistment(Guid id)
        {
            try
            {
                if (await _enlistment.DeleteEnlistment(id))
                {
                    return Ok();
                }
                else
                {
                    return BadRequest("enlistment could not be properly deleted");
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting enlistment: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("GetAllEnlistments")]
        public async Task<IActionResult> GetAllStGetAllEnlistmentsudents()
        {
            return Ok(await _enlistment.GetAllEnlistments());
        }
    }
}
