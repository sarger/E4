using E4App.DTO;
using E4App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace E4App.Logic
{
    public class CustomerLogic
    {
        Repo repo = new Repo();

        public bool AddCustomer(Customer customer)
        {
           
            return repo.AddXML(customer);

 ;
        }

        
        public bool UpdateCustomer(Customer customer)
        {

           return  repo.UpdateXML(customer);
          

        }

       
        public bool DeleteCustomer(Guid customerID)
        {



           return repo.RemoveXML(customerID);

        }

     
        public List<Customer> GetCustomers()
        {

            return repo.GetCustomers();
                     

        }

        public  Customer GetCustomerByID(Guid CustomerID)
        {
           
            return repo.GetCustomerByID(CustomerID);

        }

        


    }
}
