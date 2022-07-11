using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using emiranda4BAIS3150Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace emiranda4BAIS3150Project.Pages
{
    public class ProcessASaleModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }

        public string ItemCode { get; set; }
        public int Quantity { get; set; }

        public int SaleNumber { get; set; }

        public void OnPostProcessSale()
        {
            ABCPOS ABCHardware = new ABCPOS();
            string SaleString = Request.Form["SaleString"];
            List<CartItem> CartItemList;

            CartItemList = JsonSerializer.Deserialize(SaleString, typeof(List<CartItem>)) as List<CartItem>;

            List<LineItem> LineItems = new List<LineItem>();

            foreach (var item in CartItemList)
            {
                LineItem lineItem = new LineItem();
                lineItem.ItemCode = ABCHardware.FindAnItemByDescription(item.ItemCode).ItemCode;
                lineItem.Quantity = int.Parse(item.Quantity);
                lineItem.SaleNumber = 1;
                LineItems.Add(lineItem);
            }

            Sale newSale = new Sale
            {
                SaleDate = DateTime.Now,
                SalespersonId = 1,
                CustomerId = 1,
                LineItems = LineItems
            };

            SaleNumber = ProcessSale(newSale);
            if (SaleNumber != 0)
            {
                FormMessage = "You have successfully processed a Sale with Sale Number: " + SaleNumber;
            }
            else
            {
                ErrorMessage = "You have not successfully processed the sale.";
            }
        }

        public int ProcessSale(Sale ABCSale)
        {
            int saleNumber = 0;
            ABCPOS ABCHardware = new ABCPOS();

            try
            {
                saleNumber = ABCHardware.ProcessSale(ABCSale);
                ABCSale.SaleNumber = saleNumber;
                foreach (var lineItem in ABCSale.LineItems)
                {
                    lineItem.SaleNumber = saleNumber;
                    ABCHardware.AddLineItem(lineItem);
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }
            return saleNumber;
        }
    }
}
