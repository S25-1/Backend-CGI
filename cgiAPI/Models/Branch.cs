using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public string Name { get; set; }
        public Adress Adress { get; set; }
    
    }
}