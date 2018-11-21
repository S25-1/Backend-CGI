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
        //[Route("api/vacancy/test")]
        //[HttpGet]
        //public IEnumerable<int> testfunctie()
        //{
        //    return new int[] { 5 };
        //}

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

        [Route("api/vacancy/GetRespondVacancyUserList")]
        [HttpGet]
        public ArrayList GetListRespondVacancyUser()
        {
            return Vacancy.GetListRespondVacancyUser();
        }

        //Krijgt een lijst van gereageerde werknemers met het aangegeven VacancyID en StatusID
        [Route("api/vacancy/GetRespondVacancyUser")]
        [HttpGet]
        public ArrayList GetRespondVacancyUserList(int userID, int vacancyID, int statusID)
        {
            Employer employer = new Employer();
            employer = Employer.GetEmployer(userID);
            return VacancyAPI.GetListRespondVacancyUser(employer.UserID, vacancyID, statusID);
        }


        [Route("api/vacancy/addtest")]
        [HttpPost]
        public IEnumerable<string> AddVacancyTest([FromBody]VacancyAPI vacancy, User user)
        {
            return new string[] { "database connection works!" };
        }



        //Voegt een vacature in de database met de class VacancyAPI
        [Route("api/vacancy/add")]
        [HttpPost]
        public HttpResponseMessage AddVacancy([FromBody]VacancyAPI vacancy)
        {
            HttpResponseMessage message = new HttpResponseMessage();
            if (vacancy == null)
            {
                message.Content = new StringContent(VacancyAPI.AddVacancy(vacancy));
            }
            else
            {
                message.Content = new StringContent("object is null, please check parameters");
                message.ReasonPhrase = "check the json format or the right parameters";
            }

            return message;
        }

        [Route("api/vacancy/addRespondVacancyUser")]
        [HttpPost]
        public HttpResponseMessage AddRespondVacancyUser([FromBody]RespondVacancyUser user)
        {
            if (Vacancy.AddRespondVacancyUser(user))
            {
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            else
            {
                return new HttpResponseMessage(HttpStatusCode.Conflict);
            }
        }

        //[HttpPost]
        //public void AddRespondVacancyUser([FromBody]int id)
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
