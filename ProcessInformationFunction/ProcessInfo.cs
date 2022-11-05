using Domain;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProcessInformationFunction
{
    public class ProcessInfo
    {
        private readonly IMortgageService _mortgageService;
        public ProcessInfo(IMortgageService mortgageService)
        {
            _mortgageService = mortgageService;
        }
        
        
        [FunctionName("ProcessInformation")]
        public async Task Run([TimerTrigger("0 0 * * * *")]TimerInfo myTimer, ILogger<ProcessInfo> log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            List<Mortgage> mortgages = await GetMortgages();

            foreach (Mortgage mortgage in mortgages)
            {
                mortgage.MonthlyPayment = CalculateMonthlyPayment(mortgage);
                mortgage.DateIssued = DateTime.Now;
                await _mortgageService.UpdateMortgage(mortgage.Id, mortgage);
            }

            log.LogInformation($"C# Timer trigger function finished at: {DateTime.Now}");
        }
        
        public async Task<List<Mortgage>> GetMortgages()
        {
            // Get all the mortgages from the database
            List<Mortgage> mortgages = await _mortgageService.GetMortgages();
            return mortgages;
        }

        public static double CalculateMonthlyPayment(Mortgage mortgage)
        {
            // Formula  a = [ P(1 + r)Yr ] / [ (1 + r)Y - 1 ]

            // p = loan amount
            // r = interest rate ((interes / 100) / 12 months)
            // Y = loan duration in months (10 years == 120 months etc)

            double loanAmount = mortgage.LoanAmount;
            double interestRate = mortgage.InterestRate;
            int loanDuration = mortgage.LoanDuration;

            double monthlyInterestRate = (interestRate / 100) / 12;
            double monthlyPayment = loanAmount * Math.Pow(1 + monthlyInterestRate,12 * loanDuration) / (Math.Pow(1 + monthlyInterestRate, loanDuration) - 1);
            return monthlyPayment;
        }
    }
}
