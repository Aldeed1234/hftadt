using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ALDEED_HFT_2021221.Logic
{
    public class ClientLogic
    {
        /// <summary>
        /// This is the order repo.
        /// </summary>
        private IOrdersRepository orderRepo;

        /// <summary>
        /// This is the customer repo.
        /// </summary>
        private ICustomersRepository customerRepo;

        /// <summary>
        /// This is the connections repo.
        /// </summary>
        private IConnectorRepository connectorRepo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientLogic"/> class.
        /// </summary>
        /// <param name="orderRepo">IOrdersRepository type parameter.</param>
        /// <param name="customerRepo">ICustomerRepository type parameter.</param>
        /// <param name="connectorRepo">IConnectorRepository type parameter.</param>
        public ClientLogic(IOrdersRepository orderRepo, ICustomersRepository customerRepo, IConnectorRepository connectorRepo)
        {
            this.orderRepo = orderRepo;
            this.customerRepo = customerRepo;
            this.connectorRepo = connectorRepo;
        }

        /// <summary>
        /// This method will change the customer's address.
        /// </summary>
        /// <param name="id">This is the id parameter. </param>
        /// <param name="address">New address.</param>
        /// <param name="city">New city.</param>
        public void ChangeCustomerAddress(int id, string address, string city)
        {
            this.customerRepo.ChangeAddress(id, city, address);
        }

        /// <summary>
        /// This method will change the customer's email address.
        /// </summary>
        /// <param name="id">This is the id parameter. </param>
        /// <param name="email">New email address. </param>
        public void ChangeCustomerEmail(int id, string email)
        {
            this.customerRepo.ChangeEmail(id, email);
        }

        /// <summary>
        /// This method will change the number of participants.
        /// </summary>
        /// <param name="id">This is the id parameter. </param>
        /// <param name="newNum">This is the number of kids. </param>
        public void ChangeNumOfKids(int id, int newNum)
        {
            Customers customer = this.customerRepo.GetOne(id);
            if (customer == null)
            {
                throw new InvalidOperationException("ERROR: No corresponding record! Did you mistype the ID?");
            }
            else
            {
                this.customerRepo.ChangeNumOfKids(id, newNum);
            }
        }

        /// <summary>
        /// This method can change the datetime for an order.
        /// </summary>
        /// <param name="id">This is the id parameter. </param>
        /// <param name="newDate">This is the new date time. </param>
        public void ChangeOrderDate(int id, DateTime newDate)
        {
            this.orderRepo.ChangeDate(id, newDate);
        }

        /// <summary>
        /// This method adds a new customer to the database.
        /// </summary>
        /// <param name="name">The name is a string.</param>
        /// <param name="address">The address is a string.</param>
        /// <param name="city">The city is a string.</param>
        /// <param name="email">The email is a string.</param>
        /// <param name="kidage">The age is an int.</param>
        /// <param name="numofkids">The number of kids is an int.</param>
        /// <returns>Returns a Customers object.</returns>
        public Customers InsertNewCustomer(string name, string address, string city, string email, int kidage, int numofkids)
        {
            Customers newObj = new Customers()
            {
                Name = name,
                Address = address,
                City = city,
                Email = email,
                KidAge = kidage,
                NumOfKids = numofkids,
            };
            this.customerRepo.Insert(newObj);
            return newObj;
        }

        /// <summary>
        /// This method removes a Customer record from the database.
        /// </summary>
        /// <param name="id">The id is an integer.</param>
        public void DeleteCustomer(int id)
        {
            Customers customer = this.customerRepo.GetOne(id);
            if (customer == null)
            {
                throw new InvalidOperationException("ERROR: No corresponding record! Did you mistype the ID?");
            }
            else
            {
                this.customerRepo.Remove(id);
            }
        }

        /// <summary>
        /// This method inserts a new order into the database.
        /// </summary>
        /// <param name="customerid">Integer referring to a customer id.</param>
        /// <param name="clownid">Integer referring to a clown id.</param>
        /// <param name="dateTime">Datetime for the order.</param>
        /// <returns>Returns an order object.</returns>
        public Orders InsertNewOrder(int customerid, int clownid, DateTime dateTime)
        {
            Orders newObj = new Orders()
            {
                CustomerId = customerid,
                ClownId = clownid,
                DateTime = dateTime,
            };
            this.orderRepo.Insert(newObj);
            return newObj;
        }

        /// <summary>
        /// This method removes an order object from the database.
        /// </summary>
        /// <param name="id">The parameter is an int.</param>
        public void DeleteOrder(int id)
        {
            Orders order = this.orderRepo.GetOne(id);
            if (order == null)
            {
                throw new InvalidOperationException("ERROR: No corresponding record! Did you mistype the ID?");
            }
            else
            {
                this.orderRepo.Remove(id);
            }
        }

        /// <summary>
        /// This method inserts a new connector table entity to the database.
        /// </summary>
        /// <param name="orderid">Integer referring to the order's id.</param>
        /// <param name="serviceid">Integer referring to the service's id.</param>
        /// <returns>Returns a Connector object.</returns>
        public ConnectorOrdersServices InsertNewConnection(int orderid, int serviceid)
        {
            ConnectorOrdersServices newObj = new ConnectorOrdersServices()
            {
                OrderId = orderid,
                ServiceId = serviceid,
            };
            this.connectorRepo.Insert(newObj);
            return newObj;
        }

        /// <summary>
        /// This method removes a record from the connector table.
        /// </summary>
        /// <param name="id">Integer.</param>
        public void DeleteConnection(int id)
        {
            ConnectorOrdersServices conn = this.connectorRepo.GetOne(id);
            if (conn == null)
            {
                throw new InvalidOperationException("ERROR: No corresponding record! Did you mistype the ID?");
            }
            else
            {
                this.connectorRepo.Remove(id);
            }
        }
    }
}
