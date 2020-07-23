
using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class CategoryModel
    {
        public int CategoryId { get; set; }
        [Required(ErrorMessage ="Category name is required")]
        [StringLength(100,MinimumLength =5,ErrorMessage ="Category name should be between 5-100 chars.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Url is required")]
        public string  Url { get; set; }
        public List<Product> Products { get; set; }
    }
}
