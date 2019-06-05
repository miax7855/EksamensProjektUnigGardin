using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using library;

namespace Domainlayer
{
    public class Order : IOrder , IEquatable<IOrder>
    {
        public int OrderId { get; set; }
		public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Zip { get; set; }
        public string Country { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
        public DateTime TimeStamp { get; set; }
        public List<string> SampleType { get; set; }

		// constructor overload
		public Order(int orderId, string firstName, string lastName, int zip, string country, int phoneNumber, string email, DateTime timeStamp, List<string> sampleType) :
			 this(firstName, lastName, zip, country, phoneNumber, email, timeStamp, sampleType)
		{
            OrderId = orderId;
		}
		// constructor overload
		public Order(string firstName, string lastName, int zip, string country, int phoneNumber, string email, DateTime timeStamp, List<string> sampleType)
		{
			FirstName = firstName;
			LastName = lastName;
			Zip = zip;
			Country = country;
			PhoneNumber = phoneNumber;
			Email = email;
			SampleType = sampleType;
            TimeStamp = timeStamp;
		}
		// default constructor
		public Order()
		{

		}
		// benyttes til test
		public string PrintOrderInfo(Order o)
		{
			char[] symbol = new char[] { ',', ' ' };

			string sampleTypesInOrder = string.Empty;

			if(o.SampleType.Count == 1)
			{
				sampleTypesInOrder = o.SampleType[0].ToString();
			}
			else
			{
				foreach (string sample in o.SampleType)
				{
					sampleTypesInOrder += sample + ", ";
				}
				sampleTypesInOrder = sampleTypesInOrder.TrimEnd(symbol);
			}

			return "FirstName: " + o.FirstName + " " + "LastName: " + o.LastName + "Bestilling: " + sampleTypesInOrder;
		}
		// Overwriter Equals for Order, bruges til sammenligning af Orders, ved Mail og timestamp.
        public bool Equals(IOrder other)
        {
            if (other is null)
                return false;
            return this.Email.Equals(other.Email) && this.TimeStamp == other.TimeStamp;
        }
		// metoden Equals Overrides så den kan benytte objekt af typen IOrder
        public override bool Equals(object obj) => Equals(obj as IOrder);
		// generer en hashcode for denne specifikke instans udfra Email og TimeStamp, dermed skabes en unik værdi
		// der bruges til at relatere objekterne.
		// GetHashCode(); returnerer en 32bit interger.
        public override int GetHashCode()
        {
            return (Email+TimeStamp).GetHashCode();
        }
    }
}
