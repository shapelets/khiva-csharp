using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Runtime.InteropServices;

namespace khiva
{
    namespace array
    {
        /**
         * Khiva Array Class.
         */
        public class Array
        {
            private IntPtr reference;

            public enum Dtype
            {
                /**
                 * Floating point of single precision. khiva.dtype.
                 */
                f32,
                /**
                 * Complex floating point of single precision. khiva.dtype.
                 */
                c32,
                /**
                 * Floating point of double precision. khiva.dtype.
                 */
                f64,
                /**
                 * Complex floating point of double precision. khiva.dtype.
                 */
                c64,
                /**
                 * Boolean. khiva.dtype.
                 */
                b8,
                /**
                 * 32 bits Int. khiva.dtype.
                 */
                s32,
                /**
                 * 32 bits Unsigned Int. khiva.dtype.
                 */
                u32,
                /**
                 * 8 bits Unsigned Int. khiva.dtype.
                 */
                u8,
                /**
                 * 64 bits Integer. khiva.dtype.
                 */
                s64,
                /**
                 * 64 bits Unsigned Int. khiva.dtype.
                 */
                u64,
                /**
                 * 16 bits Int. khiva.dtype.
                 */
                s16,
                /**
                 * 16 bits Unsigned Int. khiva.dtype.
                 */
                u16
            }
/*
            public Array(double[] arr)
            {
                int type = (int)Dtype.f64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }
                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }
            */
            public Array(float[] arr)
            {
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, ref reference, ref type);
                /*int type = (int)Dtype.f32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }*/
            }

            public Array(int[] arr)
            {
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, ref reference, ref type);
                /*int type = (int)Dtype.s32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }*/
            }
            /*
            public Array(uint[] arr)
            {
                int type = (int)Dtype.u32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(Complex[] arr, bool doublePrecision)
            {
                int type;
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;

                if (doublePrecision)
                {
                    complexArrDouble = new double[arr.Length * 2];
                    type = (int)Dtype.c64;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        complexArrDouble[2*i] = arr[i].Real;
                        complexArrDouble[2*i + 1] = arr[i].Imaginary;
                    }
                }
                else
                {
                    complexArrFloat = new float[arr.Length * 2];
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        complexArrFloat[2*i] = (float)arr[i].Real;
                        complexArrFloat[2*i + 1] = (float)arr[i].Imaginary;
                    }
                }

                GCHandle gchArr = default(GCHandle);

                try
                {
                    if (doublePrecision)
                    {
                        gchArr = GCHandle.Alloc(complexArrDouble, GCHandleType.Pinned);
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(complexArrFloat, GCHandleType.Pinned);
                    }

                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    if (doublePrecision)
                    {
                        GCHandle.Alloc(complexArrDouble, GCHandleType.Weak);
                    }
                    else
                    {
                        GCHandle.Alloc(complexArrFloat, GCHandleType.Weak);
                    }
                }
            }

            public Array(Boolean[] arr)
            {
                int type = (int)Dtype.b8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(short[] arr)
            {
                int type = (int)Dtype.s16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ushort[] arr)
            {
                int type = (int)Dtype.u16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(byte[] arr)
            {
                int type = (int)Dtype.u8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(long[] arr)
            {
                int type = (int)Dtype.s64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ulong[] arr)
            {
                int type = (int)Dtype.u64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(double[,] arr)
            {
                int type = (int)Dtype.f64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }
                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(float[,] arr)
            {
                int type = (int)Dtype.f32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }*/

            public Array(int[,] arr)
            {
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, ref reference, ref type);
                /*
                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }*/
            }
            /*

            public Array(uint[,] arr)
            {
                int type = (int)Dtype.u32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(Complex[,] arr, bool doublePrecision)
            {
                int type;
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;

                if (doublePrecision)
                {
                    complexArrDouble = new double[arr.Length*2];
                    type = (int)Dtype.c64;
                    
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            complexArrDouble[2 * i * arr.GetLength(1) + 2 * j] = arr[i, j].Real;
                            complexArrDouble[2 * i * arr.GetLength(1) + 2 * j + 1] = arr[i, j].Imaginary;
                        }
                    }
                }
                else
                {
                    complexArrFloat = new float[arr.Length * 2];
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++) {
                            complexArrFloat[2 * i * arr.GetLength(1) + 2*j] = (float)arr[i, j].Real;
                            complexArrFloat[2 * i * arr.GetLength(1) + 2*j + 1] = (float)arr[i, j].Imaginary;
                        }
                    }
                }

                GCHandle gchArr = default(GCHandle);

                try
                {
                    if (doublePrecision)
                    {
                        gchArr = GCHandle.Alloc(complexArrDouble, GCHandleType.Pinned);
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(complexArrFloat, GCHandleType.Pinned);
                    }

                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    if (doublePrecision)
                    {
                        GCHandle.Alloc(complexArrDouble, GCHandleType.Weak);
                    }
                    else
                    {
                        GCHandle.Alloc(complexArrFloat, GCHandleType.Weak);
                    }
                }
            }

            public Array(Boolean[,] arr)
            {
                int type = (int)Dtype.b8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(short[,] arr)
            {
                int type = (int)Dtype.s16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ushort[,] arr)
            {
                int type = (int)Dtype.u16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(byte[,] arr)
            {
                int type = (int)Dtype.u8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(long[,] arr)
            {
                int type = (int)Dtype.s64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ulong[,] arr)
            {
                int type = (int)Dtype.u64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(double[,,] arr)
            {
                int type = (int)Dtype.f64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }
                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(float[,,] arr)
            {
                int type = (int)Dtype.f32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }
            */
            public Array(int[,,] arr)
            {
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ref ndims, dims, ref reference, ref type);
                /*
                int type = (int)Dtype.s32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }*/
            }
            /*
            public Array(uint[,,] arr)
            {
                int type = (int)Dtype.u32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(Complex[,,] arr, bool doublePrecision)
            {
                int type;
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;

                if (doublePrecision)
                {
                    complexArrDouble = new double[arr.Length * 2];
                    type = (int)Dtype.c64;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            for (int k = 0; k < arr.GetLength(2); k++)
                            {
                                complexArrDouble[2*i * (arr.GetLength(1) + arr.GetLength(2)) + 2*j * arr.GetLength(2) + 2*k] = arr[i, j, k].Real;
                                complexArrDouble[2*i * (arr.GetLength(1) + arr.GetLength(2)) + 2*j * arr.GetLength(2) + 2*k + 1] = arr[i, j, k].Imaginary;  
                            }
                        }
                    }
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            for (int k = 0; k < arr.GetLength(2); k++)
                            {
                                Console.WriteLine("(" + (complexArrDouble[2 * i * (arr.GetLength(1) + arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k]) + "," +
                                (complexArrDouble[2 * i * (arr.GetLength(1) + arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k + 1]) + ")");
                            }
                        }
                    }
                }
                else
                {
                    complexArrFloat = new float[arr.Length * 2];
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            for (int k = 0; k < arr.GetLength(2); k++)
                            {
                                complexArrFloat[2 * i * (arr.GetLength(1) + arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k] = (float)arr[i, j, k].Real;
                                complexArrFloat[2 * i * (arr.GetLength(1) + arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k + 1] = (float)arr[i, j, k].Imaginary;
                            }
                        }
                    }
                }

                GCHandle gchArr = default(GCHandle);

                try
                {
                    if (doublePrecision)
                    {
                        gchArr = GCHandle.Alloc(complexArrDouble, GCHandleType.Pinned);
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(complexArrFloat, GCHandleType.Pinned);
                    }

                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    if (doublePrecision)
                    {
                        GCHandle.Alloc(complexArrDouble, GCHandleType.Weak);
                    }
                    else
                    {
                        GCHandle.Alloc(complexArrFloat, GCHandleType.Weak);
                    }
                }
            }

            public Array(Boolean[,,] arr)
            {
                int type = (int)Dtype.b8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(short[,,] arr)
            {
                int type = (int)Dtype.s16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ushort[,,] arr)
            {
                int type = (int)Dtype.u16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(byte[,,] arr)
            {
                int type = (int)Dtype.u8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(long[,,] arr)
            {
                int type = (int)Dtype.s64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ulong[,,] arr)
            {
                int type = (int)Dtype.u64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = {  arr.GetLength(1), arr.GetLength(2), arr.GetLength(0), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(double[,,,] arr)
            {
                int type = (int)Dtype.f64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }
                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(float[,,,] arr)
            {
                int type = (int)Dtype.f32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }
            */
            public Array(int[,,,] arr)
            {
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ref ndims, dims, ref reference, ref type);
                /*
                    int type = (int)Dtype.s32;

                    GCHandle gchArr = default(GCHandle);

                    try
                    {
                        gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                        IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                        if (arr == null)
                        {
                            throw new Exception("Null elems object provided");
                        }

                        uint ndims = (uint)arr.Rank;
                        long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                        create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                    }
                    finally
                    {
                        GCHandle.Alloc(arr, GCHandleType.Weak);
                    }*/
            }
            /*
            public Array(uint[,,,] arr)
            {
                int type = (int)Dtype.u32;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(Complex[,,,] arr, bool doublePrecision)
            {
                int type;
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;

                if (doublePrecision)
                {
                    complexArrDouble = new double[arr.Length * 2];
                    type = (int)Dtype.c64;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            for (int k = 0; k < arr.GetLength(2); k++)
                            {
                                for (int z = 0; z < arr.GetLength(3); z++)
                                {
                                    complexArrDouble[2*i * arr.GetLength(0) + 2*j * arr.GetLength(1) + 2*k * arr.GetLength(2) + 2*z] = arr[i, j, k, z].Real;
                                    complexArrDouble[2*i * arr.GetLength(0) + 2*j * arr.GetLength(1) + 2*k * arr.GetLength(2) + 2*z + 1] = arr[i, j, k, z].Imaginary;
                                }
                            }
                        }
                    }
                }
                else
                {
                    complexArrFloat = new float[arr.Length * 2];
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            for (int k = 0; k < arr.GetLength(2); k++)
                            {
                                for (int z = 0; z < arr.GetLength(3); z++)
                                {
                                    complexArrFloat[2*(i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z)] = (float)arr[i, j, k, z].Real;
                                    complexArrFloat[2*(i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z + 1)] = (float)arr[i, j, k, z].Imaginary;
                                }
                            }
                        }
                    }
                }

                GCHandle gchArr = default(GCHandle);

                try
                {
                    if (doublePrecision)
                    {
                        gchArr = GCHandle.Alloc(complexArrDouble, GCHandleType.Pinned);
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(complexArrFloat, GCHandleType.Pinned);
                    }

                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    if (doublePrecision)
                    {
                        GCHandle.Alloc(complexArrDouble, GCHandleType.Weak);
                    }
                    else
                    {
                        GCHandle.Alloc(complexArrFloat, GCHandleType.Weak);
                    }
                }
            }

            public Array(Boolean[,,,] arr)
            {
                int type = (int)Dtype.b8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(short[,,,] arr)
            {
                int type = (int)Dtype.s16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ushort[,,,] arr)
            {
                int type = (int)Dtype.u16;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(byte[,,,] arr)
            {
                int type = (int)Dtype.u8;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(long[,,,] arr)
            {
                int type = (int)Dtype.s64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(ulong[,,,] arr)
            {
                int type = (int)Dtype.u64;

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    uint ndims = (uint)arr.Rank;
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3) };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }*/

            public Array(IntPtr reference)
            {
                this.reference = reference;
            }

            public Array(Array other)
            {
                this.reference = other.GetReference();
            }

            public ref IntPtr GetReference()
            {
                return ref reference;
            }

            private static long[] Dim4(long[] dims)
            {
                if (dims == null)
                {
                    throw new Exception("Null dimensions object provided");
                }
                else if (dims.Length > 4)
                {
                    throw new Exception("ArrayFire supports up to 4 dimensions only");
                }

                long[] adims;
                adims = new long[] { 1, 1, 1, 1 };
                for (int i = 0; i < dims.Length; i++)
                {
                    adims[i] = dims[i];
                }

                return adims;
            }

            /**
             * Gets the data stored in the array.
             *
             * @param <Any> The data type to be returned.
             * @return The data to an array of its type.
             */
             /*
            public T[] GetData1D<T>()
            {
                if (CheckType(typeof(T)))
                {
                    long[] dims = GetDims();
                    CheckNdims(dims, 1);
                    if (typeof(T) == typeof(Complex))
                    {
                        int dimsMultiplied = 2;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        if (GetArrayType() == Dtype.c32)
                        {
                            float[] data = new float[dimsMultiplied];
                            T[] complexData = new T[dims[0]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    complexData[i] = (T)Convert.ChangeType(new Complex(data[2*i], data[2*i + 1]), typeof(T));
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }
                        else
                        {
                            double[] data = new double[dimsMultiplied];
                            T[] complexData = new T[dims[0]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    complexData[i] = (T)Convert.ChangeType(new Complex(data[2*i], data[2*i + 1]), typeof(T));
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }


                    }
                    else
                    {
                        int dimsMultiplied = 1;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        T[] data = new T[dims[0]];

                        GCHandle gchArr;
                        try
                        {
                            gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            get_data(ref reference, dataPtr);
                        }
                        finally
                        {
                            GCHandle.Alloc(data, GCHandleType.Weak);
                        }
                        return data;
                    }

                }
                else
                {
                    throw new Exception("Type does not match");
                }
            }
            
            public T[,] GetData2D<T>()
            {
                if (CheckType(typeof(T)))
                {
                    long[] dims = GetDims();
                    CheckNdims(dims, 2);
                    if (typeof(T) == typeof(Complex))
                    {
                        int dimsMultiplied = 2;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        if (GetArrayType() == Dtype.c32)
                        {
                            float[] data = new float[dimsMultiplied];
                            T[,] complexData = new T[dims[0], dims[1]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        complexData[i, j] = (T)Convert.ChangeType(new Complex(data[2*i * dims[0] + 2*j], data[2*i * dims[0] + 2*j]), typeof(T));
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }
                        else
                        {
                            double[] data = new double[dimsMultiplied];
                            T[,] complexData = new T[dims[0], dims[1]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        complexData[i, j] = (T)Convert.ChangeType(new Complex(data[2*i * dims[0] + 2*j], data[2*i * dims[0] + 2*j + 1]), typeof(T));
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }
                    }
                    else
                    {
                        int dimsMultiplied = 1;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        T[,] data = new T[dims[0], dims[1]];

                        GCHandle gchArr;
                        try
                        {
                            gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            get_data(ref reference, dataPtr);
                        }
                        finally
                        {
                            GCHandle.Alloc(data, GCHandleType.Weak);
                        }
                        return data;
                    }
                }
                else
                {
                    throw new Exception("Type does not match");
                }
            }

            public T[,,] GetData3D<T>()
            {
                if (CheckType(typeof(T)))
                {
                    long[] dims = GetDims();
                    CheckNdims(dims, 3);
                    if (typeof(T) == typeof(Complex))
                    {
                        int dimsMultiplied = 2;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        if (GetArrayType() == Dtype.c32)
                        {
                            float[] data = new float[dimsMultiplied];
                            T[,,] complexData = new T[dims[0], dims[1], dims[2]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        for (int k = 0; k < complexData.GetLength(2); k++)
                                        {
                                            complexData[i, j, k] = (T)Convert.ChangeType(new Complex(data[2*i * dims[0] + 2*j * dims[1] + 2*k], data[2*i * dims[0] + 2*j * dims[1] + 2*k + 1]), typeof(T));
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }
                        else
                        {
                            double[] data = new double[dimsMultiplied];
                            T[,,] complexData = new T[dims[0], dims[1], dims[2]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        for (int k = 0; k < complexData.GetLength(2); k++)
                                        {
                                            complexData[i, j, k] = (T)Convert.ChangeType(new Complex(data[2 * i * dims[0] + 2 * j * dims[1] + 2 * k], data[2 * i * dims[0] + 2 * j * dims[1] + 2 * k + 1]), typeof(T));
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }


                    }
                    else
                    {
                        int dimsMultiplied = 1;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        T[,,] data = new T[dims[0], dims[1], dims[2]];

                        GCHandle gchArr;
                        try
                        {
                            gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            get_data(ref reference, dataPtr);
                        }
                        finally
                        {
                            GCHandle.Alloc(data, GCHandleType.Weak);
                        }
                        return data;
                    }
                }
                else
                {
                    throw new Exception("Type does not match");
                }
            }
            */
            public T[,,,] GetData4D<T>()
            {
                long[] dims = GetDims();
                int[,,,] data = new int[dims[0],dims[1],dims[2],dims[3]];
                interop.DLLArray.get_data(ref reference, data);
                T[,,,] dataT = new T[dims[0], dims[1], dims[2], dims[3]];
                for ( int i = 0; i< dims[0]; i++)
                {
                    for ( int j = 0; j<dims[1]; j++)
                    {
                        for(int k = 0; k<dims[2]; k++)
                        {
                            for(int z = 0; z<dims[3]; z++)
                            {
                                dataT[i,j,k,z] = (T)Convert.ChangeType(data[i,j,k,z], typeof(T));
                            }
                        }
                    }
                }
                return dataT;
                /*
                if (CheckType(typeof(T)))
                {
                    long[] dims = GetDims();
                    if (typeof(T) == typeof(Complex))
                    {
                        int dimsMultiplied = 2;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        if(GetArrayType() == Dtype.c32)
                        {
                            float[] data = new float[dimsMultiplied];
                            T[,,,] complexData = new T[dims[0], dims[1], dims[2], dims[3]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        for (int k = 0; k < complexData.GetLength(2); k++)
                                        {
                                            for (int z = 0; z < complexData.GetLength(3); z++)
                                            {
                                                complexData[i, j, k, z] = (T)Convert.ChangeType(new Complex(data[2*i * dims[0] + 2*j * dims[1] + 2*k * dims[2] + 2*z], data[2*i * dims[0] + 2*j * dims[1] + 2*k * dims[2] + 2*z + 1]), typeof(T));
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }
                        else
                        {
                            double[] data = new double[dimsMultiplied];
                            T[,,,] complexData = new T[dims[0], dims[1], dims[2], dims[3]];
                            GCHandle gchArr;
                            try
                            {
                                gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                                IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                                get_data(ref reference, dataPtr);
                                for (int i = 0; i < complexData.GetLength(0); i++)
                                {
                                    for (int j = 0; j < complexData.GetLength(1); j++)
                                    {
                                        for (int k = 0; k < complexData.GetLength(2); k++)
                                        {
                                            for (int z = 0; z < complexData.GetLength(3); z++)
                                            {
                                                complexData[i, j, k, z] = (T)Convert.ChangeType(new Complex(data[2 * i * dims[0] + 2 * j * dims[1] + 2 * k * dims[2] + 2 * z], data[2 * i * dims[0] + 2 * j * dims[1] + 2 * k * dims[2] + 2 * z + 1]), typeof(T));
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                GCHandle.Alloc(data, GCHandleType.Weak);
                            }
                            return complexData;
                        }

                        
                    }
                    else
                    {
                        int dimsMultiplied = 1;

                        for (int i = 0; i < 4; i++)
                        {
                            dimsMultiplied *= (int)dims[i];
                        }

                        T[,,,] data = new T[dims[0], dims[1], dims[2], dims[3]];

                        GCHandle gchArr;
                        try
                        {
                            gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            get_data(ref reference, dataPtr);
                        }
                        finally
                        {
                            GCHandle.Alloc(data, GCHandleType.Weak);
                        }
                        return data;
                    }
                    
                }
                else
                {
                    throw new Exception("Type does not match");
                }*/
            }
            
            private bool CheckType(Type type)
            {
                switch (GetArrayType()){
                    case Dtype.b8:
                        return type == typeof(bool);
                    case Dtype.c32:
                        return type == typeof(Complex);
                    case Dtype.c64:
                        return type == typeof(Complex);
                    case Dtype.f32:
                        return type == typeof(float);
                    case Dtype.f64:
                        return type == typeof(double);
                    case Dtype.s16:
                        return type == typeof(short);
                    case Dtype.s32:
                        return type == typeof(int);
                    case Dtype.s64:
                        return type == typeof(long);
                    case Dtype.u16:
                        return type == typeof(ushort);
                    case Dtype.u32:
                        return type == typeof(uint);
                    case Dtype.u64:
                        return type == typeof(ulong);
                    case Dtype.u8:
                        return type == typeof(byte);
                    default:
                        return false;
                }
            }

            private void CheckNdims(long[] dims, int maxNdims)
            {
                int i = 3;
                while(i > maxNdims-1)
                {
                    if (dims[i] > 1)
                    {
                        throw new Exception("The array must be have at most " + maxNdims + " dimensions for using this method and have " + (i+1));
                    }
                    i--;
                }
            }

            public long[] GetDims()
            {
                GCHandle gchArr;
                long[] dims = new long[4];
                try
                {
                    gchArr = GCHandle.Alloc(dims, GCHandleType.Pinned);
                    interop.DLLArray.get_dims(ref reference, ref dims[0]);
                }
                finally
                {
                    GCHandle.Alloc(dims, GCHandleType.Weak);
                }
                return dims;
            }

            public void Display()
            {
                interop.DLLArray.display(ref reference);
            }

            public void DeleteArray()
            {
                interop.DLLArray.delete_array(ref reference);
            }

            public Dtype GetArrayType()
            {
                int type = 0;
                interop.DLLArray.get_type(ref reference, ref type);
                Enum.TryParse<Dtype>(type.ToString(), out Dtype dtype);
                return dtype;
            }

            public static Array operator +(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_add(ref lhs.reference, ref rhs.reference, ref result);
                return new Array(result);
            }

            public static Array operator *(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_mul(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator -(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_sub(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator /(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_div(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator %(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_mod(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public Array Pow(Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_pow(ref reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator &(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_bitand(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator |(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_bitor(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator ^(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_bitxor(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <<(Array lhs, int shift)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_bitshiftl(ref lhs.reference, ref shift, ref result);
                return (new Array(result));
            }

            public static Array operator >>(Array lhs, int shift)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_bitshiftr(ref lhs.reference, ref shift, ref result);
                return (new Array(result));
            }

           /* public static Array operator -(Array rhs)
            {
                IntPtr result = new IntPtr();
                Array zeros;
                long[] dims = rhs.GetDims();
                if (rhs.GetArrayType() == Dtype.c32 | rhs.GetArrayType() == Dtype.c64)
                {
                    Complex[,,,] tss = new Complex[dims[0], dims[1], dims[2], dims[3]];
                    if (rhs.GetArrayType() == Dtype.c32)
                    {
                        zeros = new Array(tss, false);
                    }
                    else
                    {
                        zeros = new Array(tss, true);
                    }
                }
                else
                {
                    int[,,,] tss = new int[dims[0], dims[1], dims[2], dims[3]];
                    zeros = new Array(tss);
                }
                khiva_sub(ref zeros.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }*/

            public static Array operator !(Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_not(ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_lt(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator >(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_gt(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_le(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator >=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_ge(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator ==(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_eq(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator !=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_ne(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public Array Transpose(bool conjugate = false)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_transpose(ref reference, ref conjugate, ref result);
                return (new Array(result));
            }

            public Array Col(int index)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_col(ref reference, ref index, ref result);
                return (new Array(result));
            }

            public Array Cols(int first, int last)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_cols(ref reference, ref first, ref last, ref result);
                return (new Array(result));
            }
        }
    }
}
