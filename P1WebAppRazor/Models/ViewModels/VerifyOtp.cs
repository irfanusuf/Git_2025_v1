using System;

namespace P1WebAppRazor.Models.ViewModels;

public class VerifyOtp
{


    public required string Email { get; set; }
    public required string OTP { get; set; }
    public required string Password { get; set; }
    public required string CPassword { get; set; }


}
