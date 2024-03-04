using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
    }
}