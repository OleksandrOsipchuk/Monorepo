using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using RickAndMortyAPI.Services;

namespace RickAndMortyAPI.Controllers
{
    public class CharacterController : Controller
    {
        [HttpGet]
        public IActionResult GetStartPage()
        {
            return Content("Start Page");
        }

        [ActionName("character")]
        [HttpGet]
        public async Task<IActionResult> GetCharacter([FromServices] ICharacterService characterService, int id)
        {
            var character = await characterService.GetCharacterAsync(id);
            return Json(character);
        }

        [ActionName("characters")]
        [HttpGet]
        public async Task<IActionResult> GetCharacters([FromServices] ICharacterService characterService)
        {
            var characters = await characterService.GetCharactersAsync();
            return Json(characters);
        }
    }
}
