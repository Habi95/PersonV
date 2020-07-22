using PersonData.model;
using System.Collections.Generic;
using System.Numerics;

namespace PersonData
{
    public class Address : BaseAddress
    {
        public List<AddressPerson> persons { get; set; }
    }
}