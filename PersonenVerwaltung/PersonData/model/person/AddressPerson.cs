namespace PersonData.model
{
    public class AddressPerson
    {
        public int id { get; set; }
        public int addressId { get; set; }
        public int personId { get; set; }

        public Address address { get; set; }
        public Person person { get; set; }
    }
}