using E4App.DTO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Linq;

namespace E4App.Repository
{
    internal class Repo
    {
        private  string filename = HttpContext.Current.Server.MapPath("/UploadedFiles/Customer.XML");  

        public List<Customer> GetCustomers()
        {
        

            var doc = LoadXML();

            var rows = doc.Descendants("Customer");
            if (rows.Count() == 0) { return null; }

            var result = rows.Select(el => new Customer
            {
                CustomerID = Guid.Parse(el.Element("CustomerID").Value),
                Name = el.Element("Name").Value,
                Surname = el.Element("Surname").Value,
                Cellphone = el.Element("Cellphone").Value,
            }).ToList();

            return result;
             
        }

        public Customer GetCustomerByID(Guid CustomerID)
        {

            var  Node = GetNodeByID(CustomerID);
            var customerNode = Node.Item1;
            var customer = new Customer()
            {
                CustomerID = Guid.Parse(customerNode.Element("CustomerID").Value),
                Name = customerNode.Element("Name").Value,
                Surname = customerNode.Element("Surname").Value,
                Cellphone = customerNode.Element("Cellphone").Value,
            };

            return customer;

        }

        private  (XElement, XDocument) GetNodeByID(Guid CustomerID)
        {
            var document = LoadXML();
            var node = from r in document.Descendants("Customer")
                              where r.Element("CustomerID").Value == CustomerID.ToString()
                              select r;

            if (node.Count() != 1) { throw new Exception("Can not find a node"); }

            return (node.Single(), document);
        }

        public bool UpdateXML(Customer cust) 
        {

               var Element = GetNodeByID(cust.CustomerID);
                        
                Element.Item1.Element("Name").SetValue(cust.Name);
                Element.Item1.Element("Surname").SetValue(cust.Surname);
                Element.Item1.Element("Cellphone").SetValue(cust.Cellphone);

                Element.Item2.Save(filename);

            return true;
        }


        public bool RemoveXML(Guid CustomerID)
        {

            var Element = GetNodeByID(CustomerID);
            Element.Item1.Remove();
            Element.Item2.Save(filename);

            return true;
        }

        public bool AddXML(Customer cust)
        {

            var xmlDoc = LoadXML();

            var grandmom = xmlDoc.Descendants("Customers").FirstOrDefault();
        
            var  customeHolder = new XElement("Customer");
            customeHolder.Add(new XElement("Cellphone", cust.Cellphone));
            customeHolder.Add(new XElement("Name", cust.Name));
            customeHolder.Add(new XElement("CustomerID", cust.CustomerID.ToString()));
            customeHolder.Add(new XElement("Surname", cust.Surname));
            grandmom.Add(customeHolder);

            xmlDoc.Save(filename);

            return true;
        }

        private XDocument LoadXML()
        {
            try
            {
                return XDocument.Load(filename);
            }
            catch (FileNotFoundException)
            {
               return CreateEmptyXML();
            }
            catch (DirectoryNotFoundException)
            {
                throw new Exception("Please create a file here : "+filename);
            }
            
        }

        private XDocument CreateEmptyXML() 
        {
            XDocument doc = new XDocument();
            doc.Add(new XElement("Customers"));
            doc.Save(filename);
            return doc;
        } 

    }
}