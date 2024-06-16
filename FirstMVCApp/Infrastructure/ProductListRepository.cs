using FirstMVCApp.Models;

namespace FirstMVCApp.Infrastructure   
{
    public class ProductListRepository : IRepository<Product, int>
    {
        static List<Product> productList = new List<Product>()
        {
            new Product{ProductId=1,ProductName="Markers",UnitPrice=1234,UnitsInStock=123},
            new Product{ProductId=2,ProductName="Umbrellas",UnitPrice=4567,UnitsInStock=456},
            new Product{ProductId=3,ProductName="Flower Pots",UnitPrice=7890,UnitsInStock=789},
            new Product{ProductId=4,ProductName="Mixer Grinders",UnitPrice=8901,UnitsInStock=890},
            new Product{ProductId=5,ProductName="LED Bulbs",UnitPrice=9876,UnitsInStock=987}
        };
        public void CreateNew(Product item)
        {
            item.ProductId = productList.Max(c=>c.ProductId)+1;
            productList.Add(item);
        }

        public IEnumerable<Product> GetAll()
        {
            return productList;
        }

        public IEnumerable<Product> GetBy(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Product GetById(int id)
        {
            return productList.FirstOrDefault(c => c.ProductId == id);
        }

        public void Remove(int id)
        {
            var obj = productList.FirstOrDefault(c => c.ProductId == id);
            if (obj != null)
            {
                productList.Remove(obj);
            }
        }

        public void Update(Product item)
        {
            var obj = productList.FirstOrDefault(c=>c.ProductId==item.ProductId);
            if (obj != null)
            {
                obj.ProductName = item.ProductName;
                obj.UnitPrice = item.UnitPrice;
                obj.UnitsInStock = item.UnitsInStock;
            }
        }
    }
}
