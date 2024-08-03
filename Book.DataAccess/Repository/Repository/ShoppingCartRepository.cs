using Book.DataAccessLayer.Repository.IRepository;
using Book.Models;
using BooK.DataAccessLayer.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Book.DataAccessLayer.Repository.Repository
{
    public class ShoppingCartRepository : Repository<ShoppingCart>, IShoppingCartRepository
    {
        public ShoppingCartRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
