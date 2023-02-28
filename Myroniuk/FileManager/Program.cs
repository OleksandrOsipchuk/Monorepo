﻿using FileManager.FileWriters;
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
            var parameters = ParametersParser.ParseParameters(args);
            if (parameters is ReadParameters)
            {
                var readParameters = parameters as ReadParameters;
                var resultTask = AnyFileReader.ReadAsync(readParameters);
                var result = await resultTask;
                Console.WriteLine(result.Message);
            }
            else if (parameters is WriteParameters)
            {
                var writeParameters = parameters as WriteParameters;
                IFileWriteFactory fileWriteFactory = new FileWriteFactory();
                var fileWriter = fileWriteFactory.Create(writeParameters.Extension);
                var resultTask = fileWriter.WriteAsync(writeParameters);
                var result = await resultTask;
                Console.WriteLine(result.Message);
            }
            else
                Console.WriteLine("Please use: read --filename={example.*} --zip=true\n" +
                "where * - json, xml, txt OR:\n" +
                "write --data={data123} --filename={example.*} --zip=true");
        }
    }
}