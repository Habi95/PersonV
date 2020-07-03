using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonData.model
{
    public class DocumentClass
    {
        public int id { get; set; }
        public int doc_id { get; set; }

        [Column("class")]
        public string classValue { get; set; }
        public int class_id { get; set; }

        //public List<Document> documents { get; set; }
    }
}
