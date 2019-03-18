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
                long[] adims = Array.Dim4(dims);

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
                long[] adims = Array.Dim4(dims);

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
                long[] adims = Array.Dim4(dims);

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
                long[] adims = Array.Dim4(dims);

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
                    for (int i = 0; i < arr.Length; i++)
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
                long[] adims = Array.Dim4(dims);

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
                int type = (int)Dtype.b8;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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
                int type = (int)Dtype.s16;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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

            public Array(ushort[] arr, long[] dims)
            {
                int type = (int)Dtype.u16;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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
                int type = (int)Dtype.u8;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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
                int type = (int)Dtype.s64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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

            public Array(ulong[] arr, long[] dims)
            {
                int type = (int)Dtype.u64;
                uint ndims = (uint)dims.Length;
                long[] adims = Array.Dim4(dims);

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
            private extern static void create_array(ref IntPtr arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

            /**
             * @brief Retrieves the data from the device to the host.
             *
             * @param array The Array that contains the data to be retrieved.
             * @param data Pointer to previously allocated memory in the host.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void get_data(ref IntPtr array, ref IntPtr data);

            /**
             * @brief Gets the Array dimensions.
             *
             * @param array Array from which to get the dimensions.
             * @param dims The dimensions.
             */
            [DllImport(khivaPath, CallingConvention = CallingConvention.Cdecl)]
            private extern static void get_dims(ref IntPtr array, ref long[] dims);


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

            public IntPtr GetReference()
            {
                return reference;
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
            public unsafe void* GetData()
            {
                IntPtr data = new IntPtr();
                get_data(ref reference, ref data);
                return data.ToPointer();
            }

            public long[] GetDims()
            {
                long[] dims = new long[4];
                get_dims(ref reference, ref dims);
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

            public int GetArrayType()
            {
                int type = 0;
                get_type(ref reference, ref type);
                return type;
            }

            public Array KhivaAdd(Array rhs)
            {
                IntPtr result = new IntPtr();
                IntPtr rhsReference = rhs.GetReference();
                khiva_add(ref reference, ref rhsReference, ref result);
                return (new Array(result));
            }

            public Array KhivaMul(Array rhs)
            {
                IntPtr result = new IntPtr();
                IntPtr rhsReference = rhs.GetReference();
                khiva_mul(ref reference, ref rhsReference, ref result);
                return (new Array(result));
            }

            public Array KhivaSub(Array rhs)
            {
                IntPtr result = new IntPtr();
                IntPtr rhsReference = rhs.GetReference();
                khiva_sub(ref reference, ref rhsReference, ref result);
                return (new Array(result));
            }

            public Array KhivaDiv(Array rhs)
            {
                IntPtr result = new IntPtr();
                IntPtr rhsReference = rhs.GetReference();
                khiva_sub(ref reference, ref rhsReference, ref result);
                return (new Array(result));
            }

        }
    }
}
