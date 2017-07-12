using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplicationForSummerCamp.Models
{
    public class NewAnn
    {
        [Required(ErrorMessage = "CategoryId is Required")]
        public string CategoryId { get; set; }

        public List<Category> CategoriesDataSource { get; set; }

        [Required(ErrorMessage = "Title is Required")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Phonenumber is Required")]
        public string Phonenumber { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        public string Email { get; set; }

        public string Description { get; set; }

    }
}