using SollisHealth.Login.Model;
using System.Threading.Tasks;

namespace SollisHealth.Login.Interface
{
    public interface IUserBO
    {
        Task<ADUserDetailsResponse> Authenticate(UserAuthencateRequest request, string authority, string clientId, string tenatId);
        Task<ADUserDetailsResponse> UserValidateUserByEmail(Model.UserRequest request, string authority, string clientId, string tenatId, string secretId, string graphUrl);
        Task<ADUserDetailsResponse> UserPasswordReset(UserPasswordResetRequest request, string authority, string clientId, string tenatId, string secretId, string graphUrl);
        

    }
}