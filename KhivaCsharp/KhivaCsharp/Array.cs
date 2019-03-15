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

            public Array(double[] arr, long[] dims)
            {
                int type = (int)Dtype.f64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();    

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(float[] arr, long[] dims)
            {
                int type = (int)Dtype.f32;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(int[] arr, long[] dims)
            {
                int type = (int)Dtype.s32;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(uint[] arr, long[] dims)
            {
                int type = (int)Dtype.u32;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(Complex[] arr, long[] dims, bool doublePrecision)
            {
                int type;
                double[] complexArrDouble = new double[arr.Length * 2];
                float[] complexArrFloat = new float[arr.Length * 2];

                if (doublePrecision)
                {
                    type = (int)Dtype.c64;
                    for(int i = 0; i < arr.Length; i++)
                    {
                        complexArrDouble[i] = arr[i].Real;
                        complexArrDouble[i + arr.Length] = arr[i].Imaginary;
                    }
                }
                else
                {
                    type = (int)Dtype.c32;
                    for (int i = 0; i < arr.Length; i++)
                    {
                        complexArrFloat[i] = (float)arr[i].Real;
                        complexArrFloat[i + arr.Length] = (float)arr[i].Imaginary;
                    }
                }
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    if(doublePrecision)
                    {
                        gchArr = GCHandle.Alloc(complexArrDouble, GCHandleType.Pinned);
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(complexArrFloat, GCHandleType.Pinned);
                    }
                    
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
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

            public Array(Boolean[] arr, long[] dims)
            {
                int type = (int)Dtype.f64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(short[] arr, long[] dims)
            {
                int type = (int)Dtype.f64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(byte[] arr, long[] dims)
            {
                int type = (int)Dtype.f64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
                }
                finally
                {
                    GCHandle.Alloc(arr, GCHandleType.Weak);
                }
            }

            public Array(long[] arr, long[] dims)
            {
                int type = (int)Dtype.f64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.dim4(dims);

                GCHandle gchArr = default(GCHandle);

                try
                {
                    gchArr = GCHandle.Alloc(arr, GCHandleType.Pinned);
                    IntPtr ptrArr = gchArr.AddrOfPinnedObject();

                    int totalSize = 1;

                    for (int i = 0; i < adims.Length; i++)
                    {
                        totalSize = (int)(totalSize * adims[i]);
                    }

                    if (arr == null)
                    {
                        throw new Exception("Null elems object provided");
                    }

                    if (arr.Length > totalSize || arr.Length < totalSize)
                    {
                        throw new Exception("Mismatching dims and array size");
                    }

                    create_array(ref ptrArr, ref ndims, dims, ref reference, ref type);
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
                this.reference = other.reference;
            }

            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void create_array(ref IntPtr arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

            protected static long[] dim4(long[] dims)
            {
                if (dims == null)
                {
                    throw new Exception("Null dimensions object provided");
                } else if (dims.Length > 4)
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


        }
    }
}
