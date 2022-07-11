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
    public class DeleteACustomerModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }
        public bool Disable { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Customer ID is required")]
        public int CustomerId { get; set; }

        public Customer ExistingCustomer { get; set; }

        public void OnGet()
        {
            Disable = true;
        }
        public void OnPostClear()
        {
            CustomerId = new int();

            Disable = true;

            ModelState.Clear();
        }

        public void OnPostSearch()
        {
            Disable = true;
            try
            {
                ExistingCustomer = new ABCPOS().FindACustomer(CustomerId);
                Disable = false;
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
        }
        public void OnPostRemove()
        {
            if (DeleteACustomer(CustomerId))
            {
                ErrorMessage = "";
                FormMessage = "You have successfully deleted " + ExistingCustomer.CustomerFirstName + " " + ExistingCustomer.CustomerLastName + "'s Account";
            }
            Disable = true;
        }
        public bool DeleteACustomer(int customerId)
        {
            bool success = false;
            ABCPOS ABCHardware = new ABCPOS();
            try
            {
                ExistingCustomer = ABCHardware.FindACustomer(customerId);
                ErrorMessage = "";
                success = ABCHardware.DeleteACustomer(customerId);
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }
            return success;
        }
    }
}
