using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTP.Models
{
    public class DocumentTemplateData
    {
        public int TemplateDataId { get; set; }
        public TemplateData TemplateData { get; set; }

        public int DocumentId { get; set; }
        public Document Document { get; set; }
    }
}
