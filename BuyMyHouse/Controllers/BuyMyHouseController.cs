using BuyMyHouse.DTO;
using Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using SendGrid.Helpers.Mail;
using Service.ServiceInterface;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using ServiceStack.Logging;
using Microsoft.Azure.Cosmos.Serialization.HybridRow;
using System.ComponentModel.DataAnnotations.Schema;
using Service;

namespace BuyMyHouse.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyMyHouseController : ControllerBase
    {
        private readonly IBuyerService _buyerService;
        private readonly IMortgageService _mortgageService;
        private readonly ILogger<BuyMyHouseController> _logger;

        public BuyMyHouseController(ILogger<BuyMyHouseController> logger, IBuyerService buyerService, IMortgageService mortgageService)
        {
            _logger = logger;
            _buyerService = buyerService;
            _mortgageService = mortgageService;
        }

        [HttpPost("buyer", Name = "AddBuyer")]
        public async Task<Buyer> AddBuyer(Buyer buyer)
        {
            Buyer result = await _buyerService.AddBuyer(buyer);
            return result;
        }

        [HttpPost("mortage", Name = "AddMortgage")]
        public async Task<Mortgage> AddMortgage(MortgageDTO mortgage)
        {

            Mortgage newMortgage = new Mortgage()
            {
                Id = mortgage.Id,
                LoanAmount = mortgage.LoanAmount,
                InterestRate = mortgage.InterestRate,
                LoanDuration = mortgage.LoanDuration,
                DateIssued = DateTime.Now,
                MonthlyPayment = 0,
                PartitionKey = mortgage.LoanDuration.ToString()
            };

            Mortgage result = await _mortgageService.AddMortage(newMortgage);
            return result;
        }

    }
    
}