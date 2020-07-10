using PersonData.model;
using System.Collections.Generic;

namespace PersonData
{
    public class Address : BaseAddress
    {
        public List<AddressPerson> persons { get; set; }
    }
}