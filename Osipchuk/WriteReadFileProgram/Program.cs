using Newtonsoft.Json;
using System.IO;

namespace jsonAndXml
{
    class Program
    {
        static void Main(string[] args)
        {
            string pathForJson = @"D:\NewFolder\db.json";
            string pathForXml = @"D:\NewFolder\db.xml";

            if (!Directory.Exists(@"D:\NewFolder"))
            {
                Directory.CreateDirectory(@"D:\NewFolder");
            }
            if (File.Exists(pathForJson) == false)
            {
                var file = File.Create(pathForJson);
                file.Close();
            }
            if (File.Exists(pathForXml) == false)
            {
                var file = File.Create(pathForXml);
                file.Close();
            }

            bool isWork = true;
            while (isWork)
            {
                Console.WriteLine("-----------------------------");
                Console.WriteLine("Choose needed operation:" +
                " \nWrite <save> to add new object to DB." +
                "\nWrite <read> to read object from DB." +
                "\nWrite <leave> to leave the program");

                string operation = Console.ReadLine();
                switch (operation)
                {
                    case "save":
                        Console.Write("Write Name: ");
                        string Name = Console.ReadLine();
                        Console.Write("Write Info: ");
                        var info = new NestedData(Console.ReadLine());
                        Console.Write("Write which format you would like to use(json,xml): ");
                        string format = GetFormat();
                        if (format == "json") JsonHandler.WriteToJsonDb(pathForJson, Name, info);
                        else if (format == "xml") XmlHandler.WriteToXmlDb(pathForXml, Name, info);
                        else Console.WriteLine(format);
                        break;

                    case "read":
                        Console.Write("Write id:");
                        int id = GetId();
                        if (id == -1) Console.WriteLine("There is no object with this Id.");
                        else
                        {
                            Console.Write("Write which format you would like to use(json,xml): ");
                            format = GetFormat();
                            if (format == "json") JsonHandler.ReadFromJsonDb(pathForJson,id);
                            else if (format == "xml") XmlHandler.ReadFromXmlDb(pathForXml,id);
                            else Console.WriteLine(format);
                        }
                        break;

                    case "leave":
                        isWork = false;
                        break;
                    default:
                        Console.WriteLine("This operation don't exist.");
                        break;
                }
            }
            static int GetId()
            {
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
            static string GetFormat()
            {
                string format = Console.ReadLine();
                switch (format)
                {
                    case "json":
                        return "json";
                    case "xml":
                        return "xml";
                    default: return "There is no this formar";
                }
            }
        }
    }
}
