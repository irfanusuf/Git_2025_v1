using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Models;

namespace P1WebAppRazor.Pages
{
    public class RegisterModel : PageModel
    {

        private readonly SqlDbContext dbcontext;

        public RegisterModel(SqlDbContext dbContext)
        {
            this.dbcontext = dbContext;
        }


        public void OnGet()
        {
        }

        public async Task OnPost(User user)
        {
            // actual work is save the above data in db
            // user ko create 

            if(string.IsNullOrEmpty(user.Password )  || user.Password.Length < 8 )
            {
                
                 ViewData["errorMessage"] = "All the details are required!";
                 return;

            } 

            var existingUser = await dbcontext.Users.FirstOrDefaultAsync((u) => u.Email == user.Email);

            if (existingUser != null)
            {
                ViewData["errorMessage"] = "user with This email already exists";
            }
            else
            {
                var encryptedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.Password = encryptedPass;

                var RegisterNewuser = await dbcontext.Users.AddAsync(user);
                await dbcontext.SaveChangesAsync();

                // mail send 

                if (RegisterNewuser != null)
                {

                    ViewData["successMessage"] = "User Registerd Succesfully";

                }

            }


            // redirect the user to the profile page 


            // return RedirectToAction("/profile");

        }
    }
}
