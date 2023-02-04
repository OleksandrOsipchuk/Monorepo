using Newtonsoft.Json;
using System.IO;

namespace Program
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
                "\nWrite <read> to read object by Id from DB." +
                "\nWrite <readAll> to read all objects from DB.");
                string operation = GetOperation();
                HandlerFactory factory = new HandlerFactory();
                IHandler handler = factory.GetHandler(format);
                try
                {
                    switch (operation)
                    {
                        case "save":
                            (string name, NestedData info) parametrs = GetParamsForWriter();
                            handler.Write(parametrs.name, parametrs.info);
                            break;
                        case "read":
                            Data data = handler.Read(GetId());
                            Console.WriteLine($"Name: {data.Name}, Info: {data.Nested.Info}");
                            break;
                        case "readAll":
                            IList<Data> allData = handler.ReadAll();
                            foreach (Data dat in allData)
                            {
                                Console.WriteLine($"Name: {dat.Name}, Info: {dat.Nested.Info}, Id: {dat.Id}\n");
                            }
                            break;
                        default:
                            Console.WriteLine(operation);
                            continue;
                    }
                }
                catch (ReadFromDBException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                isWork = CheckIfExit();

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
                case "readAll":
                    return "readAll";
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
        private static bool CheckIfExit()
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