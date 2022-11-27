namespace Admin.Data.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        StudentRepository StudentRepository { get; }
        
        SubscriptionRepository SubscriptionRepository { get; }
        void Save();
    }
}
