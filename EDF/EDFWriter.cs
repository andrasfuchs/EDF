//#define TRACE_BYTES

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;

namespace EDFCSharp
{
    /// <summary>
    /// EDF+ file writer
    /// </summary>
    public class EDFWriter : BinaryWriter
    {
        public EDFWriter(FileStream fs) : base(fs) { }

        public void WriteEDF(EDFFile edf, string edfFilePath)
        {
            List<IEDFBaseSignal> allSignals = new List<IEDFBaseSignal>();
            allSignals.AddRange(edf.Signals);
            allSignals.AddRange(edf.AnnotationSignals);

            edf.Header.NumberOfSignalsInRecord.Value = allSignals.Count;
            edf.Header.SizeInBytes.Value = CalcNumOfBytesInHeader(edf);

            //----------------- Fixed length header items -----------------
            WriteItem(edf.Header.Version);
            WriteItem(edf.Header.PatientID);
            WriteItem(edf.Header.RecordID);
            WriteItem(edf.Header.RecordingStartDate);
            WriteItem(edf.Header.RecordingStartTime);
            WriteItem(edf.Header.SizeInBytes);
            WriteItem(edf.Header.Reserved);
            WriteItem(edf.Header.NumberOfDataRecords);
            WriteItem(edf.Header.RecordDurationInSeconds);
            WriteItem(edf.Header.NumberOfSignalsInRecord);

            //----------------- Variable length header items -----------------
            IEnumerable<FixedLengthString> headerSignalsLabel = allSignals.Select(s => s.Label);
            WriteItem(headerSignalsLabel);

            IEnumerable<FixedLengthString> trandsducerTypes = allSignals.Select(s => s.TransducerType);
            WriteItem(trandsducerTypes);

            IEnumerable<FixedLengthString> physicalDimensions = allSignals.Select(s => s.PhysicalDimension);
            WriteItem(physicalDimensions);

            IEnumerable<FixedLengthDouble> physicalMinimums = allSignals.Select(s => s.PhysicalMinimum);
            WriteItem(physicalMinimums);

            IEnumerable<FixedLengthDouble> physicalMaximuns = allSignals.Select(s => s.PhysicalMaximum);
            WriteItem(physicalMaximuns);

            IEnumerable<FixedLengthInt> digitalMinimuns = allSignals.Select(s => s.DigitalMinimum);
            WriteItem(digitalMinimuns);

            IEnumerable<FixedLengthInt> digitalMaximuns = allSignals.Select(s => s.DigitalMaximum);
            WriteItem(digitalMaximuns);

            IEnumerable<FixedLengthString> prefilterings = allSignals.Select(s => s.Prefiltering);
            WriteItem(prefilterings);

            IEnumerable<FixedLengthInt> samplesCountPerRecords = allSignals.Select(s => s.NumberOfSamplesInDataRecord);
            WriteItem(samplesCountPerRecords);

            IEnumerable<FixedLengthString> reservedValues = allSignals.Select(s => s.Reserved);
            WriteItem(reservedValues);

            Debug.WriteLine("Writer position after header: " + BaseStream.Position);

            Debug.WriteLine($"Writing {edf.Signals.Length} signal(s).");
            WriteSignals(edf);

            Close();
            Debug.WriteLine("File size: " + new FileInfo(edfFilePath).Length);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int CalcNumOfBytesInHeader(EDFFile edf)
        {
            int totalFixedLength = 256;
            int ns = edf.Signals.Length;
            ns = edf.AnnotationSignals != null ? ns + edf.AnnotationSignals.Count() : ns;
            int totalVariableLength = (ns * 16) + (ns * 80 * 2) + (ns * 8 * 6) + (ns * 32);
            return totalFixedLength + totalVariableLength;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteItem(HeaderItem headerItem)
        {
            string strItem = headerItem.ToAscii();
            strItem ??= "";
            byte[] itemBytes = AsciiToBytes(strItem);
            Write(itemBytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteItem(IEnumerable<HeaderItem> headerItems)
        {
            string joinedItems = StrJoin(headerItems);
            joinedItems ??= "";
            byte[] itemBytes = AsciiToBytes(joinedItems);
            Write(itemBytes);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private string StrJoin(IEnumerable<HeaderItem> list)
        {
            string joinedString = "";

            foreach (HeaderItem item in list)
            {
                joinedString += item.ToAscii();
            }

            return joinedString;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] AsciiToBytes(string strItem)
        {
            return Encoding.ASCII.GetBytes(strItem);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static byte[] AsciiToIntBytes(string strItem, int length)
        {
            string strInt = "";
            string str = strItem[..length];
            double val = Convert.ToDouble(str);
            strInt += val.ToString("0").PadRight(length, ' ');
            return Encoding.ASCII.GetBytes(strInt);
        }

        /// <summary>
        /// Write signals to the EDF+ file
        /// </summary>
        /// <param name="edf"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteSignals(EDFFile edf)
        {
            if (!edf.Signals.Any() && !edf.AnnotationSignals.Any())
            {
                Debug.WriteLine("There are no signals to write");
                return;
            }
            long numberOfRecords = edf.Header.NumberOfDataRecords.Value;

            // Write each record after one another. Each record represents a time period.
            for (int recordIndex = 0; recordIndex < numberOfRecords; recordIndex++)
            {
                // Write each signal segment into the record
                foreach (EDFSignal signal in edf.Signals)
                {
                    // Let's find the start and end position of the signal segment
                    int signalIndexStart = recordIndex * signal.NumberOfSamplesInDataRecord.Value;
                    int signalIndexEnd = Math.Min(signalIndexStart + signal.NumberOfSamplesInDataRecord.Value, signal.Samples.Count);

                    int bytesWritten = 0;
                    for (int signalIndex = signalIndexStart; signalIndex < signalIndexEnd; signalIndex++)
                    {
                        Write(BitConverter.GetBytes(signal.Samples[signalIndex]));
                        bytesWritten += 2;
                    }

                    // If the signal segment is not full, fill the rest of the segment with 0
                    int blockSize = signal.NumberOfSamplesInDataRecord.Value * 2;
                    for (int i = bytesWritten; i < blockSize; i++)
                    {
                        Write((byte)0);
                    }
                }

                // Write each annotation signal segment into the record
                if (edf.AnnotationSignals != null && edf.AnnotationSignals.Any())
                {
                    foreach (AnnotationSignal annotationSignal in edf.AnnotationSignals)
                    {
                        WriteAnnotations(recordIndex, annotationSignal.Samples, annotationSignal.NumberOfSamplesInDataRecord.Value);
                    }
                }

            }
        }

        /// <summary>
        /// If the EDF file has annotations inside it, for each record there has to be an "annotation index"
        /// and an annotation value. If there is not annotation value, the index has to be written anyway.
        /// Given a record index and the whole collection of Time-stamped Annotations it writes the annotation 
        /// index and its value. The rest of bytes are filled by 0 based on the sampleCountPerRecord.
        /// </summary>
        /// <param name="index">Record index, necessary to locate the TAL and to write index</param>
        /// <param name="annotations">List of Time-stamped Annotations</param>
        /// <param name="sampleCountPerRecord"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void WriteAnnotations(int index, List<TAL> annotations, int sampleCountPerRecord)
        {
            int bytesWritten = 0;
            bytesWritten += WriteAnnotationIndex(index);
            if (index < annotations.Count)
            {
                bytesWritten += WriteAnnotation(annotations[index]);
            }

            //Fills block size left with 0
            int blockSize = sampleCountPerRecord * 2;
#if TRACE_BYTES
            Debug.WriteLine($"Total bytes for Annotation index {0} is {bytesWritten}");
#endif
            Debug.Assert(bytesWritten <= blockSize, "Annotation signal too big for NumberOfSamplesInDataRecord");
#if TRACE_BYTES
            Debug.WriteLine($"Filling with {blockSize - bytesWritten} bytes");
#endif
            for (int i = bytesWritten; i < blockSize; i++)
            {
                Write(TAL.byte_0);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int WriteAnnotation(TAL tal)
        {
            byte[] bytesToWrite = TALExtensions.GetBytes(tal);
            Write(bytesToWrite);
            return bytesToWrite.Length;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private int WriteAnnotationIndex(int index)
        {
            byte[] bytesToWrite = TALExtensions.GetBytesForTALIndex(index);
            Write(bytesToWrite);
            return bytesToWrite.Length;
        }
    }
}