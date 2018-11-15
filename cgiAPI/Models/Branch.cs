using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        public int Name { get; set; }
        public Address Address { get; set; }

        public Branch(int branchID, int name, Address address)
        {
            BranchID = branchID;
            Name = name;
            Address = address;
        }
    }
}