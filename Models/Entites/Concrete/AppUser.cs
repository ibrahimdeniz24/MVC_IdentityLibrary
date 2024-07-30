using _22_MVC_Identity.Models.Entites.Interface;
using _22_MVC_Identity.Models.Enums;
using Microsoft.AspNetCore.Identity;

namespace _22_MVC_Identity.Models.Entites.Concrete
{
    public class AppUser : IdentityUser<Guid>, IBaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}
