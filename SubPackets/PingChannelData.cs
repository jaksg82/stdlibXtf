using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace stdlibXtf.SubPackets
{
    /// <summary>
    /// Represent the channel data.
    /// </summary>
    public class PingChannelData
    {
        #region Private Properties

        private byte[] _chanData;

        #endregion Private Properties

        #region Public Properties

        /// <summary>
        /// Get the number of samples.
        /// </summary>
        public int Count { get { return _chanData.Length; } }

        #endregion Public Properties

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the PingChannelData class that has default zero values.
        /// </summary>
        public PingChannelData()
        {
            _chanData = new byte[0];
        }

        /// <summary>
        /// Initializes a new instance of the PingChannelData class that contain the values extracted from the given byte array.
        /// </summary>
        /// <param name="byteArray">The size of array need to be at least of NumberOfSamples multiplied by BytesPerSample.</param>
        /// <param name="NumberOfSamples">The number of samples that are stored.</param>
        /// <param name="BytesPerSample">The quantity of bytes for each sample stored.</param>
        /// <param name="IsDataUnipolar">The polarity of the data.</param>
        public PingChannelData(byte[] byteArray, UInt32 NumberOfSamples, UInt16 BytesPerSample, bool IsDataUnipolar)
        {
            _chanData = new byte[NumberOfSamples - 1];
            if (byteArray.Length >= (NumberOfSamples * BytesPerSample))
            {
                using (BinaryReader dp = new BinaryReader(ArrayToStream.BytesToMemory(byteArray)))
                {
                    for (UInt32 s = 0; s < NumberOfSamples; s++)
                    {
                        if (IsDataUnipolar)
                        {
                            if (BytesPerSample == 1)
                            {
                                _chanData[s] = ConvertToByte(dp.ReadByte());
                            }
                            else
                            {
                                if (BytesPerSample == 2)
                                {
                                    _chanData[s] = ConvertToByte(dp.ReadUInt16());
                                }
                                else
                                {
                                    _chanData[s] = ConvertToByte(dp.ReadUInt32());
                                }
                            }
                        }
                        else // Data is Bipolar
                        {
                            if (BytesPerSample == 1)
                            {
                                _chanData[s] = ConvertToByte(dp.ReadSByte());
                            }
                            else
                            {
                                if (BytesPerSample == 2)
                                {
                                    _chanData[s] = ConvertToByte(dp.ReadInt16());
                                }
                                else
                                {
                                    _chanData[s] = ConvertToByte(dp.ReadInt32());
                                }
                            }
                        }
                    }
                }
            }
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        /// Gets an array of byte values that represent the channel data.
        /// </summary>
        /// <returns></returns>
        public byte[] GetData()
        {
            return _chanData;
        }

        #endregion Public Methods

        #region Private Methods

        private byte ConvertToByte(Byte value)
        {
            return value;
        }

        private byte ConvertToByte(UInt16 value)
        {
            return (byte)(value / 256);
        }

        private byte ConvertToByte(UInt32 value)
        {
            return (byte)(value / (256 ^ 3));
        }

        private byte ConvertToByte(SByte value)
        {
            byte shiftValue = (byte)(value - SByte.MinValue);
            return shiftValue;
        }

        private byte ConvertToByte(Int16 value)
        {
            UInt16 shiftValue = (UInt16)(value - Int16.MinValue);
            return (byte)(shiftValue / 256);
        }

        private byte ConvertToByte(Int32 value)
        {
            UInt32 shiftValue = (UInt32)(value - Int32.MinValue);
            return (byte)(shiftValue / (256 ^ 3));
        }

        #endregion Private Methods
    }
}