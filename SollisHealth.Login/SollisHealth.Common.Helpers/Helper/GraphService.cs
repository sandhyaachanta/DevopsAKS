using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace SollisHealth.Common.Helpers
{
    public class GraphService
    {

        public static GraphServiceClient CreateGraphServiceClient(string authority, string clientId, string tenatId, string secretId, string graphUrl)
        {
            var clientCredential = new ClientCredential(clientId, secretId);
            var authenticationContext = new AuthenticationContext(authority+tenatId);
            var authenticationResult = authenticationContext.AcquireTokenAsync(graphUrl, clientCredential).Result;

            var delegateAuthProvider = new DelegateAuthenticationProvider((requestMessage) =>
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("bearer", authenticationResult.AccessToken);

                return Task.FromResult(0);
            });

            return new GraphServiceClient(delegateAuthProvider);
        }
    }
}
