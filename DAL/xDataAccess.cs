using System;
using System.Configuration;
using CodingExercise.Entities;
using Dapper;
using System.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace CodingExercise.DAL
{
    public class DataAccess
    {
        string connString = ConfigurationManager.ConnectionStrings["connString"].ConnectionString;

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
    }
}
