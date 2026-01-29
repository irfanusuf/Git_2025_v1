using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Interfaces;
using P1WebAppRazor.Models.ViewModels;

namespace P1WebAppRazor.Pages
{
    public class VerifyOtpModel : PageModel

    {

        public readonly SqlDbContext dbContext;
        public readonly IMailService mailService;


        public VerifyOtpModel(SqlDbContext dbContext, IMailService mailService)
        {
            this.dbContext = dbContext;
            this.mailService = mailService;
        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost(VerifyOtp req)
        {
            try
            {
                if (req.Password != req.CPassword)
                {
                    TempData["errorMessage"] = "Password and confirm password must be equal ! ";
                    TempData["ResetEmail"] = req.Email;
                    return Page();
                }

                var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == req.Email);
                if (user != null)
                {
                    if (user.Otp == req.OTP && user.OtpExpiry < DateTime.UtcNow )
                    {
                        var encryptPass = BCrypt.Net.BCrypt.HashPassword(req.Password);
                        user.Password = encryptPass;
                        user.Otp = "";    // otp sanitization 
                        await dbContext.SaveChangesAsync();
                        TempData["successMessage"] = "Password updated Succesfully !";
                        return RedirectToPage("/login");
                    }
                    else
                    {
                        TempData["errorMessage"] = "Otp is incorrect or Expired ! ";
                        TempData["ResetEmail"] = req.Email;
                        return Page();
                    }
                }
                else
                {
                    TempData["errorMessage"] = "User Not found !";
                    TempData["ResetEmail"] = req.Email;
                    return Page();
                }
            }
            catch (System.Exception)
            {

                throw;
            }



        }
    }
}
