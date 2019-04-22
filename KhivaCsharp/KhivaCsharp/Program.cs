// Copyright (c) 2019 Shapelets.io
//
// This Source Code Form is subject to the terms of the Mozilla Public
// License, v. 2.0. If a copy of the MPL was not distributed with this
// file, You can obtain one at http://mozilla.org/MPL/2.0/.

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
            Complex[,] data = { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(5, 6), new Complex(7, 8) } };
            array.KhivaArray<Complex> arr = new array.KhivaArray<Complex>(data, false);
            Complex[,] dataArr = arr.GetData2D<Complex>();
            for (int i = 0; i < dataArr.GetLength(0); i++)
            {
                for(int j = 0; j < dataArr.GetLength(1); j++)
                {
                    Console.WriteLine(dataArr[i,j]);
                }
            }
            Console.ReadKey();
            /*float[] tss = new float[] { 2, 2, 2, 3, 3, 3, 4, 4, 4, 5, 5, 5 };
            array.Array arr = new array.Array(tss);
            array.Array aggregatedLinearTrendResult = features.Features.AggregatedLinearTrend(arr, 3, 0).Item1;
            float[] result = aggregatedLinearTrendResult.GetData1D<float>();
            Console.WriteLine(result.Length);*/
            /*
            float[,] tss = new float[,] { { 0, 1, 2, 3 }, { 10, 11, 12, 13 } };
            float[,] tss2 = new float[,] { { 4, 6, 8, 10, 12 }, { 14, 16, 18, 20, 22 } };
            array.Array xss = new array.Array(tss);
            array.Array yss = new array.Array(tss2);
            array.Array approximateEntropyResult = features.Features.CrossCovariance(xss, yss, false);
            approximateEntropyResult.Display();
            float[,,] dataArr = approximateEntropyResult.GetData3D<float>();
            float[] flattenResult = new float[dataArr.Length];
            Flatten3D<float>(ref flattenResult, dataArr);
            for (int  i = 0; i < flattenResult.Length; i++)
            {
                Console.WriteLine(flattenResult[i]);
            }*/
            //int[,,,] tss = { { { { 1, 2 }, { 3, 4 } }, { { 5, 6 }, { 7, 8 } } }, { { { 9, 10 }, { 11, 12 } }, { { 13, 14 }, { 15, 16 } } } };
            /*
            Complex[,,] tss = { { { new Complex(1, 2), new Complex(3, 4) }, { new Complex(5, 6), new Complex(7, 8) } }, { { new Complex(9, 10), new Complex(11, 12) }, { new Complex(13, 14), new Complex(15, 16) } } };
            array.Array arr = new array.Array(tss, true);
            Complex[,,] dataArr = arr.GetData3D<Complex>();
            */
            /*int[] tss = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            array.Array arr = new array.Array(tss);
            array.Array absEnergyResult = features.Features.AbsEnergy(arr);
            int[] result = absEnergyResult.GetData1D<int>();
            Console.WriteLine(result.Length);*/
            //int[,] tss = { { 1, 2 }, { 3, 4 }, { 5, 6 } };
            //int[] tss = { 1, 2  };
            //array.Array arr = new array.Array(tss);
            //array.Array arrRows = arr.Rows(0, 1);
            //array.Array arr = new array.Array(tss);
            /*int[,] dataArr = arrRows.GetData2D<int>();
            for (int i = 0; i < dataArr.GetLength(0); i++) {
                for (int j = 0; j < dataArr.GetLength(1); j++)
                {
                    Console.Write(dataArr[i,j]);
                }
                Console.WriteLine();
            }
            arrRows.Display();*/
            /*bool[,,] tss = { { { true, true }, { false, true } }, { { false, true }, { true, true } } };
            array.Array arr = new array.Array(tss);*/
            /*arr.Display();
            bool[,,] dataArr = arr.GetData3D<bool>();
            long[] dims = arr.GetDims();*/
            /*
            Console.WriteLine("test");
            string[] info_splitted;
            using(StringWriter writer = new StringWriter()){
                Console.SetOut(writer);
                khiva.Khiva.PrintBackendInfo();
                string info = writer.ToString();
                info_splitted = info.Split(' ');
            }
            StreamWriter standardOutput = new StreamWriter(Console.OpenStandardOutput());
            standardOutput.AutoFlush = true;
            Console.SetOut(standardOutput);
            Console.WriteLine(info_splitted[0]);
            Console.WriteLine((int)khiva.Khiva.SupportedBackends & (int)khiva.Khiva.Backend.KHIVA_BACKEND_OPENCL);
            Console.WriteLine("Backend: " + khiva.Khiva.ActualBackend);
            Console.WriteLine(khiva.Khiva.Version);
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
        private static void Flatten3D<T>(ref T[] flattenArr, T[,,] arr)
        {
            Console.WriteLine("Flatten");
            for (int i = 0; i < arr.GetLength(0); i++)
            {
                for (int j = 0; j < arr.GetLength(1); j++)
                {
                    for (int k = 0; k < arr.GetLength(2); k++)
                    {
                        Console.WriteLine("(" + i + "," + j + "," + k + ")");
                        Console.WriteLine("Position: " + (i * (arr.GetLength(1) * arr.GetLength(2)) + j * arr.GetLength(2) + k));
                        flattenArr[i * (arr.GetLength(1) * arr.GetLength(2)) + j * arr.GetLength(2) + k] = arr[i, j, k];
                    }
                }
            }*/
        }
    }
}