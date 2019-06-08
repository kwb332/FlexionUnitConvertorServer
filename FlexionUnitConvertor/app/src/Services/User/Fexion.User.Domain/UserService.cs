using Flexion.User.Domain.DomainInterface;
using Flexion.User.Domain.DomainModel;
using Flexion.User.Infrastructure.InfrastructureInterface;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Flexion.User.Domain
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<bool> AddStudent(Student student)
        {
            var studentData = new Infrastructure.DataModel.Student()
            {
                UserId = student.UserId,
                UserRoleId = student.UserId,
                FirstName = student.FirstName,
                LastName = student.LastName

            };
            return await _userRepository.AddStudent(studentData);
        }

        public async Task<bool> AddTeacher(Teacher teacher)
        {
            var teacherData = new Infrastructure.DataModel.Teacher()
            {
                UserId = teacher.UserId,
                UserRoleId = teacher.UserId,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName

            };
            return await _userRepository.AddTeacher(teacherData);
        }

        public async Task<Student> GetStudentByID(int userID)
        {
           var userData = await _userRepository.GetStudentByID(userID);
            Student student = new Student()
            {
                UserRoleId = userData.UserRoleId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                UserId = userData.UserId,
                UserRole = new UserRole()
                {
                    UserRoleId = userData.UserRoleId.Value,
                    RoleName = userData.UserRole.RoleName
                }
            };
           return student;
        }

        public async Task<List<Student>> GetStudents()
        {
            try
            {
                var userData = await _userRepository.GetStudents();
                var users = userData.Select(x => new Student()
                {
                    UserRoleId = x.UserRoleId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    UserId = x.UserId,
                    UserRole = new UserRole()
                    {
                        UserRoleId = x.UserRoleId.Value,
                        RoleName = x.UserRole.RoleName
                    }
                }).ToList();

                return users;
            }
            catch
            {
                throw;
            }
        }

        public async Task<Teacher> GetTeacherByID(int userID)
        {
            var userData = await _userRepository.GetTeacherByID(userID);
            Teacher teacher = new Teacher()
            {
                UserRoleId = userData.UserRoleId,
                FirstName = userData.FirstName,
                LastName = userData.LastName,
                UserId = userData.UserId,
                UserRole = new UserRole()
                {
                    UserRoleId = userData.UserRoleId.Value,
                    RoleName = userData.UserRole.RoleName
                }
            };
            return teacher;
        }

        public async Task<List<Teacher>> GetTeachers()
        {
            var userData = await _userRepository.GetTeachers();
            var users = userData.Select(x => new Teacher()
            {
                UserRoleId = x.UserRoleId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                UserId = x.UserId,
                UserRole = new UserRole()
                {
                    UserRoleId = x.UserRoleId.Value,
                    RoleName = x.UserRole.RoleName
                }
            }).ToList();

            return users;
        }
    }
}
