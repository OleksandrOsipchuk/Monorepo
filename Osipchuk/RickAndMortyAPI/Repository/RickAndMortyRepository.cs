using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RickAndMortyAPI.CharacterInfo;
using RickMorty;
using System.Net.Http;
using System.Reflection;

namespace RickAndMortyAPI.Repository
{
    public class RickAndMortyRepository : IRepository<Character>
    {
        private readonly RickAndMortyContext db;
        public RickAndMortyRepository(RickAndMortyContext db)
        {
            this.db = db;
        }

        public async Task<Character> GetCharacterAsync(int id)
        {
            var character = await db.Characters.FindAsync(id);
            if (character != null)
            {
                return character;
            }
            else throw new NullReferenceException();
        }

        public async IAsyncEnumerable<Character> GetCharactersAsync()
        {
            IQueryable<Character> characters = db.Characters;
            var pageSize = 3;
            var count = await characters.CountAsync();
            var pageCount = Math.Ceiling(count / (double)pageSize);
            var currentPage = 0;
            while (currentPage < pageCount)
            {
                var page = await characters
                    .OrderBy(c => c.Id)
                    .Where(c => c.Id > currentPage * pageSize)
                    .Take(pageSize)
                    .ToListAsync();
                foreach (var character in page)
                {
                    yield return character;
                }
                currentPage++;
            }
        }
        public void CreateAsync(Character item)
        {
            db.Characters.Add(item);
        }

        public async Task<Character> DeleteAsync(int id)
        {
            Character? character = await db.Characters.FindAsync(id);
            if (character != null)
            {
                db.Remove(character);
                return character;
            }
            else throw new NullReferenceException();
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
