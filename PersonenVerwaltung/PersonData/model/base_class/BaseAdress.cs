namespace PersonData.model
{
    public class BaseAdress : CreatedModify
    {
        public int id { get; set; }
        public string street { get; set; }
        public string place { get; set; }
        public int zip { get; set; }
        public string country { get; set; }
    }
}