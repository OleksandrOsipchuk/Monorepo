using Microsoft.EntityFrameworkCore;
using RickAndMortyAPI.CharacterInfo;
using RickMorty;
using System.Reflection;

namespace RickAndMortyAPI.Repository
{
    public class RickAndMortyRepository : IRepository<Character>
    {
        private RickAndMortyContext db;
        public RickAndMortyRepository(RickAndMortyContext db)
        {
            this.db = db;
        }

        public async Task<Character> GetCharacterAsync(int id)
        {
            var character = await db.Characters.FindAsync(id);
            return character;
        }

        public async IAsyncEnumerable<Character> GetCharactersAsync()
        {
            var characters = await db.Characters.ToListAsync();            
            foreach (var character in characters)
            {

                yield return character;
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
        public void Save()
        {
            db.SaveChanges();
        }
    }
}
