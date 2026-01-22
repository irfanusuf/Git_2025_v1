using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using P1WebAppRazor.Data;
using P1WebAppRazor.Interfaces;
using P1WebAppRazor.Models;
using P1WebAppRazor.Models.ViewModels;

namespace P1WebAppRazor.Pages
{
    public class ForgotPasswordModel : PageModel
    {

        public readonly SqlDbContext dbContext;
        public readonly IMailService mailService;


        public ForgotPasswordModel(SqlDbContext dbContext, IMailService mailService)
        {
            this.dbContext = dbContext;
            this.mailService = mailService;
        }

        public void OnGet()
        {
        }

        public async Task <IActionResult> OnPost(ForgotPass req)
        {
            // logic for sending otp to the mail
            // user ko find kery gey db se 
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == req.Email);

            if (user == null)
            {
                ViewData["errorMessage"] = "User With this Email Doesnot Exist ";
                return Page();
            }
            else
            {
                // Generate a better OTP (using Guid or other secure method)
                var otp = new Random().Next(100000, 999999).ToString(); // 6-digit OTP

                // update otp in the context
                user.Otp = otp;

                // otp ko db save kerna padega 
                await dbContext.SaveChangesAsync();



                Console.WriteLine(otp);
                   

                await mailService.SendMail(
                    req.Email,
                    "contact@algoacademy.in",
                    "OTP verification",
                    $"Thank you for reaching us your Otp is {otp}, This will be valid for 5 minutes");

                

                TempData["ResetEmail"] = req.Email;
                TempData["successMessage"] = "OTP is sent to your registered Email";

                // Redirect to VerifyOtp page
                return RedirectToPage("/VerifyOtp"); 
            }
        }


    }
}
