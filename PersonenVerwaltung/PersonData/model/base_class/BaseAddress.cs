using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData.model
{
    public class BaseAddress : BaseClassCreatedModify
    {
        public string street { get; set; }
        public string place { get; set; }
        public int zip { get; set; }
        public string country { get; set; }
    }
}