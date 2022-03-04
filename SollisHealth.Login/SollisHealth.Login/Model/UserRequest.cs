using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SollisHealth.Login.Model
{
    public class UserRequest
    {
        [Required(ErrorMessage ="UserName/Email is null")]
        public string UserName { get; set; }

        
    }
}
