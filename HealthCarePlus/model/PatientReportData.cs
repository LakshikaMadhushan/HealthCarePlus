using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HealthCarePlus.model
{
    public class PatientReportData
    {
        public string FileName { get; set; }
        public string Date { get; set; }
        public string PatientName { get; set; }
        public string PatientId { get; set; }
        public string Remark { get; set; }
        public string Path { get; set; }
        public byte[] PdfData { get; set; }
    }
}
