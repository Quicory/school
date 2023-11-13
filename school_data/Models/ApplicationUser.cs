using Microsoft.AspNetCore.Identity;

namespace School_Data.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string CompleteName { get; set; }
    }
}
