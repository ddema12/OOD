using BankAccount;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
/*using System.Transactions;*/

namespace multiple_accounts
{
    class Bank
    {

        private List<Account> _accounts { get; set; }
        private List<Transaction> _transactions { get; set; }
        public Bank()
        {
            _accounts = new List<Account>();
            _transactions = new List<Transaction>();
        }

        public List<Transaction> getTheList()
        {
            return _transactions;
        }
        public void AddAccount(Account account)
        {
            _accounts.Add(account);
        }

        public Account GetAccount(String name) //user enters name of their bank account
        {
            foreach (Account account in _accounts) //iterate through accounts, to find users account0
            {
                if (name == account.get_name())
                    return account;
            }
            return null;
        }


        public void ExecuteTransaction(Transaction transaction)
        {
            _transactions.Add(transaction);
            transaction.Execute();
        }

        public void RollbackTransaction(Transaction transaction)
        {
            transaction.Rollback();
            _transactions.Remove(transaction);   
        }

        public void PrintTranscationHistory()
        {
            int i = 0; 
            foreach (Transaction transaction in _transactions)
                {
                i = i + 1;
                Console.Write(i + " "); transaction.Print();
                }
        }
    }
}

