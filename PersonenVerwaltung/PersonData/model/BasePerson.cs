using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.model
{
    public class BasePerson : CreatedModify
    {
        //TODO ask marc for the base person class 
        public int id { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public DateTime date { get; set; }
    }
}
