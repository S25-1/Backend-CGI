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
                using (SqlConnection conn = new SqlConnection(@"Server=tcp:cgi-matchup.database.windows.net,1433;Initial Catalog=MatchUp;Persist Security Info=False;User ID=cgi;Password=Fontys12345;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;"))
                {
                    conn.Open();
                }
                return new string[] { "database connection works!" };
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message, "Invalid Connection String");
                return new string[] { "database connection failed!" };
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
        [Route("api/vacancy/add")]
        [HttpPost]
        public HttpResponseMessage AddVacancy([FromBody]VacancyAPI vacancy)
        {
            HttpResponseMessage message = new HttpResponseMessage(HttpStatusCode.OK);
            message.ReasonPhrase = VacancyAPI.AddVacancy(vacancy);

            return message;
        }

        [Route("api/vacancy/addaccepteduser")]
        [HttpPost]
        public HttpResponseMessage AddAcceptedUser([FromBody]AcceptedUser user)
        {
            if (Vacancy.AddAcceptedUser(user))
            {
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
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
