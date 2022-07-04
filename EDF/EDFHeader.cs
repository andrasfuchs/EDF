using System;
using System.Globalization;

namespace EDF
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
        public FixedLengthLong RecordCount { get; } = new FixedLengthLong(HeaderItems.NumberOfDataRecords);
        public FixedLengthDouble RecordDurationInSeconds { get; } = new FixedLengthDouble(HeaderItems.RecordDurationInSeconds);
        public FixedLengthInt SignalCount { get; } = new FixedLengthInt(HeaderItems.SignalCount);
        public SignalDefinition Signals { get; } = new SignalDefinition();


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
        /// <param name="aSignal"></param>
        /// <param name="aSampleIndex"></param>
        /// <returns></returns>
        public DateTime SampleTime(EDF.Signal aSignal, int aSampleIndex)
        {
            int recordIndex = aSampleIndex / aSignal.SampleCountPerRecord.Value;
            int modulo = aSampleIndex % aSignal.SampleCountPerRecord.Value;
            DateTime recordTime = RecordTime(recordIndex);
            // That will only give us milliseconds precision
            DateTime sampleTime = recordTime.AddMilliseconds(RecordDurationInSeconds.Value * 1000 * modulo / aSignal.SampleCountPerRecord.Value);
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
            strOutput += "8b\tRecord count [" + RecordCount.Value + "]\n";
            strOutput += "8b\tRecord duration in seconds [" + RecordDurationInSeconds.Value + "]\n";
            strOutput += "4b\tSignal count [" + SignalCount.Value + "]\n\n";
            //strOutput += "First record time: " + FirstRecordTime + "\n\n";

            // For each signal
            for (int i = 0; i < SignalCount.Value; i++)
            {
                strOutput += "\tSignal " + i + ": " + Signals.Labels.Value[i] + "\n\n";
                //strOutput += "\tLabel [" + Signals.Labels.Value[i] + "]\n";
                strOutput += "\t\tTransducer type [" + Signals.TransducerTypes.Value[i] + "]\n";
                strOutput += "\t\tPhysical dimension [" + Signals.PhysicalDimensions.Value[i] + "]\n";
                strOutput += "\t\tPhysical minimum [" + Signals.PhysicalMinimums.Value[i] + "]\n";
                strOutput += "\t\tPhysical maximum [" + Signals.PhysicalMaximums.Value[i] + "]\n";
                strOutput += "\t\tDigital minimum [" + Signals.DigitalMinimums.Value[i] + "]\n";
                strOutput += "\t\tDigital maximum [" + Signals.DigitalMaximums.Value[i] + "]\n";
                strOutput += "\t\tPrefiltering [" + Signals.PreFilterings.Value[i] + "]\n";
                strOutput += "\t\tSample count per record [" + Signals.SampleCountPerRecords.Value[i] + "]\n";
                strOutput += "\t\tSignals reserved [" + Signals.Reserveds.Value[i] + "]\n\n";
            }

            strOutput += "\n-----------------------------------\n";

            //Console.WriteLine();

            return strOutput;
        }
    }
}
