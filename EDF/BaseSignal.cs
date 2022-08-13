using System;
using System.Collections.Generic;
using System.Linq;

namespace EDFCSharp
{
    public class EDFSignal : IEDFBaseSignal<short>
    {
        private List<DateTimeOffset> _times;
        public int Index { get; set; }

        public FixedLengthString Label { get; } = new FixedLengthString(HeaderItems.Label);

        public FixedLengthString TransducerType { get; } = new FixedLengthString(HeaderItems.TransducerType);

        public FixedLengthString PhysicalDimension { get; } = new FixedLengthString(HeaderItems.PhysicalDimension);

        public FixedLengthDouble PhysicalMinimum { get; } = new FixedLengthDouble(HeaderItems.PhysicalMinimum);

        public FixedLengthDouble PhysicalMaximum { get; } = new FixedLengthDouble(HeaderItems.PhysicalMaximum);

        public FixedLengthInt DigitalMinimum { get; } = new FixedLengthInt(HeaderItems.DigitalMinimum);

        public FixedLengthInt DigitalMaximum { get; } = new FixedLengthInt(HeaderItems.DigitalMaximum);

        public FixedLengthString Prefiltering { get; } = new FixedLengthString(HeaderItems.Prefiltering);

        public FixedLengthInt NumberOfSamplesInDataRecord { get; } = new FixedLengthInt(HeaderItems.NumberOfSamplesInDataRecord);

        public FixedLengthString Reserved { get; } = new FixedLengthString(HeaderItems.SignalsReserved);
        public double FrequencyInHZ { get; set; }
        public List<short> Samples { get; set; } = new List<short>();
        public List<long> Timestamps { get; set; } = new List<long>();

        public List<DateTimeOffset> Times
        {
            get
            {
                if (_times == null || _times.Count != Samples.Count)
                {
                    _times = Timestamps.Select(DateTimeOffset.FromUnixTimeMilliseconds).ToList();

                }
                return _times;
            }
            set => _times = value;
        }

        public long SamplesCount => Samples.Count;

        public EDFSignal()
        {
            
        }
        public EDFSignal(int index, double frequencyInHz)
        {
            Index = index;
            FrequencyInHZ = frequencyInHz;
        }
        /// <summary>
        /// Provided sample value after scaling.
        /// </summary>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public double ScaledSample(int aIndex) { return Samples[aIndex] * ScaleFactor(); }

        /// <summary>
        /// Provide sample scaling factor.
        /// </summary>
        /// <returns></returns>
        public double ScaleFactor() { return (PhysicalMaximum.Value - PhysicalMinimum.Value) / (DigitalMaximum.Value - DigitalMinimum.Value); }

        public override string ToString()
        {
            return Label.Value + " " + NumberOfSamplesInDataRecord.Value.ToString() + "/" + Samples.Count().ToString() + " ["
                + string.Join(",", Samples.Skip(0).Take(10).ToArray()) + " ...]";
        }

        public void CalculateAllTimeStamps(DateTime startTime,double frequency, long totalSamples)
        {
            
        }
    }


}
