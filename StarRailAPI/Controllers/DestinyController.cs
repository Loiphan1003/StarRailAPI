using Asp.Versioning;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using StarRailAPI.Models;
using StarRailAPI.Service.Repositories;

namespace StarRailAPI.Controllers
{
    [ApiController]
    [Route("api/destiny")]
    [ApiVersion("1.0", Deprecated = true)]
    [ApiVersion("2.0")]
    public class DestinyController : ControllerBase
    {
        private readonly IDestinyRepository _destinyRepository;

        public DestinyController(IDestinyRepository destinyRepository)
        {
            _destinyRepository = destinyRepository;
        }


        [HttpGet]
        public IActionResult GetV1()
        {
            var result = _destinyRepository.Get();
            return Ok(result);
        }

        [HttpGet]
        [MapToApiVersion("2.0")]
        public IActionResult GetV2()
        {
            return Ok("V2");
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromForm] DestinyAdd model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _destinyRepository.Add(model);

            return Ok(result);
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            var result = await _destinyRepository.Remove(id);

            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Update(DestinyUpdate model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var result = await _destinyRepository.Update(model);

            return Ok(result);
        }
    }
}