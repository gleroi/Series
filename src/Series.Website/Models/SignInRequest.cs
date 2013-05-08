using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Series.Website.Models
{
    public class SignInRequest
    {
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Url)]
        public string ReturnUrl { get; set; }

        [Required]
        public string Username { get; set; }
    }
}