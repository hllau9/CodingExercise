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
    public class UserManager 
    {
        //private string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

        //public int AddUser(AppUser appUser)
        //{
        //    string sql = @"insert into Users (username, firstname, lastname, email, phone)
        //                                values (@username, @firstname, @lastname, @email, @phone);
        //                    ";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, appUser);
        //    }
        //    return affectedRow;
        //}

        //public int DeleteRole(Role role)
        //{
        //    string sql = @"delete from Roles where id = @id;";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, new { id = role.Id });
        //    }
        //    return affectedRow;
        //}

        //public int DeleteUser(AppUser appUser)
        //{
        //    string sql = @"delete from Users where id = @id; ";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, new { id = appUser.Id });
        //    }
        //    return affectedRow;
        //}

        //public AppUser GetUserById(int Id)
        //{
        //    string sql = @"select * from Users where Id = @Id;";

        //    AppUser appUser;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        appUser = conn.Query<AppUser>(sql, new { Id = Id }).FirstOrDefault();
        //    }
        //    return appUser;
        //}

        //public AppUser GetUserByEmail(string email)
        //{
        //    string sql = @"select * from Users where email = @email;";

        //    AppUser appUser;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        appUser = conn.Query<AppUser>(sql, new { email = email }).FirstOrDefault();
        //    }
        //    return appUser;
        //}

        //public IEnumerable<AppUser> GetUsers()
        //{
        //    string sql = @"select * from Users;";

        //    IEnumerable<AppUser> appUser = new List<AppUser>();
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        appUser = conn.Query<AppUser>(sql);
        //    }
        //    return appUser;
        //}

        //public int UpdateRole(Role role)
        //{
        //    string sql = @"update Roles set Name = @name 
        //                    where Id = Id";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, role);
        //    }
        //    return affectedRow;
        //}

        //public int UpdateUser(AppUser appUser)
        //{
        //    string sql = @"update Users set username = @username, firstname = @firstname, lastname = @lastname, email = @email, phone = @phone 
        //                    where Id = Id";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, appUser);
        //    }
        //    return affectedRow;
        //}

        //public int AddRole(Role role)
        //{
        //    string sql = @"insert into Roles (Name) values (@Name);";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, role);
        //    }
        //    return affectedRow;
        //}

        //public IEnumerable<Role> GetRoles()
        //{
        //    string sql = @"select * from Roles;";

        //    IEnumerable<Role> appUser = new List<Role>();
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        appUser = conn.Query<Role>(sql);
        //    }
        //    return appUser;
        //}

        //public IEnumerable<UserRole> GetUserRoles(AppUser appUser)
        //{
        //    string sql = @"select * from UserRoles where UserId = @UserId";

        //    IEnumerable<UserRole> userRoles = new List<UserRole>();
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        userRoles = conn.Query<UserRole>(sql, new { UserId = appUser.Id });
        //    }
        //    return userRoles;
        //}

        //public int AddUserRole(UserRole userRole)
        //{
        //    string sql = @"insert into UserRoles (RoleId, UserId) values (@RoleId, @UserId);";

        //    int affectedRow;
        //    using (SqlConnection conn = new SqlConnection(connString))
        //    {
        //        affectedRow = conn.Execute(sql, new { RoleId = userRole.RoleId, UserId = userRole.UserId });
        //    }
        //    return affectedRow;
        //}

        //public int DeleteUserRole(UserRole userRole)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
