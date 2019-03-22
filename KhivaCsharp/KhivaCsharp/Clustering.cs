using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace clustering
    {
        public class Clustering
        {
            public Clustering()
            {

            }

            public static Tuple<array.Array, array.Array> KMeans(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr centroids = new IntPtr();
                IntPtr labels = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLClustering.k_means(ref reference, ref k, ref centroids, ref labels, ref tolerance, ref max_iterations);
                return (new Tuple<array.Array, array.Array>(new array.Array(centroids), new array.Array(labels)));
            }

            public static Tuple<array.Array, array.Array> KShape(array.Array arr, int k, float tolerance = 1e-10F, int max_iterations = 100)
            {
                IntPtr centroids = new IntPtr();
                IntPtr labels = new IntPtr();
                IntPtr reference = arr.GetReference();
                interop.DLLClustering.k_shape(ref reference, ref k, ref centroids, ref labels, ref tolerance, ref max_iterations);
                return (new Tuple<array.Array, array.Array>(new array.Array(centroids), new array.Array(labels)));
            }
        }
    }
}
