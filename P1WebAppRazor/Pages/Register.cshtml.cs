using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using P1WebAppRazor.Models;

namespace P1WebAppRazor.Pages
{
    public class RegisterModel : PageModel
    {
        public void OnGet()
        {
        }

        public void OnPost(User user)
        {
            //testing purpose 

            Console.WriteLine(user.UserId);
            Console.WriteLine(user.Username);
            Console.WriteLine(user.Email);
            Console.WriteLine(user.Password);


            // actual work is save the above data in db
            // redirect the user to the profile page 


            // return RedirectToAction("/profile");

        }
    }
}
