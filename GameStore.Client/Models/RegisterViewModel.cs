using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GameStore.Client.Models
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Введіть логін")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Email некоректний")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Введіть пароль")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Введіть пароль ще раз")]
        [Compare(nameof(Password), ErrorMessage = "Паролі мають співпадати")]
        public string Confirm { get; set; }
        [Required]
        public bool AgreeTerms { get; set; }

    }
}