using Microsoft.AspNetCore.Identity;

namespace Auth.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public int Age { get; set; }
    }
}
