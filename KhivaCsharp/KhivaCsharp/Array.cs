using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace khiva
{
    namespace array
    {
        /**
         * Khiva Array Class.
         */
        class Array : library.Library
        {
            private long reference;

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
                long[] adims = Array.dim4(dims);

                int totalSize = 1;

                for ( int i = 0; i < adims.Length; i++)
                {
                    totalSize = (int)(totalSize * adims[i]);
                }

                if(arr == null)
                {
                    throw new Exception("Null elems object provided");
                }

                if (arr.Length > totalSize || arr.Length < totalSize)
                {
                    throw new Exception("Mismatching dims and array size");
                }

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(float[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(int[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(Complex[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(Boolean[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(short[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(byte[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(long[] arr, long[] dims)
            {
                long[] adims = Array.dim4(dims);

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

                this.reference = createArrayFromFloat(arr, dims);
            }

            public Array(long reference)
            {
                this.reference = reference;
            }

            public Array(Array other)
            {
                this.reference = other.reference;
            }

        }
    }
}
