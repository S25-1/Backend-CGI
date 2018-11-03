using System;
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
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Vacancy
        public void Post([FromBody]Employer employer)
        {
            Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            Vacancy Vacancy = new Vacancy(userID, name, 1, new List<SkillType>(), description, beginDate, endDate, 1);
            return Vacancy.AddVacancy(Vacancy);
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
