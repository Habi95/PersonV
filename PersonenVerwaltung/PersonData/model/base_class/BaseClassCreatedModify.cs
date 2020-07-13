using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    public class BaseClassCreatedModify
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column("id")]
        public int Id { get; set; }

        [Column("created@")]
        public DateTime? CreatedAt { get; set; }

        [Column("modified@")]
        public DateTime? ModifyAt { get; set; }

        [NotMapped]
        public DateTime ModifyDate { get; set; }
    }
}