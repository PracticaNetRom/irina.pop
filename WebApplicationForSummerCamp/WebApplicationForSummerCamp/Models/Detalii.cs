using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplicationForSummerCamp.Models
{
    public class Detalii
    {
        public int Id { get; set; }

        public string Phonenumber { get; set; }

        public DateTime PostDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public bool Closed { get; set; }

        public string CategoryName { get; set; }

        public string CategoryId { get; set; }

        //pentru comentarii
        public List<Review> Comentarii { get; set; }
    }
}