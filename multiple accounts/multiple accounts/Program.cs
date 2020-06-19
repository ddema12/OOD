using multiple_accounts;
using System;
using System.ComponentModel.Design;
using System.Dynamic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace BankAccount
{
    enum MenuOptions
    {
        Withdraw = 0,
        Deposit = 1,
        Print = 2,
        Transfer = 3,
        Add_New_Account = 4,
        Rollback = 5,
        Quit = 6
        //add new account
    }

    class BankingSystem
    {
        static MenuOptions ReadUserOption()
        {           
                int result;
                do
                {
                    Console.WriteLine("Please select from the following options :");
                    Console.WriteLine("1 Withdraw ");
                    Console.WriteLine("2 Deposit ");
                    Console.WriteLine("3 Print ");
                    Console.WriteLine("4 Transfer ");
                    Console.WriteLine("5 Add New Account ");
                    Console.WriteLine("6 Rollback ");
                    Console.WriteLine("7 Quit ");
                    
                    result = Convert.ToInt32(Console.ReadLine());
                }
                while (result < 1 || result > 7);
                return (MenuOptions)(result - 1);    
        }

        static void DoWithdraw(Bank bank)
        {
            var account = FindAccount(bank);
            Console.WriteLine("Enter the amount you would like to withdraw: ");
            if (account == null) return; //stop continuation of procedure
            decimal x = Convert.ToDecimal(Console.ReadLine());
            var transaction = new WithdrawTransaction(account, x);
            bank.ExecuteTransaction(transaction);
            transaction.Print();
            /*transaction.Rollback();*/
    
        }
        static void DoDeposit(Bank bank)
        {
            var account = FindAccount(bank);
            if (account == null) return; //stop continuation of procedure
            Console.WriteLine("Enter the amount you would like to deposit: ");
            decimal x = Convert.ToDecimal(Console.ReadLine());
            var transaction = new DepositTransaction(account, x);
            bank.ExecuteTransaction(transaction);
            transaction.Print();
        }
        static void DoPrint(Bank bank)
        {
            Account account = FindAccount(bank);
            if (account == null) return; //stop continuation of procedure
            account.PrintContents();
        }
        static void DoTransfer(Bank bank)
        {
           
                Console.WriteLine("Sender Account :");
                Account from_account = FindAccount(bank);
                if (from_account == null) return; //stop continuation of procedure
                Console.WriteLine("Reciever Account :");
                Account to_account = FindAccount(bank);
                if (to_account == null) return; //stop continuation of procedure
                Console.WriteLine("Enter the amount you would like to transfer: ");
                decimal x = Convert.ToDecimal(Console.ReadLine());
                var transaction = new TransferTransaction(from_account, to_account, x);
                bank.ExecuteTransaction(transaction);
                transaction.Print();                
        }

        static void Add_Account(Bank bank)
        {
            Console.WriteLine("Enter Account name :"); 
            String name = Console.ReadLine();
            Console.WriteLine("How much do you want to put in your account?");
            decimal bal = Convert.ToDecimal(Console.ReadLine());
            bank.AddAccount(new Account(name, bal));

        }
        private static Account FindAccount(Bank bank)
        {
            
            try
            {
                Console.WriteLine("Enter your account name");
                string name = Console.ReadLine();
                Account account = bank.GetAccount(name);
                if (account == null) Console.WriteLine("account not found");
                return account;
            }
            catch (NullReferenceException E)
            {
                Console.WriteLine(E.Message + " Please try again ");
                return null;
            }
     
        }
        private static void DoRollback(Bank bank) 
        {
            bank.PrintTranscationHistory();
            Console.WriteLine("Do you want to rollback a transaction? Y/N ");
            string x = Console.ReadLine();
            try
            {
                if (x == "n") return;
                else if (x == "y")
                {
                    Console.WriteLine("Select which transaction using the index");
                    int r = Convert.ToInt32(Console.ReadLine());
                    bank.RollbackTransaction(bank.getTheList()[r - 1]);

                }
                else throw new InvalidOperationException("Unable to rollback transaction, please try again");
            }
            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
            }
           
        }

        static void Main(string[] args)
        {
            var bank = new Bank();
            MenuOptions userInput;
            do
            {
                userInput = ReadUserOption();

                switch (userInput)
                {
                    case MenuOptions.Withdraw:
                        DoWithdraw(bank);
                        break;

                    case MenuOptions.Deposit:
                        DoDeposit(bank);
                        break;

                    case MenuOptions.Print:
                        DoPrint(bank);
                        break;

                    case MenuOptions.Transfer:
                        DoTransfer(bank);
                        break;

                   case MenuOptions.Add_New_Account:
                        Add_Account(bank);
                        break;

                    case MenuOptions.Rollback:
                        DoRollback(bank);
                        break;

                    case MenuOptions.Quit:
                        Console.WriteLine("Ending interaction");
                        break;
                }

            } while (userInput != MenuOptions.Quit);

        }
    }
}
