using SollisHealth.Login.Model;
using System.Threading.Tasks;

namespace SollisHealth.Login.Interface
{
    public interface IADHelper
    {
       Task<ADUserDetailsResponse> GettUserDetails(string authority, string clientId, string tenatId, string Username, string Password);
        Task<ADUserDetailsResponse> UserValidateEmail(string authority, string clientId, string tenatId, string Username, string secretId, string graphUrl);
        Task<ADUserDetailsResponse> UserPasswordReset(string authority, string clientId, string tenatId, string Username, string newPassword, string secretId, string graphUrl);





    }
}
