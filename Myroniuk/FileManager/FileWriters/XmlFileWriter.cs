using System.Xml.Serialization;
using System.Xml;
using FileValidator.OperationParameters;
using FileManager.FileWriters;
using FileManager.OperationParameters;

namespace FileWorker.FileWriters
{
    public class XmlFileWriter : FileWriterBase, IFileWriter
    {
        public async Task<OperationResult> Write(WriteParameters parameters)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(string));
            try
            {
                using (StreamWriter sw = new StreamWriter(parameters.FilePath))
                    await sw.WriteAsync(parameters.Data);
                if (parameters.Zip) return WriteArchive(parameters.FilePath, parameters.Extension);
                return new OperationResult(true, $"Data was written to the \\{parameters.FilePath} file successfully.");
            }
            catch (ArgumentException ex)
            {
                return new OperationResult(false, "The extension of the file is not predicted by the program: " + ex.Message);
            }
            catch (XmlException ex)
            {
                return new OperationResult(false, "The data provided is not valid for XML file" + ex.Message);
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading/writing the file: " + ex.Message);
            }
        }
    }
}
