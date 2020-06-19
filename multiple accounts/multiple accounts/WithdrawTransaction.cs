using BankAccount;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace BankAccount
{
    class WithdrawTransaction : Transaction
    {
        private Account _account;
       
        public WithdrawTransaction(Account account, decimal amount)
            : base(amount)
        {
            this._account = account;
        }

        public override void Print()
        {
            if (_success == true)
            Console.WriteLine(_dateStamp + " Succesfully withdrawn " + _amount.ToString("C") + " From " + _account.get_name());          
        }
        public override void Execute()
        {
            base.Execute();
            try
            {
                if (_account.get_balance() > _amount && _amount > 0)
                {
                    _account.Withdraw(_amount);
                    _success = true;
                }
                else throw new InvalidOperationException("Insufficeint funds or negative withdraw attempted");
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
                base.Execute();
                _account.Deposit(_amount);
                Print();
            }
            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
            }
        }
    }
}