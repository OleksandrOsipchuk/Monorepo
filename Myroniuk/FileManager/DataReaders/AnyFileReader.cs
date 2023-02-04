using FileManager.OperationParameters;
using FileValidator.OperationParameters;
using System.IO.Compression;

namespace FileWorker.DataReaders
{
    public static class AnyFileReader
    {
        public static async Task<OperationResult> Read(ReadParameters parameters)
        {
            try
            {
                if (parameters.Zip)
                {
                    using (ZipArchive archive = ZipFile.OpenRead(parameters.FilePath))
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            using (StreamReader sr = new StreamReader(entry.Open()))
                                return new OperationResult(true, await sr.ReadToEndAsync());
                        }
                }
                else
                {
                    using (StreamReader sr = new StreamReader(parameters.FilePath))
                        return new OperationResult(true, sr.ReadToEnd());
                }
            }
            catch (FileNotFoundException ex)
            {
                return new OperationResult(false, "The file could not be found: " + ex.Message); 
            }
            catch (IOException ex)
            {
                return new OperationResult(false, "An error occurred while reading the file: " + ex.Message);
            }
            catch (Exception ex)
            {
                return new OperationResult(false, "Unexpected exception: " + ex.Message);
            }
            return new OperationResult(false, "Reading went wrong");
        }
    }
}
