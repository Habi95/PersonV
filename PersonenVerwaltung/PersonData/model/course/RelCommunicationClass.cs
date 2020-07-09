using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PersonData.model.course
{
    /// <summary>
    /// intermediate data used to create relations between communcations and different classes (e.g. Person or Course)
    /// </summary>
    [Table("communication_class")]
    public class RelCommunicationClass
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// the communications' id
        /// </summary>
        [Column("communication_id")]
        public int CommunicationId { get; set; }

        /// <summary>
        /// the class the communication is linked to
        /// </summary>
        [Column("class", TypeName = ("varchar(200)"))]
        public string Class { get; set; }

        /// <summary>
        /// the id of the object the communication is linked to
        /// </summary>
        [Column("class_id")]
        public int ClassId { get; set; }

        [NotMapped]
        public Communication Communication { get; set; }

        [NotMapped]
        public BaseClassCreatedModify sender { get; set; }
    }
}