using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace SecurityData.model
{
    public class BaseClassCreatedModify
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("created@")]
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        [Column("modified@")]
        public DateTime? ModifyAt { get; set; }

        [NotMapped]
        public DateTime? ModifyDate { get; set; }

        [NotMapped]
        public bool Delete { get; set; }
    }
}