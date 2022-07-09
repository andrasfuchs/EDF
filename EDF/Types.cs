using System.Globalization;

namespace EDFCSharp
{
    public static class HeaderItems
    {
        //Fixed length header items
        public static EDFField Version { get; } = new EDFField("Version", 8);
        public static EDFField PatientID { get; } = new EDFField("PatientID", 80);
        public static EDFField RecordID { get; private set; } = new EDFField("RecordID", 80);
        public static EDFField RecordingStartDate { get; private set; } = new EDFField("StartDate", 8);
        public static EDFField RecordingStartTime { get; private set; } = new EDFField("StartTime", 8);
        public static EDFField SizeInBytes { get; private set; } = new EDFField("NumberOfBytesInHeader", 8);
        public static EDFField Reserved { get; private set; } = new EDFField("Reserved", 44);
        public static EDFField NumberOfDataRecords { get; private set; } = new EDFField("NumberOfDataRecords", 8);
        public static EDFField RecordDurationInSeconds { get; private set; } = new EDFField("DurationOfDataRecord", 8);
        public static EDFField NumberOfSignalsInRecord { get; private set; } = new EDFField("NumberOfSignals", 4);

        //Variable size signal header items
        public static EDFField Label { get; private set; } = new EDFField("Labels", 16);
        public static EDFField TransducerType { get; private set; } = new EDFField("TransducerType", 80);
        public static EDFField PhysicalDimension { get; private set; } = new EDFField("PhysicalDimension", 8);
        public static EDFField PhysicalMinimum { get; private set; } = new EDFField("PhysicalMinimum", 8);
        public static EDFField PhysicalMaximum { get; private set; } = new EDFField("PhysicalMaximum", 8);
        public static EDFField DigitalMinimum { get; private set; } = new EDFField("DigitalMinimum", 8);
        public static EDFField DigitalMaximum { get; private set; } = new EDFField("DigitalMaximum", 8);
        public static EDFField Prefiltering { get; private set; } = new EDFField("Prefiltering", 80);
        public static EDFField NumberOfSamplesInDataRecord { get; private set; } = new EDFField("NumberOfSamplesInDataRecord", 8);
        public static EDFField SignalsReserved { get; private set; } = new EDFField("SignalsReserved", 32);
    }

    public abstract class HeaderItem
    {
        public HeaderItem(EDFField info)
        {
            Name = info.Name;
            AsciiLength = info.AsciiLength;
        }
        public string Name { get; set; }
        public int AsciiLength { get; set; }
        public abstract string ToAscii();
    }

    public class FixedLengthString : HeaderItem
    {
        public string Value { get; set; }
        public FixedLengthString(EDFField info) : base(info) { }

        public override string ToAscii()
        {
            string asciiString = "";
            if (Value != null)
                asciiString = Value.PadRight(AsciiLength, ' ');
            else
                asciiString = asciiString.PadRight(AsciiLength, ' ');

            return asciiString;
        }
    }
    public class FixedLengthInt : HeaderItem
    {
        public int Value { get; set; }
        public FixedLengthInt(EDFField info) : base(info) { }
        public override string ToAscii() => Value.ToString(CultureInfo.InvariantCulture).PadRight(AsciiLength, ' ');

    }
    public class FixedLengthLong : HeaderItem
    {
        public long Value { get; set; }
        public FixedLengthLong(EDFField info) : base(info) { }
        public override string ToAscii() => Value.ToString(CultureInfo.InvariantCulture).PadRight(AsciiLength, ' ');

    }
    public class FixedLengthDouble : HeaderItem
    {
        public double Value { get; set; }
        public FixedLengthDouble(EDFField info) : base(info) { }

        public override string ToAscii()
        {
            string asciiString = Value.ToString(CultureInfo.InvariantCulture);
            asciiString = asciiString.Length >= AsciiLength
                ? asciiString.Substring(0, AsciiLength)
                : Value.ToString(CultureInfo.InvariantCulture).PadRight(AsciiLength, ' ');
            return asciiString;
        }
    }

    public class VariableLengthString : HeaderItem
    {
        public string[] Value { get; set; }
        public VariableLengthString(EDFField info) : base(info) { }

        public override string ToAscii()
        {
            string ascii = "";
            foreach (var strVal in Value)
            {
                string temp = strVal.PadRight(AsciiLength, ' ');
                ascii += temp;
            }

            return ascii;
        }
    }

    public class VariableLengthInt : HeaderItem
    {
        public int[] Value { get; set; }
        public VariableLengthInt(EDFField info) : base(info) { }

        public override string ToAscii()
        {
            string ascii = "";
            foreach (var intVal in Value)
            {
                string temp = intVal.ToString(CultureInfo.InvariantCulture);
                if (temp.Length > AsciiLength)
                    temp = temp.Substring(0, AsciiLength);
                ascii += temp;
            }
            return ascii;
        }
    }

    public class VariableLengthDouble : HeaderItem
    {
        public double[] Value { get; set; }
        public VariableLengthDouble(EDFField info) : base(info) { }

        public override string ToAscii()
        {
            string ascii = "";
            foreach (var doubleVal in Value)
            {
                string temp = doubleVal.ToString(CultureInfo.InvariantCulture);
                if (temp.Length > AsciiLength)
                    temp = temp.Substring(0, AsciiLength);
                ascii += temp;
            }
            return ascii;
        }
    }
}
