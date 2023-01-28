using FileWorker.DataReaders;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FileWorker
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var request = Console.ReadLine();
            if (request.StartsWith("read --filename={"))
            {
                AnyFileReader anyFileReader = new AnyFileReader();
                anyFileReader.Notify += (string s) => Console.WriteLine(s);
                anyFileReader.Read(request);
            }
            else if (request.StartsWith("write --data="))
            {
                //write --data={"Id":"Test"} --filename={test.json} [--zip=true]
                var extension = Path.GetExtension(Regex.Match(request, @"filename={([^}]+)").Value);
                IFileWriteableFactory factory = new FileManagerFactory();
                var fileWriter = factory.Create(extension);
                fileWriter.Notify += (string s) => Console.WriteLine(s);
                
                try{ 
                    fileWriter.Write(request);
                }
                catch (JsonReaderException ex){
                    Console.WriteLine("The data provided is not in the correct format for a json file: " + ex.Message);
                }
                catch (IOException ex){
                    Console.WriteLine("An error occurred while reading/writing the file: " + ex.Message);
                }
            }
            else
                Console.WriteLine("Please use: read --filename={example.*} [--zip=true]\n" +
                "where * - json, xml, txt OR:\n" +
                "write --data={\"id\":\"example\"} --filename={example.*} [--zip=true]");
        }
    }
}