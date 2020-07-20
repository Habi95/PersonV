using System;

namespace PersonData.model
{
    /// <summary>
    /// Contains all the data that a Person or Company contains
    /// </summary>
    public class BasePerson : BaseClassCreatedModify
    {
        public string name1 { get; set; }   // Fistname Person or company name
        public string name2 { get; set; }   // Lastname
        public DateTime? date { get; set; } // Birthday
    }
}