using System.ComponentModel.DataAnnotations.Schema;

namespace PersonData.model
{
    /// <summary>
    /// Contains all the data that a standard address contains
    /// </summary>
    public class BaseAddress : BaseClassCreatedModify
    {
        public string street { get; set; }
        public string place { get; set; }
        public int zip { get; set; }
        public string country { get; set; }
    }
}