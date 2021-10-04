using CodingExercise.Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace CodingExercise.DAL
{
    public class UserRoleStore : IUserRoleStore
    {
        private string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;
        
        public IEnumerable<UserRole> GetUserRoles(AppUser appUser)
        {
            string sql = @"select * from UserRoles where UserId = @UserId";

            IEnumerable<UserRole> userRoles = new List<UserRole>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                userRoles = conn.Query<UserRole>(sql, new { UserId = appUser.Id });
            }
            return userRoles;
        }

        public int AddUserRole(UserRole userRole)
        {
            string sql = @"insert into UserRoles (RoleId, UserId) values (@RoleId, @UserId);";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                try {
                    affectedRow = conn.Execute(sql, new { RoleId = userRole.RoleId, UserId = userRole.UserId });
                }
                catch(Exception ex)
                {
                    affectedRow = 0;
                }
            }
            return affectedRow;
        }

        public int DeleteUserRole(UserRole userRole)
        {
            throw new NotImplementedException();
        }
    }
}
