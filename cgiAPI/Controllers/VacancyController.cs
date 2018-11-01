using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CGIdatabase;

namespace cgiAPI.Controllers
{
    public class VacancyController : ApiController
    {

        //public IEnumerable<Vacancy> Get()
        //{
        //    using (CGIdatabaseEntities entities = new CGIdatabaseEntities())
        //    {
        //        //return entities.Vacancies.ToList();
        //    }
        //}

        //public Vacancy Get(int id)
        //{
        //    using (CGIdatabaseEntities entities = new CGIdatabaseEntities())
        //    {
        //        //return entities.Vacancies.FirstOrDefault(e => e.VacancyID == id);
        //    }
        //}

//userid int
//name string
//requiredskills array[string]
//begindatetime string
//enddatetime string
//description string
//job_type string

        public void CreateVacancy(int userID, string name, string[] requiredSkills, string beginDate, string endDate, string description, string jobType)
        {
            //return value;
            //Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            //Vacancy Vacancy = new Vacancy(userTest, "We zoeken naar programmeur", 1, new List<SkillType>() { new SkillType(1, "programmer") }, "Een geervaarde programmeur", new DateTime(2018, 11, 1), new DateTime(2018, 11, 11), 5);
            //Vacancy.AddVacancy(Vacancy);
           // Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            Vacancy Vacancy = new Vacancy(userID, name, 1, new List<SkillType>(), description, beginDate, endDate, 1);
            Vacancy.AddVacancy(Vacancy);
        }

        // GET api/values
        //public IEnumerable<string> Get()
        //{


        //    return new string[] { "value1", "value2" };

        //}

        //// GET api/values/5
        //public int Get(int value)
        //{
        //    return value;
        //}

        //public void CreateVacancy(int value)
        //{
        //    //return value;
        //}

        //// POST api/values
        //public void Post([FromBody]string value)
        //{
        //    Employer userTest = new Employer(1, 1, "name", "test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
        //    Vacancy Vacancy = new Vacancy(userTest, "We zoeken naar programmeur", 1, new List<SkillType>() { new SkillType(1, "programmer") }, "Een geervaarde programmeur", new DateTime(2018, 11, 1), new DateTime(2018, 11, 11), 5);
        //    Vacancy.AddVacancy(Vacancy);
        //}


        //// PUT api/values/5
        //public void Put(int id, [FromBody]string value)
        //{
        //}

        //// DELETE api/values/5
        //public void Delete(int id)
        //{
        //}
    }
}
