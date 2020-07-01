using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.model
{
    class BasePerson
    {
        //TODO ask marc for the base person class 
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public DateTime geb_date { get; set; }
    }
}
