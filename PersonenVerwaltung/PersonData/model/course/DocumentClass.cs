using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    public class DocumentClass
    {
        public int id { get; set; }
        public int doc_id { get; set; }

        [Column("class")]
        public string classValue { get; set; }

        public int class_id { get; set; }
    }
}