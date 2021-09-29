using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Diagnostics;

namespace SharpLib.EuropeanDataFormat
{
    class Writer : BinaryWriter
    {
        public Writer(FileStream fs) : base(fs) { }

        public void WriteEDF(File edf, string edfFilePath)
        {
            edf.Header.SizeInBytes.Value = CalcNumOfBytesInHeader(edf);

            //----------------- Fixed length header items -----------------
            WriteItem(edf.Header.Version);
            WriteItem(edf.Header.PatientID);
            WriteItem(edf.Header.RecordID);
            WriteItem(edf.Header.RecordingStartDate);
            WriteItem(edf.Header.RecordingStartTime);
            WriteItem(edf.Header.SizeInBytes);
            WriteItem(edf.Header.Reserved);
            WriteItem(edf.Header.RecordCount);
            WriteItem(edf.Header.RecordDurationInSeconds);
            WriteItem(edf.Header.SignalCount);

            //----------------- Variable length header items -----------------
            var headerSignalsLabel = edf.Signals.Select(s => s.Label);
            if (edf.AnnotationSignal != null)
                headerSignalsLabel = headerSignalsLabel.Concat(new List<FixedLengthString>() { edf.AnnotationSignal.Label });
            WriteItem(headerSignalsLabel);

            var trandsducerTypes = edf.Signals.Select(s => s.TransducerType);
            if (edf.AnnotationSignal != null)
                trandsducerTypes = trandsducerTypes.Concat(new List<FixedLengthString>() { edf.AnnotationSignal.TransducerType });
            WriteItem(trandsducerTypes);

            var physicalDimensions = edf.Signals.Select(s => s.PhysicalDimension);
            if (edf.AnnotationSignal != null)
                physicalDimensions = physicalDimensions.Concat(new List<FixedLengthString>() { edf.AnnotationSignal.PhysicalDimension });
            WriteItem(physicalDimensions);

            var physicalMinimums = edf.Signals.Select(s => s.PhysicalMinimum);
            if (edf.AnnotationSignal != null)
                physicalMinimums = physicalMinimums.Concat(new List<FixedLengthDouble>() { edf.AnnotationSignal.PhysicalMinimum });
            WriteItem(physicalMinimums);

            var physicalMaximuns = edf.Signals.Select(s => s.PhysicalMaximum);
            if (edf.AnnotationSignal != null)
                physicalMaximuns = physicalMaximuns.Concat(new List<FixedLengthDouble>() { edf.AnnotationSignal.PhysicalMaximum });
            WriteItem(physicalMaximuns);

            var digitalMinimuns = edf.Signals.Select(s => s.DigitalMinimum);
            if (edf.AnnotationSignal != null)
                digitalMinimuns = digitalMinimuns.Concat(new List<FixedLengthInt>() { edf.AnnotationSignal.DigitalMinimum });
            WriteItem(digitalMinimuns);

            var digitalMaximuns = edf.Signals.Select(s => s.DigitalMaximum);
            if (edf.AnnotationSignal != null)
                digitalMaximuns = digitalMaximuns.Concat(new List<FixedLengthInt>() { edf.AnnotationSignal.DigitalMaximum });
            WriteItem(digitalMaximuns);

            var prefilterings = edf.Signals.Select(s => s.Prefiltering);
            if (edf.AnnotationSignal != null)
                prefilterings = prefilterings.Concat(new List<FixedLengthString>() { edf.AnnotationSignal.Prefiltering });
            WriteItem(prefilterings);

            var samplesCountPerRecords = edf.Signals.Select(s => s.SampleCountPerRecord);
            if (edf.AnnotationSignal != null)
                samplesCountPerRecords = samplesCountPerRecords.Concat(new List<FixedLengthInt>() { edf.AnnotationSignal.SampleCountPerRecord });
            WriteItem(samplesCountPerRecords);

            var reservedValues = edf.Signals.Select(s => s.Reserved);
            if (edf.AnnotationSignal != null)
                reservedValues = reservedValues.Concat(new List<FixedLengthString>() { edf.AnnotationSignal.Reserved });
            WriteItem(reservedValues);

            Console.WriteLine("Writer position after header: " + BaseStream.Position);

            Console.WriteLine("Writing signals.");
            WriteSignals(edf);

            Close();
            Console.WriteLine("File size: " + System.IO.File.ReadAllBytes(edfFilePath).Length);
        }



        private int CalcNumOfBytesInHeader(File edf)
        {
            int totalFixedLength = 256;
            int ns = edf.Signals.Length;
            ns = edf.AnnotationSignal != null ? ++ns : ns;
            int totalVariableLength = ns * 16 + (ns * 80) * 2 + (ns * 8) * 6 + (ns * 32);
            return totalFixedLength + totalVariableLength;
        }

        private void WriteItem(HeaderItem headerItem)
        {
            string strItem = headerItem.ToAscii();
            if (strItem == null) strItem = "";
            byte[] itemBytes = AsciiToBytes(strItem);
            this.Write(itemBytes);
            Console.WriteLine(headerItem.Name + " [" + strItem + "] \n\n-- ** BYTES LENGTH: " + itemBytes.Length
                + "> Position after write item: " + this.BaseStream.Position + "\n");
        }

        private void WriteItem(IEnumerable<HeaderItem> headerItems)
        {
            string joinedItems = StrJoin(headerItems);
            if (joinedItems == null) joinedItems = "";
            byte[] itemBytes = AsciiToBytes(joinedItems);
            this.Write(itemBytes);
            Console.WriteLine("[" + joinedItems + "] \n\n-- ** BYTES LENGTH: " + itemBytes.Length
                + ", Position after write item: " + this.BaseStream.Position + "\n");
        }

        private string StrJoin(IEnumerable<HeaderItem> list)
        {
            string joinedString = "";

            foreach (var item in list)
            {
                joinedString += item.ToAscii();
            }

            return joinedString;
        }

        private static byte[] AsciiToBytes(string strItem)
        {
            return Encoding.ASCII.GetBytes(strItem);
        }

        private static byte[] AsciiToIntBytes(string strItem, int length)
        {
            string strInt = "";
            string str = strItem.Substring(0, length);
            double val = Convert.ToDouble(str);
            strInt += val.ToString("0").PadRight(length, ' ');
            return Encoding.ASCII.GetBytes(strInt);
        }

        private void WriteSignals(File edf)
        {
            if (!edf.Signals.Any())
            {
                Console.WriteLine("There are no signals to write");
                return;
            }

            Console.WriteLine("Write position before signal: " + this.BaseStream.Position);

            int numberOfRecords = edf.Header.RecordCount.Value;

            for (int recordIndex = 0; recordIndex < numberOfRecords; recordIndex++)
            {
                foreach (Signal signal in edf.Signals)
                {
                    int signalStartPos = recordIndex * signal.SampleCountPerRecord.Value;
                    int signalEndPos = Math.Min(signalStartPos + signal.SampleCountPerRecord.Value, signal.Samples.Count);
                    for (; signalStartPos < signalEndPos; signalStartPos++)
                        this.Write(BitConverter.GetBytes(signal.Samples[signalStartPos]));
                }
                if (edf.AnnotationSignal != null && edf.AnnotationSignal.Samples.Any())
                    WriteAnnotations(recordIndex, edf.AnnotationSignal.Samples, edf.AnnotationSignal.SampleCountPerRecord.Value);
            }
            Console.WriteLine("Write position after signals: " + this.BaseStream.Position);
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
        private void WriteAnnotations(int index, List<TAL> annotations, int sampleCountPerRecord)
        {
            var bytesWritten = 0;
            bytesWritten += WriteAnnotationIndex(index);
            if (index < annotations.Count)
                bytesWritten += WriteAnnotation(annotations[index]);

            //Fills block size left with 0
            var blockSize = sampleCountPerRecord * 2;
            Console.WriteLine($"Total bytes for Annotation index {0} is {bytesWritten}");
            Debug.Assert(bytesWritten <= blockSize, "Annotation signal too big for SampleCountPerRecord");
            Console.WriteLine($"Filling with {blockSize - bytesWritten} bytes");
            for (int i = bytesWritten; i < blockSize; i++)
                this.Write(TAL.byte_0);
            Console.WriteLine("Write position after filled by 0: " + this.BaseStream.Position);
        }

        private int WriteAnnotation(TAL annotations)
        {
            Console.WriteLine("Write position before annotation: " + this.BaseStream.Position);
            var bytesToWrite = TALExtensions.GetBytes(annotations);
            Console.WriteLine("Bytes to write: " + bytesToWrite.Length);
            this.Write(bytesToWrite);
            Console.WriteLine("Write position after annotation: " + this.BaseStream.Position);
            return bytesToWrite.Length;
        }

        private int WriteAnnotationIndex(int index)
        {
            Console.WriteLine("Write position before annotation index: " + this.BaseStream.Position);
            var bytesToWrite = TALExtensions.GetBytesForTALIndex(index);
            Console.WriteLine("Bytes to write: " + bytesToWrite.Length);
            this.Write(bytesToWrite);
            Console.WriteLine("Write position after annotation index: " + this.BaseStream.Position);
            return bytesToWrite.Length;


        }
    }
}