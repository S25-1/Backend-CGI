using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string Country { get; set; }
        public string Province { get; set; }
        public string City { get; set; }
        public string Street_name { get; set; }
        public int House_number { get; set; }
        public string Postal_code { get; set; }

        public Address(int addressID, string country, string province, string city, string street_name, int house_number, string postal_code)
        {
            AddressID = addressID;
            Country = country;
            Province = province;
            City = city;
            Street_name = street_name;
            House_number = house_number;
            Postal_code = postal_code;
        }
    }
}