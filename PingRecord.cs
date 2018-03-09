using stdlibXtf.Packets;
using stdlibXtf.SubPackets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stdlibXtf
{
    /// <summary>
    /// Define a sonar ping header and data record.
    /// </summary>
    public class PingRecord
    {
        #region Private Properties

        private PingHeader _header;
        private List<PingChannelHeader> _channelHeaders;
        private List<PingChannelData> _channelData;

        #endregion Private Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PingRecord class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of 256 bytes.</param>
        /// <param name="startByte">The first byte of this ping.</param>
        /// <param name="NumberOfSamples">The number of samples that are stored.</param>
        /// <param name="BytesPerSample">The quantity of bytes for each sample stored.</param>
        /// <param name="IsDataUnipolar">The polarity of the data.</param>
        public PingRecord(byte[] byteArray, UInt32 startByte, UInt32 NumberOfSamples, UInt16 BytesPerSample, bool IsDataUnipolar)
        {
            _channelHeaders = new List<PingChannelHeader>();
            _channelData = new List<PingChannelData>();

            var bytesToEnd = byteArray.Length - startByte;
            if (bytesToEnd >= 320)
            {
                // Convert the array in a stream
                using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
                {
                    // Read the PingHeader
                    dp.BaseStream.Seek(startByte, SeekOrigin.Begin);
                    _header = new PingHeader(dp.ReadBytes(256));

                    if (bytesToEnd >= _header.NumberBytesThisRecord)
                    {
                        for (int c = 0; c < _header.NumberChannelsToFollow; c++)
                        {
                            // Read the PingChannelHeader
                            _channelHeaders.Add(new PingChannelHeader(dp.ReadBytes(64)));

                            // Extract the bytes that contain the data
                            long oft = startByte + ((NumberOfSamples * BytesPerSample) * c) + ((c + 1) * 64);
                            byte[] chnData = byteArray.SubArray(oft, NumberOfSamples * BytesPerSample);

                            for (int d = 0; d < NumberOfSamples; d++)
                            {
                                chnData[d] = byteArray[d + oft];
                            }
                            // Read the PingChannelData
                            _channelData.Add(new PingChannelData(chnData, NumberOfSamples, BytesPerSample, IsDataUnipolar));
                            // Update the position in the stream
                            dp.ReadBytes((int)NumberOfSamples * BytesPerSample);
                        }
                    }
                }
            }
        }

        #endregion Constructors
    }
}