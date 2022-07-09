namespace EDFCSharp
{
    public class EDFField
    {
        public string Name { get; set; }
        public int AsciiLength { get; set; }

        public EDFField() { }

        public EDFField(string name, int asciiLength)
        {
            Name = name;
            AsciiLength = asciiLength;
        }
    }
}