using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domainlayer
{
    public class Order
    {
		public int OrderId { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public List<string> SampleType { get; set; }
      
        public DateTime TimeStamp { get; set; }

        public Order()
        {

        }
        public Order(int orderId, string firstName, string lastName, int zip, string city, string country, int phoneNumber, string email, List<string> sampleType)
        {
			OrderId = orderId;
            FirstName = firstName;
            LastName = lastName;
            Zip = zip;
			City = city;
			Country = country;
			PhoneNumber = phoneNumber;
			Email = email;
			SampleType = sampleType;
			TimeStamp = DateTime.Now;
        }

		public string PrintOrderInfo(Order o)
		{
			return "OrderId: " + o.OrderId + " " + "FirstName: " + o.FirstName + " " + "LastName: " + o.LastName;
		}
		
		
		
	}
}
