using System;
using System.Collections.Generic;

class BankAccount
{
    private double balance;
    private readonly string accountNumber;

    public BankAccount(string accountNumber, double initialBalance)
    {
        this.accountNumber = accountNumber;
        if (initialBalance < 100)
        {
            throw new Exception("Initial balance must be at least $100.");
        }
        this.balance = initialBalance;
    }

    public double Balance
    {
        get { return balance; }
    }

    public void Deposit(double amount)
    {
        if (amount > 10000)
        {
            throw new Exception("Cannot deposit more than $10,000 in a single transaction.");
        }
        balance += amount;
    }

    public void Withdraw(double amount)
    {
        if (balance - amount < 100)
        {
            throw new Exception("Account balance cannot be less than $100.");
        }
        if (amount > balance * 0.9)
        {
            throw new Exception("Cannot withdraw more than 90% of the account balance in a single transaction.");
        }
        balance -= amount;
    }
}

class User
{
    private readonly string username;
    private readonly List<BankAccount> accounts;

    public User(string username)
    {
        this.username = username;
        this.accounts = new List<BankAccount>();
    }

    public void CreateAccount(string accountNumber, double initialBalance)
    {
        accounts.Add(new BankAccount(accountNumber, initialBalance));
    }

    public void DeleteAccount(string accountNumber)
    {
        foreach (BankAccount account in accounts)
        {
            if (accountNumber == account.AccountNumber)
            {
                accounts.Remove(account);
                return;
            }
        }
        throw new Exception("Account not found.");
    }

    public void Deposit(string accountNumber, double amount)
    {
        foreach (BankAccount account in accounts)
        {
            if (accountNumber == account.AccountNumber)
            {
                account.Deposit(amount);
                return;
            }
        }
        throw new Exception("Account not found.");
    }

    public void Withdraw(string accountNumber, double amount)
    {
        foreach (BankAccount account in accounts)
        {
            if (accountNumber == account.AccountNumber)
            {
                account.Withdraw(amount);
                return;
            }
        }
        throw new Exception("Account not found.");
    }

    public double GetTotalBalance()
    {
        double totalBalance = 0;
        foreach (BankAccount account in accounts)
        {
            totalBalance += account.Balance;
        }
        return totalBalance;
    }
}

class Program
{
    static void Main()
    {
        User user = new User("JohnDoe");
        user.CreateAccount("1234", 500);
        user.CreateAccount("5678", 1000);

        Console.WriteLine("Total balance: $" + user.GetTotalBalance());

        user.Deposit("1234", 500);
        user.Withdraw("5678", 300);

        Console.WriteLine("Total balance: $" + user.GetTotalBalance());

        user.Withdraw("5678", 1000);
    }
}