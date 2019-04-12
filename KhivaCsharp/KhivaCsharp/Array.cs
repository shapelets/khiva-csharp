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
                fixed(T* data = &values[0])
                    return Create<T>(1, new long[] { values.Length, 1, 1, 1 }, doublePrecision, values);
            }

            public unsafe static MyArray2 Create<T>(T[,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0])
                    return Create<T>(2, new long[] { values.GetLength(1), values.GetLength(0), 1, 1 }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(T[,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0,0])
                    return Create<T>(3, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), 1 }, doublePrecision, data);
            }

            public unsafe static MyArray2 Create<T>(T[,,,] values, bool doublePrecision = false) where T : unmanaged
            {
                fixed (T* data = &values[0,0,0,0])
                    return Create<T>(4, new long[] { values.GetLength(1), values.GetLength(0), values.GetLength(2), values.GetLength(3) }, doublePrecision, data);
            }

            /*public unsafe static MyArray2 Create<T>(uint ndims, long[] dims, bool doublePrecision, T* values) where T : unmanaged
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
                        Marshal.StructureToPtr<T>(*values, IntPtr.Add(data, i * Marshal.SizeOf(*values)), true);
                        values++;
                    }
                    void* dataPtr = data.ToPointer();
                    var type = (int)GetDtypeFromT<T>(doublePrecision);
                    interop.DLLArray.create_array(ref dataPtr, ref ndims, dims, out arr.reference, ref type);
                    arr.Reference = arr.reference;
                }
                finally
                {
                    Marshal.FreeHGlobal(data);
                }                
                return arr;
            }*/

                public unsafe static MyArray2 Create<T>(uint ndims, long[] dims, bool doublePrecision, T* values) where T : unmanaged
            {
                MyArray2 arr = new MyArray2();
                long totalLength = (long)(dims[0] * dims[1] * dims[2] * dims[3]);
                /*IntPtr[] data = new IntPtr[totalLength];
                for (int i = 0; i < totalLength; i++)
                {
                    data[i] = new IntPtr(sizeof(T));
                    data[i] = (IntPtr)values;
                    values++;
                }*/
                var type = (int)GetDtypeFromT<T>(doublePrecision);
                IntPtr data = (IntPtr)values;
                interop.DLLArray.create_array(ref data, ref ndims, dims, out arr.reference, ref type);
                arr.Reference = arr.reference;        
                return arr;
            }

            public unsafe static MyArray2 Create<T>(uint ndims, long[] dims, bool doublePrecision, T[] values) where T : unmanaged
            {
                MyArray2 arr = new MyArray2();
                long totalLength = (long)(dims[0] * dims[1] * dims[2] * dims[3]);
                var type = (int)GetDtypeFromT<T>(doublePrecision);
                interop.DLLArray.create_array(values, ref ndims, dims, out arr.reference, ref type);
                arr.Reference = arr.reference;
                return arr;
            }

            public T[] GetData1D<T>()
            {
                T[] data = new T[10];
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
                    interop.DLLArray.get_type(ref reference, out int type);
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
                set
                {
                    reference = value;
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
    }
}