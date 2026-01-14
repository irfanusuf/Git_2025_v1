using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Interfaces;
using P1WebAppRazor.Models;
using P1WebAppRazor.Services;

namespace P1WebAppRazor.Pages
{
    public class LoginModel : PageModel
    {


        private readonly SqlDbContext dbcontext;

        private readonly ITokenService tokenService;

        public LoginModel(SqlDbContext dbContext, ITokenService tokenService)
        {
            this.dbcontext = dbContext;
            this.tokenService = tokenService;


        }

        public void OnGet()
        {
        }

        public async Task OnPost(User user)
        {
            try
            {

                if (string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
                {
                    ViewData["errorMessage"] = "Email and password both are required  ! ";
                    return;
                }
                // sql queries in LINQ
                var existingUser = await dbcontext.Users.FirstOrDefaultAsync(u => u.Email == user.Email);   //O(1)    // O(n)

                if (existingUser == null)
                {
                    ViewData["errorMessage"] = "User with this email doesnt exist ! ";
                    return;
                }

                bool passVerify = BCrypt.Net.BCrypt.Verify(user.Password, existingUser.Password);

                if (passVerify)
                {

                    // jwt token generate  // it is very effiecnet 
                    var token = tokenService.CreateToken(existingUser.UserId, existingUser.Email, existingUser.Username, 7);

                    // cookie send kernay haiuu // kisko browser kpo 
                    HttpContext.Response.Cookies.Append("auth_token" , token , new CookieOptions
                    {
                        Expires = DateTime.UtcNow.AddDays(7),
                        SameSite = SameSiteMode.Strict,
                        HttpOnly = true,
                        Secure = false
                        
                    });

                    // success mesaage 
                    TempData["successMessage"] = "Logged in SuccesFully ! ";


                    Redirect("Profile");
              
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
