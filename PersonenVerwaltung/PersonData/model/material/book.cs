using PersonData.model.interfaces;

namespace PersonData.model.material
{
    public class book : IMaterial
    {
        public int id { get; set; }
        public string isbn { get; set; }
        public string title { get; set; }
        public int? location_id { get; set; }
        public int? person_id { get; set; }

        //public classroom classroom { get; set; }
        public Person person { get; set; }
    }
}