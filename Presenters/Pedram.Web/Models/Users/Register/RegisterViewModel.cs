using Pedram.Framework.CustomizedAttributes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Pedram.Web.Models.Users.Register
    {
    public class RegisterViewModel
    {


        [Required(ErrorMessage = "وارد کردن پست الکترونیک الزامی می باشد!!!")]
        [EmailAddress]
        [PedramDisplay(ResourceName: "Pedram.Register.Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "وارد کردن نام کاربری الزامی می باشد!!!")]
        [PedramDisplay(ResourceName: "Pedram.Register.UserName")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "وارد کردن نام این الزامی می باشد!!!")]
        [PedramDisplay(ResourceName: "Pedram.Register.Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "وارد کردن نام خانوادگی الزامی می باشد!!!")]
        [PedramDisplay(ResourceName: "Pedram.Register.FamilyName")]
        public string FamilyName { get; set; }

        [Required(ErrorMessage = "وارد کردن تاریخ تولد الزامی می باشد!!!")]
        [PedramDisplay(ResourceName: "Pedram.Register.BirthDay")]
        public DateTime BirthDay { get; set; }



        [PedramDisplay(ResourceName: "Pedram.Register.WorkDescription")]
        public string WorkDescription { get; set; }




        [Required(ErrorMessage = "وارد کردن کلمه عبور الزامی می باشد!!!")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [PedramDisplay(ResourceName: "Pedram.Register.Password")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [PedramDisplay(ResourceName: "Pedram.Register.ConfirmPassword")]
        [Compare("Password", ErrorMessage = "کلمه عبور و تکرار کلمه عبور یکی نمی باشد!!!")]
        public string ConfirmPassword { get; set; }
    }
}