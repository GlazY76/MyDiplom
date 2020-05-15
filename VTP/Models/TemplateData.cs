using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTP.Models
{
    public class TemplateData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string NameForDoc { get; set; }

        public List<DocumentTemplateData> DocumentTemplateDatas { get; set; }

        public TemplateData()
        {
            DocumentTemplateDatas = new List<DocumentTemplateData>();
        }
    }
}
