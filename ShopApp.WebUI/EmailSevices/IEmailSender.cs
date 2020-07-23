using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ShopApp.WebUI.EmailSevices
{
    public interface IEmailSender
    {
        //smtp => gmail, hotmail
        //api => sendgrid (max 100 free)

        Task SendEmailAsync(string email, string subject, string htmlMessage);

    }
}
