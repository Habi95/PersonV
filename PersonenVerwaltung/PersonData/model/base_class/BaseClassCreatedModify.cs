using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData
{
    /// <summary>
    /// Contains all data that each class contains
    /// </summary>
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