using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Availability
    {
        public int AvailabilityID { get; set; }
        public bool Available { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
}