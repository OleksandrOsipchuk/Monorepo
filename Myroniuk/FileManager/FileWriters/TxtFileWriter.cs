﻿using FileManager.FileWriters;
using FileManager.OperationParameters;
using FileValidator.OperationParameters;

namespace FileWorker.FileWriters
{
    public class TxtFileWriter : FileWriterBase, IFileWriter
    {
        async public Task<OperationResult> Write(WriteParameters parameters)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(parameters.FilePath))
                    await sw.WriteAsync(parameters.Data.TrimStart('{').TrimEnd('}'));
                if (parameters.Zip) return WriteArchive(parameters.FilePath, parameters.Extension);
                return new OperationResult(true, $"Data was written to the \\{parameters.FilePath} file successfully.");
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, "The extension of the file is not predicted by the program: " + ex.Message);
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading/writing the file: " + ex.Message);
            } 
        }
        
    }
}
