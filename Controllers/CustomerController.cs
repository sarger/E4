using E4App.DTO;
using E4App.Logic;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;

namespace E4App.Controllers
{
    public class CustomerController : Controller
    {
        CustomerLogic cust = new CustomerLogic();

        [HttpPost]
        public string AddCustomer(Customer customer)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    customer.CustomerID = Guid.NewGuid();
                    var status = cust.AddCustomer(customer);

                    return (JsonConvert.SerializeObject(cust.GetCustomerByID(customer.CustomerID)));
                }
                else
                {

                    throw new Exception(JsonConvert.SerializeObject(CollectModelErrors()));
                }

            }
            catch (Exception ex)
            {
                Response.StatusCode = 512;
                return JsonConvert.SerializeObject(ex.Message);
            }

        }

        [HttpPost]
        public string UpdateCustomer(Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var result = cust.UpdateCustomer(customer);

                    return (JsonConvert.SerializeObject(cust.GetCustomerByID(customer.CustomerID)));
                }
                else
                {

                    throw new Exception(JsonConvert.SerializeObject(CollectModelErrors()));
                }
            }
            catch (Exception ex)
            {
                Response.StatusCode = 512;
                return JsonConvert.SerializeObject(ex.Message);
            }

        }

        [HttpPost]
        public dynamic DeleteCustomer(Customer customer)
        {
            try
            {

                return cust.DeleteCustomer(customer.CustomerID);
            }
            catch (Exception ex)
            {
                Response.StatusCode = 512;
                return JsonConvert.SerializeObject(ex.Message);
            }

        }

        [HttpGet]
        public string GetCustomers()
        {
            try
            {

                return (JsonConvert.SerializeObject(cust.GetCustomers()));
            }
            catch (Exception ex)
            {
                Response.StatusCode = 512;
            
                return JsonConvert.SerializeObject(ex.Message);
            }

        }


        private string CollectModelErrors()
        {
         

            var errors = string.Join("; ", ModelState.Values
                                          .SelectMany(x => x.Errors)
                                          .Select(x => x.ErrorMessage));

            return errors;
        }

    }
}