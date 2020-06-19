using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    class Account
    {
        private String _name;
        private decimal _balance;

        public Account(String name, decimal balance)
        {
            this._name = name;
            this._balance = balance;
        }

        public string get_name()
        {
            return this._name;
        }
        public decimal get_balance()
        {
            return this._balance;
        }

        public bool Deposit(decimal amount)
        {
            if (amount < 0)
            {
                return false;
            }
            else
                this._balance += amount;
            return true;
        }


        public bool Withdraw(decimal withdraw)
        {
            try
            {
                if (withdraw < _balance && withdraw >= 0)
                {
                    this._balance -= withdraw;
                    return true;
                }
                else
                    throw new InvalidOperationException("Insuffiecnt funds or negative withdraw attempted");
            }
            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
                return false;
            }
        }

        public void PrintContents()
        {
            Console.WriteLine("Account type : " + _name + "\nBalance : " + _balance.ToString("C"));
        }
    }


}
