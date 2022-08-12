using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EDFCSharp
{
    /// <summary>
    /// Represents a Time-stamped Annotation (TAL)
    /// </summary>
    public class TAL
    {
        private const string StringDoubleFormat = "0.###";
        //Standard TAL separators
        public static readonly byte byte_21 = BitConverter.GetBytes(21)[0];
        public static readonly byte byte_20 = BitConverter.GetBytes(20)[0];
        public static readonly byte byte_0 = BitConverter.GetBytes(0)[0];
        public static readonly byte byte_46 = BitConverter.GetBytes(46)[0];


        private double startSeconds;
        private double durationSeconds;
        public string StartSecondsString => startSeconds < 0 ?
            $"-{startSeconds.ToString(StringDoubleFormat, CultureInfo.InvariantCulture)}" :
            $"+{startSeconds.ToString(StringDoubleFormat, CultureInfo.InvariantCulture)}";
        public string DurationSecondsString => durationSeconds >= 0 ? durationSeconds.ToString(StringDoubleFormat, CultureInfo.InvariantCulture) : null;
        public string AnnotationDescription { get; private set; }

        public TAL(double startSeconds, double durationSeconds, string description)
        {
            this.startSeconds = startSeconds;
            this.durationSeconds = durationSeconds;
            AnnotationDescription = description;
        }

    }



    public static class TALExtensions
    {
        /// <summary>
        /// Returns a byte array witch represent a TAL format according to
        /// https://www.edfplus.info/specs/edfplus.html#annotationssignal section 2.2.2.
        /// </summary>
        /// <param name="tal"></param>
        /// <returns></returns>
        public static byte[] GetBytes(TAL tal)
        {
            List<byte> result = new List<byte>();
            result.AddRange(Encoding.ASCII.GetBytes(tal.StartSecondsString));
            if (tal.DurationSecondsString != null)
            {
                result.Add(TAL.byte_21); //15 in HEX
                result.AddRange(Encoding.ASCII.GetBytes(tal.DurationSecondsString));
            }
            result.Add(TAL.byte_20);
            result.AddRange(Encoding.ASCII.GetBytes(tal.AnnotationDescription));
            result.Add(TAL.byte_20);
            result.Add(TAL.byte_0);
            return result.ToArray();
        }

        public static List<TAL> BytesToTALs(byte[] raw, DateTime start)
        {
            List<TAL> result = new List<TAL>();
            String outlet = "";
            bool inside = false;
            int duration = 0;
            for (int i = 0; i < raw.Length; ++i)
            {
                if (inside)
                {
                    if (raw[i] == TAL.byte_0)
                    {
                        inside = false;
                        outlet += Environment.NewLine;
                    }
                    else
                    {
                        byte it = raw[i];
                        bool fact = (it == TAL.byte_20) || (it == TAL.byte_21);
                        raw[i] = (fact) ? (byte)' ' : it;
                        outlet += (char)raw[i];
                    }
                }
                else if (raw[i] == '+' || raw[i] == '-')
                {
                    inside = true;
                    outlet += (char)raw[i];
                }
            }

            //return outlet;
            string[] annotations = outlet.Split(new String[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string annotation in annotations)
            {

            }
            return result;
        }
        public static byte[] GetBytesForTALIndex(int index)
        {
            List<byte> result = new List<byte>();
            var leftSide = index / 10;
            var rightSide = index % 10;
            result.AddRange(Encoding.ASCII.GetBytes($"+{leftSide}"));
            result.Add(TAL.byte_46);
            result.AddRange(Encoding.ASCII.GetBytes($"{rightSide}"));
            result.Add(TAL.byte_20);
            result.Add(TAL.byte_20);
            result.Add(TAL.byte_0);
            return result.ToArray();

        }
    }
}
