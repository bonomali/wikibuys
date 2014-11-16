using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seoWebApplication.Data.Models
{
    [MetadataType(typeof(cuisineMetaData))]
    public partial class cuisine
    { 
       
    }

    public class cuisineMetaData
    { 
        [Display(Name = "Name Code")]
        [System.ComponentModel.DataAnnotations.StringLength(1, ErrorMessage = "Name must be 1 characters or less")]
        [Required(ErrorMessage = "Name Code is required.")] 
        public string name { get; set; }
        [Display(Name = "Seo Description Code")]
        [System.ComponentModel.DataAnnotations.StringLength(255, ErrorMessage = "Seo Description must be 255 characters or less")]
        [Required(ErrorMessage = "Seo Description is required.")]
        public string SeoDescription { get; set; }

        [Display(Name = "Seo Title Code")]
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessage = "Seo Title must be 50 characters or less")]
        [Required(ErrorMessage = "Seo Title is required.")]
        public string SeoTitle { get; set; }

        [Display(Name = "Seo Keywords Code")]
        [System.ComponentModel.DataAnnotations.StringLength(50, ErrorMessage = "Seo Keywords must be 50 characters or less")]
        [Required(ErrorMessage = "Seo Keywords is required.")]
        public string SeoKeywords { get; set; } 

    }
}
