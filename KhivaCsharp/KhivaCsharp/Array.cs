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
using System.Numerics;
using System.Runtime.InteropServices;





namespace khiva
{
    namespace array
    {

        public class KhivaArray
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

            protected KhivaArray()
            {
            }

            protected KhivaArray(System.Array tss, bool doublePrecision = false)
            {
                Create<T>(tss, doublePrecision);
            }

            public KhivaArray(IntPtr reference)
            {
                Reference = reference;
            }

            public KhivaArray(KhivaArray other)
            {
                Reference = other.Reference;
            }



            //toDevice() <-- ArrayOpaco  --> toMemory

            public unsafe static KhivaArray Create<T>(T[] values, bool doublePrecision = false) where T:unmanaged
            {
                fixed(T* data = &values[0])
                    return Create<T>(1, new long[] { values.Length, 1, 1, 1 }, doublePrecision, data);
            }

            public unsafe static KhivaArray Create<T>(T[,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0])
                    return Create<T>(2, new long[] { values.GetLength(1), values.GetLength(0), 1, 1 }, doublePrecision, data);
            }

            public unsafe static KhivaArray Create<T>(T[,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0,0])
                    return Create<T>(3, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), 1 }, doublePrecision, data);
            }

            public unsafe static KhivaArray Create<T>(T[,,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0,0,0])
                    return Create<T>(4, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), values.GetLength(3) }, doublePrecision, data);
            }

            private unsafe static KhivaArray Create<T>(uint ndims, long[] dims, bool doublePrecision, T* values) where T : unmanaged
            {
                KhivaArray arr = new KhivaArray();
                long totalLength = (long)(dims[0] * dims[1] * dims[2] * dims[3]);
                var type = (int)GetDtypeFromT<T>(doublePrecision);
                if(typeof(T) == typeof(Complex))
                {
                    fixed (Complex* data = new Complex[totalLength])
                    {
                        for (int i = 0; i < totalLength; i++)
                        {
                            data[i] = (Complex)Convert.ChangeType(values[i], typeof(Complex));
                        }
                        if (type == (int)Dtype.c32)
                        {  
                            float* flatten = FlatComplex<float>(data, totalLength);
                            interop.DLLArray.create_array(flatten, ref ndims, dims, out arr.reference, ref type);
                        }

                        else if (type == (int)Dtype.c64)
                        {
                            double* flatten = FlatComplex<double>(data, totalLength);
                            interop.DLLArray.create_array(flatten, ref ndims, dims, out arr.reference, ref type);
                        }
                    }   
                }
                else
                {
                    interop.DLLArray.create_array(values, ref ndims, dims, out arr.reference, ref type);
                }

                arr.Reference = arr.reference;
                return arr;
            }

            private unsafe static T* FlatComplex<T>(Complex* data, long totalLength) where T : unmanaged
            {
                fixed(T* flatten = new T[totalLength * 2])
                {
                    for (int i = 0; i<totalLength; i++)
                    {
                        flatten[2 * i] = (T)Convert.ChangeType(data[i].Real, typeof(T));
                        flatten[(2 * i) + 1] = (T)Convert.ChangeType(data[i].Imaginary, typeof(T));
                    }
                    return flatten;
                }
            }

            public T[] GetData1D<T>()
            {
                T[] data = new T[10];
                try
                {
                    if (Type == Dtype.c32)
                    {
                       var values = GetDataAux<float>(20);
                        for(int i = 0; i < 10; i++)
                        {
                            data[i] = (T)Convert.ChangeType(new Complex(values[2*i], values[2*i + 1]), typeof(T));
                        }
                    }
                    else
                    {
                        data = GetDataAux<T>(10);
                    }
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            private T[] GetDataAux<T>(long size)
            {
                T[] data = new T[size];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference, dataPtr);
                    Reference = reference;
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            private static Dtype GetDtypeFromT<T>(bool doublePrecision)
            {
                var type = typeof(T);
                if (type == typeof(double))
                {
                    return Dtype.f64;
                }
                else if (type == typeof(float))
                {
                    return Dtype.f32;
                }
                else if (type == typeof(int))
                {
                    return Dtype.s32;
                }
                else if (type == typeof(uint))
                {
                    return Dtype.u16;
                }
                else if (type == typeof(bool))
                {
                    return Dtype.b8;
                }
                else if (type == typeof(long))
                {
                    return Dtype.s64;
                }
                else if (type == typeof(ulong))
                {
                    return Dtype.u64;
                }
                else if (type == typeof(short))
                {
                    return Dtype.s16;
                }
                else if (type == typeof(ushort))
                {
                    return Dtype.u16;
                }
                else if (type == typeof(byte))
                {
                    return Dtype.u8;
                }
                else if (type == typeof(Complex))
                {
                    if (doublePrecision)
                    {
                        return Dtype.c64;
                    }
                    else
                    {
                        return Dtype.c32;
                    }
                }
                else
                {
                    throw new Exception("Type not supported");
                }
            }

            public Dtype Type
            {
                get
                {
                    interop.DLLArray.get_type(ref reference, out int type);
                    return (Dtype)type;
                }
            }
/*
            public T[] GetData<T>(KhivaArray arr) where T : unmanaged
            {
                T[] data = new T[dims[0]];
                for (int i = 0; i < arr.dims[0]; i++)
                {
                    unsafe
                    {
                        data[i] = (T)Marshal.PtrToStructure(arr.reference + sizeof(T), typeof(T));
                    }
                }
                return data;
            }

*/
/*
            public static KhivaArray Create<T>(T[] values) where T: unmanaged {
                // No funciona
                /*
                 * KhivaArray arr = new KhivaArray();
                var size = Marshal.SizeOf(typeof(T));
                arr.reference = Marshal.AllocHGlobal(size);
                return arr;
                */
                /*
                 * KhivaArray arr = new KhivaArray();
                unsafe
                {
                    var size = sizeof(T);
                    T* data = null;
                    arr.reference = new IntPtr(data);
                }
                return arr;
                */
                // FUNCIONAAAAA
                /*
                KhivaArray arr = new KhivaArray
                {
                    reference = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(T)))
                };
                try
                {
                    for (int i = 0; i < values.Length; i++)
                    {
                        unsafe
                        {
                            Marshal.StructureToPtr<T>(values[i], arr.reference + i * sizeof(T), true); 
                        }
                    }
                    
                }
                finally
                {
                    Marshal.FreeHGlobal(arr.reference);
                }
                T anotherValue;
                unsafe
                {
                    anotherValue = (T)Marshal.PtrToStructure(arr.reference + sizeof(T), typeof(T));
                }

                Console.WriteLine("Another value is: " + anotherValue);
                return arr;
            }
*/
            public IntPtr Reference
            {
                get
                {
                    return reference;
                }
                set
                {
                    reference = value;
                }
            }

            private bool CheckType(Type type)
            {
                switch (ArrayType)
                {
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
                while (i > maxNdims - 1)
                {
                    if (dims[i] > 1)
                    {
                        throw new Exception("The array must be have at most " + maxNdims + " dimensions for using this method and have " + (i + 1));
                    }
                    i--;
                }
            }

            /// <summary>
            /// Gets the KhivaArray dimensions.
            /// </summary>
            public long[] Dims
            {
                get
                {
                    long[] dims = new long[4];
                    interop.DLLArray.get_dims(ref reference, dims);
                    this.Reference = reference;
                    return dims;
                }
            }

            /// <summary>
            /// Displays an KhivaArray.
            /// </summary>
            public void Display()
            {
                interop.DLLArray.display(ref reference);
                this.Reference = reference;
            }

            /// <summary>
            /// Decreases the references count of the given array.
            /// </summary>
            public void DeleteArray()
            {
                interop.DLLArray.delete_array(ref reference);
                this.Reference = reference;
            }

            /// <summary>
            /// Gets the type of the array.
            /// </summary>
            public Dtype ArrayType
            {
                get
                {
                    int type;
                    interop.DLLArray.get_type(ref reference, out type);
                    this.Reference = reference;
                    return (Dtype)type;
                }
            }

            /// <summary>
            /// Adds two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator +(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_add(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return new KhivaArray(result);
            }

            /// <summary>
            /// Multiplies two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator *(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mul(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Substracts two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator -(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_sub(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Divides two arrays.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator /(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_div(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs the modulo operation of lhs by rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator %(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mod(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Powers lhs with rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray Pow(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_pow(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an AND operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator &(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitand(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator |(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitor(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ^(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitxor(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <<(KhivaArray lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftl(ref lhs.reference, ref shift, out result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
            /// </summary>
            /// <param name="lhs">array KHIVA KhivaArray to shift.</param>
            /// <param name="shift">Number of bits to be shifted.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >>(KhivaArray lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftr(ref lhs.reference, ref shift, out result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Unary minus of one array.
            /// </summary>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator -(KhivaArray rhs)
            {
                KhivaArray zeros;
                long[] dims = rhs.Dims;
                uint maxDim = GetMaxDim(dims);
                if (rhs.ArrayType == Dtype.c32 | rhs.ArrayType == Dtype.c64)
                {
                    if (maxDim == 1)
                    {
                        Complex[] tss = new Complex[dims[0]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else if (maxDim == 2)
                    {
                        Complex[,] tss = new Complex[dims[0], dims[1]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else if (maxDim == 3)
                    {
                        Complex[,,] tss = new Complex[dims[0], dims[1], dims[2]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                    else
                    {
                        Complex[,,,] tss = new Complex[dims[0], dims[1], dims[2], dims[3]];
                        if (rhs.ArrayType == Dtype.c32)
                        {
                            zeros = new KhivaArray(tss, false);
                        }
                        else
                        {
                            zeros = new KhivaArray(tss, true);
                        }
                    }
                }
                else
                {
                    if (maxDim == 1)
                    {
                        int[] tss = new int[dims[0]];
                        zeros = new KhivaArray(tss);
                    }
                    else if (maxDim == 2)
                    {
                        int[,] tss = new int[dims[0], dims[1]];
                        zeros = new KhivaArray(tss);
                    }
                    else if (maxDim == 3)
                    {
                        int[,,] tss = new int[dims[0], dims[1], dims[2]];
                        zeros = new KhivaArray(tss);
                    }
                    else
                    {
                        int[,,,] tss = new int[dims[0], dims[1], dims[2], dims[3]];
                        zeros = new KhivaArray(tss);
                    }
                }
                IntPtr result;
                interop.DLLArray.khiva_sub(ref zeros.reference, ref rhs.reference, out result);
                zeros.Reference = zeros.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            private static uint GetMaxDim(long[] dims)
            {
                uint i = 3;
                while (i > 0)
                {
                    if (dims[i] > 1)
                    {
                        return (i + 1);
                    }
                    i--;
                }
                return (i + 1);
            }

            /// <summary>
            /// Logical NOT operation to array.
            /// </summary>
            /// <param name="lhs">KHIVA KhivaArray to negate.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator !(KhivaArray lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_not(ref lhs.reference, out result);
                lhs.Reference = lhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_lt(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_gt(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is lower or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator <=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_le(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is greater or equal than rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator >=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ge(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if rhs is equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator ==(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_eq(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Compares (element-wise) if lhs is not equal to rhs.
            /// </summary>
            /// <param name="lhs">Left-hand side KHIVA array for the operation.</param>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public static KhivaArray operator !=(KhivaArray lhs, KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ne(ref lhs.reference, ref rhs.reference, out result);
                lhs.Reference = lhs.reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Equals object method
            /// </summary>
            /// <param name="o">Object to compare</param>
            /// <returns></returns>
            public override bool Equals(object o)
            {
                if (o.GetType() == typeof(KhivaArray))
                {
                    KhivaArray arr = (KhivaArray)o;
                    return reference.Equals(arr.reference);
                }
                else
                {
                    return false;
                }
            }

            /// <summary>
            /// GetHashCode object method
            /// </summary>
            /// <returns>Hashcode of the KhivaArray</returns>
            public override int GetHashCode()
            {
                return reference.GetHashCode();
            }

            /// <summary>
            /// Transposes array.
            /// </summary>
            /// <param name="conjugate">If true a conjugate transposition is performed.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Transpose(bool conjugate = false)
            {
                IntPtr result;
                interop.DLLArray.khiva_transpose(ref reference, ref conjugate, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a given column of array.
            /// </summary>
            /// <param name="index">The column to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Col(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_col(ref reference, ref index, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of columns to be retrieved.</param>
            /// <param name="last">End of the subset of columns to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Cols(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_cols(ref reference, ref first, ref last, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a given row of array.
            /// </summary>
            /// <param name="index">The row to be retrieved.</param>
            /// <returns> KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Row(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_row(ref reference, ref index, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
            /// </summary>
            /// <param name="first">Start of the subset of rows to be retrieved.</param>
            /// <param name="last">End of the subset of rows to be retrieved.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Rows(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_rows(ref reference, ref first, ref last, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a matrix multiplication of lhs and rhs.
            /// </summary>
            /// <param name="rhs">Right-hand side KHIVA array for the operation.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray Matmul(KhivaArray rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_matmul(ref reference, ref rhs.reference, out result);
                this.Reference = reference;
                rhs.Reference = rhs.reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Performs a deep copy of array.
            /// </summary>
            /// <returns>KHIVA KhivaArray which contains a copy of array.</returns>
            public KhivaArray Copy()
            {
                IntPtr result;
                interop.DLLArray.copy(ref reference, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }

            /// <summary>
            /// Changes the type of array.
            /// </summary>
            /// <param name="type">Target type of the output array.</param>
            /// <returns>KHIVA KhivaArray with the result of this operation.</returns>
            public KhivaArray As(int type)
            {
                IntPtr result;
                interop.DLLArray.khiva_as(ref reference, ref type, out result);
                this.Reference = reference;
                return (new KhivaArray(result));
            }
        }
    }
}