using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Models.Class;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [ApiVersion("1.0")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterRepository _characterRepository;

        public CharacterController(ICharacterRepository characterRepository)
        {
            _characterRepository = characterRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _characterRepository.Get();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CharacterAdd character)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _characterRepository.Add(character);

            if (result.IsFailure)
            {
                return Ok(result.Error);
            }

            return Ok(result.Message);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return BadRequest();
            }

            var result = await _characterRepository.Remove(name);

            if (result.IsFailure)
            {
                return Ok(result.Error);
            }

            return Ok(result.Message);
        }

        // [HttpPut]
        // public
    }
}