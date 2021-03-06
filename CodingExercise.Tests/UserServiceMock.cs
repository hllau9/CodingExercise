using CodingExercise.DAL;
using CodingExercise.Entities;
using CodingExercise.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise.Tests
{
    public class UserServiceMock : IUserService
    {
        private readonly IUserManager _userManager;

        public UserServiceMock(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public bool AddUser(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Role> GetRoles()
        {
            List<Role> roles = new List<Role>();
            roles.Add(new Role { Id = 1, Name = "Admin" });
            roles.Add(new Role { Id = 2, Name = "User" });

            return roles;
        }

        public IEnumerable<Role> GetRolesById(int roleId)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUserByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public AppUser GetUserById(int Id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<UserRole> GetUserRoles(AppUser appUser)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.GetUsers();
        }
    }
}
