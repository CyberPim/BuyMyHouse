using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Service;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailFunction
{
    public class Mail
    {
        private readonly IBuyerService _buyerService;
        private readonly IMortgageService _mortgageService;

        public Mail(IMortgageService mortgageService, IBuyerService buyerService)
        {
            _buyerService = buyerService;
            _mortgageService = mortgageService;
        }
        
        [FunctionName("Mail")]
        public async Task Run([TimerTrigger("0 0 6 * * *")]TimerInfo myTimer, ILogger log)
        {
            // Get all the applications and send them to the buyers
            List<Mortgage> mortgages = GetMortgages().Result;

            foreach (Mortgage mortgage in mortgages)
            {
                Buyer buyer = await _buyerService.GetBuyer(mortgage.Id);
                await SendMail(mortgage, buyer);
            }
        }

        public Task SendMail(Mortgage mortgage, Buyer buyer)
        {
            string fromMail = Environment.GetEnvironmentVariable("FromMail");
            string fromPassword = Environment.GetEnvironmentVariable("FromPassword");

            MailMessage message = new MailMessage();
            message.From = new MailAddress(fromMail);
            message.Subject = $"{mortgage.Id} - Your mortgage application";
            message.To.Add(new MailAddress($"{buyer.Email}"));
            message.Body = $"<html><body> We calculated your montly payment according to your deliverd information. <br>" +
                $" Your monthly payment is calculated at: {mortgage.MonthlyPayment} per month. </body></html>";
            message.IsBodyHtml = true;

            var smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                Credentials = new System.Net.NetworkCredential(fromMail, fromPassword),
                EnableSsl = true,
            };

            smtpClient.Send(message);
            return Task.CompletedTask;
        }

        public async Task<List<Mortgage>> GetMortgages()
        {
            // Get the mortgages from the database where the date issued is less then 1 day ago
            List<Mortgage> mortgages = await _mortgageService.GetMortgagesByIssuedDate();
            return mortgages;
        }
    }
}
