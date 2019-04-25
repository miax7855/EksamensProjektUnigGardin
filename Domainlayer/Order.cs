using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EksamensProjektUnigGardin
{
    public class Order
    {
        public Order()
        {

        }
        public Order(string firstName, string lastName, int zip, string city, string country, int phoneNumber, string email)
        {
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
        }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> SampleType { get; set; }
        
        public DateTime timeStamp { get; set; }
        
        
    }
}
