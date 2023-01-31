using FileValidator;
using FileWorker.DataReaders;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FileWorker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            string request = args.Length > 0 ? args[0] : Console.ReadLine();
            if (request.StartsWith("read --filename={"))
            {
                AnyFileReader anyFileReader = new AnyFileReader();
                var resultTask = anyFileReader.Read(request);
                var result = await resultTask;
                Console.WriteLine(result.Message);
            }
            else if (request.StartsWith("write --data="))
            {
                //write --data={"Id":"Test"} --filename={test.json} [--zip=true]
                var extension = Path.GetExtension(Regex.Match(request, @"filename={([^}]+)").Value);
                IFileWriteFactory factory = new FileManagerFactory();
                var fileWriter = factory.Create(extension);
                var resultTask = fileWriter.Write(request);
                var result = await resultTask;
                Console.WriteLine(result.Message);
            }
            else
                Console.WriteLine("Please use: read --filename={example.*} [--zip=true]\n" +
                "where * - json, xml, txt OR:\n" +
                "write --data={\"id\":\"example\"} --filename={example.*} [--zip=true]");
        }
    }
}