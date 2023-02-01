using FileManager.FileWriters;
using FileValidator;
using FileValidator.OperationParameters;
using FileWorker.DataReaders;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace FileWorker
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            IParametersFactory parameterFactory = new ParametersFactory();
            var parameters = parameterFactory.ParseParameters(args);
            if (parameters is ReadParameters)
            {
                var readParameters = parameters as ReadParameters;
                AnyFileReader anyFileReader = new AnyFileReader();
                var resultTask = anyFileReader.Read(readParameters);
                var result = await resultTask;
                Console.WriteLine(result.Message);
            }
            else if (parameters is WriteParameters)
            {   
                var writeParameters = parameters as WriteParameters;
                IFileWriteFactory fileWriteFactory = new FileWriteFactory();
                var fileWriter = fileWriteFactory.Create(writeParameters.Extension);
                var resultTask = fileWriter.Write(writeParameters);
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