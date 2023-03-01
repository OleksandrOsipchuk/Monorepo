using System;

namespace BugdetTracker
{
    public class Transaction
    {
        public int Balance { get; }
        public int Sum { get; set; }
        public DateTime Date { get; }

        public Transaction(int balance, int sum)
        {
            Balance = balance;
            Sum = sum;
            Date = DateTime.Now;
        }
    }
}
