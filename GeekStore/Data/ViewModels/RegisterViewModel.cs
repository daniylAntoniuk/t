using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GeekStore.Data.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(Name = "FirstName")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "LastName")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Пароль")]
        public string Password { get; set; }

        [Required]
        [Compare("Password", ErrorMessage = "Paswords don`t match")]
        [DataType(DataType.Password)]
        [Display(Name = "Reinput Password")]
        public string PasswordConfirm { get; set; }
        [Required]
        public int PostDepartament { get; set; }
        [Required]
        public string Sity { get; set; }
        [Required]
        public string Phone { get; set; }
    }
}
