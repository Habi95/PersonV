using PersonData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    /// <summary>
    /// contains information to a certain course
    /// </summary>
    [Table("course")]
    public class Course : BaseClassCreatedModify
    {
        /// <summary>
        /// id in DB (is assigned by DB as autoIncrement)
        /// </summary>
        //[Column("id")]
        //public int Id { get; set; }

        /// <summary>
        /// the courses' title
        /// </summary>
        [Column("title", TypeName = "varchar(250)")]
        public string Title { get; set; }

        /// <summary>
        /// the courses number (can contain letters)
        /// </summary>
        [Column("course_number", TypeName = "varchar(15)")]
        public string? CourseNumber { get; set; }

        /// <summary>
        /// a short description for the course
        /// </summary>
        [Column("description")]
        public string? Description { get; set; }

        /// <summary>
        /// the category the course belongs to
        /// </summary>
        [Column("category", TypeName = "varchar(100)")]
        public ECourseCategory Category { get; set; }

        /// <summary>
        /// the courses' start date
        /// </summary>
        [Column("start")]
        public DateTime? Start { get; set; }

        /// <summary>
        /// the courses' end date
        /// </summary>
        [Column("end")]
        public DateTime? End { get; set; }

        /// <summary>
        /// the amount a teaching units of the course
        /// </summary>
        [Column("unit")]
        public int? Unit { get; set; }

        /// <summary>
        /// the price of the course
        /// </summary>
        [Column("price")]
        public double? Price { get; set; }

        /// <summary>
        /// the id of the classromm the course is held in
        /// </summary>
        [Column("classroom_id")]
        public int? ClassroomID { get; set; }

        /// <summary>
        /// the amount of maximum participants
        /// </summary>
        [Column("participant_max")]
        public int? MaxParticipants { get; set; }

        /// <summary>
        /// the amount of minimum participants
        /// </summary>
        [Column("participant_min")]
        public int? MinParticipants { get; set; }

        /// <summary>
        /// the date the course was created at
        /// </summary>
        [Column("created@")]
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// the date the course was modified at
        /// </summary>
        [Column("modified@")]
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// a list of relations to the courses' contents
        /// </summary>
        //[NotMapped]
        //public List<RelCourseContent> CourseContents { get; set; }

        /// <summary>
        /// a list of relations to the courses' subventions
        /// </summary>
        //[NotMapped]
        //public List<RelCourseSubvention> CourseSubventions { get; set; }

        [NotMapped]
        public List<RelCourseParticipant> RelCourseParticipants { get; set; }

        [NotMapped]
        public List<RelCourseTrainer> RelCourseTrainers { get; set; }

        //[NotMapped]
        //public List<Absence> Absences { get; set; }
    }
}