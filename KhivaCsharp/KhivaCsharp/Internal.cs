using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace array{
        internal static class Internal
        {
            private static Dictionary<Type, Action<IntPtr, System.Array>>[] GetDataFn;

            static Internal()
            {
                GetDataFn =  new Dictionary<Type, Action<IntPtr, System.Array>>[4];
                for (int i = 0; i < GetDataFn.Length; i++)
                {
                    GetDataFn[i] = new Dictionary<Type, Action<IntPtr, System.Array>>();
                }
                GetDataFn[0].Add(typeof(float), (ptr, data) => interop.DLLArray.get_data(ref ptr, (float[])data));
                GetDataFn[1].Add(typeof(float), (ptr, data) => interop.DLLArray.get_data(ref ptr, (float[,])data));
                GetDataFn[2].Add(typeof(float), (ptr, data) => interop.DLLArray.get_data(ref ptr, (float[,,])data));
                GetDataFn[3].Add(typeof(float), (ptr, data) => interop.DLLArray.get_data(ref ptr, (float[,,,])data));

                GetDataFn[0].Add(typeof(double), (ptr, data) => interop.DLLArray.get_data(ref ptr, (double[])data));
                GetDataFn[1].Add(typeof(double), (ptr, data) => interop.DLLArray.get_data(ref ptr, (double[,])data));
                GetDataFn[2].Add(typeof(double), (ptr, data) => interop.DLLArray.get_data(ref ptr, (double[,,])data));
                GetDataFn[3].Add(typeof(double), (ptr, data) => interop.DLLArray.get_data(ref ptr, (double[,,,])data));

                GetDataFn[0].Add(typeof(int), (ptr, data) => interop.DLLArray.get_data(ref ptr, (int[])data));
                GetDataFn[1].Add(typeof(int), (ptr, data) => interop.DLLArray.get_data(ref ptr, (int[,])data));
                GetDataFn[2].Add(typeof(int), (ptr, data) => interop.DLLArray.get_data(ref ptr, (int[,,])data));
                GetDataFn[3].Add(typeof(int), (ptr, data) => interop.DLLArray.get_data(ref ptr, (int[,,,])data));

                GetDataFn[0].Add(typeof(uint), (ptr, data) => interop.DLLArray.get_data(ref ptr, (uint[])data));
                GetDataFn[1].Add(typeof(uint), (ptr, data) => interop.DLLArray.get_data(ref ptr, (uint[,])data));
                GetDataFn[2].Add(typeof(uint), (ptr, data) => interop.DLLArray.get_data(ref ptr, (uint[,,])data));
                GetDataFn[3].Add(typeof(uint), (ptr, data) => interop.DLLArray.get_data(ref ptr, (uint[,,,])data));

                GetDataFn[0].Add(typeof(byte), (ptr, data) => interop.DLLArray.get_data(ref ptr, (byte[])data));
                GetDataFn[1].Add(typeof(byte), (ptr, data) => interop.DLLArray.get_data(ref ptr, (byte[,])data));
                GetDataFn[2].Add(typeof(byte), (ptr, data) => interop.DLLArray.get_data(ref ptr, (byte[,,])data));
                GetDataFn[3].Add(typeof(byte), (ptr, data) => interop.DLLArray.get_data(ref ptr, (byte[,,,])data));

                GetDataFn[0].Add(typeof(long), (ptr, data) => interop.DLLArray.get_data(ref ptr, (long[])data));
                GetDataFn[1].Add(typeof(long), (ptr, data) => interop.DLLArray.get_data(ref ptr, (long[,])data));
                GetDataFn[2].Add(typeof(long), (ptr, data) => interop.DLLArray.get_data(ref ptr, (long[,,])data));
                GetDataFn[3].Add(typeof(long), (ptr, data) => interop.DLLArray.get_data(ref ptr, (long[,,,])data));

                GetDataFn[0].Add(typeof(ulong), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ulong[])data));
                GetDataFn[1].Add(typeof(ulong), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ulong[,])data));
                GetDataFn[2].Add(typeof(ulong), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ulong[,,])data));
                GetDataFn[3].Add(typeof(ulong), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ulong[,,,])data));

                GetDataFn[0].Add(typeof(short), (ptr, data) => interop.DLLArray.get_data(ref ptr, (short[])data));
                GetDataFn[1].Add(typeof(short), (ptr, data) => interop.DLLArray.get_data(ref ptr, (short[,])data));
                GetDataFn[2].Add(typeof(short), (ptr, data) => interop.DLLArray.get_data(ref ptr, (short[,,])data));
                GetDataFn[3].Add(typeof(short), (ptr, data) => interop.DLLArray.get_data(ref ptr, (short[,,,])data));

                GetDataFn[0].Add(typeof(ushort), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ushort[])data));
                GetDataFn[1].Add(typeof(ushort), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ushort[,])data));
                GetDataFn[2].Add(typeof(ushort), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ushort[,,])data));
                GetDataFn[3].Add(typeof(ushort), (ptr, data) => interop.DLLArray.get_data(ref ptr, (ushort[,,,])data));
            }


            internal static void GetData<T>(ref IntPtr ptr, System.Array arr)
            {
                GetDataFn[arr.Rank - 1][typeof(T)](ptr, arr);
            }

        }
    }
}
