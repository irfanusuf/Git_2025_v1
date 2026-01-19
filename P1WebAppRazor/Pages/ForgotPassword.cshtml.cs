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


        public ForgotPasswordModel( SqlDbContext dbContext , IMailService mailService)
        {
            this.dbContext = dbContext;
            this.mailService = mailService;
        }

        public void OnGet()
        {
        }

        public async Task OnPost(ForgotPass req)
        {
            // logic for sending otp to the mail
            // user ko find kery gey db se 
          var user =   await dbContext.Users.FirstOrDefaultAsync(u => u.Email == req.Email);

          if(user == null)
            {
                ViewData["errorMessage"]  = "User With this Email Doesnot Exist ";
            }
            else
            {
                var otp = new Random(57239847);  // security threat // random class can be predicted 

                // otp ko  db save  kerna padega 
                
               await mailService.SendMail(req.Email , "contact@algoacademy.in" , "OTP verification" , $"Thank you for reaching us your Otp is {otp} , This will valid for 5 minutes");
            };


            // service
         

        }

    }
}
