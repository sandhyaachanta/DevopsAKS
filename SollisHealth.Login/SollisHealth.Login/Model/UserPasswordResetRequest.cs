using System.ComponentModel.DataAnnotations;

namespace SollisHealth.Login.Model
{
    public class UserPasswordResetRequest:UserRequest
    {
        //[Required(ErrorMessage = "Old Password is null")]
        //public string oldPassword { get; set; }

        [Required(ErrorMessage = "New Password is null")]
        public string newPassword { get; set; }

    }
}
