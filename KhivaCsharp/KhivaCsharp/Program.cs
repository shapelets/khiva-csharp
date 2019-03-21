using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Numerics;

namespace khiva
{
    class Program
    {
        static void Main(String[] args)
        {
            //int[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } }, { { { 9, 10 }, { 11, 12 } }, { { 13, 14 }, { 15, 16 } } } };
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(5, 6), new Complex(7, 8) } }, { { new Complex(9, 10), new Complex(11, 12) }, { new Complex(13, 14), new Complex(15, 16) } } };
            array.Array arr = new array.Array(tss, true);
            Complex[,,] dataArr = arr.GetData3D<Complex>();
            for(int i = 0; i < dataArr.GetLength(0); i++)
            {
                for (int j = 0; j < dataArr.GetLength(1); j++)
                {
                    for (int k = 0; k < dataArr.GetLength(2); k++)
                    {
                        Console.WriteLine(dataArr[i, j, k]);
                    }
                }
            }
            //array.Array arr = new array.Array(tss);
            arr.Display();
            /*bool[,,] tss = { { { true, true }, { false, true } }, { { false, true }, { true, true } } };
            array.Array arr = new array.Array(tss);*/
            /*arr.Display();
            bool[,,] dataArr = arr.GetData3D<bool>();
            long[] dims = arr.GetDims();*/
            Console.WriteLine("test");
            string[] info_splitted;
            using(StringWriter writer = new StringWriter()){
                Console.SetOut(writer);
                khiva.library.Library.PrintBackendInfo();
                string info = writer.ToString();
                info_splitted = info.Split(' ');
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine(info_splitted[0]);

            Console.WriteLine(khiva.library.Library.GetKhivaBackends() & (int)khiva.library.Library.Backend.KHIVA_BACKEND_OPENCL);

            Console.WriteLine("Backend: " + khiva.library.Library.GetKhivaBackend());
            Console.WriteLine(khiva.library.Library.GetKhivaVersion());

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
