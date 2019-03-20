using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace khiva
{
    namespace array{
        internal static class Internal
        {
            private static Dictionary<Type, Action<IntPtr, System.Array>>[] GetDataFn = new Dictionary<Type, Action<IntPtr, System.Array>>[4];

            static Internal()
            {
                GetDataFn[0].Add(typeof(int), (ptr, data) => interop.DLLArray.get_data(ref ptr, (int[,,,])data));
            }

        }
    }
}
