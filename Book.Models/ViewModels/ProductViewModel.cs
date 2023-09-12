using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Models.ViewModels
{
    public class ProductViewModel
    {
        //[ValidateNever]
        public Product Products { get; set; }

        [ValidateNever]
        public IEnumerable<SelectListItem> CagetoryList { get; set; }
    }
}
