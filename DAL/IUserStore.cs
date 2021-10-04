using System.Collections.Generic;
using CodingExercise.Entities;

namespace CodingExercise.DAL
{
    public interface IUserStore
    {
        int AddUser(AppUser user);
        int UpdateUser(AppUser user);
        int DeleteUser(AppUser user);
        IEnumerable<AppUser> GetUsers();
        AppUser GetUserById(int Id);
        AppUser GetUserByEmail(string email); 
    }
}
