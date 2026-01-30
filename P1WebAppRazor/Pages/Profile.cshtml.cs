using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Interfaces;

namespace P1WebAppRazor.Pages
{
    public class ProfileModel : PageModel
    {


        public readonly SqlDbContext dbContext;
        public readonly ITokenService tokenService;


        public ProfileModel(SqlDbContext dbContext,  ITokenService  tokenService)
        {
            this.dbContext = dbContext;
            this.tokenService = tokenService;
        }
        public async Task<IActionResult> OnGet()
        {

            // retrieve auth token from cookies
            HttpContext.Request.Cookies.TryGetValue("auth_token", out var token);

            if(token == null)
            {
                TempData["errorMessage"] = "Your  Session has expired Kindly login again";
                return RedirectToPage("/login");
            }


           var userId =  tokenService.VerifyTokenAndGetId(token);


            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserId == userId);

            if (user != null)
            {
                   return Page();
            }
            else
            {
            TempData["errorMessage"] = "Something Went Wrong Kindly try agin after sometime";
               return RedirectToPage("/login");  
            }

         


        }
    }
}
