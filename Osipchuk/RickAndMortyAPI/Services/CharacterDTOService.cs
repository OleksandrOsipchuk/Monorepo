using RickAndMortyAPI.CharacterInfo;
using RickMorty;

namespace RickAndMortyAPI.IOHandler
{
    public class CharacterDTOService : IDTOService<Character,CharacterDTO>
    {     
        public CharacterDTO GetDTO(Character obj)
        {
            var characterDTO = new CharacterDTO()
            {
                Id = obj.Id,
                Name = obj.Name,
                Status = obj.Status,
                Species = obj.Species,
                Gender = obj.Gender
            };
            return characterDTO;
        }

        public  IEnumerable<CharacterDTO> GetDTOs(IEnumerable<Character> objs)
        {
            var charactersDTO = from c in objs
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
