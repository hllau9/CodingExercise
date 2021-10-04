using System.Collections.Generic;
using CodingExercise.Entities;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace CodingExercise.DAL
{
    public class RoleStore : IRoleStore
    {
        private string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public int AddRole(Role role)
        {
            string sql = @"insert into Roles (Name) values (@Name);";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, role);
            }
            return affectedRow;
        }

        public IEnumerable<Role> GetRoles()
        {
            string sql = @"select * from Roles;";

            IEnumerable<Role> roles = new List<Role>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                roles = conn.Query<Role>(sql);
            }
            return roles;
        }

        public int DeleteRole(Role role)
        {
            string sql = @"delete from Roles where id = @id;";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, new { id = role.Id });
            }
            return affectedRow;
        }

        public int UpdateRole(Role role)
        {
            string sql = @"update Roles set Name = @name 
                            where Id = Id";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, role);
            }
            return affectedRow;
        }

        public IEnumerable<Role> GetRolesById(int roleId)
        {
            string sql = @"select * from Roles where Id = @Id;";

            IEnumerable<Role> roles = new List<Role>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                roles = conn.Query<Role>(sql, new { Id = roleId });
            }
            return roles;
        }
    }
}
