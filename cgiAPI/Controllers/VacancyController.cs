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
    public class VacancyController : ApiController
    {
        // GET: api/Vacancy
        public IEnumerable<string> Get()
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Mike\OneDrive\school\mike_backend\CGIdatabase.mdf;Integrated Security=True;Connect Timeout=30"))
                {
                    conn.Open();
                }
                return new string[] { "success" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Invalid Connection String");
                return new string[] { "rip" };
            }
            
        }

        [Route("api/vacancy/GetVacancyList")]
        [HttpGet]
        public ArrayList GetVacancyList()
        {
            return Vacancy.GetListVacancy();
        }

        [Route("api/vacancy/GetAcceptedUserList")]
        [HttpGet]
        public ArrayList GetAcceptedUserList()
        {
            return Vacancy.GetListAcceptedUser();
        }

        // POST: api/Vacancy
        [Route("api/vacancy/AddVacancy")]
        [HttpPost]
        public void AddVacancy([FromBody]Vacancy vacancy)
        {
            Vacancy.AddVacancy(vacancy);
        }

        [Route("api/vacancy/AddAcceptedUser")]
        [HttpPost]
        public void AddAcceptedUser([FromBody]AcceptedUser user)
        {
            Vacancy.AddAcceptedUser(user);  
        }

        //[HttpPost]
        //public void AddAcceptedUser([FromBody]int id)
        //{
        //    string kek = "success";
        //}


        // PUT: api/Vacancy/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Vacancy/5
        public void Delete(int id)
        {
        }
    }
}
