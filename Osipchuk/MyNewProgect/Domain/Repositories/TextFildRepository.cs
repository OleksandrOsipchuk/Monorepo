using Microsoft.EntityFrameworkCore;
using SuperCompany.Domain.Entities;

namespace SuperCompany.Domain.Repositories
{
    public class TextFildRepository : ITextFildRepository
    {
        private AppDbContext _dbContext;
        public TextFildRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public TextFild GetTextFild(string codeWord)
        {
            var textFilds = _dbContext.TextFilds.FirstOrDefault(x => x.CodeWord == codeWord);
            if (textFilds != null)
            {
                return textFilds;
            }
            else throw new NullReferenceException();
        }

        public TextFild GetTextFildById(Guid id)
        {
            var textFilds = _dbContext.TextFilds.FirstOrDefault(x => x.Id == id);
            if (textFilds != null)
            {
                return textFilds;
            }
            else throw new NullReferenceException();
        }

        public IQueryable<TextFild> GetTextFilds()
        {
            return _dbContext.TextFilds;
        }
        public void DeleteTextFild(Guid id)
        {
            var textFilds = _dbContext.TextFilds.FirstOrDefault(x => x.Id == id);
            if (textFilds != null)
            {
                _dbContext.TextFilds.Remove(textFilds);
                _dbContext.SaveChanges();
            }
            else throw new NullReferenceException();
        }
        public void SaveTextFild(TextFild textFild)
        {
            if (textFild.Id == default)
                _dbContext.Entry(textFild).State = EntityState.Added;
            else
                _dbContext.Entry(textFild).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
