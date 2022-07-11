using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using emiranda4BAIS3150Project.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace emiranda4BAIS3150Project.Pages
{
    public class AddAnItemModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Item Code is required")]
        [RegularExpression("[A-Z]{1}[0-9]{5}", ErrorMessage = "Item Code must have one letter and followed by 5 numbers (e.g. A12345)")]
        public string ItemCode { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Unit Price is required")]
        [RegularExpression(@"^\d+\.{1}\d{1,2}$", ErrorMessage = "It must be a decimal number and must not have special characters.")]
        public string UnitPrice { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Status is required")]
        [RegularExpression(@"^(IN STOCK|OUT OF STOCK)$", ErrorMessage = "Please assign a status: IN STOCK or OUT OF STOCK.")]
        public string Status { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Quantity on Hand is required")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Quantity on Hand must be a whole number.")]
        public string QuantityOnHand { get; set; }

        public void OnPostAddItem()
        {
            Item newItem = new Item
            {
                ItemCode = ItemCode,
                Description = Description,
                UnitPrice = double.Parse(UnitPrice),
                Status = Status,
                QuantityOnHand = int.Parse(QuantityOnHand)
            };

            if (AddAnItem(newItem))
            {
                FormMessage = "You have successfully added " + newItem.ItemCode + ": " + newItem.Description + ".";

                ItemCode = "";
                Description = "";
                UnitPrice = "";
                Status = "";
                QuantityOnHand = "";
                ModelState.Clear();
            }
            else
            {
                ErrorMessage = "Item was not successfully added.";
            }
        }

        public void OnPostClear()
        {
            ItemCode = "";
            Description = "";
            UnitPrice = "";
            Status = "";
            QuantityOnHand = "";
            ModelState.Clear();
        }

        public bool AddAnItem(Item newItem)
        {
            bool success = false;
            ABCPOS ABCHardware = new ABCPOS();

            try
            {
                success = ABCHardware.AddAnItem(newItem);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }

            return success;
        }
    }
}
