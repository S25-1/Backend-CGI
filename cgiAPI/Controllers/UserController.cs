using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using cgiAPI.Models;
using Newtonsoft.Json;
using System.Web.Http.Cors;

namespace cgiAPI.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class UserController : ApiController
    {
        [Route("api/employee/add")]
        [HttpPost]
        public void AddUser([FromBody]User user)
        {
            cgiAPI.Models.User.AddUser(user);
        }
    }
}
