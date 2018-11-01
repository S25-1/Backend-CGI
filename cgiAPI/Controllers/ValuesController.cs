using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace cgiAPI.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            Employer userTest = new Employer(1,1,"name","test@hotmail.com", "password", 1, new List<SkillType>() { new SkillType(1, "programmer") });
            JobOffer jobOffer = new JobOffer(userTest, "We zoeken naar programmeur", 1, new List<SkillType>() { new SkillType(1, "programmer") }, "Een geervaarde programmeur", new DateTime(2018,11,1), new DateTime(2018,11-11), 5 );
            JobOffer.AddJobOffer(jobOffer);
            return new string[] { "value1", "value2" };
            
        }

        // GET api/values/5
        public string Get(int id)
        {
            
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
