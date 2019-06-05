
using Flexion.User.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.User.Domain.DomainInterface
{
    public interface IUserService
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudentByID(int userID);
        Task<List<Teacher>> GetTeachers();
        Task<Teacher> GetTeacherByID(int userID);
        Task<bool> AddTeacher(Teacher teacher);
        Task<bool> AddStudent(Student student);
    }
}
