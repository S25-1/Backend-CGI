using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Job_Type
    {
        public int Job_typeID { get; set; }
        public string Job_name { get; set; }

        public Job_Type(int job_typeID, string job_name)
        {
            Job_typeID = job_typeID;
            Job_name = job_name;
        }
    }
}