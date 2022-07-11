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
    public class AddACustomerModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string CustomerFirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string CustomerLastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage="Address is required")]
        public string Address { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "City is required")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string City { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Province is required")]
        public string Province { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Postal Code is required")]
        [RegularExpression("[A-Z][0-9][A-Z] [0-9][A-Z][0-9]", ErrorMessage = "Please enter a correct format for Postal Code (eg. A1B 0C2")]
        public string PostalCode { get; set; }

        public void OnPostClear()
        {
            CustomerFirstName = "";
            CustomerLastName = "";
            Address = "";
            City = "";
            Province = "";
            PostalCode = "";
            ModelState.Clear();
        }

        public void OnPostAddCustomer()
        {
            Customer newCustomer = new Customer
            {
                CustomerFirstName = CustomerFirstName,
                CustomerLastName = CustomerLastName,
                Address = Address,
                City = City,
                Province = Province,
                PostalCode = PostalCode,
                Status = "ACTIVE"
            };

            if (AddACustomer(newCustomer))
            {
                FormMessage = "You have successfully added " + newCustomer.CustomerFirstName + " " + newCustomer.CustomerLastName + ".";

                OnPostClear();
            }
            else
            {
                ErrorMessage = "Customer was not successfully added.";
            }
        }

        public bool AddACustomer(Customer newCustomer)
        {
            bool success = false;

            ABCPOS ABCHardware = new ABCPOS();

            try
            {
                success = ABCHardware.AddACustomer(newCustomer);
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message.ToString();
            }

            return success;
        }
    }
}
