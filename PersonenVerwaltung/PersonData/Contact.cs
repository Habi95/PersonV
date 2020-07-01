using System;
using System.Collections.Generic;
using System.Security.Principal;
using System.Text;

namespace PersonData
{
   public class Contact :  CreatedModify
    {
        public int id { get; set; }
        public int master_file_id { get; set; }
        public EKindOfCommunication kindOfCommunication { get; set; }
        public string contactValue { get; set; }
        public EContactType contactType { get; set; }
        public bool mainContact { get; set; }
    }
}
