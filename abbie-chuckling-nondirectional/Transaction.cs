using System;
using System.Collections.Generic;
using System.Text;

namespace abbie_chuckling_nondirectional
{
    /// <summary>
    /// A new Class, Transaction, is created to contain information that was contained in JSON format.
    /// </summary>
    public class Transaction
    {
        public IList<Transaction> Transactions { get; set; } //After added Transaction class, we can update our Block to replace Data with Transactions. One misunderstanding I had before was that one Blockchain can only contain one transaction. Actually, one block can contain many transactions. Therefore, we uses a collection to store transaction data.

        public string FromAddress { get; set; }
        public string ToAddress { get; set; }
        public int Amount { get; set; }

        public Transaction(string fromAddress, string toAddress, int amount)
        {
            FromAddress = fromAddress;
            ToAddress = toAddress;
            Amount = amount;
        }
    }
}
