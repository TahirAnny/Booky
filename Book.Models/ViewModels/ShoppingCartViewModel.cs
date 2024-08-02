﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Book.Models.ViewModels
{
    public class ShoppingCartViewModel
    {
        public OrderHeader OrderHeader { get; set; }

        public IEnumerable<ShoppingCart> ShoppingCartList { get; set; }
    }
}
