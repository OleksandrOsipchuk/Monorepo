
namespace BugdetTracker
{
    public class Transaction
    {
        public int currentSum { get; }
        public int transactionSum { get; set; }

        public Transaction(int currentSum, int transactionSum)
        {
            this.currentSum = currentSum;
            this.transactionSum = transactionSum;
        }
    }
}
