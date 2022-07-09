using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace EDFCSharp
{
    class Reader : BinaryReader
    {
        public Reader(FileStream fs) : base(fs) { }
        public Reader(byte[] edfBytes) : base(new MemoryStream(edfBytes)) { }

        public EDFHeader ReadHeader()
        {
            EDFHeader h = new EDFHeader();

            BaseStream.Seek(0, SeekOrigin.Begin);

            // Fixed size header
            h.Version.Value = ReadAscii(HeaderItems.Version);
            h.PatientID.Value = ReadAscii(HeaderItems.PatientID);
            h.RecordID.Value = ReadAscii(HeaderItems.RecordID);
            h.RecordingStartDate.Value = ReadAscii(HeaderItems.RecordingStartDate);
            h.RecordingStartTime.Value = ReadAscii(HeaderItems.RecordingStartTime);
            h.SizeInBytes.Value = ReadInt16(HeaderItems.SizeInBytes);
            h.Reserved.Value = ReadAscii(HeaderItems.Reserved);
            h.NumberOfDataRecords.Value = ReadLong(HeaderItems.NumberOfDataRecords);
            h.RecordDurationInSeconds.Value = ReadDouble(HeaderItems.RecordDurationInSeconds);
            h.NumberOfSignalsInRecord.Value = ReadInt16(HeaderItems.NumberOfSignalsInRecord);

            // Variable size header
            // Contains signal headers
            int ns = h.NumberOfSignalsInRecord.Value;
            h.Labels.Value = ReadMultipleAscii(HeaderItems.Label, ns);
            h.TransducerTypes.Value = ReadMultipleAscii(HeaderItems.TransducerType, ns);
            h.PhysicalDimensions.Value = ReadMultipleAscii(HeaderItems.PhysicalDimension, ns);
            h.PhysicalMinimums.Value = ReadMultipleDouble(HeaderItems.PhysicalMinimum, ns);
            h.PhysicalMaximums.Value = ReadMultipleDouble(HeaderItems.PhysicalMaximum, ns);
            h.DigitalMinimums.Value = ReadMultipleInt(HeaderItems.DigitalMinimum, ns);
            h.DigitalMaximums.Value = ReadMultipleInt(HeaderItems.DigitalMaximum, ns);
            h.PreFilterings.Value = ReadMultipleAscii(HeaderItems.Prefiltering, ns);
            h.NumberOfSamplesPerRecord.Value = ReadMultipleInt(HeaderItems.NumberOfSamplesInDataRecord, ns);
            h.SignalsReserved.Value = ReadMultipleAscii(HeaderItems.SignalsReserved, ns);

            h.ParseRecordingStartTime();

            return h;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        public EDFSignal[] AllocateSignals(EDFHeader header)
        {
            EDFSignal[] signals = new EDFSignal[header.NumberOfSignalsInRecord.Value];

            for (int i = 0; i < signals.Length; i++)
            {
                signals[i] = new EDFSignal();
                signals[i].Index = i;
                signals[i].Label.Value = header.Labels.Value[i];
                signals[i].TransducerType.Value = header.TransducerTypes.Value[i];
                signals[i].PhysicalDimension.Value = header.PhysicalDimensions.Value[i];
                signals[i].PhysicalMinimum.Value = header.PhysicalMinimums.Value[i];
                signals[i].PhysicalMaximum.Value = header.PhysicalMaximums.Value[i];
                signals[i].DigitalMinimum.Value = header.DigitalMinimums.Value[i];
                signals[i].DigitalMaximum.Value = header.DigitalMaximums.Value[i];
                signals[i].Prefiltering.Value = header.PreFilterings.Value[i];
                signals[i].Reserved.Value = header.SignalsReserved.Value[i];
                signals[i].NumberOfSamplesInDataRecord.Value = header.NumberOfSamplesPerRecord.Value[i];
                signals[i].FrequencyInHZ = signals[i].NumberOfSamplesInDataRecord.Value / header.RecordDurationInSeconds.Value;
            }

            return signals;
        }

        /// <summary>
        /// Read the requested signal for our file
        /// </summary>
        /// <param name="aEdfHeader"></param>
        /// <param name="aEdfSignal"></param>
        public void ReadSignal(EDFHeader aEdfHeader, EDFSignal aEdfSignal)
        {
            // Make sure we start just after our header
            BaseStream.Seek(aEdfHeader.SizeInBytes.Value, SeekOrigin.Begin);

            aEdfSignal.Samples.Clear();
            // For each record
            for (int j = 0; j < aEdfHeader.NumberOfDataRecords.Value; j++)
            {
                // For each signal
                for (int i = 0; i < aEdfHeader.NumberOfSignalsInRecord.Value; i++)
                {
                    // Read that signal samples
                    if (i == aEdfSignal.Index)
                    {
                        ReadNextSignalSamples(aEdfSignal.Samples, aEdfSignal.NumberOfSamplesInDataRecord.Value);
                    }
                    else
                    {
                        // Just skip it
                        SkipSignalSamples(aEdfHeader.NumberOfSamplesPerRecord.Value[i]);
                    }
                }
            }
        }

        /// <summary>
        /// Read all signal sample value from our file.
        /// </summary>
        /// <returns></returns>
        public EDFSignal[] ReadSignals(EDFHeader header)
        {
            EDFSignal[] signals = AllocateSignals(header);
            // For each record
            for (int j = 0; j < header.NumberOfDataRecords.Value; j++)
            {
                // For each signal
                for (int i = 0; i < signals.Length; i++)
                {
                    // Read that signal samples
                    ReadNextSignalSamples(signals[i].Samples, signals[i].NumberOfSamplesInDataRecord.Value);
                }
            }

            return signals;
        }


        /// <summary>
        /// Read n next samples
        /// </summary>
        /// <param name="aSamples"></param>
        /// <param name="aSampleCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadNextSignalSamples(ICollection<short> aSamples, int aSampleCount)
        {
            // Single file read operation per record
            byte[] intBytes = ReadBytes(sizeof(short) * aSampleCount);
            for (int i = 0; i < aSampleCount; i++)
            {
                // Fetch our sample short from our record buffer
                short intVal = BitConverter.ToInt16(intBytes, i * sizeof(short));
                // TODO: use a static array for better performance? I guess it's not needed since we prealloc using Capacity.
                aSamples.Add(intVal);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aSampleCount"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipSignalSamples(int aSampleCount)
        {
            BaseStream.Seek(aSampleCount * sizeof(short), SeekOrigin.Current);
        }
        #region Read Types   
        private short ReadInt16(EDFField itemInfo)
        {
            string strInt = ReadAscii(itemInfo).Trim();
            short intResult = -1;
            try { intResult = Convert.ToInt16(strInt); }
            catch (Exception ex) { Console.WriteLine("Error, could not convert string to integer. " + ex.Message); }
            return intResult;
        }
        private long ReadLong(EDFField itemInfo)
        {
            string strlong = ReadAscii(itemInfo).Trim();
            if (long.TryParse(strlong, out var result))
            {
                return result;
            }
            return -1;


        }
        private double ReadDouble(EDFField itemInfo)
        {
            string value = ReadAscii(itemInfo).Trim();
            try
            {
                return double.Parse(value, CultureInfo.InvariantCulture);
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Error, could not convert string to integer: " + ex.Message);
                return -1;
            }
        }

        private string ReadAscii(EDFField itemInfo)
        {
            byte[] bytes = ReadBytes(itemInfo.AsciiLength);
            return AsciiString(bytes).Trim();
        }

        private string[] ReadMultipleAscii(EDFField itemInfo, int numberOfParts)
        {
            var parts = new List<string>();

            for (int i = 0; i < numberOfParts; i++)
            {
                byte[] bytes = ReadBytes(itemInfo.AsciiLength);
                parts.Add(AsciiString(bytes).Trim());
            }

            return parts.ToArray();
        }

        private int[] ReadMultipleInt(EDFField itemInfo, int numberOfParts)
        {
            var parts = new List<int>();

            for (int i = 0; i < numberOfParts; i++)
            {
                byte[] bytes = ReadBytes(itemInfo.AsciiLength);
                string ascii = AsciiString(bytes);
                parts.Add(Convert.ToInt32(ascii));
            }

            return parts.ToArray();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="itemInfo"></param>
        /// <param name="numberOfParts"></param>
        /// <returns></returns>
        private double[] ReadMultipleDouble(EDFField itemInfo, int numberOfParts)
        {
            var parts = new List<double>();

            for (int i = 0; i < numberOfParts; i++)
            {
                byte[] bytes = ReadBytes(itemInfo.AsciiLength);
                string ascii = AsciiString(bytes);
                // Use invariant culure as we have a '.' as decimal separator
                parts.Add(double.Parse(ascii, CultureInfo.InvariantCulture));
            }

            return parts.ToArray();
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string AsciiString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
    }
}
