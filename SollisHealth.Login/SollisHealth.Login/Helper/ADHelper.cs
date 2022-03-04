using Azure.Identity;
using Microsoft.Graph;
using SollisHealth.Common.Helpers;
using SollisHealth.Login.Interface;
using SollisHealth.Login.Model;
using SollisHealth.Login.Sercvices;
using System;
using System.Threading.Tasks;

namespace SollisHealth.Login.Helper
{
    public class ADHelper : IADHelper
    {
        public async Task<ADUserDetailsResponse> GettUserDetails(string authority, string clientId, string tenatId, string Username, string Password)
        {
            ADUserDetailsResponse Aduser = new ADUserDetailsResponse();
            try
            {


                string[] scopes = { "user.read" };
                UsernamePasswordCredential usernamePasswordCredential = new UsernamePasswordCredential(Username, Password, tenatId, clientId);
                GraphServiceClient graphClient = new GraphServiceClient(usernamePasswordCredential, scopes); // you can pass the TokenCredential directly to the GraphServiceClient

                User ADResponse = await graphClient.Me.Request()
                                .GetAsync();

                if (ADResponse != null)
                {
                    Aduser.isSuccess = 1;
                    Aduser.ADuser = ADResponse;

                }
                else
                {

                    Aduser.isSuccess = 0;
                }

            }
            catch (Exception)
            {

                Aduser.isSuccess = 0;

            }
            return Aduser;
        }

        public async Task<ADUserDetailsResponse> UserPasswordReset(string authority, string clientId, string tenatId, string Username, string newPassword, string secretId, string graphUrl)
        {
            ADUserDetailsResponse Aduser = new ADUserDetailsResponse();
            try
            {


                //string[] scopes = { "Directory.AccessAsUser.All" };
                //UsernamePasswordCredential usernamePasswordCredential = new UsernamePasswordCredential(Username,oldPassword, tenatId, clientId);
                //GraphServiceClient graphClient = new GraphServiceClient(usernamePasswordCredential, scopes); // you can pass the TokenCredential directly to the GraphServiceClient

                //await graphClient.Me.ChangePassword(oldPassword, newPassword).Request().PostAsync();
                var graphServiceClient = GraphService.CreateGraphServiceClient(authority, clientId, tenatId, secretId, graphUrl);
                await graphServiceClient.Users[Username].Request().UpdateAsync(new User
                {
                    PasswordProfile = new PasswordProfile
                    {
                        Password = "SHpass123!13456",
                        ForceChangePasswordNextSignIn = false
                    },
                   
                });



                // await graphServiceClient.Users[Username].ChangePassword("SHpass123!123", "SHpass123!13456").Request().PostAsync();
                //await graphServiceClient.Users[Username].Authentication.PasswordMethods["{Directory.AccessAsUser.All}"]
                //                        .ResetPassword(newPassword, null)
                //                        .Request()
                //                        .PostAsync();

                Aduser.isResetPasswordSuccess = 1;
            }
            catch (Exception ex)
            {

                Aduser.isResetPasswordSuccess = 0;

            }
            return Aduser;
        }

        public async Task<ADUserDetailsResponse> UserValidateEmail(string authority, string clientId, string tenatId, string Username, string secretId,string graphUrl)
        {
            ADUserDetailsResponse Aduser = new ADUserDetailsResponse();
            try
            {
                var graphServiceClient = GraphService.CreateGraphServiceClient(authority, clientId, tenatId, secretId, graphUrl);

                var user = await graphServiceClient.Users[Username].Request().GetAsync();
                

                if (user == null)
                {
                    Aduser.isValidEmail = 0;
                    
                }

                else
                {

                    Aduser.isValidEmail = 1;
                }

            }
            catch (Exception)
            {

                Aduser.isValidEmail = 0;

            }
            return Aduser;
        }



    }
}
