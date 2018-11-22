using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace cgiAPI.Models
{
    public class Address
    {
        public int AddressID { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string House_number { get; set; }
        public string Postal_code { get; set; }
        public string Province { get; set; }
        public string Street_name { get; set; }

        public Address(int addressID, string city, string country, string house_number, string postal_code, string street_name)
        {
            AddressID = addressID;
            Country = country;
            City = city;
            Street_name = street_name;
            House_number = house_number;
            Postal_code = postal_code;
        }
        public Address(int addressID)
        {
            AddressID = addressID;
            Country = "null";
            City = "null";
            Street_name = "null";
            House_number = "null";
            Postal_code = "null";
        }
    }
}