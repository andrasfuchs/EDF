using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace EDFCSharp
{
    internal class Reader : BinaryReader
    {
        public Reader(FileStream fs) : base(fs) { }
        public Reader(byte[] edfBytes) : base(new MemoryStream(edfBytes)) { }

        public EDFHeader ReadHeader()
        {


            BaseStream.Seek(0, SeekOrigin.Begin);

            // Fixed size header
            var version = ReadAscii(HeaderItems.Version);
            var patientID = ReadAscii(HeaderItems.PatientID);
            var recordID = ReadAscii(HeaderItems.RecordID);
            var recordingStartDate = ReadAscii(HeaderItems.RecordingStartDate);
            var recordingStartTime = ReadAscii(HeaderItems.RecordingStartTime);
            var sizeInBytes = ReadInt16(HeaderItems.SizeInBytes);
            var reserved = ReadAscii(HeaderItems.Reserved);
            var numberOfDataRecords = ReadLong(HeaderItems.NumberOfDataRecords);
            var recordDurationInSeconds = ReadDouble(HeaderItems.RecordDurationInSeconds);
            var numberOfSignalsInRecord = ReadInt16(HeaderItems.NumberOfSignalsInRecord);

            // Variable size header
            // Contains signal headers
            int ns = numberOfSignalsInRecord;
            var labels = ReadMultipleAscii(HeaderItems.Label, ns);
            var transducerTypes = ReadMultipleAscii(HeaderItems.TransducerType, ns);
            var physicalDimensions = ReadMultipleAscii(HeaderItems.PhysicalDimension, ns);
            var physicalMinimums = ReadMultipleDouble(HeaderItems.PhysicalMinimum, ns);
            var physicalMaximums = ReadMultipleDouble(HeaderItems.PhysicalMaximum, ns);
            var digitalMinimums = ReadMultipleInt(HeaderItems.DigitalMinimum, ns);
            var digitalMaximums = ReadMultipleInt(HeaderItems.DigitalMaximum, ns);
            var preFilterings = ReadMultipleAscii(HeaderItems.Prefiltering, ns);
            var numberOfSamplesPerRecord = ReadMultipleInt(HeaderItems.NumberOfSamplesInDataRecord, ns);
            var signalsReserved = ReadMultipleAscii(HeaderItems.SignalsReserved, ns);

            EDFHeader h = new EDFHeader(version, patientID, recordID, recordingStartDate, recordingStartTime,
                sizeInBytes, reserved, numberOfDataRecords, recordDurationInSeconds, numberOfSignalsInRecord, labels,
                transducerTypes, physicalDimensions, physicalMinimums, physicalMaximums, digitalMinimums, digitalMaximums, preFilterings,
                numberOfSamplesPerRecord, signalsReserved);

            return h;


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <returns></returns>
        internal EDFSignal[] AllocateSignals(EDFHeader header)
        {

            EDFSignal[] signals = new EDFSignal[header.NumberOfSignalsInRecord.Value];
            for (int i = 0; i < signals.Length; i++)
            {
                var numberOfSamplesInRecord = header.NumberOfSamplesPerRecord.Value[i];
                var frequency = numberOfSamplesInRecord / header.RecordDurationInSeconds.Value;
                var totalSamples = numberOfSamplesInRecord * header.NumberOfDataRecords.Value;
                signals[i] = new EDFSignal(i, frequency);
                signals[i].Label.Value = header.Labels.Value[i];
                signals[i].TransducerType.Value = header.TransducerTypes.Value[i];
                signals[i].PhysicalDimension.Value = header.PhysicalDimensions.Value[i];
                signals[i].PhysicalMinimum.Value = header.PhysicalMinimums.Value[i];
                signals[i].PhysicalMaximum.Value = header.PhysicalMaximums.Value[i];
                signals[i].DigitalMinimum.Value = header.DigitalMinimums.Value[i];
                signals[i].DigitalMaximum.Value = header.DigitalMaximums.Value[i];
                signals[i].Prefiltering.Value = header.PreFilterings.Value[i];
                signals[i].Reserved.Value = header.SignalsReserved.Value[i];
                signals[i].NumberOfSamplesInDataRecord.Value = numberOfSamplesInRecord;
                signals[i].CalculateAllTimeStamps(header.StartTime, frequency, totalSamples);
            }

            return signals;
        }

        /// <summary>
        /// Read the requested signal for our file
        /// </summary>
        /// <param name="header"></param>
        /// <param name="signal"></param>
        public void ReadSignal(EDFHeader header, EDFSignal signal)
        {
            var current = ((DateTimeOffset)header.StartTime).ToUnixTimeMilliseconds();
            var interval = 1000 / (int)signal.FrequencyInHZ;

            // Make sure we start just after our header
            BaseStream.Seek(header.SizeInBytes.Value, SeekOrigin.Begin);

            signal.Samples.Clear();
            signal.Timestamps.Clear();
            // For each record
            for (int j = 0; j < header.NumberOfDataRecords.Value; j++)
            {
                // For each signal
                for (int i = 0; i < header.NumberOfSignalsInRecord.Value; i++)
                {
                    // Read that signal samples
                    if (i == signal.Index)
                    {
                        ReadNextSignalSamples(signal.Samples, signal.Timestamps, signal.NumberOfSamplesInDataRecord.Value, current);
                    }
                    else
                    {
                        // Just skip it
                        SkipSignalSamples(header.NumberOfSamplesPerRecord.Value[i]);
                    }
                    current += interval;
                }
            }
        }

        /// <summary>
        /// Read all signal sample value from our file.
        /// </summary>
        /// <returns></returns>
        public EDFSignal[] ReadSignals(EDFHeader header)
        {
            var current = ((DateTimeOffset)header.StartTime).ToUnixTimeMilliseconds();
            EDFSignal[] signals = AllocateSignals(header);
            // For each record
            for (int j = 0; j < header.NumberOfDataRecords.Value; j++)
            {
                var currentPerRecord = (current + j * header.RecordDurationInSeconds.Value * 1000);
                // For each signal
                for (int i = 0; i < signals.Length; i++)
                {
                    var interval = 1000 / signals[i].FrequencyInHZ;

                    // Read that signal samples
                    ReadNextSignalSamples(signals[i].Samples, signals[i].Timestamps, signals[i].NumberOfSamplesInDataRecord.Value, (long)currentPerRecord);
                    currentPerRecord += interval;
                }
            }

            return signals;
        }

        /// <summary>
        /// Read n next samples
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void ReadNextSignalSamples(ICollection<short> samples, List<long> timestamps, int sampleCount, long currentTimestamp)
        {
            // Single file read operation per record
            byte[] intBytes = ReadBytes(sizeof(short) * sampleCount);
            for (int i = 0; i < sampleCount; i++)
            {
                // Fetch our sample short from our record buffer
                short intVal = BitConverter.ToInt16(intBytes, i * sizeof(short));
                samples.Add(intVal);
                timestamps.Add(currentTimestamp);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void SkipSignalSamples(int sampleCount)
        {
            BaseStream.Seek(sampleCount * sizeof(short), SeekOrigin.Current);
        }
        #region Read Types 
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private short ReadInt16(EDFField itemInfo)
        {
            string strInt = ReadAscii(itemInfo).Trim();
            short intResult = -1;
            try { intResult = Convert.ToInt16(strInt); }
            catch (Exception ex) { Console.WriteLine("Error, could not convert string to integer. " + ex.Message); }
            return intResult;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private long ReadLong(EDFField itemInfo)
        {
            string strlong = ReadAscii(itemInfo).Trim();
            if (long.TryParse(strlong, out var result))
            {
                return result;
            }
            return -1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string ReadAscii(EDFField itemInfo)
        {
            byte[] bytes = ReadBytes(itemInfo.AsciiLength);
            return AsciiString(bytes).Trim();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string AsciiString(byte[] bytes)
        {
            return Encoding.ASCII.GetString(bytes);
        }
        #endregion
    }
}
