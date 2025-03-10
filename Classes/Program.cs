using Classes;
using System.Security.Principal;

var account = new BankAccount("Justin Harder", 1000);
Console.WriteLine($"Account {account.Number} was created for {account.Owner} with {account.Balance} initial balance.");

account.MakeWithdrawal(500, DateTime.Now, "Rent payment");
Console.WriteLine(account.Balance);
account.MakeDeposit(100, DateTime.Now, "Friend paid me back");
Console.WriteLine(account.Balance);

Console.WriteLine(account.GetAccountHistory());

// Test for a negative balance.
try
{
    account.MakeWithdrawal(750, DateTime.Now, "Attempt to overdraw");
}
catch (InvalidOperationException e)
{
    Console.WriteLine("Exception caught trying to overdraw");
    Console.WriteLine(e.ToString());
}

// Test that the initial balances must be positive.
BankAccount invalidAccount;
try
{
    invalidAccount = new BankAccount("invalid", -55);
}
catch (ArgumentOutOfRangeException e)
{
    Console.WriteLine("Exception caught creating account with negative balance");
    Console.WriteLine(e.ToString());
    return;
}

var giftCard = new GiftCardAccount("gift card", 100, 50);
giftCard.MakeWithdrawal(20, DateTime.Now, "get expensive coffee");
giftCard.MakeWithdrawal(50, DateTime.Now, "buy groceries");
giftCard.PerformMonthEndTransactions();
// can make additional deposits
giftCard.MakeDeposit(27.50m, DateTime.Now, "add some additional spending money");
Console.WriteLine(giftCard.GetAccountHistory());

var savings = new InterestEarningAccount("savings account", 10000);
savings.MakeDeposit(750, DateTime.Now, "save some money");
savings.MakeDeposit(1250, DateTime.Now, "Add more savings");
savings.MakeWithdrawal(250, DateTime.Now, "Needed to pay monthly bills");
savings.PerformMonthEndTransactions();
Console.WriteLine(savings.GetAccountHistory());

var lineOfCredit = new LineOfCreditAccount("line of credit", 0, 2000);
// How much is too much to borrow?
lineOfCredit.MakeWithdrawal(1000m, DateTime.Now, "Take out monthly advance");
lineOfCredit.MakeDeposit(50m, DateTime.Now, "Pay back small amount");
lineOfCredit.MakeWithdrawal(5000m, DateTime.Now, "Emergency funds for repairs");
lineOfCredit.MakeDeposit(150m, DateTime.Now, "Partial restoration on repairs");
lineOfCredit.PerformMonthEndTransactions();
Console.WriteLine(lineOfCredit.GetAccountHistory());

// Test the LineOfCreditAccount class with a CreditLine
var accountWithCreditLine = new LineOfCreditAccount("credit card", 0, 500, 1000);
accountWithCreditLine.MakeWithdrawal(250m, DateTime.Now, "New clothes");
accountWithCreditLine.MakeWithdrawal(500m, DateTime.Now, "Laptop for school");
accountWithCreditLine.MakeDeposit(50m, DateTime.Now, "Monthly payment");
accountWithCreditLine.PerformMonthEndTransactions();
Console.WriteLine(accountWithCreditLine.GetAccountHistory());

// Test the CertificateOfDepositAccount class
var cdAccount = new CertificateOfDepositAccount("CD Account", 5000, 12, 0.05m);
Console.WriteLine($"CD Account created with balance: {cdAccount.Balance}");
cdAccount.MakeDeposit(1000, DateTime.Now, "Additional deposit");
Console.WriteLine($"After deposit: {cdAccount.Balance}");
cdAccount.MakeWithdrawal(2000, DateTime.Now, "Early withdrawal"); // Attempt an early withdrawal before maturity
cdAccount.ApplyMaturityInterest(); // Simulate the term completion
Console.WriteLine(cdAccount.GetAccountHistory());

// Attempt to exceed the CreditLine ammount
try
{
    decimal withdrawalAmount = 300;
    accountWithCreditLine.CheckCreditLine(withdrawalAmount);
    accountWithCreditLine.MakeWithdrawal(withdrawalAmount, DateTime.Now, "Attempt to exceed credit line amount");
}
catch (InvalidOperationException e)
{
    Console.WriteLine("Exception caught trying to overdraw");
    Console.WriteLine(e.ToString());
}






