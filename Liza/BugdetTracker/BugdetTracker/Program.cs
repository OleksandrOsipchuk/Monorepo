using Newtonsoft.Json;

namespace BugdetTracker
{
    public class BudgetTracking
    {
        public static void Main(string[] args)
        {
            StartTracking();
        }
        public static void StartTracking()
        {
            List<Transaction> transactions = ReadDB();

            int sum;
            if (transactions.Count==0) sum = 0;

            else sum = transactions[transactions.Count - 1].currentSum;

            while (true)
            {
                Console.WriteLine("\nChoose the option:\n" +
                    "\n1 Add income " +
                    "\n2 Add expenses " +
                    "\n3 Show current balance" +
                    "\n4 Show 10 recent transactions" +
                    "\n5 Exit\n");

                int option = CheckInt(Console.ReadLine());
                switch (option)
                {
                    case 1:
                    case 2:
                        Console.WriteLine("Enter the transaction sum:");
                        int TransSum = -1;
                        while (TransSum == -1) TransSum = CheckInt(Console.ReadLine());
                        if(option ==1 ) sum += TransSum;
                        else sum -= TransSum;
                        transactions.Add(new Transaction(sum, TransSum));
                        WritedDB(transactions);
                        break;
                    case 3:
                        if (sum < 0) Console.WriteLine("You are in debt");
                        Console.WriteLine("Current balance: " + sum);
                        break;
                    case 4:
                        if (transactions.Count == 0) Console.WriteLine("No recent transactions");
                        break;
                    case 5:
                        return;
                    case -1:
                        break;
                    default:
                        Console.WriteLine("enter nuber from 1 to 4 only");
                        break;
                }
            }
        }
        public static int CheckInt(string? input)
        {
            if (Int32.TryParse(input, out int m)) return m;
            Console.WriteLine("Enter valid integer");
            return -1;
        }
        public static void WritedDB(List<Transaction> transactions)
        {

            string json = JsonConvert.SerializeObject(transactions, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(@"E:\test.json", json);

        }
        public static List<Transaction> ReadDB()
        {

            string text = File.ReadAllText(@"E:\test.json");
            return JsonConvert.DeserializeObject<List<Transaction>>(text);
  
        }

    }
}