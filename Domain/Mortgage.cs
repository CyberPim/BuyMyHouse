using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Mortgage : IEntityBase
    {
        public int Id { get; set; }
        public Double LoanAmount { get; set; }
        public int InterestRate { get; set; }
        public int LoanDuration { get; set; }
        public Double MonthlyPayment { get; set; }
        public DateTime DateIssued { get; set; }
        public string PartitionKey { get; set; }
    }
}
