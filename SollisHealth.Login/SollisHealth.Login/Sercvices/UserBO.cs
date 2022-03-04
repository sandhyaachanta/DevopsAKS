using SollisHealth.Login.Helper;
using SollisHealth.Login.Interface;
using SollisHealth.Login.Model;
using System.Threading.Tasks;

namespace SollisHealth.Login.Sercvices
{
    public class UserBO : IUserBO
    {
        private readonly ADHelper _adHelper;

        public UserBO(ADHelper adHelper)
        {
            _adHelper = adHelper;

        }
        public async Task<ADUserDetailsResponse> Authenticate(UserAuthencateRequest request, string authority, string clientId, string tenatId)
        {
            ADUserDetailsResponse ADuser = await _adHelper.GettUserDetails(authority, clientId, tenatId, request.UserName, request.Password);
            return ADuser;

        }

       

        public async Task<ADUserDetailsResponse> UserPasswordReset(UserPasswordResetRequest request, string authority, string clientId, string tenatId, string secretId, string graphUrl)
        {
            ADUserDetailsResponse ADuser = await _adHelper.UserPasswordReset(authority, clientId, tenatId, request.UserName,request.newPassword, secretId, graphUrl);
            return ADuser;
        }

        public async Task<ADUserDetailsResponse> UserValidateUserByEmail(Model.UserRequest request, string authority, string clientId, string tenatId, string secretId, string graphUrl)
        {
            ADUserDetailsResponse ADuser = await _adHelper.UserValidateEmail(authority, clientId, tenatId, request.UserName,secretId, graphUrl);
            return ADuser;
        }
    }
}
