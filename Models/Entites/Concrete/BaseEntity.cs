using _22_MVC_Identity.Models.Entites.Interface;
using _22_MVC_Identity.Models.Enums;

namespace _22_MVC_Identity.Models.Entites.Concrete
{
    public abstract class BaseEntity : IBaseEntity
    {
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public Status Status { get; set; }
    }
}
