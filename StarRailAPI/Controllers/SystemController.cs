using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SystemController : ControllerBase
    {
        private readonly ISystemRepository _systemRepository;

        public SystemController(ISystemRepository systemRepository)
        {
            _systemRepository = systemRepository;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var result = _systemRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] SystemAdd model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _systemRepository.Add(model);

            if (result.IsFailure)
            {
                return Ok(result.Error);
            }

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] SystemUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _systemRepository.Update(model);

            if (result.IsFailure)
            {
                return Ok(result.Error);
            }

            return Ok(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {

            var result = await _systemRepository.Remove(id);

            if (result.IsFailure)
            {
                return Ok(result.Error);
            }

            return Ok(result.Message);
        }

    }
}