using System;
using System.Globalization;
using System.Reflection.Emit;

namespace EDFCSharp
{
    public class EDFHeader
    {
        public DateTime StartTime { get; }
        public DateTime EndTime { get; }
        public FixedLengthString Version { get; }
        public FixedLengthString PatientID { get; }
        public FixedLengthString RecordID { get; }
        public FixedLengthString RecordingStartDate { get; }
        public FixedLengthString RecordingStartTime { get; }
        public FixedLengthInt SizeInBytes { get; }
        public FixedLengthString Reserved { get; }
        public FixedLengthLong NumberOfDataRecords { get; }
        public FixedLengthDouble RecordDurationInSeconds { get; }
        public FixedLengthInt NumberOfSignalsInRecord { get; }
        public VariableLengthString Labels { get; }
        public VariableLengthString TransducerTypes { get; }
        public VariableLengthString PhysicalDimensions { get; }
        public VariableLengthDouble PhysicalMinimums { get; }
        public VariableLengthDouble PhysicalMaximums { get; }
        public VariableLengthInt DigitalMinimums { get; }
        public VariableLengthInt DigitalMaximums { get; }
        public VariableLengthString PreFilterings { get; }
        public VariableLengthInt NumberOfSamplesPerRecord { get; }
        public VariableLengthString SignalsReserved { get; }
        public double TotalDurationInSeconds => NumberOfDataRecords.Value * RecordDurationInSeconds.Value;

        public EDFHeader()
        {
            Version = new FixedLengthString(HeaderItems.Version);
            PatientID = new FixedLengthString(HeaderItems.PatientID);
            RecordID = new FixedLengthString(HeaderItems.RecordID);
            RecordingStartDate = new FixedLengthString(HeaderItems.RecordingStartDate);
            RecordingStartTime = new FixedLengthString(HeaderItems.RecordingStartTime);
            SizeInBytes = new FixedLengthInt(HeaderItems.SizeInBytes);
            Reserved = new FixedLengthString(HeaderItems.Reserved);
            NumberOfDataRecords = new FixedLengthLong(HeaderItems.NumberOfDataRecords);
            RecordDurationInSeconds = new FixedLengthDouble(HeaderItems.RecordDurationInSeconds);
            NumberOfSignalsInRecord = new FixedLengthInt(HeaderItems.NumberOfSignalsInRecord);
            Labels = new VariableLengthString(HeaderItems.Label);
            TransducerTypes = new VariableLengthString(HeaderItems.TransducerType);
            PhysicalDimensions = new VariableLengthString(HeaderItems.PhysicalDimension);
            PhysicalMinimums = new VariableLengthDouble(HeaderItems.PhysicalMinimum);
            PhysicalMaximums = new VariableLengthDouble(HeaderItems.PhysicalMaximum);
            DigitalMinimums = new VariableLengthInt(HeaderItems.DigitalMinimum);
            DigitalMaximums = new VariableLengthInt(HeaderItems.DigitalMaximum);
            PreFilterings = new VariableLengthString(HeaderItems.Prefiltering);
            NumberOfSamplesPerRecord = new VariableLengthInt(HeaderItems.NumberOfSamplesInDataRecord);
            SignalsReserved = new VariableLengthString(HeaderItems.SignalsReserved);
        }
        public EDFHeader(string version, string patientId, string recordId, string recordingStartDate, string recordingStartTime,
            short sizeInBytes, string reserved, long numberOfDataRecords, double recordDurationInSeconds,
            short numberOfSignalsInRecord, string[] labels, string[] transducerTypes, string[] physicalDimensions,
            double[] physicalMinimums, double[] physicalMaximums, int[] digitalMinimums, int[] digitalMaximums,
            string[] preFilterings, int[] numberOfSamplesPerRecord, string[] signalsReserved):this()
        {
            Version.Value = version;
            PatientID.Value = patientId;
            RecordID . Value = recordId;
            RecordingStartDate.Value = recordingStartDate;
            RecordingStartTime.Value = recordingStartTime;
            SizeInBytes.Value = sizeInBytes;
            Reserved.Value = reserved;
            NumberOfDataRecords.Value = numberOfDataRecords;
            RecordDurationInSeconds.Value = recordDurationInSeconds;
            NumberOfSignalsInRecord .Value = numberOfSignalsInRecord;
            Labels. Value = labels;
            TransducerTypes .Value = transducerTypes;
            PhysicalDimensions .Value = physicalDimensions;
            PhysicalMinimums .Value = physicalMinimums;
            PhysicalMaximums.Value = physicalMaximums;
            DigitalMinimums . Value = digitalMinimums;
            DigitalMaximums .Value = digitalMaximums;
            PreFilterings .Value = preFilterings;
            NumberOfSamplesPerRecord . Value = numberOfSamplesPerRecord;
            SignalsReserved .Value = signalsReserved;
            StartTime = GetDateTime(RecordingStartDate.Value, RecordingStartTime.Value);
            EndTime = StartTime.AddSeconds(TotalDurationInSeconds);
        }

        private DateTime GetDateTime(string datePart, string timePart)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime date = DateTime.MinValue;
            try
            {
                date = DateTime.ParseExact(datePart, "dd.MM.yy", provider);
            }
            catch (FormatException)
            {
                //do nothing
            }
            DateTime time = DateTime.MinValue;
            try
            {
                time = DateTime.ParseExact(timePart, "HH.mm.ss", provider);
            }
            catch (FormatException)
            {
                //do nothing
            }
            DateTime combined = date.Date.Add(time.TimeOfDay);
            return combined;
        }


        /// <summary>
        /// Provides the time corresponding to the given record index.
        /// </summary>
        /// <param name="aRecordIndex"></param>
        /// <returns></returns>
        private DateTime RecordTime(int aRecordIndex)
        {
            return StartTime.AddSeconds(aRecordIndex * RecordDurationInSeconds.Value);
        }


        /// <summary>
        /// Provides the time corresponding to the given signal sample with millisecond precision.
        /// </summary>
        /// <param name="signal"></param>
        /// <param name="sampleIndex"></param>
        /// <returns></returns>
        public DateTime SampleTime(EDFSignal signal, int sampleIndex)
        {
            int recordIndex = sampleIndex / signal.NumberOfSamplesInDataRecord.Value;
            int modulo = sampleIndex % signal.NumberOfSamplesInDataRecord.Value;
            DateTime recordTime = RecordTime(recordIndex);
            // That will only give us milliseconds precision
            DateTime sampleTime = recordTime.AddMilliseconds(RecordDurationInSeconds.Value * 1000 * modulo / signal.NumberOfSamplesInDataRecord.Value);
            return sampleTime;
        }



        /// <summary>
        /// Useful for debug and inspection.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            string strOutput = "";

            strOutput += "\n---------- Header ---------\n";
            strOutput += "8b\tVersion [" + Version.Value + "]\n";
            strOutput += "80b\tPatient ID [" + PatientID.Value + "]\n";
            strOutput += "80b\tRecording ID [" + RecordID.Value + "]\n";
            strOutput += "8b\tRecording start date [" + RecordingStartDate.Value + "]\n";
            strOutput += "8b\tRecording start time [" + RecordingStartTime.Value + "]\n";
            strOutput += "8b\tHeader size (bytes) [" + SizeInBytes.Value + "]\n";
            strOutput += "44b\tReserved [" + Reserved.Value + "]\n";
            strOutput += "8b\tRecord count [" + NumberOfDataRecords.Value + "]\n";
            strOutput += "8b\tRecord duration in seconds [" + RecordDurationInSeconds.Value + "]\n";
            strOutput += "4b\tSignal count [" + NumberOfSignalsInRecord.Value + "]\n\n";
            //strOutput += "First record time: " + FirstRecordTime + "\n\n";

            // For each signal
            for (int i = 0; i < NumberOfSignalsInRecord.Value; i++)
            {
                strOutput += "\tSignal " + i + ": " + Labels.Value[i] + "\n\n";
                //strOutput += "\tLabel [" + Signals.Labels.Value[i] + "]\n";
                strOutput += "\t\tTransducer type [" + TransducerTypes.Value[i] + "]\n";
                strOutput += "\t\tPhysical dimension [" + PhysicalDimensions.Value[i] + "]\n";
                strOutput += "\t\tPhysical minimum [" + PhysicalMinimums.Value[i] + "]\n";
                strOutput += "\t\tPhysical maximum [" + PhysicalMaximums.Value[i] + "]\n";
                strOutput += "\t\tDigital minimum [" + DigitalMinimums.Value[i] + "]\n";
                strOutput += "\t\tDigital maximum [" + DigitalMaximums.Value[i] + "]\n";
                strOutput += "\t\tPrefiltering [" + PreFilterings.Value[i] + "]\n";
                strOutput += "\t\tSample count per record [" + NumberOfSamplesPerRecord.Value[i] + "]\n";
                strOutput += "\t\tSignals reserved [" + SignalsReserved.Value[i] + "]\n\n";
            }

            strOutput += "\n-----------------------------------\n";

            //Console.WriteLine();

            return strOutput;
        }
    }
}
