using SuperCompany.Domain.Entities;

namespace SuperCompany.Domain.Repositories
{
    public interface ITextFildRepository
    {
        IQueryable<TextFild> GetTextFilds();
        TextFild GetTextFild(string CodeWord);
        TextFild GetTextFildById(Guid id);
        void SaveTextFild(TextFild fild);
        void DeleteTextFild(Guid id);
    }
}
