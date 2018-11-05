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
        public bool Accepted { get; set; }

        public AcceptedUser(int userID, int vacancyID, bool accepted)
        {
            UserID = userID;
            VacancyID = vacancyID;
            Accepted = accepted;
        }
    }
}