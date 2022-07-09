using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace EDFCSharp
{
    public class EDFFile : IDisposable
    {
        public EDFHeader Header { get; set; }
        public EDFSignal[] Signals { get; set; }
        public IList<AnnotationSignal> AnnotationSignals { get; set; }

        private Reader iReader;

        public EDFFile()
        {
            AnnotationSignals = new List<AnnotationSignal>();
        }
        public EDFFile(string edfFilePath)
        {
            ReadAll(edfFilePath);
        }

        public EDFFile(byte[] edfBytes)
        {
            ReadAll(edfBytes);
        }


        public override string ToString()
        {
            return $@"Header: {Header}";
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
            if (iReader != null)
            {
                iReader.Dispose();
                iReader = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="edfBase64"></param>
        public void ReadBase64(string edfBase64)
        {
            byte[] edfBytes = Convert.FromBase64String(edfBase64);
            ReadAll(edfBytes);
        }

        /// <summary>
        /// Open the given EDF file, read its header and allocate corresponding Signal objects.
        /// </summary>
        /// <param name="edfFilePath"></param>
        public void Open(string edfFilePath)
        {
            // Open file
            iReader = new Reader(File.Open(edfFilePath, FileMode.Open, FileAccess.Read, FileShare.Read));
            // Read headers
            Header = iReader.ReadHeader();
            // Allocate signals
            Signals = iReader.AllocateSignals(Header);
        }

        /// <summary>
        /// Read the signal at the given index.
        /// </summary>
        /// <param name="aIndex"></param>
        public void ReadSignal(int aIndex)
        {
            iReader.ReadSignal(Header, Signals[aIndex]);
        }

        /// <summary>
        /// Read the signal matching the given name.
        /// </summary>
        /// <param name="aContains"></param>
        /// <returns></returns>
        public EDFSignal ReadSignal(string aMatch)
        {
            var signal = Signals.FirstOrDefault(s => s.Label.Value.Equals(aMatch));
            if (signal == null)
            {
                return null;
            }

            iReader.ReadSignal(Header, signal);
            return signal;
        }

        /// <summary>
        /// Read the whole file into memory
        /// </summary>
        /// <param name="edfFilePath"></param>
        public void ReadAll(string edfFilePath)
        {
            using (var reader = new Reader(File.Open(edfFilePath, FileMode.Open, FileAccess.Read, FileShare.Read)))
            {
                Header = reader.ReadHeader();
                Signals = reader.ReadSignals(Header);
                //AnnotationSignals = reader.ReadAnnotationSignals(Header);
            }
        }

        /// <summary>
        /// Read a whole EDF file from a memory buffer. 
        /// </summary>
        /// <param name="edfBytes"></param>
        public void ReadAll(byte[] edfBytes)
        {
            using (var r = new Reader(edfBytes))
            {
                Header = r.ReadHeader();
                Signals = r.ReadSignals(Header);
            }
        }

        public void Save(string edfFilePath)
        {
            if (Header == null) return;

            using (var writer = new EDFWriter(File.Open(edfFilePath, FileMode.Create)))
            {
                writer.WriteEDF(this, edfFilePath);
            }
        }
    }
}
