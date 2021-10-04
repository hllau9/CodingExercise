using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodingExercise.Entities;

namespace CodingExercise.DAL
{
    public interface IRoleStore
    {
        int AddRole(Role role);
        int UpdateRole(Role role);
        int DeleteRole(Role role);
        IEnumerable<Role> GetRoles();
    }
}
