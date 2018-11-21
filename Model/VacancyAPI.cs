using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
//using System.Web.Http.Cors;

namespace cgiAPI.Models
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class VacancyAPI
    {
        public int vacancyID { get; set; }
        public int userID { get; set; }
        public string name { get; set; }
        public int jobType { get; set; }
        public List<int> requiredSkills { get; set; }
        public string description { get; set; }
        public DateTime beginDateTime { get; set; }
        public DateTime endDateTime { get; set; }
        public int minimalExperience { get; set; }


        [JsonConstructor]
        public VacancyAPI(int userID, string name, int jobType, List<int> requiredSkills, string description, DateTime beginDateTime, DateTime endDateTime, int minimalExperience)
        {
            this.userID = userID;
            this.name = name;
            this.jobType = jobType;
            this.requiredSkills = requiredSkills;
            this.description = description;
            this.beginDateTime = beginDateTime;
            this.endDateTime = endDateTime;
            this.minimalExperience = minimalExperience;
        }

        
}
