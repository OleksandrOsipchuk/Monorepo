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
            int sum = 0;
            bool isExit = true;

            while (isExit)
            {
                Console.WriteLine("\nChoose the option:\n\n1 Add income \n2 Add expenses \n3 Show current balance\n4 Exit\n");
                int option = CheckInt(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        Console.WriteLine("Enter the income sum:");
                        int income = -1;
                        while (income == -1) income = CheckInt(Console.ReadLine());
                        sum += income;
                        break;
                    case 2:
                        Console.WriteLine("Enter the expence sum:");
                        int expence = -1;
                        while (expence == -1) expence = CheckInt(Console.ReadLine());
                        sum -= expence;
                        break;
                    case 3:
                        if (sum < 0) Console.WriteLine("You are in debt");
                        Console.WriteLine("Current balance: " + sum);

                        break;
                    case 4:
                        isExit = false;
                        break;
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

    }
}