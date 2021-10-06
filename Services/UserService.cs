using CodingExercise.DAL;
using CodingExercise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise.Services
{
    public class UserService : IUserService
    {
        private readonly IUserManager _userManager;

        public UserService(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public bool AddUser(AppUser appUser)
        {
            return _userManager.AddUser(appUser); 
        }

        public IEnumerable<AppUser> GetUsers()
        {
            return _userManager.GetUsers();
        }

        public IEnumerable<UserRole> GetUserRoles(AppUser appUser)
        {
            return _userManager.GetUserRoles(appUser);
        }

        public IEnumerable<Role> GetRolesById(int roleId)
        {
            return _userManager.GetRolesById(roleId);
        }

        public IEnumerable<Role> GetRoles()
        {
            return _userManager.GetRoles();
        }

        public AppUser GetUserById(int Id)
        {
            return _userManager.GetUserById(Id);
        }

        public AppUser GetUserByEmail(string email)
        {
            return _userManager.GetUserByEmail(email);
        }
    }
}
