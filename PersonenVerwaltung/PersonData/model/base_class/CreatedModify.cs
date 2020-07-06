using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonData
{
   public class CreatedModify
    {
        [Column("created@")]
        public DateTime createdAt { get; set; }
        [Column("modify@")]
        public DateTime? modifyAt { get; set; }

        [NotMapped]
        public DateTime modifyDate { get; set; }
    }
}
