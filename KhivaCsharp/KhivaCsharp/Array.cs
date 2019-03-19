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
            private const String khivaPath = library.Library.khivaPath;

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

            public Array(float[] arr)
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
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(int[] arr)
            {
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
                    long[] dims = { arr.Length, 1, 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

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
                        complexArrDouble[i] = arr[i].Real;
                        complexArrDouble[i + 1] = arr[i].Imaginary;
                    }
                }
                else
                {
                    complexArrFloat = new float[arr.Length * 2];
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        complexArrFloat[i] = (float)arr[i].Real;
                        complexArrFloat[i + 1] = (float)arr[i].Imaginary;
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(int[,] arr)
            {
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    complexArrDouble = new double[arr.Length * 2];
                    type = (int)Dtype.c64;
                    for (int i = 0; i < arr.GetLength(0); i++)
                    {
                        for (int j = 0; j < arr.GetLength(1); j++)
                        {
                            complexArrDouble[i * arr.GetLength(0) + j] = arr[i, j].Real;
                            complexArrDouble[i * arr.GetLength(0) + j + 1] = arr[i, j].Imaginary;
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
                            complexArrFloat[i * arr.GetLength(0) + j] = (float)arr[i, j].Real;
                            complexArrFloat[i * arr.GetLength(0) + j + 1] = (float)arr[i, j].Imaginary;
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), 1, 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(int[,,] arr)
            {
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
                    create_array(ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                                complexArrDouble[i * arr.GetLength(0) + j * arr.GetLength(1) + k] = arr[i, j, k].Real;
                                complexArrDouble[i * arr.GetLength(0) + j * arr.GetLength(1) + k + 1] = arr[i, j, k].Imaginary;
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
                                complexArrFloat[i * arr.GetLength(0) + j * arr.GetLength(1) + k] = (float)arr[i, j, k].Real;
                                complexArrFloat[i * arr.GetLength(0) + j * arr.GetLength(1) + k + 1] = (float)arr[i, j, k].Imaginary;
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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
                    long[] dims = { arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), 1 };
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

            public Array(int[,,,] arr)
            {
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
                }
            }

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
                                    complexArrDouble[i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z] = arr[i, j, k, z].Real;
                                    complexArrDouble[i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z + 1] = arr[i, j, k, z].Imaginary;
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
                                    complexArrFloat[i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z] = (float)arr[i, j, k, z].Real;
                                    complexArrFloat[i * arr.GetLength(0) + j * arr.GetLength(1) + k * arr.GetLength(2) + z + 1] = (float)arr[i, j, k, z].Imaginary;
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
            }

            public Array(IntPtr reference)
            {
                this.reference = reference;
            }

            public Array(Array other)
            {
                this.reference = other.GetReference();
            }

            /**
             * @brief Creates an Array object.
             *
             * @param data Data used in order to create the array.
             * @param ndims Number of dimensions of the data.
             * @param dims Cardinality of dimensions of the data.
             * @param result Array created.
             * @param type Data type.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void create_array(IntPtr arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

            /**
             * @brief Retrieves the data from the device to the host.
             *
             * @param array The Array that contains the data to be retrieved.
             * @param data Pointer to previously allocated memory in the host.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void get_data(ref IntPtr array, IntPtr data);

            /**
             * @brief Gets the Array dimensions.
             *
             * @param array Array from which to get the dimensions.
             * @param dims The dimensions.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void get_dims(ref IntPtr array, ref long dims);


            /**
             * @brief Displays an Array.
             *
             * @param array The array to display.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void display(ref IntPtr array);

            /**
             * @brief Decreases the references count of the given array.
             *
             * @param array The Array to release.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void delete_array(ref IntPtr array);

            /**
             * @brief Gets the type of the array.
             *
             * @param array The array to obtain the type information from.
             * @param type Value of the Dtype enumeration.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void get_type(ref IntPtr array, ref int t);

            /**
             * @brief Adds two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_add(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);



            /**
             * @brief Multiplies two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_mul(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Subtracts two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_sub(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Divides lhs by rhs (element-wise).
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_div(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs the modulo operation of lhs by rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_mod(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Powers lhs with rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation. Base.
             * @param rhs Right-hand side KHIVA array for the operation. Exponent.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_pow(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if lhs is lower than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_lt(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if lhs is greater than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_gt(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if lhs is lower or equal than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_le(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if lhs is greater or equal than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_ge(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if rhs is equal to rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_eq(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Compares (element-wise) if lhs is not equal to rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_ne(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs an AND operation (element-wise) with lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_bitand(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs an OR operation (element-wise) with lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_bitor(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_bitxor(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
             *
             * @param array KHIVA Array to shift.
             * @param n Number of bits to be shifted.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_bitshiftl(ref IntPtr array, ref int n, ref IntPtr result);

            /**
             * @brief Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
             *
             * @param array KHIVA Array to shift.
             * @param n Number of bits to be shifted.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_bitshiftr(ref IntPtr array, ref int n, ref IntPtr result);

            /**
             * @brief Logical NOT operation to array.
             *
             * @param array KHIVA Array to negate.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_not(ref IntPtr array, ref IntPtr result);

            /**
             * @brief Transposes array.
             *
             * @param array KHIVA Array to transpose.
             * @param conjugate If true a conjugate transposition is performed.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_transpose(ref IntPtr array, ref bool conjugate, ref IntPtr result);

            /**
             * @brief Retrieves a given column of array.
             *
             * @param array KHIVA Array.
             * @param index The column to be retrieved.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_col(ref IntPtr array, ref int index, ref IntPtr result);

            /**
             * @brief Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
             *
             * @param array KHIVA Array.
             * @param first Start of the subset of columns to be retrieved.
             * @param last End of the subset of columns to be retrieved.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_cols(ref IntPtr array, ref int first, ref int last, ref IntPtr result);

            /**
             * @brief Retrieves a given row of array.
             *
             * @param array KHIVA Array.
             * @param index The row to be retrieved.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_row(ref IntPtr array, ref int index, ref IntPtr result);

            /**
             * @brief Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
             *
             * @param array KHIVA Array.
             * @param first Start of the subset of rows to be retrieved.
             * @param last End of the subset of rows to be retrieved.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_rows(ref IntPtr array, ref int first, ref int last, ref IntPtr result);

            /**
             * @brief Creates a KHIVA array from an ArrayFire array.
             *
             * @param arrayfire ArrayFire array reference.
             * @param result KHIVA Array.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void from_arrayfire(ref IntPtr arrayfire, ref IntPtr result);

            /**
             * @brief Performs a matrix multiplication of lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_matmul(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

            /**
             * @brief Performs a deep copy of array.
             *
             * @param array KHIVA Array.
             * @param result KHIVA Array which contains a copy of array.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void copy(ref IntPtr array, ref IntPtr result);

            /**
             * @brief Changes the type of array.
             *
             * @param array KHIVA Array.
             * @param type Target type of the output array.
             * @param result KHIVA Array with the result of this operation.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void khiva_as(ref IntPtr array, ref int type, ref IntPtr result);

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
                                    complexData[i] = (T)Convert.ChangeType(new Complex(data[i], data[i + complexData.Length]), typeof(T));
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
                                    complexData[i] = (T)Convert.ChangeType(new Complex(data[i], data[i + complexData.Length]), typeof(T));
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
                                        complexData[i, j] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j], data[i * dims[0] + j + complexData.Length]), typeof(T));
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
                                        complexData[i, j] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j], data[i * dims[0] + j + complexData.Length]), typeof(T));
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
                                            complexData[i, j, k] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j * dims[1] + k], data[i * dims[0] + j * dims[1] + k + complexData.Length]), typeof(T));
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
                                            complexData[i, j, k] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j * dims[1] + k], data[i * dims[0] + j * dims[1] + k + complexData.Length]), typeof(T));
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

            public T[,,,] GetData4D<T>()
            {
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
                                                complexData[i, j, k, z] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j * dims[1] + k * dims[2] + z], data[i * dims[0] + j * dims[1] + k * dims[2] + z + complexData.Length]), typeof(T));
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
                                                complexData[i, j, k, z] = (T)Convert.ChangeType(new Complex(data[i * dims[0] + j * dims[1] + k * dims[2] + z], data[i * dims[0] + j * dims[1] + k * dims[2] + z + complexData.Length]), typeof(T));
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
                }
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
                    get_dims(ref reference, ref dims[0]);
                }
                finally
                {
                    GCHandle.Alloc(dims, GCHandleType.Weak);
                }
                return dims;
            }

            public void Display()
            {
                display(ref reference);
            }

            public void DeleteArray()
            {
                delete_array(ref reference);
            }

            public Dtype GetArrayType()
            {
                int type = 0;
                get_type(ref reference, ref type);
                Enum.TryParse<Dtype>(type.ToString(), out Dtype dtype);
                return dtype;
            }

            public static Array operator +(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_add(ref lhs.reference, ref rhs.reference, ref result);
                return new Array(result);
            }

            public static Array operator *(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_mul(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator -(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_sub(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator /(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_div(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator %(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_mod(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public Array Pow(Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_pow(ref reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator &(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_bitand(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator |(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_bitor(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator ^(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                khiva_bitxor(ref lhs.reference, ref rhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <<(Array lhs, int shift)
            {
                IntPtr result = new IntPtr();
                khiva_bitshiftl(ref lhs.reference, ref shift, ref result);
                return (new Array(result));
            }

            public static Array operator >>(Array lhs, int shift)
            {
                IntPtr result = new IntPtr();
                khiva_bitshiftr(ref lhs.reference, ref shift, ref result);
                return (new Array(result));
            }

            public static Array operator -(Array rhs)
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
            }

            public static Array operator !(Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_not(ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_lt(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator >(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_gt(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator <=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_le(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator >=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_ge(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator ==(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_eq(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public static Array operator !=(Array rhs, Array lhs)
            {
                IntPtr result = new IntPtr();
                khiva_ne(ref rhs.reference, ref lhs.reference, ref result);
                return (new Array(result));
            }

            public Array Transpose(bool conjugate = false)
            {
                IntPtr result = new IntPtr();
                khiva_transpose(ref reference, ref conjugate, ref result);
                return (new Array(result));
            }

            public Array Col(int index)
            {
                IntPtr result = new IntPtr();
                khiva_col(ref reference, ref index, ref result);
                return (new Array(result));
            }

            public Array Cols(int first, int last)
            {
                IntPtr result = new IntPtr();
                khiva_cols(ref reference, ref first, ref last, ref result);
                return (new Array(result));
            }
        }
    }
}
