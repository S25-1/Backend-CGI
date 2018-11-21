using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class RespondVacancyUser
    {
        public int UserID { get; set; }
        public int VacancyID { get; set; }
        public int StatusID { get; set; }

        [JsonConstructor]
        public RespondVacancyUser(int userID, int vacancyID)
        {
            UserID = userID;
            VacancyID = vacancyID;
            StatusID = 1;
        }

        public RespondVacancyUser(int userID, int vacancyID, int statusID)
        {
            UserID = userID;
            VacancyID = vacancyID;
            StatusID = statusID;
        }
    }
}