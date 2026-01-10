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
            // password validation  and length
            if (string.IsNullOrEmpty(user.Password) || user.Password.Length < 8)
            {
                ViewData["errorMessage"] = "Password must be at least 8 Characters !";
                return;
            }
            // email and username validation 
            if (string.IsNullOrEmpty(user.Username) || string.IsNullOrEmpty(user.Email))
            {
                ViewData["errorMessage"] = "Email and username both are required !";
                return;
            }
            // check existing user 
            var existingUser = await dbcontext.Users.FirstOrDefaultAsync((u) => u.Email == user.Email);
            if (existingUser != null)
            {
                ViewData["errorMessage"] = "user with This email already exists";
            }
            else
            {
                var encryptedPass = BCrypt.Net.BCrypt.HashPassword(user.Password);

                user.Password = encryptedPass;
                await dbcontext.Users.AddAsync(user);
                await dbcontext.SaveChangesAsync();
                ViewData["successMessage"] = "User Registerd Succesfully";
            }
        }
    }
}
