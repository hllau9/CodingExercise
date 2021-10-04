using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CodingExercise.DAL;

namespace CodingExercise.Controllers
{
    public class UserAPIController : ApiController
    {
        private readonly IUserStore _userStore;
        private readonly IRoleStore _roleStore;
        private readonly IUserRoleStore _userRoleStore;

        public UserAPIController()
        {
        }

        public UserAPIController(IUserStore userStore, IRoleStore roleStore, IUserRoleStore userRoleStore)
        {
            _userStore = userStore;
            _roleStore = roleStore;
            _userRoleStore = userRoleStore;
        }

        // GET api/<controller>
        public IHttpActionResult Get()
        {
            var userList = _userStore.GetUsers();
            return Ok(userList);
        }

        // GET api/<controller>/5
        public IHttpActionResult Get(int id)
        {
            var user = _userStore.GetUserById(id);

            if (user == null)
                return NotFound();

            return Ok(user);
        }
    }
}
