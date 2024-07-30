using _22_MVC_Identity.Models.Entites.Interface;

namespace _22_MVC_Identity.Models.Entites.Concrete
{
    public class Product : BaseEntity, IEntity<int>
    {
        public int Id { get; set; }
    }
}
