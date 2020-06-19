using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    class TransferTransaction : Transaction
    {
        private Account _fromAccount;
        private Account _toAccount;
        private DepositTransaction _deposit;
        private WithdrawTransaction _withdraw;
        
        public TransferTransaction(Account fromAccount, Account toAccount, decimal amount)
             : base(amount)
        {
            this._fromAccount = fromAccount;
            this._toAccount = toAccount;
            _deposit = new DepositTransaction(toAccount, amount);
            _withdraw = new WithdrawTransaction(fromAccount, amount);
        }
        public override void Print()
        {
            if (_success == true)
            Console.WriteLine(_dateStamp + " Successfully transferred " + _amount.ToString("C") + " from " + _fromAccount.get_name() + " to "
            + _toAccount.get_name());
        }
        public override void Execute()
        {
            base.Execute();
            try
            {
                if (_amount < 0)
                throw new InvalidOperationException("Cannot deposit negative amount");

                if (_fromAccount.get_balance() > _amount)                    
                {
                    
                    _fromAccount.Withdraw(_amount);
                    _toAccount.Deposit(_amount);
                    Console.WriteLine("\nTransfer Succesful");
                    _success = true;
                }
                throw new InvalidOperationException("Insufficient funds in sender account");
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
                if (_toAccount.get_balance() > _amount)
                {
                    base.Rollback();
                    _toAccount.Withdraw(_amount);
                    _fromAccount.Deposit(_amount);
                    Console.WriteLine("\nRollback Succesful ");
                    Print();
                }
                else throw new InvalidOperationException(_toAccount.get_name() + " has insuficient funds to complete transaction");
            }
            catch (InvalidOperationException E)
            {
                Console.WriteLine(E.Message);
            }
        }
    }
}
