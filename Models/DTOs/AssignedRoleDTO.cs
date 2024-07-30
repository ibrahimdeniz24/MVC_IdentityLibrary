using _22_MVC_Identity.Models.Entites.Concrete;
using Microsoft.AspNetCore.Identity;

namespace _22_MVC_Identity.Models.DTOs
{
    public class AssignedRoleDTO
    {
        public IdentityRole<Guid> Role { get; set; }

        public string RoleName { get; set; }

        public IEnumerable<AppUser> HasRole { get; set; }
        public IEnumerable<AppUser> HasNotRole { get; set; }


        public string[] AddIds { get; set; }
        public string[] DeleteIds { get; set; }
    }

}
