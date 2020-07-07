using PersonData.model.interfaces;

namespace PersonData.model.material
{
    public class equipment : IMaterial
    {
        public int id { get; set; }
        public string type { get; set; }
        public string make { get; set; }
        public string model { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }
        public int quantity { get; set; }

        //public classroom classroom { get; set; }
        public Person person { get; set; }
    }
}