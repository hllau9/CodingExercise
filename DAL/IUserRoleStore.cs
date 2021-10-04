using System.Collections.Generic;
using CodingExercise.Entities;

namespace CodingExercise.DAL
{
    public interface IUserRoleStore
    {
        IEnumerable<UserRole> GetUserRoles(AppUser appUser);
        int AddUserRole(UserRole userRole); 
        int DeleteUserRole(UserRole userRole);
    }
}
