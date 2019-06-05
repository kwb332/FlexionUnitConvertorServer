
using Flexion.User.Domain.DomainInterface;
using Flexion.User.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.User.Application.ApplicationInterface
{
    public interface IUserApplicationDriver
    {
        Task<List<Student>> GetStudents();
        Task<Student> GetStudentByID(int userID);
        Task<List<Teacher>> GetTeachers();
        Task<Teacher> GetTeacherByID(int userID);
        Task<bool> AddTeacher(Teacher teacher);
        Task<bool> AddStudent(Student student);

    }
}
