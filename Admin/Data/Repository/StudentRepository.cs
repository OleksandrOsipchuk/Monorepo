using Admin.Data.Entity;
using Microsoft.EntityFrameworkCore;
using Admin.Data.Repository.Interfaces;

namespace Admin.Data.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        private readonly AppDbContext _context;

        public StudentRepository(AppDbContext dbContext)
            : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task<Student> GetByTelegramIdAsync(long TelegramIdIn)
        {
            return await _context.Students.Where(b => b.TelegramId == TelegramIdIn).FirstOrDefaultAsync<Student>();
        }



        /*public async Task<IEnumerable<Student>> GetAsync()
        {
            var students = _context.Students.Include(s => s.Subscription);
            return students;
        }
        public async Task<IEnumerable<Student>> GetAsyncTest()
        {
            var students = _context.Students.OrderBy(s => s.Id);
            return students;
        }

        public async Task<Student> GetByIDAsync(int id)
        {
            return await _context.Students.FindAsync(id);
        }

        public async Task<Student> GetByTelegramIdAsync(long TelegramIdIn)
        {
            Student user = null;
            try
            {
                user = await _context.Students.Where(b => b.TelegramId == TelegramIdIn).FirstOrDefaultAsync<Student>();
            }
            catch (InvalidOperationException ex)
            {

            }
            return user;
        }

        public async Task InsertAsync(Student student)
        {
            await _context.Students.AddAsync(student);
            Console.WriteLine($"User with name field '{student.Name}' was added.");
        }

        public async Task DeleteAsync(int studentID)
        {
            Student student = await _context.Students.FindAsync(studentID);
            _context.Students.Remove(student);
        }

        public void Update(Student student)
        {
            var entity = _context.Attach(student);
            _context.Entry(student).State = EntityState.Modified;
            _context.SaveChanges();
        }


        private bool _disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    } */
    }
}