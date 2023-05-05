using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using RickAndMortyAPI.Services;

namespace RickAndMortyAPI.Controllers
{
    [Route("{controller=Character}/{action=GetStartPage}")]
    [Route("api")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("character/{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacterAsync(id);
            return Json(character);
        }

        [HttpGet("characters")]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _characterService.GetCharactersAsync();
            return Json(characters);
        }
        public IActionResult GetStartPage()
        {
            return Content("Start page");
        }
    }
}
