
using PersonData.model.ENUM;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonData
{
    [Table("documents")]
    public class Document
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("url", TypeName = "varchar(200)")]
        public string Url { get; set; }
        [Column("name", TypeName = "varchar(100)")]
        public string Name { get; set; }
        [Column("comment", TypeName = "varchar(500)")]
        public string Comment { get; set; }
        [Column("reminder_id")]
        public int? ReminderId { get; set; }
        [Column("created@")]
        public DateTime CreatedAt { get; set; }
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }
        [Column("type", TypeName = "varchar(50)")]
         public EDocumentType type { get; set; }

        

    }
}
