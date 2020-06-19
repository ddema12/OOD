using System;
using System.Collections.Generic;
using System.Text;

namespace BankAccount
{
    abstract class Transaction
    {
        public decimal _amount { get; protected set; }
        public bool _success { get; protected set; }
        public bool _executed { get; private set; }
        public bool _reversed { get; private set; }
        public DateTime _dateStamp { get; private set; }

        public Transaction(decimal amount)
        {
            _amount = amount;
        }

        public virtual void Print()
        {
            if (_success == true)
                Console.WriteLine();
        }
        public virtual void Execute()
        {
            if (_executed == false)
            {
                _executed = true;
                _dateStamp = DateTime.Now;
            }
            else throw new InvalidOperationException("This transaction has already been peformed");
        }
        public virtual void Rollback()
        {
            if (_executed != true) throw new InvalidOperationException("Original transaction not finalized");
            if (_success == true && _reversed == false)
            {
                _reversed = true;
                _dateStamp = DateTime.Now;
            }
            else throw new InvalidOperationException("This transaction has already been peformed");
        }
    }
        
}

