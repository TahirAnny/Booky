﻿using Book.DataAccessLayer.Repository.IRepository;
using BooK.DataAccessLayer.Data;

namespace Book.DataAccessLayer.Repository.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Category = new CategoryRepository(_context);
            Product = new ProductRepository(_context);
            Company = new CompanyRepository(_context);
            ShoppingCart = new ShoppingCartRepository(_context);
            ApplicationUser = new ApplicationUserRepository(_context);
            OrderHeader = new OrderHeaderRepository(_context);
            OrderDetail = new OrderDetailRepository(_context);
        }

        public ICategoryRepository Category { get; private set; }

        public IProductRepository Product { get; private set; } 

        public ICompanyRepository Company { get; private set; } 

        public IShoppingCartRepository ShoppingCart { get; private set; } 

        public IApplicationUserRepository ApplicationUser { get; private set; } 

        public IOrderHeaderRepository OrderHeader { get; private set; } 

        public IOrderDetailRepository OrderDetail { get; private set; } 

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
