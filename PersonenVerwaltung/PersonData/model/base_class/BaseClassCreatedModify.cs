using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    public class BaseClassCreatedModify
    {
        public int id { get; set; }

        [Column("created@")]
        public DateTime createdAt { get; set; }

        [Column("modified@")]
        public DateTime? modifyAt { get; set; }

        [NotMapped]
        public DateTime modifyDate { get; set; }
    }
}