using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flexion.User.Infrastructure.DataModel;
namespace Flexion.User.Infrastructure.InfrastructureInterface
{
    public interface IUserRepository
    {
        Task<List<DataModel.User>> GetStudents();
        Task<DataModel.User> GetStudentByID(int userID);
        Task<List<DataModel.User>> GetTeachers();
        Task<DataModel.User> GetTeacherByID(int userID);
        Task<bool> AddTeacher(DataModel.Teacher teacher);
        Task<bool> AddStudent(DataModel.Student student);
      
    }
}
