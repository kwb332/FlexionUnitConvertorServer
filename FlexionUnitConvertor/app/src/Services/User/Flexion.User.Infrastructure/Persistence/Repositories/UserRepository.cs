using Flexion.User.Infrastructure.DataModel;
using Flexion.User.Infrastructure.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Flexion.User.Infrastructure.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {

        private readonly UserDBContext _db;
        public UserRepository(UserDBContext db)
        {
            _db = db;
        }

        public async Task<bool> AddStudent(Student student)
        {
            try
            {
                await _db.User.AddAsync(student);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> AddTeacher(Teacher teacher)
        {
           try
            {
                await _db.User.AddAsync(teacher);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public async Task<DataModel.User> GetStudentByID(int userID)
        {
            return await _db.User.AsNoTracking().Include(x=>x.UserRole).FirstOrDefaultAsync(x => x.UserId == userID && x.UserRole.RoleName == "Student");
        }
        public async Task<List<DataModel.User>> GetStudents()
        {
            try
            {
                return await _db.User.AsNoTracking().Include(x => x.UserRole).Where(x => x.UserRole.RoleName == "Student").ToListAsync();
            }
            catch
            {
                throw;
            }
        }

        public async Task<DataModel.User> GetTeacherByID(int UserID)
        {
            return await _db.User.AsNoTracking().Include(x => x.UserRole).FirstOrDefaultAsync(x => x.UserId == UserID && x.UserRole.RoleName == "Teacher");
        }

        public async Task<List<DataModel.User>> GetTeachers()
        {
            return await _db.User.AsNoTracking().Include(x => x.UserRole).Where(x => x.UserRole.RoleName == "Teacher").ToListAsync();
        }
    }
}
