using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace dimensionality
    {
        public class Dimensionality
        {
            public static array.Array PAA(array.Array arr, int bins)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLDimensionality.paa(ref reference, ref bins, ref result);
                return (new array.Array(result));
            }

            public static array.Array PIP(array.Array arr, int numberIps)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLDimensionality.pip(ref reference, ref numberIps, ref result);
                return (new array.Array(result));
            }

            public static array.Array PLABottomUp(array.Array arr, float maxError)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLDimensionality.pla_bottom_up(ref reference, ref maxError, ref result);
                return (new array.Array(result));
            }

            public static array.Array PLASlidingWindow(array.Array arr, float maxError)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLDimensionality.pla_sliding_window(ref reference, ref maxError, ref result);
                return (new array.Array(result));
            }

            public static array.Array RamerDouglasPeucker(array.Array points, double epsilon)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = points.GetReference();
                interop.DLLDimensionality.ramer_douglas_peucker(ref reference, ref epsilon, ref result);
                return (new array.Array(result));
            }

            public static array.Array SAX(array.Array arr, int alphabetSize)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLDimensionality.sax(ref reference, ref alphabetSize, ref result);
                return (new array.Array(result));
            }

            public static array.Array Visvalingam(array.Array points, int numPoints)
            {
                IntPtr result = new IntPtr();
                IntPtr reference = points.GetReference();
                interop.DLLDimensionality.visvalingam(ref reference, ref numPoints, ref result);
                return (new array.Array(result));
            }
        }
    } 
}
