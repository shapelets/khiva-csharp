using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace KhivaCsharp
{
    class Program
    {
        static void Main(String[] args)
        {
            Console.WriteLine("test");
            string[] info_splitted;
            using(StringWriter writer = new StringWriter()){
                Console.SetOut(writer);
                library.Library.PrintBackendInfo();
                string info = writer.ToString();
                info_splitted = info.Split(' ');
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine(info_splitted[0]);

            Console.WriteLine(library.Library.GetKhivaBackends() & (int)library.Library.Backend.KHIVA_BACKEND_OPENCL);

            Console.WriteLine("Backend: " + library.Library.GetKhivaBackend());
            Console.WriteLine(library.Library.GetKhivaVersion());

            String version = "";
            String filePath;
            String Os = System.Environment.OSVersion.Platform.ToString().ToLower();

            if (Os.Contains("win"))
            {
                filePath = "C:\\Program Files\\Khiva\\v0\\include\\khiva\\version.h";
            }
            else
            {
                filePath = "/usr/local/include/khiva/version.h";
            }

            String data = "";

            try
            {
                data = File.ReadAllText(filePath);
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }

            MatchCollection matches = Regex.Matches(data, "([0-9]+\\.[0-9]+\\.[0-9]+)");

            if (matches.Count != 0)
            {
                version = matches[0].Groups[1].Value;
            }
            Console.WriteLine("Filepath:");
            Console.WriteLine(filePath);
            Console.WriteLine("Data:");
            Console.WriteLine(data);
            Console.WriteLine("Version:");
            Console.WriteLine(version);

            Console.ReadKey();
        }
    }
}
