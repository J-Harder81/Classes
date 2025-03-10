using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class CertificateOfDepositAccount : InterestEarningAccount
    {
        public int TermLength { get; }  // term length is in months
        public decimal InterestRate { get; }
        private DateTime _maturityDate;

        public CertificateOfDepositAccount(string name, decimal initialBalance, int termLength, decimal interestRate)
            : base(name, initialBalance)
        {
            TermLength = termLength;
            InterestRate = interestRate;
            _maturityDate = DateTime.Now.AddMonths(termLength);
        }

        public override void PerformMonthEndTransactions()
        {
            // override PerformMonthEndTransactions since ther is no monthly interest, interest only applied at the end of the term.
        }

        public void ApplyMaturityInterest()
        {
            if (DateTime.Now >= _maturityDate)
            {
                decimal interestApplied = Balance * InterestRate;  // apply interest at the maturity date
                MakeDeposit(interestApplied, DateTime.Now, "CD account matured - interest applied");
            }
            else
            {
                Console.WriteLine("CD account has not yet matured.");
            }
        }

        public new void MakeWithdrawal(decimal amount, DateTime date, string note)
        {
            if (DateTime.Now < _maturityDate)
            {
                decimal penalty = Balance * 0.10m;  // apply a 10% early withdrawal penalty if withdrawl before maturity date
                Console.WriteLine($"Early withdrawal penalty of {penalty:C} applied.");
                base.MakeWithdrawal(amount + penalty, date, note);
            }
            else
            {
                base.MakeWithdrawal(amount, date, note);
            }
        }
    }
}
