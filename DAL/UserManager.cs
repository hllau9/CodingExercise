using CodingExercise.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace CodingExercise.DAL
{
    public class UserManager : IUserManager
    {
        private readonly string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public bool AddUser(AppUser appUser)
        {
            string sqlUserTable = @"insert into Users (firstname, lastname, email, phone, passwordhash)
                                    values (@firstname, @lastname, @email, @phone, @passwordhash);
                                    select scope_identity();
                            ";

            string sqlRoleTable = @"select Id from Roles where Name = @RoleName";

            string sqlUserRoleTable = @"insert into UserRoles (RoleId, UserId)
                                        values (@RoleId, @UserId);
                            ";

            using (SqlConnection conn = new SqlConnection(connString))
            {
                conn.Open();
                SqlTransaction transaction = conn.BeginTransaction();
                try
                {
                    var insertedRowId = conn.ExecuteScalar<int>(sqlUserTable, appUser, transaction);
                    var roleId = conn.Query<int>(sqlRoleTable, new { RoleName = "General User" }, transaction); // assign General User role to all new users by default
                    var affectedRow = conn.Execute(sqlUserRoleTable, new { UserId = insertedRowId, RoleId = roleId }, transaction);
                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    //log exception
                    transaction.Rollback();
                    return false;
                }
            }
        }

        public IEnumerable<AppUser> GetUsers()
        {
            string sql = @"select * from Users;";

            IEnumerable<AppUser> appUser = new List<AppUser>();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                appUser = conn.Query<AppUser>(sql);
            }
            return appUser;
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

        public AppUser GetUserByEmail(string email)
        {
            string sql = @"select * from Users where email = @email;";

            AppUser appUser;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                appUser = conn.Query<AppUser>(sql, new { email = email }).FirstOrDefault();
            }
            return appUser;
        }

        public AppUser GetUserById(int Id)
        {
            string sql = @"select * from Users where Id = @Id;";

            AppUser appUser;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                appUser = conn.Query<AppUser>(sql, new { Id = Id }).FirstOrDefault();
            }
            return appUser;
        }
    }
}
