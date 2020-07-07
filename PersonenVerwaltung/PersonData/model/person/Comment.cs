using System;

namespace PersonData
{
    public class Comment : CreatedModify
    {
        public int id { get; set; }
        public int person_id { get; set; }
        public string comment_value { get; set; }
        public DateTime value_date { get; set; }

        public Person person { get; set; }
    }
}