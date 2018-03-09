using System;
using System.Collections.Generic;
using System.Text;

namespace stdlibXtf
{
    /// <summary>
    /// A collection of methods for the arrays
    /// </summary>
    public static class ArrayExtensions
    {
        /// <summary>
        /// Extract an array from this array
        /// </summary>
        /// <param name="values"></param>
        /// <param name="startIndex">The start index</param>
        /// <param name="length">The number of elements to extract</param>
        /// <returns></returns>
        public static byte[] SubArray(this byte[] values, long startIndex, long length)
        {
            if (values == null) { throw new ArgumentNullException(); }

            // Prepare the sub array
            byte[] sub = new byte[length];

            // Make some checks
            if (values.LongLength >= (startIndex + length))
            {
                // copy the data from this array to the sub array
                for (int d = 0; d < length; d++)
                {
                    sub[d] = values[d + startIndex];
                }
                // return the sub array
                return sub;
            }
            else
            {
                if (values.LongLength > startIndex) // Extract to the end of array
                {
                    // copy the data from this array to the sub array
                    for (int d = 0; d < (values.LongLength - startIndex); d++)
                    {
                        sub[d] = values[d + startIndex];
                    }

                    // return the sub array
                    return sub;
                }
                else
                { throw new IndexOutOfRangeException(); }
            }
        }
    }
}