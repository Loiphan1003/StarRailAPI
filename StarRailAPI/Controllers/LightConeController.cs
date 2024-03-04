using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LightConeController : ControllerBase
    {
        private readonly ILightConeRepository _lightConeRepository;

        public LightConeController(ILightConeRepository lightConeRepository)
        {
            _lightConeRepository = lightConeRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _lightConeRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] LightConeAdd model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _lightConeRepository.Add(model);

            return Ok(result);
        }
    }
}