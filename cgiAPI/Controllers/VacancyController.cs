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
                using (SqlConnection conn = new SqlConnection(@"Server = studmysql01.fhict.local; Uid = dbi387318; Database = dbi387318; Pwd = 1234qwerty;"))
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

        // GET: api/Vacancy/5
        public ArrayList Get(string command)
        {
            if (command == "getVacancyList")
            {
                return Vacancy.GetListVacancy();
            }
            else
            {
                return new ArrayList();
            }
        }



        // POST: api/Vacancy
        public void Post([FromBody]Vacancy vacancy)
        {
            //Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            //Vacancy.AddVacancy(vacancy);
        }

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
