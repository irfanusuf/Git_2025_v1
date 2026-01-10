using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Models;
using P1WebAppRazor.Services;

namespace P1WebAppRazor.Pages
{
    public class LoginModel : PageModel
    {


        private readonly SqlDbContext dbcontext;

        public LoginModel(SqlDbContext dbContext)
        {
            this.dbcontext = dbContext;

    
        }

        public void OnGet()
        {
        }

        public async Task OnPost(User user)
        {
            try
            {
                var existingUser = await dbcontext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);
                if(existingUser == null)
                {
                    ViewData["errorMessage"] = "User with this email doesnt exist ! ";
                    return;
                }

                bool passVerify = BCrypt.Net.BCrypt.Verify(user.Password , existingUser.Password);

                if (passVerify)
                {

                    // jwt token generate 




                    // success mesaage 
                    ViewData["successMessage"] = "Logged in SuccesFully ! ";
                }
                else
                {
                     ViewData["errorMessage"] = "Password Incorrect ! ";
                    return;
                }
            }
            catch (Exception ex)
            {
                ViewData["errorMessage"] = ex.Message;
                throw;
            }
        }


    }
}
