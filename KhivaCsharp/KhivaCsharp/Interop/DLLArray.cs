using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace khiva.interop
{
    public static class DLLArray
    {
        /**
            * @brief Creates an Array object.
            *
            * @param data Data used in order to create the array.
            * @param ndims Number of dimensions of the data.
            * @param dims Cardinality of dimensions of the data.
            * @param result Array created.
            * @param type Data type.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] float[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] double[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] int[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] uint[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] byte[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] long[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ulong[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] short[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void create_array([In] ushort[,,,] arr, ref uint ndims, long[] dims, ref IntPtr result, ref int type);

    /**
        * @brief Retrieves the data from the device to the host.
        *
        * @param array The Array that contains the data to be retrieved.
        * @param data Pointer to previously allocated memory in the host.
        */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_data(ref IntPtr array, IntPtr data);

        /**
            * @brief Gets the Array dimensions.
            *
            * @param array Array from which to get the dimensions.
            * @param dims The dimensions.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_dims(ref IntPtr array, long[] dims);


        /**
            * @brief Displays an Array.
            *
            * @param array The array to display.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void display(ref IntPtr array);

        /**
            * @brief Decreases the references count of the given array.
            *
            * @param array The Array to release.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void delete_array(ref IntPtr array);

        /**
            * @brief Gets the type of the array.
            *
            * @param array The array to obtain the type information from.
            * @param type Value of the Dtype enumeration.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void get_type(ref IntPtr array, ref int t);

        /**
            * @brief Adds two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_add(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);



        /**
            * @brief Multiplies two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_mul(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Subtracts two arrays.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_sub(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Divides lhs by rhs (element-wise).
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_div(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs the modulo operation of lhs by rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_mod(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Powers lhs with rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation. Base.
            * @param rhs Right-hand side KHIVA array for the operation. Exponent.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_pow(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is lower than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_lt(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is greater than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_gt(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is lower or equal than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_le(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is greater or equal than rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_ge(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if rhs is equal to rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_eq(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Compares (element-wise) if lhs is not equal to rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_ne(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs an AND operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitand(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs an OR operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitor(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitxor(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
            *
            * @param array KHIVA Array to shift.
            * @param n Number of bits to be shifted.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitshiftl(ref IntPtr array, ref int n, ref IntPtr result);

        /**
            * @brief Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
            *
            * @param array KHIVA Array to shift.
            * @param n Number of bits to be shifted.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_bitshiftr(ref IntPtr array, ref int n, ref IntPtr result);

        /**
            * @brief Logical NOT operation to array.
            *
            * @param array KHIVA Array to negate.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_not(ref IntPtr array, ref IntPtr result);

        /**
            * @brief Transposes array.
            *
            * @param array KHIVA Array to transpose.
            * @param conjugate If true a conjugate transposition is performed.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_transpose(ref IntPtr array, ref bool conjugate, ref IntPtr result);

        /**
            * @brief Retrieves a given column of array.
            *
            * @param array KHIVA Array.
            * @param index The column to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_col(ref IntPtr array, ref int index, ref IntPtr result);

        /**
            * @brief Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
            *
            * @param array KHIVA Array.
            * @param first Start of the subset of columns to be retrieved.
            * @param last End of the subset of columns to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_cols(ref IntPtr array, ref int first, ref int last, ref IntPtr result);

        /**
            * @brief Retrieves a given row of array.
            *
            * @param array KHIVA Array.
            * @param index The row to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_row(ref IntPtr array, ref int index, ref IntPtr result);

        /**
            * @brief Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
            *
            * @param array KHIVA Array.
            * @param first Start of the subset of rows to be retrieved.
            * @param last End of the subset of rows to be retrieved.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_rows(ref IntPtr array, ref int first, ref int last, ref IntPtr result);

        /**
            * @brief Creates a KHIVA array from an ArrayFire array.
            *
            * @param arrayfire ArrayFire array reference.
            * @param result KHIVA Array.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void from_arrayfire(ref IntPtr arrayfire, ref IntPtr result);

        /**
            * @brief Performs a matrix multiplication of lhs and rhs.
            *
            * @param lhs Left-hand side KHIVA array for the operation.
            * @param rhs Right-hand side KHIVA array for the operation.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_matmul(ref IntPtr lhs, ref IntPtr rhs, ref IntPtr result);

        /**
            * @brief Performs a deep copy of array.
            *
            * @param array KHIVA Array.
            * @param result KHIVA Array which contains a copy of array.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void copy(ref IntPtr array, ref IntPtr result);

        /**
            * @brief Changes the type of array.
            *
            * @param array KHIVA Array.
            * @param type Target type of the output array.
            * @param result KHIVA Array with the result of this operation.
            */
        [DllImport(DLLLibrary.khivaPath, CallingConvention = CallingConvention.Cdecl)]
        public extern static void khiva_as(ref IntPtr array, ref int type, ref IntPtr result);

    }
}
