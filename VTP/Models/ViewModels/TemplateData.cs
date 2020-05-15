using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTP.Models.ViewModels
{
    public class TemplateData
    {
        public List<int> StudentIds { get; set; }

        public List<Colum> Colums { get; set; }

        public bool IsSave { get; set; }

        public int DocumentId { get; set; }

        public class Colum
        {
            public string ColumName { get; set; }

            public int? Order { get; set; }
        }
    }
}
