using System.ComponentModel.DataAnnotations;

namespace _22_MVC_Identity.Models.DTOs
{
    public class UserUpdateDTO
    {

        public string UserName { get; set; }


        public string Password { get; set; }


        public string Email { get; set; }
    }
}
