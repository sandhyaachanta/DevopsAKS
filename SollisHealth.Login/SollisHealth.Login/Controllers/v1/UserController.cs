using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SollisHealth.Login.Interface;
using SollisHealth.Login.Model;
using System;
using System.Threading.Tasks;

namespace SollisHealth.Login.v1
{

    [ApiController]
    [ApiVersion("1.0")]
   
    [Route("v{version:apiVersion}")]
    //[Route("/")]
    
    public class UserController : ControllerBase
    {
        private readonly IConfiguration _iconfiguration;
        private readonly ILogger<UserController> _logger;
        public readonly IUserBO _iUserBO;
        string clientId = "";
        string tenatId = "";
        string authority = "";
        string secretId = "";
        string grapthUrl = "";

        public UserController(IConfiguration iconfiguration, ILogger<UserController> logger, ILoggerFactory loggerFactory, IUserBO iUserBO)
        {
            _iconfiguration = iconfiguration;
            _iUserBO = iUserBO;
            _logger = logger;
            _logger = loggerFactory.CreateLogger<UserController>();
            _logger.LogWarning("Start Authenticate " + DateTime.Now);

            clientId = _iconfiguration["clientId"];
            tenatId = _iconfiguration["tenatId"];
            authority = _iconfiguration["authority"];
            secretId = _iconfiguration["secretId"];
            grapthUrl= _iconfiguration["grapthUrl"];

        }

        [HttpPost]
        [MapToApiVersion("1.0")]
        [Route("AuthenticateUser")]
        
        public async Task<IActionResult> Authenticate(UserAuthencateRequest request)
        {
            
            UserAuthenticateResponse response = null;
            _logger.LogInformation("Start Authenticate " + DateTime.Now);
            ADUserDetailsResponse Aduser = await _iUserBO.Authenticate(request, authority, clientId, tenatId);
            if (Aduser.isSuccess == 1)
            {
                response = BuildResponseMessage("Success");
                return Ok(response);
            }
            else
            {
                response = BuildResponseMessage("Username/password combination entered is incorrect.");
                return BadRequest(response);
            }
         
        }



        [HttpPost]
        [Route("ValidateUserByEmail")]
        public async Task<IActionResult> ValidateUserByEmail(Model.UserRequest request)
        {

            UserEmailValidationResponse response = null;
       
            _logger.LogInformation("Start Get User " + DateTime.Now);
            ADUserDetailsResponse Aduser = await _iUserBO.UserValidateUserByEmail(request, authority, clientId, tenatId,secretId,grapthUrl);
            if (Aduser.isValidEmail == 1)
            {
                response = BuildResponseEmailValidationMessage("Success");
                return Ok(response);
            }
            else
            {
                response = BuildResponseEmailValidationMessage("Username does not exists");
                return BadRequest(response);
            }
           
        }

        [HttpPost]
        [Route("PasswordReset")]
        public async Task<IActionResult> PasswordReset(UserPasswordResetRequest request)
        {
            UserAuthenticateResponse response = null;

            _logger.LogInformation("Start Reset Password " + DateTime.Now);
            ADUserDetailsResponse Aduser = await _iUserBO.UserPasswordReset(request, authority, clientId, tenatId, secretId, grapthUrl);
            if (Aduser.isResetPasswordSuccess == 1)
            {
                response = BuildResponseMessage("Password Reset Success");
                return Ok(response);
            }
            else
            {
                response = BuildResponseMessage("Something went wrong please check.");
                return BadRequest(response);
            }
           
        }

        private UserAuthenticateResponse BuildResponseMessage(string message)
        {
            UserAuthenticateResponse response = new UserAuthenticateResponse();
            _logger.LogInformation(message + DateTime.Now);
            response.message = message;
            return response;
        }
        private UserEmailValidationResponse BuildResponseEmailValidationMessage(string message)
        {
            UserEmailValidationResponse response = new UserEmailValidationResponse();
            _logger.LogInformation(message + DateTime.Now);
            response.message = message;
            return response;
        }
    }
}
