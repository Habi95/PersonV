using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    /// <summary>
    /// Identifies the document from whom it comes Person or Course
    /// </summary>
    public class DocumentClass
    {
        public int id { get; set; }
        public int doc_id { get; set; }

        public Document Document { get; set; }

        [Column("class")]
        public string classValue { get; set; }

        public int class_id { get; set; }
    }
}