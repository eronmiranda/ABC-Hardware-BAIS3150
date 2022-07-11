using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using emiranda4BAIS3150Project.Domain;
using emiranda4BAIS3150Project.TechnicalServices;

namespace emiranda4BAIS3150Project
{
    public class ABCPOS
    {
        public bool AddAnItem(Item newItem)
        {
            bool confirmation = false;

            Items ItemManager = new Items();

            confirmation = ItemManager.AddItem(newItem);

            return confirmation;
        }

        public Item FindAnItem(string itemCode)
        {
            Items ItemManager = new Items();

            return ItemManager.GetItem(itemCode);
        }

        public bool DeleteAnItem(string itemCode)
        {
            bool confirmation = false;

            Items ItemManager = new Items();

            confirmation = ItemManager.DeleteItem(itemCode);

            return confirmation; 
        }

        public bool UpdateAnItem(Item existingItem)
        {
            bool confirmation = false;

            Items ItemManager = new Items();

            confirmation = ItemManager.UpdateItem(existingItem);

            return confirmation;
        }

        public bool AddACustomer(Customer newCustomer)
        {
            bool confirmation = false;

            Customers CustomerManager = new Customers();

            confirmation = CustomerManager.AddCustomer(newCustomer);

            return confirmation;
        }

        public Customer FindACustomer(int customerId)
        {
            Customers CustomerManager = new Customers();

            return CustomerManager.GetCustomer(customerId);
        }

        public bool DeleteACustomer(int customerId)
        {
            bool confirmation = false;

            Customers CustomerManager = new Customers();

            confirmation = CustomerManager.DeleteCustomer(customerId);

            return confirmation;
        }

        public bool UpdateACustomer(Customer existingCustomer)
        {
            bool confirmation = false;

            Customers CustomerManager = new Customers();

            confirmation = CustomerManager.UpdateCustomer(existingCustomer);

            return confirmation;
        }

        public Item FindAnItemByDescription(string description)
        {
            Items ItemManager = new Items();

            return ItemManager.GetItemByDescription(description);
        }

        public int ProcessSale(Sale ABCSale)
        {
            Sales SaleManager = new Sales();

            return SaleManager.AddSale(ABCSale);
        }

        public bool AddLineItem(LineItem newLineItem)
        {
            bool confirmation = false;

            LineItems LineItemManager = new LineItems();

            confirmation = LineItemManager.AddLineItem(newLineItem);

            return confirmation;
        }
    }
}
