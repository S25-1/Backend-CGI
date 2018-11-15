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
    public class testController : ApiController
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/test/testfunctie")]
        [HttpGet]
        public int testfunctie()
        {
            return 5;
        }
    }
}