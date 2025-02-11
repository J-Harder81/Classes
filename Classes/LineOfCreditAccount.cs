using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Classes
{
    public class LineOfCreditAccount : BankAccount
    {
        public LineOfCreditAccount(string name, decimal initialBalance, decimal creditLimit, decimal? creditLine = null) : base(name, initialBalance, -creditLimit)
        {
            CreditLine = creditLine;
        }

        public decimal? CreditLine { get; set; }

        public void CheckCreditLine(decimal amount)
        { 
            if (CreditLine.HasValue && (Balance - amount) < -CreditLine.Value)
            {
                throw new ArgumentOutOfRangeException("Transaction denied! Credit line exceeded!");
            }
        }
        protected override Transaction? CheckWithdrawalLimit(bool isOverdrawn) =>
            isOverdrawn
            ? new Transaction(-20, DateTime.Now, "Apply overdraft fee")
            : default;

        public override void PerformMonthEndTransactions()
        {
            if (Balance < 0)
            {
                // Negate the balance to get a positive interest charge:
                decimal interest = -Balance * 0.07m;
                MakeWithdrawal(interest, DateTime.Now, "Charge monthly interest");
            }
        }
    }
}
