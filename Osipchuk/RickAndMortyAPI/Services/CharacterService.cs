using RickAndMortyAPI.CharacterInfo;
using RickAndMortyAPI.Repository;
using RickMorty;

namespace RickAndMortyAPI.Services
{
    public class CharacterService : ICharacterService
    {
        public RickAndMortyRepository Repository { get; private set; }
        public CharacterService(UnitOfWork unitOfWork)
        {
            Repository = unitOfWork.Repository;
        }
        public async Task<CharacterDTO> GetCharacterAsync(int id)
        {
            var character = await Repository.GetCharacterAsync(id);
            var characterDTO = new CharacterDTO()
            {
                Id = character.Id,
                Name = character.Name,
                Status = character.Status,
                Species = character.Species,
                Gender = character.Gender
            };
            return characterDTO;
        }

        public async Task<IList<CharacterDTO>> GetCharactersAsync()
        {
            var characters = new List<Character>();
            await foreach (var character in Repository.GetCharactersAsync())
            {
                characters.Add(character);
            }
            var data = from c in characters
                                select new CharacterDTO()
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Status = c.Status,
                                    Species = c.Species,
                                    Gender = c.Gender
                                };
            var charactersDTO = new List<CharacterDTO>();
            foreach (var characterDTO in data)
            {
                charactersDTO.Add(characterDTO);
            }
            return charactersDTO;
        }
    }
}
