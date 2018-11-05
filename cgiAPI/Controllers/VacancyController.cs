using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cgiAPI.Controllers
{
    public class VacancyController : ApiController
    {
        // GET: api/Vacancy
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
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
                return null;
            }
        }

        // POST: api/Vacancy
        public void Post([FromBody]Vacancy vacancy)
        {
            //Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            Vacancy.AddVacancy(vacancy);
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
