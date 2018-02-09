using System.IO;

namespace stdlibXtf
{
    /// <summary>
    /// Static class that contain methods to convert an array of bytes in to a System.IO.MemoryStream.
    /// </summary>
    public static class ArrayToStream
    {
        /// <summary>
        /// Convert an array of Bytes in to a stream
        /// </summary>
        /// <param name="byteArray">Bytes to convert</param>
        /// <returns>Stream of the bytes</returns>
        public static MemoryStream BytesToMemory(byte[] byteArray)
        {
            MemoryStream mem = new MemoryStream();
            mem.Write(byteArray, 0, byteArray.Length);
            mem.Seek(0, SeekOrigin.Begin);
            return mem;
        }
    }
}