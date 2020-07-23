using shopapp.entityy;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.Models
{
    public class ProductModel
    {
        public int ProductId { get; set; }
        //[Display(Name="Name", Prompt ="Enter product name")]
        //[Required(ErrorMessage ="Name is required")]
        //[StringLength(60,MinimumLength =5,ErrorMessage ="Product name should be between 5-10 characters")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Url is required")]
        public string Url { get; set; }
        //[Required(ErrorMessage = "Price is required")]
        //[Range(1,1000000, ErrorMessage ="Price should be in range 1-1000000")]
        public double? Price { get; set; }
        [Required(ErrorMessage = "Description is required")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Description should be between 5-100 characters")]
        public string Description { get; set; }
        [Required(ErrorMessage = "ImageUrl is required")]
        public string ImageUrl { get; set; }
        public bool IsApproved { get; set; }
        public bool IsHome { get; set; }
        public List<Category> SelectedCategories { get; set; }
    }
}
