using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplicationForSummerCamp.Models
{
    public class EmailConfirm
    {
        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }
    }
}