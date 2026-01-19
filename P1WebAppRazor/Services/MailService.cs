using System;
using System.Net;
using System.Net.Mail;
using P1WebAppRazor.Interfaces;

namespace P1WebAppRazor.Services;

public class MailService : IMailService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPass;


    public MailService(IConfiguration configuration)
    {

        _smtpServer = "mail542.mailasp.net ";
        _smtpPort = 465;
        _smtpUser = configuration["Smtp : SmtpUser"] ?? throw new ArgumentNullException("smtp user is not configured !");
        _smtpPass = configuration["Smtp : SmtpPass"] ?? throw new ArgumentNullException("smtp password is not configured !");

    }

    public async Task SendMail(string address, string from, string subject, string body, bool isHtml = false)
    {
        try
        {
            var client = new SmtpClient(_smtpServer, _smtpPort)
            {
                Credentials = new NetworkCredential(_smtpUser, _smtpPass),
                EnableSsl = true
            };
            var mailMessage = new MailMessage
            {
                From = new MailAddress(from),
                Subject = subject,
                Body = body,
                IsBodyHtml = isHtml

            };

            mailMessage.To.Add(address);
            await client.SendMailAsync(mailMessage);
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
            throw;
        }
    }
}
