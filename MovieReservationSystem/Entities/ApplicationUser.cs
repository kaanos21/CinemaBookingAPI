using Microsoft.AspNetCore.Identity;

namespace MovieReservationSystem.Entities
{
    public class ApplicationUser: Microsoft.AspNetCore.Identity.IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname {  get; set; }
        public double Money { get; set; }
    }
}
