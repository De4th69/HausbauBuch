using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HausbauBuch.Classes
{
    public class MimeType
    {
        public string Png => "image/png";

        public string Docx => "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

        public string Doc => "application/word";

        public string Csv => "text/csv";

        public string Txt => "text/plain";

        public string Mp3 => "audio/mpeg";

        public string Pdf => "application/pdf";

        public string Xls => "application/vnd.ms-excel";

        public string Xlsx => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
    }
}
