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
        
        public class MyArray<T> where T: struct
        {

        }
        

        public class MyArray2
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

            protected MyArray2()
            {

            }



            //toDevice() <-- ArrayOpaco  --> toMemory

            public unsafe static MyArray2 Create<T>(T[] values, bool doublePrecision = false) where T:unmanaged
            {
                fixed(T* data = values)
                    return Create<T>(1, new long[] { values.Length, 1, 1, 1 }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(T[,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = values)
                    return Create<T>(2, new long[] { values.GetLength(1), values.GetLength(0), 1, 1 }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(T[,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = values)
                    return Create<T>(3, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), 1 }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(T[,,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = values)
                    return Create<T>(4, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), values.GetLength(3) }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(uint ndims, long[] dims, bool doublePrecision, T* values) where T : unmanaged
            {
                MyArray2 arr = new MyArray2();
                IntPtr data;
                long totalLength = (long)(dims[0] * dims[1] * dims[2] * dims[3]);
                unsafe
                {
                    data = Marshal.AllocHGlobal((int)(sizeof(T) * totalLength));
                }
                try
                {
                    for (int i = 0; i < totalLength; i++)
                    {
                        Marshal.StructureToPtr<T>(values[i], data + i * sizeof(T), true);
                    }
                    var type = (int)GetDtypeFromT<T>(doublePrecision);
                    interop.DLLArray.create_array(ref data, ref ndims, ref dims, ref arr.reference, ref type);
                }
                finally
                {
                    Marshal.FreeHGlobal(data);
                }                
                return arr;
            }

            public T[] GetData1D<T>()
            {
                T[] data = new T[10];
                try
                {
                    GCHandle gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                    IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                    interop.DLLArray.get_data(ref reference,out dataPtr);
                }
                finally
                {
                    GCHandle.Alloc(data, GCHandleType.Weak);
                }
                return data;
            }

            private static Dtype GetDtypeFromT<T>(bool doublePrecision = false)
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
                    interop.DLLArray.get_type(reference, out int type);
                    return (Dtype)type;
                }
            }
/*
            public T[] GetData<T>(MyArray2 arr) where T : unmanaged
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
            public static MyArray2 Create<T>(T[] values) where T: unmanaged {
                // No funciona
                /*
                 * MyArray2 arr = new MyArray2();
                var size = Marshal.SizeOf(typeof(T));
                arr.reference = Marshal.AllocHGlobal(size);
                return arr;
                */
                /*
                 * MyArray2 arr = new MyArray2();
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
                MyArray2 arr = new MyArray2
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
            }
            /*
            public T[] GetData1D<T>(MyArray2 arr) where T:unmanaged{
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
            public static MyArray2 Create<T>(T[,] values) where T : struct
            {
                return new MyArray2();
            }

            public static MyArray2 Create<T>(int dim1, int dim2, Func<int, int, T> valuesFn) where T : struct
            {
                return new MyArray2();
            }
            */
        }
        


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

            /**
             * @brief Creates an Array object.
             *
             * @param arr Data used in order to create the array.
             */
            public Array(float[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(float[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(float[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(float[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(double[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(double[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(double[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(double[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.f64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(Complex[] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten1D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ndims, dims, out reference, type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten1D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ndims, dims, out reference, type);
                }
            }

            private void Flatten1D<T>(T[] complexArr, Complex[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    complexArr[2 * i] = (T)Convert.ChangeType(arr[i].Real, typeof(T));
                    complexArr[2 * i + 1] = (T)Convert.ChangeType(arr[i].Imaginary, typeof(T));
                }
            }

            public Array(Complex[,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten2D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ndims, dims, out reference, type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten2D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ndims, dims, out reference, type);
                }
            }

            private void Flatten2D<T>(T[] complexArr, Complex[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        complexArr[2 * i * arr.GetLength(1) + 2 * j] = (T)Convert.ChangeType(arr[i, j].Real, typeof(T));
                        complexArr[2 * i * arr.GetLength(1) + 2 * j + 1] = (T)Convert.ChangeType(arr[i, j].Imaginary, typeof(T));
                    }
                }
            }

            public Array(Complex[,,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten3D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ndims, dims, out reference, type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten3D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ndims, dims, out reference, type);
                }
            }

            private void Flatten3D<T>(T[] complexArr, Complex[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k] = (T)Convert.ChangeType(arr[i, j, k].Real, typeof(T));
                            complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2)) + 2 * j * arr.GetLength(2) + 2 * k + 1] = (T)Convert.ChangeType(arr[i, j, k].Imaginary, typeof(T));
                        }
                    }
                }
            }

            public Array(Complex[,,,] arr, bool doublePrecision)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                double[] complexArrDouble = null;
                float[] complexArrFloat = null;
                if (!doublePrecision)
                {
                    type = (int)Dtype.c32;
                    complexArrFloat = new float[arr.Length * 2];
                    Flatten4D<float>(complexArrFloat, arr);
                    interop.DLLArray.create_array(complexArrFloat, ndims, dims, out reference, type);
                }
                else
                {
                    type = (int)Dtype.c64;
                    complexArrDouble = new double[arr.Length * 2];
                    Flatten4D<double>(complexArrDouble, arr);
                    interop.DLLArray.create_array(complexArrDouble, ndims, dims, out reference, type);
                }
            }

            private void Flatten4D<T>(T[] complexArr, Complex[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2) * arr.GetLength(3)) + 2 * j * (arr.GetLength(2) * arr.GetLength(3)) + 2 * k * arr.GetLength(3) + 2 * z] = (T)Convert.ChangeType(arr[i, j, k, z].Real, typeof(T));
                                complexArr[2 * i * (arr.GetLength(1) * arr.GetLength(2) * arr.GetLength(3)) + 2 * j * (arr.GetLength(2) * arr.GetLength(3)) + 2 * k * arr.GetLength(3) + 2 * z + 1] = (T)Convert.ChangeType(arr[i, j, k, z].Imaginary, typeof(T));
                            }
                        }
                    }
                }
            }

            public Array(bool[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                byte[] byteArr = new byte[arr.GetLength(0)];
                BoolToByte1D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ndims, dims, out reference, type);
            }

            private void BoolToByte1D(byte[] byteArr, bool[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    byteArr[i] = Convert.ToByte(arr[i]);
                }
            }

            public Array(bool[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                byte[,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1)];
                BoolToByte2D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ndims, dims, out reference, type);
            }

            private void BoolToByte2D(byte[,] byteArr, bool[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        byteArr[i, j] = Convert.ToByte(arr[i, j]);
                    }
                }
            }

            public Array(bool[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                byte[,,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1), arr.GetLength(2)];
                BoolToByte3D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ndims, dims, out reference, type);
            }

            private void BoolToByte3D(byte[,,] byteArr, bool[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            byteArr[i, j, k] = Convert.ToByte(arr[i, j, k]);
                        }
                    }
                }
            }

            public Array(bool[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.b8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                byte[,,,] byteArr = new byte[arr.GetLength(0), arr.GetLength(1), arr.GetLength(2), arr.GetLength(3)];
                BoolToByte4D(byteArr, arr);
                interop.DLLArray.create_array(byteArr, ndims, dims, out reference, type);
            }

            private void BoolToByte4D(byte[,,,] byteArr, bool[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                byteArr[i, j, k, z] = Convert.ToByte(arr[i, j, k, z]);
                            }
                        }
                    }
                }
            }

            public Array(int[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(int[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(int[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(int[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(uint[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(uint[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(uint[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(uint[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u32;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(byte[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(byte[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(byte[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(byte[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u8;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(long[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(long[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(long[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(long[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ulong[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ulong[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ulong[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ulong[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u64;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(short[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(short[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(short[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(short[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.s16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ushort[] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.Length, 1, 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ushort[,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), 1, 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ushort[,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), 1 };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(ushort[,,,] arr)
            {
                if (arr == null)
                {
                    throw new Exception("Null elems object provided");
                }
                int type = (int)Dtype.u16;
                uint ndims = (uint)arr.Rank;
                long[] dims = { arr.GetLength(1), arr.GetLength(0), arr.GetLength(2), arr.GetLength(3) };
                interop.DLLArray.create_array(arr, ndims, dims, out reference, type);
            }

            public Array(IntPtr reference)
            {
                this.reference = reference;
            }

            public Array(Array other)
            {
                this.reference = other.GetReference();
            }

            public IntPtr GetReference()
            {
                return reference;
            }
            
            /**
             * Gets the data stored in the array.
             *
             * @param <T> The data type to be returned.
             * @return The data to an array of its type.
             */
           /* public T[] GetData1D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = GetDims();
                CheckNdims(dims, 1);
                T[] data = new T[dims[0]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[] byteData = new byte[dims[0]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                        ByteToGeneric1D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (GetArrayType() == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex1D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex1D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                        GCHandle.Alloc(data, GCHandleType.Weak);
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric1D<T>(T[] genericArr, byte[] arr)
            {
                for (int i = 0; i < arr.Length; i++)
                {
                    genericArr[i] = (T)Convert.ChangeType(arr[i], typeof(T));
                }
            }

            private void ToGenericComplex1D<T, R>(T[] genericArr, R[] arr)
            {
                for (int i = 0; i < arr.Length / 2; i++)
                {
                    genericArr[i] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i]), Convert.ToDouble(arr[2 * i + 1])), typeof(T));
                }
            }

            public T[,] GetData2D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = GetDims();
                CheckNdims(dims, 2);
                T[,] data = new T[dims[1], dims[0]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,] byteData = new byte[dims[0], dims[1]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                        ByteToGeneric2D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (GetArrayType() == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex2D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex2D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                
                return data;
            }

            private void ByteToGeneric2D<T>(T[,] genericArr, byte[,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        genericArr[i, j] = (T)Convert.ChangeType(arr[i, j], typeof(T));
                    }
                }
            }

            private void ToGenericComplex2D<T, R>(T[,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        genericArr[i, j] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * genericArr.GetLength(1) + 2 * j]), Convert.ToDouble(arr[2 * i * genericArr.GetLength(1) + 2 * j + 1])), typeof(T));
                    }
                }
            }

            public T[,,] GetData3D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = GetDims();
                CheckNdims(dims, 3);
                T[,,] data = new T[dims[1], dims[0], dims[2]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,,] byteData = new byte[dims[0], dims[1], dims[2]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                        ByteToGeneric3D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (GetArrayType() == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * dims[2] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex3D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * dims[2] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex3D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric3D<T>(T[,,] genericArr, byte[,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            genericArr[i, j, k] = (T)Convert.ChangeType(arr[i, j, k], typeof(T));
                        }
                    }
                }
            }

            private void ToGenericComplex3D<T, R>(T[,,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        for (int k = 0; k < genericArr.GetLength(2); k++)
                        {
                            genericArr[i, j, k] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2)) + 2 * j * genericArr.GetLength(2) + 2 * k]), Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2)) + 2 * j * genericArr.GetLength(2) + 2 * k + 1])), typeof(T));
                        }
                    }
                }
            }

            public T[,,,] GetData4D<T>()
            {
                if (!CheckType(typeof(T)))
                {
                    throw new Exception("Type does mismatch");
                }
                long[] dims = GetDims();
                T[,,,] data = new T[dims[1], dims[0], dims[2], dims[3]];
                GCHandle gchArr = new GCHandle();
                try
                {
                    if (typeof(T) == typeof(bool))
                    {
                        byte[,,,] byteData = new byte[dims[0], dims[1], dims[2], dims[3]];
                        gchArr = GCHandle.Alloc(byteData, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                        ByteToGeneric4D<T>(data, byteData);
                    }
                    else if (typeof(T) == typeof(Complex))
                    {
                        if (GetArrayType() == Dtype.c64)
                        {
                            double[] complexData = new double[dims[0] * dims[1] * dims[2] * dims[3] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex4D<T, double>(data, complexData);
                        }
                        else
                        {
                            float[] complexData = new float[dims[0] * dims[1] * dims[2] * dims[3] * 2];
                            gchArr = GCHandle.Alloc(complexData, GCHandleType.Pinned);
                            IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                            interop.DLLArray.get_data(reference, dataPtr);
                            ToGenericComplex4D<T, float>(data, complexData);
                        }
                    }
                    else
                    {
                        gchArr = GCHandle.Alloc(data, GCHandleType.Pinned);
                        IntPtr dataPtr = gchArr.AddrOfPinnedObject();
                        interop.DLLArray.get_data(reference, dataPtr);
                    }
                }
                finally
                {
                    GCHandle.Alloc(gchArr.Target, GCHandleType.Weak);
                }
                return data;
            }

            private void ByteToGeneric4D<T>(T[,,,] genericArr, byte[,,,] arr)
            {
                for (int i = 0; i < arr.GetLength(0); i++)
                {
                    for (int j = 0; j < arr.GetLength(1); j++)
                    {
                        for (int k = 0; k < arr.GetLength(2); k++)
                        {
                            for (int z = 0; z < arr.GetLength(3); z++)
                            {
                                genericArr[i, j, k, z] = (T)Convert.ChangeType(arr[i, j, k, z], typeof(T));
                            }
                        }
                    }
                }
            }

            private void ToGenericComplex4D<T, R>(T[,,,] genericArr, R[] arr)
            {
                for (int i = 0; i < genericArr.GetLength(0); i++)
                {
                    for (int j = 0; j < genericArr.GetLength(1); j++)
                    {
                        for (int k = 0; k < genericArr.GetLength(2); k++)
                        {
                            for (int z = 0; z < genericArr.GetLength(3); z++)
                            {
                                genericArr[i, j, k, z] = (T)Convert.ChangeType(new Complex(Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * j * (genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * k * genericArr.GetLength(3) + 2 * z]), Convert.ToDouble(arr[2 * i * (genericArr.GetLength(1) + genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * j * (genericArr.GetLength(2) + genericArr.GetLength(3)) + 2 * k * genericArr.GetLength(3) + 2 * z + 1])), typeof(T));
                            }
                        }
                    }
                }
            }*/

            private bool CheckType(Type type)
            {
                switch (GetArrayType()) {
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

            /**
             * @brief Gets the Array dimensions.
             */
            public long[] GetDims()
            {
                GCHandle gchArr;
                long[] dims = new long[4];
                try
                {
                    gchArr = GCHandle.Alloc(dims, GCHandleType.Pinned);
                    interop.DLLArray.get_dims(reference, dims);
                }
                finally
                {
                    GCHandle.Alloc(dims, GCHandleType.Weak);
                }
                return dims;
            }

            /**
             * @brief Displays an Array.
             */
            public void Display()
            {
                interop.DLLArray.display(reference);
            }

            /**
             * @brief Decreases the references count of the given array.
             */
            public void DeleteArray()
            {
                interop.DLLArray.delete_array(ref reference);
            }

                /**
             * @brief Gets the type of the array.
             */
            public Dtype GetArrayType()
            {
                interop.DLLArray.get_type(reference, out int type);
                Enum.TryParse<Dtype>(type.ToString(), out Dtype dtype);
                return dtype;
            }

            /**
             * @brief Adds two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator +(Array lhs, Array rhs)
            {
                IntPtr result = new IntPtr();
                interop.DLLArray.khiva_add(lhs.reference, rhs.reference, out result);
                return new Array(result);
            }

            /**
             * @brief Multiplies two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator *(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mul(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Subtracts two arrays.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator -(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_sub(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Divides lhs by rhs (element-wise).
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator /(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_div(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs the modulo operation of lhs by rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator %(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_mod(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Powers lhs with rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation. Base.
             * @param rhs Right-hand side KHIVA array for the operation. Exponent.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Pow(Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_pow(reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs an AND operation (element-wise) with lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator &(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitand(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
              * @brief Performs an OR operation (element-wise) with lhs and rhs.
              *
              * @param lhs Left-hand side KHIVA array for the operation.
              * @param rhs Right-hand side KHIVA array for the operation.
              * @return result KHIVA Array with the result of this operation.
              */
            public static Array operator |(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitor(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs an eXclusive-OR operation (element-wise) with lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator ^(Array lhs, Array rhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitxor(lhs.reference, rhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs a left bit shift operation (element-wise) to array as many times as specified in the parameter n.
             *
             * @param array KHIVA Array to shift.
             * @param n Number of bits to be shifted.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator <<(Array lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftl(lhs.reference, shift, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs a right bit shift operation (element-wise) to array as many times as specified in the parameter n.
             *
             * @param array KHIVA Array to shift.
             * @param n Number of bits to be shifted.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator >>(Array lhs, int shift)
            {
                IntPtr result;
                interop.DLLArray.khiva_bitshiftr(lhs.reference, shift, out result);
                return (new Array(result));
            }

            /**
             * @brief Unary minus of one array.
             *
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator -(Array rhs)
            {
                IntPtr result;
                Array zeros;
                long[] dims = rhs.GetDims();
                uint maxDim = GetMaxDim(dims);
                if (rhs.GetArrayType() == Dtype.c32 | rhs.GetArrayType() == Dtype.c64)
                {
                    if (maxDim == 1)
                    {
                        Complex[] tss = new Complex[dims[0]];
                        if (rhs.GetArrayType() == Dtype.c32)
                        {
                            zeros = new Array(tss, false);
                        }
                        else
                        {
                            zeros = new Array(tss, true);
                        }
                    }
                    else if (maxDim == 2)
                    {
                        Complex[,] tss = new Complex[dims[0], dims[1]];
                        if (rhs.GetArrayType() == Dtype.c32)
                        {
                            zeros = new Array(tss, false);
                        }
                        else
                        {
                            zeros = new Array(tss, true);
                        }
                    }
                    else if (maxDim == 3)
                    {
                        Complex[,,] tss = new Complex[dims[0], dims[1], dims[2]];
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
                }
                else
                {
                    if (maxDim == 1)
                    {
                        int[] tss = new int[dims[0]];
                        zeros = new Array(tss);
                    } else if (maxDim == 2)
                    {
                        int[,] tss = new int[dims[0], dims[1]];
                        zeros = new Array(tss);
                    }
                    else if (maxDim == 3)
                    {
                        int[,,] tss = new int[dims[0], dims[1], dims[2]];
                        zeros = new Array(tss);
                    }
                    else
                    {
                        int[,,,] tss = new int[dims[0], dims[1], dims[2], dims[3]];
                        zeros = new Array(tss);
                    }
                }
                interop.DLLArray.khiva_sub(zeros.reference, rhs.reference, out result);
                return (new Array(result));
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

            /**
             * @brief Logical NOT operation to array.
             *
             * @param array KHIVA Array to negate.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator !(Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_not(lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if lhs is lower than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator <(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_lt(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if lhs is greater than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator >(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_gt(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if lhs is lower or equal than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator <=(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_le(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if lhs is greater or equal than rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator >=(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ge(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if rhs is equal to rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator ==(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_eq(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Compares (element-wise) if lhs is not equal to rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public static Array operator !=(Array rhs, Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_ne(rhs.reference, lhs.reference, out result);
                return (new Array(result));
            }

            public override bool Equals(object o)
            {
                if(o.GetType() == typeof(Array))
                {
                    Array arr = (Array)o;
                    return reference.Equals(arr.reference);
                }
                else
                {
                    return false;
                }
            }
            public override int GetHashCode()
            {
                return reference.GetHashCode();   
            }

            /**
             * @brief Transposes array.
             *
             * @param array KHIVA Array to transpose.
             * @param conjugate If true a conjugate transposition is performed.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Transpose(bool conjugate = false)
            {
                IntPtr result;
                interop.DLLArray.khiva_transpose(reference, conjugate, out result);
                return (new Array(result));
            }

            /**
             * @brief Retrieves a given column of array.
             *
             * @param array KHIVA Array.
             * @param index The column to be retrieved.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Col(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_col(reference, index, out result);
                return (new Array(result));
            }

            /**
             * @brief Retrieves a subset of columns of array, starting at first and finishing at last, both inclusive.
             *
             * @param array KHIVA Array.
             * @param first Start of the subset of columns to be retrieved.
             * @param last End of the subset of columns to be retrieved.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Cols(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_cols(reference, first, last, out result);
                return (new Array(result));
            }

            /**
             * @brief Retrieves a given row of array.
             *
             * @param array KHIVA Array.
             * @param index The row to be retrieved.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Row(int index)
            {
                IntPtr result;
                interop.DLLArray.khiva_row(reference, index, out result);
                return (new Array(result));
            }

            /**
             * @brief Retrieves a subset of rows of array, starting at first and finishing at last, both inclusive.
             *
             * @param array KHIVA Array.
             * @param first Start of the subset of rows to be retrieved.
             * @param last End of the subset of rows to be retrieved.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Rows(int first, int last)
            {
                IntPtr result;
                interop.DLLArray.khiva_rows(reference, first, last, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs a matrix multiplication of lhs and rhs.
             *
             * @param lhs Left-hand side KHIVA array for the operation.
             * @param rhs Right-hand side KHIVA array for the operation.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array Matmul(Array lhs)
            {
                IntPtr result;
                interop.DLLArray.khiva_matmul(reference, lhs.reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Performs a deep copy of array.
             *
             * @param array KHIVA Array.
             * @return result KHIVA Array which contains a copy of array.
             */
            public Array Copy()
            {
                IntPtr result;
                interop.DLLArray.copy(reference, out result);
                return (new Array(result));
            }

            /**
             * @brief Changes the type of array.
             *
             * @param array KHIVA Array.
             * @param type Target type of the output array.
             * @return result KHIVA Array with the result of this operation.
             */
            public Array As(int type)
            {
                IntPtr result;
                interop.DLLArray.khiva_as(reference, type, out result);
                return (new Array(result));
            }


        }
    }
}