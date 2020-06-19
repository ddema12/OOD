using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;

namespace BankAccount
{
    class DepositTransaction : Transaction
    {
        private Account _account;
       
        public DepositTransaction(Account account, decimal amount)
            : base(amount)
        {
            _account = account;
        }

        public override void Print()
        {
            if (_success == true)
                Console.WriteLine(_dateStamp + " succesfully deposited " + _amount.ToString("C") + " to " + _account.get_name());
        }
        public override void Execute()
        {
            base.Execute();
            try
            {
                if (_amount > 0)
                {
                    _account.Deposit(_amount);
                    _success = true;
                }
                else throw new InvalidOperationException("Cannot deposit negative amount");
            }

            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
            }
        }
        public override void Rollback()
        {
            try
            {
                base.Rollback();
                _account.Withdraw(_amount);
                Print();
            }
            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
            }

        }
    }
}
