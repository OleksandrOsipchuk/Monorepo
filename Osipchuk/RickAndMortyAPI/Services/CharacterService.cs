using RickAndMortyAPI.CharacterInfo;
using RickAndMortyAPI.Repository;
using RickMorty;

namespace RickAndMortyAPI.Services
{
    public class CharacterService : ICharacterService<CharacterDTO>
    {
        public RickAndMortyRepository Repository { get; private set; }
        public CharacterService(UnitOfWork unitOfWork)
        {
            Repository = unitOfWork.Repository;
        }
        public async Task<CharacterDTO> GetDTOAsync(int id)
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

        public async IAsyncEnumerable<CharacterDTO> GetDTOsAsync()
        {
            var characters = new List<Character>();
            await foreach (var character in Repository.GetCharactersAsync())
            {
                characters.Add(character);
            }
            var charactersDTO = from c in characters
                                select new CharacterDTO()
                                {
                                    Id = c.Id,
                                    Name = c.Name,
                                    Status = c.Status,
                                    Species = c.Species,
                                    Gender = c.Gender
                                };
            foreach (var characterDTO in charactersDTO)
            {

                yield return characterDTO;
            }
        }
    }
}
