using FirstMVCApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace FirstMVCApp.Infrastructure
{
    public class CustomerListRepository : IRepository<Customer,int>
    {
        /*static List<Customer> customerList = new List<Customer>()
        {
            new Customer{CustomerId="1",CompanyName="Markers",ContactName="Hello",City="Banglaore",Country="India"},
           
        };*/
        static List<Customer> customerList = new List<Customer>();
        public void CreateNew(Customer item)
        {
            item.CustomerId = Convert.ToString(Convert.ToInt32(customerList.Max(c => c.CustomerId))+1);
            customerList.Add(item);
        }

        public IEnumerable<Customer> GetAll()
        {
            return customerList;
        }

        public IEnumerable<Customer> GetBy(string filterCriteria)
        {
            throw new NotImplementedException();
        }

        public Customer GetById(int id)
        {
            var sId = Convert.ToString(id);
            return customerList.FirstOrDefault(c => c.CustomerId == sId);
        }

        public void Remove(int id)
        {
            var sId = Convert.ToString(id);
            var obj = customerList.FirstOrDefault(c => c.CustomerId == sId);
            if (obj != null)
            {
                customerList.Remove(obj);
            }
        }

        public void Update(Customer item)
        {
            var obj = customerList.FirstOrDefault(c => c.CustomerId == item.CustomerId);
            if (obj != null)
            {
                obj.CompanyName = item.CompanyName;
                obj.ContactName = item.ContactName;
                obj.City = item.City;
                obj.Country = item.Country;
            }
        }
    }
}
