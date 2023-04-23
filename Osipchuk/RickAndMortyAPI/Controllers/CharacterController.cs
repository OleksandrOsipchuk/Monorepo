using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using Newtonsoft.Json;
using RickAndMortyAPI.Services;

namespace RickAndMortyAPI.Controllers
{
    [Route("api/characters")]
    public class CharacterController : Controller
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCharacter(int id)
        {
            var character = await _characterService.GetCharacterAsync(id);
            return Json(character);
        }

        [HttpGet]
        public async Task<IActionResult> GetCharacters()
        {
            var characters = await _characterService.GetCharactersAsync();
            return Json(characters);
        }
    }
}
