using System;

namespace PersonData.model
{
    public class BasePerson : CreatedModify
    {
        public int id { get; set; }
        public string name1 { get; set; }
        public string name2 { get; set; }
        public DateTime? date { get; set; }
    }
}