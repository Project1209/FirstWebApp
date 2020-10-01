using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace PorductOnline.Models
{
    public class Product
    {
        public int id { get; set; }
        
        [Required]
        [DisplayName("Product Name")]
        public string productName { get; set; }

        [Required]
        [DisplayName("Product Price")]
        public string  productPrice { get; set; }

        
        
        [Required(ErrorMessage ="Please select the Image")]
        [DisplayName("Product Image")]
        public string file { get; set; }

        [NotMapped]
        [Required(ErrorMessage = "Please select the Image")]
        public IFormFile image { get; set; }
    }
}
