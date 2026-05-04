using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BulkyBook.DataAccess.Repository
{
    internal class ProductRepository(ApplicationDbContext db) :
        Repository<Product>(db), IProductRepository
    {
        private readonly ApplicationDbContext _db = db;
        public void Update(Product obj)
        {
            _db.Products.Update(obj);
        }
    }
}
