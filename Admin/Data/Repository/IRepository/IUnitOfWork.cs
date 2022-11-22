namespace TgModerator.Data.Repository.IRepository
{
    public interface IUnitOfWork
    {
        StudentRepository Student { get; }

        public void Save();
    }
}
