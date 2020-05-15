using System;
using System.Collections.Generic;

namespace VTP.Models
{
    public class Document
    {
        public int Id { get; set; }
        public byte[] File { get; set; }
        public string Name { get; set; }
        public DateTime CreationTime { get; set; }
        public string AppUserName { get; set; }
        public bool IsTemplate { get; set; }
        public string DocumentType { get; set; }

        public List<DocumentTemplateData> DocumentTemplateDatas { get; set; }

        public Document()
        {
            DocumentTemplateDatas = new List<DocumentTemplateData>();
        }
    }
}
