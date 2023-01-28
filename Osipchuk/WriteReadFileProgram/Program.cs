using Newtonsoft.Json;
using System.IO;

namespace JsonAndXml
{
    class Program
    {
        static void Main(string[] args)
        {
            bool isWork = true;
            while (isWork)
            {
                Console.WriteLine("-----------------------------");
                string format = GetFormat();
                if (format != "json" && format != "xml")
                {
                    Console.WriteLine(format);
                    continue;
                }
                Console.WriteLine("Choose needed operation:" +
                " \nWrite <save> to add new object to DB." +
                "\nWrite <read> to read object from DB.");
                string operation = GetOperation();
                HandlerFactory factory = new HandlerFactory();
                IHandler handler = factory.GetHandler(format);

                if (operation == "save")
                {
                    (string name, NestedData info) parametrs = GetParamsForWriter();
                    handler.WriteToDb(parametrs.name, parametrs.info);
                }
                else if (operation == "read")
                {
                    handler.ReadFromDb(GetId());
                }
                else
                {
                    Console.WriteLine(operation);
                    continue;
                }
                isWork = Enttrance();
                
            }
        }

        private static string GetOperation()
        {
            string operation = Console.ReadLine();
            switch (operation)
            {
                case "save":
                    return "save";
                case "read":
                    return "read";
                default: return "There is no this operation. Try again..";
            }
        }
        private static string GetFormat()
        {
            Console.Write("Write format you want to use(json,xml): ");
            string format = Console.ReadLine();
            switch (format)
            {
                case "json":
                    return "json";
                case "xml":
                    return "xml";
                default: return "There is no this format. Try again..";
            }
        }
        private static (string, NestedData) GetParamsForWriter()
        {
            Console.Write("Write Name: ");
            string name = Console.ReadLine();
            Console.Write("Write Info: ");
            NestedData info = new NestedData(Console.ReadLine());
            return (name, info);
        }
        private static int GetId()
        {
            Console.Write("Write Id: ");
            string strId = Console.ReadLine();
            int id = -1;
            try
            {
                id = int.Parse(strId);
            }
            catch (FormatException) { }
            catch (OverflowException) { }
            return id;
        }
        private static bool Enttrance()
        {
            Console.WriteLine("Press <Enter> if you want to continue the program.\n");
            ConsoleKey entr = Console.ReadKey().Key;
            if (entr == ConsoleKey.Enter)
            {
                return true;
            }
            else return false;
        }
    }
}
