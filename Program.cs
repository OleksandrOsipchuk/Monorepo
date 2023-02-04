
using System.Diagnostics;
using System.Text;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;

internal class Program
{
    async static Task Main(string[] args)
    {
        Stopwatch e = new Stopwatch();
        e.Start();

        // Create directory with files
        string path = "D:\\start";
        DirectoryInfo dir = new DirectoryInfo(path);
        if (!dir.Exists) 
        {
            dir.Create();
            for (int i = 1; i <= 100; i++)
            {
                string pathStr = $"D:/start/File{i}.txt";
                string text = $"File{i}";
                using (FileStream sw = new FileStream(pathStr, FileMode.OpenOrCreate))
                {
                    byte[] buffer = Encoding.Default.GetBytes(text);
                    await sw.WriteAsync(buffer, 0, buffer.Length);
                } 
            }
        }
        

        //Main
        FileInfo[] files= dir.GetFiles();
        var outputs = from p in files.Take(10).AsParallel()
                    let read = File.OpenRead($"D:/start/{p.Name}")
                    let buff = new byte[read.Length]
                    let n = read.Read(buff,0,buff.Length)
                    let value = Encoding.Default.GetString(buff)
                    let name  = p.Name
                    select new
                    {
                        Name = String.Join("", name.Substring(0, name.Length - 4).ToCharArray().Reverse()),
                        Value = String.Join("", value.ToCharArray().Reverse().ToArray()) 
                    }; //Задаю змінну outputs та оброблюю перші 10 файлів


        for (int i = 10; i < 100; i+=10)
        {
            var a = from p in files.Skip(i).Take(10).AsParallel()
                    let read = File.OpenRead($"D:/start/{p.Name}")
                    let buff = new byte[read.Length]
                    let n = read.Read(buff, 0, buff.Length)
                    let value = Encoding.Default.GetString(buff)
                    let name = p.Name
                    select new
                    {
                        Name = String.Join("", name.Substring(0, name.Length - 4).ToCharArray().Reverse()),
                        Value = String.Join("", value.ToCharArray().Reverse().ToArray())
                    };
            outputs = outputs.Concat(a);
        }

        //Create new directory and fill it
        string path2 = "D:\\start2";
        DirectoryInfo dir2 = new DirectoryInfo(path2);
        if (!dir2.Exists)
        {
            dir2.Create();

        }
        foreach (var output in outputs)
        {
            string pathStr = $"D:/start2/{output.Name}.txt";
            using (FileStream sw = new FileStream(pathStr,FileMode.OpenOrCreate)) 
            {
                byte[] buffer = Encoding.Default.GetBytes(output.Value);
                await sw.WriteAsync(buffer, 0, buffer.Length);
            }
        }
        e.Stop();
        Console.WriteLine(e.Elapsed);


    }
}