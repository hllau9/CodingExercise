using CodingExercise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodingExercise.Services
{
    public interface IUserService
    {
        bool AddUser(AppUser appUser);
        IEnumerable<AppUser> GetUsers();
        AppUser GetUserById(int Id);
        AppUser GetUserByEmail(string email);
        IEnumerable<UserRole> GetUserRoles(AppUser appUser);
        IEnumerable<Role> GetRolesById(int roleId);
        IEnumerable<Role> GetRoles();
    }
}
