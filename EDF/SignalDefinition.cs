namespace EDF
{

    public class SignalDefinition
    {
        public VariableLengthString Labels { get; } = new VariableLengthString(HeaderItems.Label);
        public VariableLengthString TransducerTypes { get; } = new VariableLengthString(HeaderItems.TransducerType);
        public VariableLengthString PhysicalDimensions { get; } = new VariableLengthString(HeaderItems.PhysicalDimension);
        public VariableLengthDouble PhysicalMinimums { get; } = new VariableLengthDouble(HeaderItems.PhysicalMinimum);
        public VariableLengthDouble PhysicalMaximums { get; } = new VariableLengthDouble(HeaderItems.PhysicalMaximum);
        public VariableLengthInt DigitalMinimums { get; } = new VariableLengthInt(HeaderItems.DigitalMinimum);
        public VariableLengthInt DigitalMaximums { get; } = new VariableLengthInt(HeaderItems.DigitalMaximum);
        public VariableLengthString PreFilterings { get; } = new VariableLengthString(HeaderItems.Prefiltering);
        public VariableLengthInt SampleCountPerRecords { get; } = new VariableLengthInt(HeaderItems.NumberOfSamplesInDataRecord);
        public VariableLengthString Reserveds { get; } = new VariableLengthString(HeaderItems.SignalsReserved);
    }
}