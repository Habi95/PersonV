using System;
using System.Collections.Generic;
using System.Text;

namespace PersonData.model
{
    public class DocumentPerson
    {
        public int id { get; set; }
        public int document_Id { get; set; }
        public int person_Id { get; set; }

        public Document document { get; set; }
        public Person person { get; set; }
    }
}
