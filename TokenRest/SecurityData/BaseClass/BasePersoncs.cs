using System;
using System.Collections.Generic;
using System.Text;

namespace SecurityData.model
{
    public class BasePerson : BaseClassCreatedModify
    {
        public string name1 { get; set; }   // Fistname Person or company name
        public string name2 { get; set; }   // Lastname
        public DateTime? date { get; set; } // Birthday
    }
}