using FirstMVCApp.Models;
using Microsoft.EntityFrameworkCore;

namespace FirstMVCApp.Infrastructure
{
    public class ProductEFRepository : IRepository<Product,int>
    {
        private readonly ProductsDbContextcs _db;

        //constructor expression pattern
        //public ProductEFRepository() => _db = new ProductsDbContextcs();
        public ProductEFRepository(ProductsDbContextcs db) => _db = db;

        public void CreateNew(Product item)
        {
            var obj = _db.Products.
                AsNoTracking().
                FirstOrDefault(c=>c.ProductId==item.ProductId);
            if(obj is null)
            {
                _db.Products.Add(item);
                _db.SaveChanges();
            }

        }

        public IEnumerable<Product> GetAll()
        {
            return _db.Products.AsNoTracking().ToList();
        }

        public IEnumerable<Product> GetBy(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return _db.Products.AsNoTracking().FirstOrDefault(c => c.ProductId == id);
        }

        public void Remove(int id)
        {
            _db.Products.Where(c => c.ProductId == id).ExecuteDelete();
        }

        public void Update(Product item)
        {
            _db.Products.Where(c => c.ProductId == item.ProductId)
                .ExecuteUpdate(props =>
                props.SetProperty(x => x.ProductName, item.ProductName)
                .SetProperty(x => x.UnitPrice, item.UnitPrice)
                .SetProperty(x => x.UnitsInStock, item.UnitsInStock)
                );
        }
    }
}
