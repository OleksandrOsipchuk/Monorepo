using Microsoft.EntityFrameworkCore;
using TgModerator.Data.Entity;
using TgModerator.Data.Repository.IRepository;

namespace TgModerator.Data.Repository
{
    public class StudentRepository : IStudentRepository, IDisposable
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAsync()
        {
            var students = await _context.Students.ToListAsync();
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
                user = _context.Students.Single(b => b.TelegramId == TelegramIdIn);
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
    }
}