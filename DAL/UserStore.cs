using System.Collections.Generic;
using System.Linq;
using CodingExercise.Entities;
using System.Data.SqlClient;
using System.Configuration;
using Dapper;

namespace CodingExercise.DAL
{
    public class UserStore : IUserStore
    {
        private string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        public int AddUser(AppUser appUser)
        {
            string sql = @"insert into Users (username, firstname, lastname, email, phone)
                                        values (@username, @firstname, @lastname, @email, @phone);
                            ";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, appUser);
            }
            return affectedRow;
        }

        public int UpdateUser(AppUser appUser)
        {
            string sql = @"update Users set username = @username, firstname = @firstname, lastname = @lastname, email = @email, phone = @phone 
                            where Id = Id";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, appUser);
            }
            return affectedRow;
        }

        public int DeleteUser(AppUser appUser)
        {
            string sql = @"delete from Users where id = @id; ";

            int affectedRow;
            using (SqlConnection conn = new SqlConnection(connString))
            {
                affectedRow = conn.Execute(sql, new { id = appUser.Id });
            }
            return affectedRow;
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
    }
}
