using FileManager.OperationParameters;
using FileValidator.OperationParameters;
using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileWorker.DataReaders
{
    public class AnyFileReader
    {
        public async Task<OperationResult> Read(ReadParameters parameters)
        {
            try
            {
                if (parameters.Zip)
                {
                    using (ZipArchive archive = ZipFile.OpenRead(parameters.FilePath))
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            using (StreamReader sr = new StreamReader(entry.Open()))
                                return await Task.Run(() => new OperationResult(true, sr.ReadToEnd()));
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
