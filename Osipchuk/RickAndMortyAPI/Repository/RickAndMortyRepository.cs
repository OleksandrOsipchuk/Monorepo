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
        private readonly RickAndMortyContext _rickAndMortyContext;
        public RickAndMortyRepository(RickAndMortyContext db)
        {
            this._rickAndMortyContext = db;
        }
        public async Task<Character> GetCharacterAsync(int id)
        {
            var character = await _rickAndMortyContext.Characters.FindAsync(id);
            if (character != null)
            {
                return character;
            }
            else throw new NullReferenceException();
        }

        public async IAsyncEnumerable<Character> GetCharactersAsync()
        {
            IQueryable<Character> characters = _rickAndMortyContext.Characters;
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
        public async Task CreateAsync(Character item)
        {
            _rickAndMortyContext.Characters.Add(item);
            await Save();
        }
        public async Task CreateAsync(IEnumerable<Character> items)
        {
            await _rickAndMortyContext.Characters.AddRangeAsync(items);
            await Save();
        }
        public async Task<Character> DeleteAsync(int id)
        {
            Character? character = await _rickAndMortyContext.Characters.FindAsync(id);
            if (character != null)
            {
                _rickAndMortyContext.Remove(character);
                await Save();
                return character;
            }
            else throw new NullReferenceException();
        }
        public async Task UpdateAsync(Character item)
        {
            _rickAndMortyContext.Update(item);
            await Save();
        }     
        public async Task UpdateAsync(IEnumerable<Character> items)
        {
            _rickAndMortyContext.UpdateRange(items);
            await Save();
        }
        public async Task<bool> CheckIfExist(int id)
        {
            return await _rickAndMortyContext.Characters.AnyAsync(ch => ch.Id == id);
        }
        public async Task Save()
        {
            await _rickAndMortyContext.SaveChangesAsync();
        }
        public void ClearTracker()
        {
            _rickAndMortyContext.ChangeTracker.Clear();
        }

    }
}
