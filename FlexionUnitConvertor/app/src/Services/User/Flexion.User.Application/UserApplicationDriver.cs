using Flexion.User.Domain.DomainInterface;
using Flexion.User.Domain.DomainModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Flexion.User.Application.ApplicationInterface
{
    public class UserApplicationDriver : IUserApplicationDriver
    {
        private readonly IUserService _userService;
        public UserApplicationDriver(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<bool> AddStudent(Student student)
        {
            return await _userService.AddStudent(student);
        }

        public async Task<bool> AddTeacher(Teacher teacher)
        {
            return await _userService.AddTeacher(teacher);
        }

        public async Task<Student> GetStudentByID(int userID)
        {
           return await _userService.GetStudentByID(userID);
        }

        public async Task<List<Student>> GetStudents()
        {
            return await _userService.GetStudents();
        }

        public async Task<Teacher> GetTeacherByID(int userID)
        {
            return await _userService.GetTeacherByID(userID);
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            return await _userService.GetTeachers();
        }
    }
}
