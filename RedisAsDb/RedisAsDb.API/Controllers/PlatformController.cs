using Microsoft.AspNetCore.Mvc;
using RedisAsDb.API.Data;
using RedisAsDb.API.Models;
using System.Net;

namespace RedisAsDb.API.Controllers
{
    [ApiController]
    [Route("api/platforms")]
    public class PlatformController : ControllerBase
    {
        private readonly ILogger<PlatformController> _logger;
        private readonly IRepository _db;

        public PlatformController(ILogger<PlatformController> logger, IRepository db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpPost]
        public ActionResult<Platform> CreatePlatform([FromBody] Platform platform)
        {
            try
            {
                _db.CreatePlatform(platform);
                return CreatedAtRoute(nameof(GetPlatformById), new { platform.Id }, platform);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet("{id}", Name = "GetPlatformById")]
        public ActionResult<Platform> GetPlatformById(string id)
        {
            if (String.IsNullOrEmpty(id)) return BadRequest();

            try
            {
                var platform = _db.GetPlatformById(id);
                if (platform is null) return NotFound();

                return Ok(platform);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpGet]
        public ActionResult<IEnumerable<Platform>> GetAllPlatforms()
        {
            var platforms = _db.GetAllPlatforms();
            return Ok(platforms?.Where(p => p is not null));
        }

        [HttpPut]
        public ActionResult<Platform> UpdatePlatform([FromBody] Platform platform)
        {
            if (platform is null) return BadRequest();

            try
            {
                _db.UpdatePlatform(platform);
                return CreatedAtRoute(nameof(GetPlatformById), new { platform.Id }, platform);
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }

        [HttpDelete("{id}")]
        public ActionResult DeletePlatform(string id)
        {
            if (String.IsNullOrEmpty(id)) return BadRequest();

            try
            {
                _db.DeletePlatform(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError("ERROR: {ex.Message}", ex.Message);
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
