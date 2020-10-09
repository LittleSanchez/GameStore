using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Client.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }
    }
}