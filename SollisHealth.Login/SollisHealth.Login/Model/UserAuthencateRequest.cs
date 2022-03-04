using System.ComponentModel.DataAnnotations;

namespace SollisHealth.Login.Model
{
    public class UserAuthencateRequest: UserRequest
    {

        [Required(ErrorMessage = "Password is empty.")]
        public string Password { get; set; }
    }
}
