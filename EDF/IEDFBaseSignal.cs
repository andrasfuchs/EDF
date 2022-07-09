using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDFCSharp
{
    public interface IEDFBaseSignal<T>
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
        List<T> Samples { get; set; }
        long SamplesCount { get; }
    }

}
