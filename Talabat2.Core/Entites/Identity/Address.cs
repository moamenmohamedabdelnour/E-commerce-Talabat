namespace Talabat2.Core.Entites.Identity
{
    public class Address
    {
        public int Id { get; set; }
        public string FirstName { get; set; }  
        public string LastName { get; set; }

        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        //Each Address Created With CheckOut Must Have AppUser
        public string AppUserId { get; set; }
        public AppUser User { get; set; }//Navaiginational Property => One
    }
}