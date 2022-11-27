using Admin.Data.Entity;
using Admin.Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Admin.Data.Repository
{
    public class SubscriptionRepository : GenericRepository<Subscription>
    {
        private readonly AppDbContext _context;

        public SubscriptionRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }
        public async Task<Subscription> GetByStudentIdAsync(int StudentId)
        {
            return await _context.Subscriptions.Where(s => s.StudentForeignKey == StudentId).FirstOrDefaultAsync<Subscription>();
        }
    }
}
