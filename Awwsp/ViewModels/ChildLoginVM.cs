using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Awwsp.ViewModels
{
    public class ChildLoginVM
    {
        [Required(ErrorMessage ="First name is reqired")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Last name is reqired")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Password is reqired")]
        [DataType(DataType.Password)]
        public string  Password { get; set; }
        public string Error { get; set; }

    }
}