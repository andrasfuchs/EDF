using System;
using System.Globalization;

namespace EDFCSharp
{
    public class EDFHeader
    {
        /// <summary>
        /// The time at which the first record was obtained.
        /// </summary>
        public DateTime FirstRecordTime;

        public FixedLengthString Version { get; } = new FixedLengthString(HeaderItems.Version);
        public FixedLengthString PatientID { get; } = new FixedLengthString(HeaderItems.PatientID);
        public FixedLengthString RecordID { get; } = new FixedLengthString(HeaderItems.RecordID);
        public FixedLengthString RecordingStartDate { get; } = new FixedLengthString(HeaderItems.RecordingStartDate);
        public FixedLengthString RecordingStartTime { get; } = new FixedLengthString(HeaderItems.RecordingStartTime);
        public FixedLengthInt SizeInBytes { get; } = new FixedLengthInt(HeaderItems.SizeInBytes);
        public FixedLengthString Reserved { get; } = new FixedLengthString(HeaderItems.Reserved);
        public FixedLengthLong NumberOfDataRecords { get; } = new FixedLengthLong(HeaderItems.NumberOfDataRecords);
        public FixedLengthDouble RecordDurationInSeconds { get; } = new FixedLengthDouble(HeaderItems.RecordDurationInSeconds);
        public FixedLengthInt NumberOfSignalsInRecord { get; } = new FixedLengthInt(HeaderItems.NumberOfSignalsInRecord);
        public VariableLengthString Labels { get; } = new VariableLengthString(HeaderItems.Label);
        public VariableLengthString TransducerTypes { get; } = new VariableLengthString(HeaderItems.TransducerType);
        public VariableLengthString PhysicalDimensions { get; } = new VariableLengthString(HeaderItems.PhysicalDimension);
        public VariableLengthDouble PhysicalMinimums { get; } = new VariableLengthDouble(HeaderItems.PhysicalMinimum);
        public VariableLengthDouble PhysicalMaximums { get; } = new VariableLengthDouble(HeaderItems.PhysicalMaximum);
        public VariableLengthInt DigitalMinimums { get; } = new VariableLengthInt(HeaderItems.DigitalMinimum);
        public VariableLengthInt DigitalMaximums { get; } = new VariableLengthInt(HeaderItems.DigitalMaximum);
        public VariableLengthString PreFilterings { get; } = new VariableLengthString(HeaderItems.Prefiltering);
        public VariableLengthInt NumberOfSamplesPerRecord { get; } = new VariableLengthInt(HeaderItems.NumberOfSamplesInDataRecord);
        public VariableLengthString SignalsReserved { get; } = new VariableLengthString(HeaderItems.SignalsReserved);

        public DateTime GetStartTime() => GetDateTime(RecordingStartDate.Value, RecordingStartTime.Value);

        public DateTime GetEndTime() => GetDateTime(RecordingStartDate.Value, RecordingStartTime.Value)
            .AddSeconds(NumberOfDataRecords.Value * RecordDurationInSeconds.Value);
        

        public double TotalDurationInSeconds => NumberOfDataRecords.Value * RecordDurationInSeconds.Value;


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
        /// Parse record start date and time string to obtain a DateTime object.
        /// </summary>
        public void ParseRecordingStartTime()
        {
            string timeString = RecordingStartDate.Value + " " + RecordingStartTime.Value.Replace('.', ':');
            // As days comes before months use German culture explicitly
            FirstRecordTime = DateTime.Parse(timeString, CultureInfo.GetCultureInfo("de-DE"));
        }

        /// <summary>
        /// Provides the time corresponding to the given record index.
        /// </summary>
        /// <param name="aRecordIndex"></param>
        /// <returns></returns>
        public DateTime RecordTime(int aRecordIndex)
        {
            return FirstRecordTime.AddSeconds(aRecordIndex * RecordDurationInSeconds.Value);
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
