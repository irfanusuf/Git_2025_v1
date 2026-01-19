using System;


namespace P1WebAppRazor.Interfaces;

public interface IMailService
{
    public Task SendMail(string address, string from, string subject, string body, bool isHtml = false);

}
