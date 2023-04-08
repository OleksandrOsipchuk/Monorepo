using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.CharacterInfo;
using RickMorty;
using System.Reflection;

namespace RickAndMortyAPI.Repository
{
    public class RickAndMortyRepository : IRepository<Character, CharacterDTO>
    {
        private RickAndMortyContext db;
        public RickAndMortyRepository(RickAndMortyContext db)
        {
            this.db = db;
        }

        public async Task<CharacterDTO> GetCharacterAsync(int id)
        {
            var character = await db.Characters.FindAsync(id);
            var characterDTO = await db.Characters.Select(c =>
            new CharacterDTO()
            {
                Id = c.Id,
                Name = c.Name,
                Status = c.Status,
                Species = c.Species,
                Gender = c.Gender
            }).SingleOrDefaultAsync(c => c.Id == id);
            return characterDTO;
        }

        public async IAsyncEnumerable<CharacterDTO> GetCharactersAsync()
        {
            var characters = await db.Characters.ToListAsync();
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
        public void CreateAsync(Character item)
        {
            db.Characters.Add(item);
        }

        public async Task<Character> DeleteAsync(int id)
        {
            Character character = await db.Characters.FindAsync(id);
            if (character != null) db.Remove(character);
            return character;
        }

        public void Update(Character item)
        {
            db.Entry(item).State = EntityState.Modified;
        }
    }
}
