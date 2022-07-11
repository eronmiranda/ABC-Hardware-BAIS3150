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
    public class DeleteAnItemModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }
        public bool Disable { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Item Code is required")]
        [RegularExpression("[A-Z]{1}[0-9]{5}", ErrorMessage = "Item Code must have one letter and followed by 5 numbers (e.g. A12345)")]
        public string ItemCode { get; set; }
        
        public Item ExistingItem { get; set; }

        public void OnGet()
        {
            Disable = true;
        }

        public void OnPostSearch()
        {
            Disable = true;
            try
            {
                ExistingItem = new ABCPOS().FindAnItem(ItemCode);
                Disable = false;
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
                return;
            }
        }

        public void OnPostRemove()
        {
            if (DeleteAnItem(ItemCode))
            {
                ErrorMessage = "";
                FormMessage = "You have successfully deleted " + ExistingItem.ItemCode;
            }
            Disable = true;
        }

        public void OnPostClear()
        {
            ItemCode = "";
            ModelState.Clear();
            Disable = true;
        }

        public bool DeleteAnItem(string itemCode)
        {
            bool success = false;
            ABCPOS ABCHardware = new ABCPOS();
            try
            {
                ExistingItem = ABCHardware.FindAnItem(itemCode);
                ErrorMessage = "";
                success = ABCHardware.DeleteAnItem(itemCode);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
            return success;
        }
    }
}
