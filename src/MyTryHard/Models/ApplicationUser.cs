using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace MyTryHard.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public string ShownName { get; set; }
    }
}
