using Microsoft.Graph;

namespace SollisHealth.Login.Model
{
    public class ADUserDetailsResponse
    {
        public int isSuccess{ get; set; }
        public User ADuser { get; set; }

        public int isValidEmail { get; set; }
        public int isResetPasswordSuccess { get; set; }
        public string oldPassword { get; set; }

    }
}
