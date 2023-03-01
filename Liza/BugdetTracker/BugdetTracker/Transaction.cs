
namespace BugdetTracker
{
    public class Transaction
    {
        public int Balance { get; }
        public int Sum { get; set; }

        public Transaction(int balance, int sum)
        {
            Balance = balance;
            Sum = sum;
        }
    }
}
