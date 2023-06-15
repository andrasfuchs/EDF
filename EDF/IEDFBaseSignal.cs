using System.Collections.Generic;

namespace EDFCSharp
{
    public interface IEDFBaseSignal
    {
        /// <summary>
        /// Index of that signal in the EDF file it belongs to.
        /// </summary>
        int Index { get; set; }
        FixedLengthString Label { get; }
        FixedLengthString TransducerType { get; }
        FixedLengthString PhysicalDimension { get; }
        FixedLengthDouble PhysicalMinimum { get; }
        FixedLengthDouble PhysicalMaximum { get; }
        FixedLengthInt DigitalMinimum { get; }
        FixedLengthInt DigitalMaximum { get; }
        FixedLengthString Prefiltering { get; }
        FixedLengthInt NumberOfSamplesInDataRecord { get; }
        FixedLengthString Reserved { get; }
        long SamplesCount { get; }
    }

    public interface IEDFBaseSignal<T> : IEDFBaseSignal
    {
        List<T> Samples { get; set; }
    }
}
