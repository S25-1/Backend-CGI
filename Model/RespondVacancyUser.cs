using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class AcceptedUser
    {
        public int UserID { get; set; }
        public int VacancyID { get; set; }
        public int StatusID { get; set; }

        [JsonConstructor]
        public AcceptedUser(int userID, int vacancyID)
        {
            UserID = userID;
            VacancyID = vacancyID;
            StatusID = 1;
        }

        public AcceptedUser(int userID, int vacancyID, int accepted)
        {
            UserID = userID;
            VacancyID = vacancyID;
            StatusID = accepted;
        }
    }
}