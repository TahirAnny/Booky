using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Book.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Book Title")]
        public string Title { get; set; }

        [Required]
        [DisplayName("Book Description")]
        public string Description { get; set; }

        [Required]
        [DisplayName("ISBN")]
        public string ISBN { get; set; }

        [Required]
        [DisplayName("Author's Name")]
        public string Author { get; set; }

        [Required]
        [DisplayName("List Price")]
        public double ListPrice { get; set; }

        [Required]
        [DisplayName("Price for 1-50")]
        public double BulkPrice { get; set; }

        [Required]
        [DisplayName("Price for 50+")]
        public double BulkPriceFifty { get; set; }

        [Required]
        [DisplayName("Price for 100+")]
        public double BulkPriceHundred { get; set; }

        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        [ValidateNever]
        public Category Category { get; set; }

        [ValidateNever]
        public string ImgUrl { get; set; }
    }
}
