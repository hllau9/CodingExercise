using CodingExercise.DAL;
using CodingExercise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise.Tests
{
    public class UserManagerMock : IUserManager
    {
        public bool AddUser(AppUser appUser)
        {
            return true;
        }

        public IEnumerable<Role> GetRoles()
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
