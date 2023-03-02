using Newtonsoft.Json;
using System.Xml.Linq;

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
            Transaction[] transactions = ReadDB();

            int sum = 0;
            if (transactions.Length!=0) sum = transactions[transactions.Length - 1].Balance;

            while (true)
            {
                Console.WriteLine("\nChoose the option:\n" +
                    "\n1 Add income " +
                    "\n2 Add expenses " +
                    "\n3 Show current balance" +
                    "\n4 Show 10 recent transactions" +
                    "\n5 Exit\n");

                int option = ParseInt(Console.ReadLine());
                switch (option)
                {
                    case 1:
                    case 2:
                        Console.WriteLine("Enter the transaction sum:");
                        int transactionSum = -1;
                        while (transactionSum == -1) transactionSum = ParseInt(Console.ReadLine());
                        if(option == 2 ) transactionSum*=-1;                     
                        sum += transactionSum;
                        transactions = transactions.Append(new Transaction(sum, transactionSum)).ToArray();
                        WritedDB(transactions);
                        break;
                    case 3:
                        if (sum < 0) Console.WriteLine("You are in debt");
                        Console.WriteLine($"Current balance: {sum}$");
                        break;
                    case 4:
                        if (transactions.Length == 0) Console.WriteLine("No recent transactions");
                        else
                        {
                            int count = 10;
                            if (transactions.Length < 10) count = transactions.Length;
                            for (int i = count - 1; i >= 0; i--)
                            {

                                if (transactions[i].Sum < 0) Console.Write($"Expense: {transactions[i].Sum}$");
                                else Console.Write($"Income: +{transactions[i].Sum}$");
                                Console.WriteLine($" Balance: {transactions[i].Balance}$ Date: {transactions[i].Date}");
                            }
                        }
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
        public static int ParseInt(string? input)
        {
            if (Int32.TryParse(input, out int m)) return m;
            Console.WriteLine("Enter valid integer");
            return -1;
        }
        public static void WritedDB(IEnumerable<Transaction> transactions)
        {

            string json = JsonConvert.SerializeObject(transactions, Formatting.Indented);
            File.WriteAllText("DataBase.json", json);

        }
        public static Transaction[] ReadDB()
        {
            string text = File.ReadAllText("DataBase.json");
            Transaction[] ?transactions = JsonConvert.DeserializeObject<Transaction[]>(text);
            if(transactions==null) return new Transaction[0];
            return transactions;
        }


    }
}