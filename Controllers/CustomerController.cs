using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using UseAll.Customers.API.DataBase;
using UseAll.Customers.API.Entities;

namespace UseAll.Customers.API.Controllers
{
    [Route("Customers")]
    [ApiController]
    public class CustomerController : Controller
    {
        private DataBaseContext _dbContext;
        public CustomerController() 
        {
            _dbContext = new DataBaseContext();
        }


        [HttpGet]
        public List<Customer> GetAllCustomers()
        {
            return _dbContext.Set<Customer>().ToList();
        }

        [HttpPost]
        public void CreateCustomers(CustomerParameters customerParameters)
        {
            var existingcustomer = GetCustomerByCNPJ(customerParameters.CNPJ);

            if (existingcustomer == null){
                var customer = new Customer();
                customer.CNPJ = customerParameters.CNPJ;
                customer.Name = customerParameters.Name;
                customer.Phone = customerParameters.Phone;
                customer.Address = customerParameters.Address;
                customer.Id = System.Guid.NewGuid();
                customer.CreationDate = System.DateTime.Now;
                customer.UpdateTime = System.DateTime.Now;
                _dbContext.Customers.Add(customer);
            } 
            else
            {
                existingcustomer.CNPJ = customerParameters.CNPJ;
                existingcustomer.Name = customerParameters.Name;
                existingcustomer.Phone = customerParameters.Phone;
                existingcustomer.Address = customerParameters.Address;
                existingcustomer.UpdateTime = System.DateTime.Now;
            }


            _dbContext.SaveChanges();
        }

        [HttpDelete]
        [Route ("{CNPJ}")]
        public void DeleteCustomer(string cnpj)
        {

            var user = GetCustomerByCNPJ(cnpj);
            if (user != null)
            {
                _dbContext.Remove(user);
                _dbContext.SaveChanges();
            }
        }

        [HttpGet]
        [Route("{CNPJ}")]
        public Customer GetCustomerByCNPJ(string cnpj)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.CNPJ == cnpj);
        }

    }
}
