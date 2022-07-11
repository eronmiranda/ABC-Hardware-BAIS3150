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
    public class UpdateACustomerModel : PageModel
    {
        public string ErrorMessage { get; set; }
        public string FormMessage { get; set; }
        public bool Disable { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Customer ID is required")]
        public int CustomerId { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "First Name is required")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string CustomerFirstName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Last Name is required")]
        [RegularExpression("[A-Za-z ]+", ErrorMessage = "Special Characters and numbers are not allowed.")]
        public string CustomerLastName { get; set; }

        [BindProperty]
        [Required(ErrorMessage = "Address is required")]
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

        public Customer ExistingCustomer { get; set; }

        public void OnPostSearch()
        {
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

        public void OnPostUpdate()
        {
            Customer existingCustomer = new Customer
            {
                CustomerId = CustomerId,
                CustomerFirstName = CustomerFirstName,
                CustomerLastName = CustomerLastName,
                Address = Address,
                City = City,
                Province = Province,
                PostalCode = PostalCode
            };

            if (UpdateACustomer(existingCustomer))
            {
                FormMessage = "You have successfully updated " + existingCustomer.CustomerFirstName + " " + existingCustomer.CustomerLastName + "'s information.";
                Disable = true;
            }
            else
            {
                ErrorMessage = "Updating item's information was not successful.";
            }
        }

        public void OnPostClear()
        {
            CustomerId = new int();
            CustomerFirstName = "";
            CustomerLastName = "";
            Address = "";
            City = "";
            Province = "";
            PostalCode = "";
            Disable = true;

            ModelState.Clear();
        }

        public void OnGet()
        {
            Disable = true;
        }

        public bool UpdateACustomer(Customer existingCustomer)
        {
            bool success = false;
            ABCPOS ABCHardware = new ABCPOS();
            try
            {
                success = ABCHardware.UpdateACustomer(existingCustomer);
                ExistingCustomer = new Customer();
            }
            catch (Exception ex)
            {

                ErrorMessage = ex.Message.ToString();
            }

            return success;
        }
    }
}

