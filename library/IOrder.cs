using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace library
{
    public interface IOrder
    {
        int OrderId { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        int Zip { get; set; }
        string City { get; set; }
        string Country { get; set; }
        int PhoneNumber { get; set; }
        string Email { get; set; }
        DateTime TimeStamp { get; set; }
        List<string> SampleType { get; set; }
    }
}
