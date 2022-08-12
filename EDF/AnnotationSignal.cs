using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace EDFCSharp
{
    public class AnnotationSignal : IEDFBaseSignal<TAL>
    {
        /// <summary>
        /// Provided sample value after scaling.
        /// </summary>
        /// <param name="aIndex"></param>
        /// <returns></returns>
        public TAL ScaledSample(int aIndex) { return Samples[aIndex]; }

        /// <summary>
        /// Provide sample scaling factor.
        /// </summary>
        /// <returns></returns>
        public double ScaleFactor() { return (PhysicalMaximum.Value - PhysicalMinimum.Value) / (DigitalMaximum.Value - DigitalMinimum.Value); }

        public override string ToString()
        {
            return Label.Value + " " + NumberOfSamplesInDataRecord.Value.ToString() + "/" + Samples.Count().ToString() + " ["
                + string.Join<TAL>(",", Samples.Skip(0).Take(10).ToArray()) + " ...]";
        }

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

        public List<TAL> Samples { get; set; } = new List<TAL> { };
        public long SamplesCount => Samples.Count;

        public AnnotationSignal()
        {
            /* /// https://www.edfplus.info/specs/edfplus.html#annotationssignal section 2.2.1
             * For the sake of EDF compatibility, the fields 'digital minimum' and 'digital maximum' must be filled with -32768 and 32767, respectively. 
             * The 'Physical maximum' and 'Physical minimum' fields must contain values that differ from each other. 
             * The other fields of this signal are filled with spaces*/
            Label.Value = EDFConstants.AnnotationLabel;
            DigitalMinimum.Value = -32768;
            DigitalMaximum.Value = 32767;
            PhysicalMinimum.Value = -1;
            PhysicalMaximum.Value = 1;
            PhysicalDimension.Value = string.Empty;
            TransducerType.Value = string.Empty;
            Prefiltering.Value = string.Empty;
            Reserved.Value = string.Empty;
        }
    }

}
