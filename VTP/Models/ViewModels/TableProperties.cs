using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTP.Models.ViewModels
{
    public class TableProperties
    {
        public TableProperties()
        {
            Font = new FontProperty() {
                Size = 14,
                Name = "Times New Roman"
            };
            Bookmark = "Bookmark";
        }

        public List<Student> Students { get; set; }

        public string Bookmark { get; set; }

        public int NumRows { get; set; }

        public int NumColumns { get; set; }

        public FontProperty Font { get; set; }

        public List<StudentProperty> ColumnsProperty { get; set; }

        public class StudentProperty
        {
            public StudentProperty()
            {
                Order = null;
                Name = false;
                BornDate = false;
                EntryDate = false;
                FamilyStatus = false;
                ScholarshipStatus = false;
                School = false;
                FatherName = false;
                FormOfStudy = false;
                GraduationDate = false;
                Group = false;
                MilitaryStatus = false;
                MotherName = false;
                TelNumber = false;
            }

            public bool Name { get; set; }
            public bool BornDate { get; set; }
            public bool FamilyStatus { get; set; }
            public bool EntryDate { get; set; }
            public bool GraduationDate { get; set; }
            public bool FatherName { get; set; }
            public bool MotherName { get; set; }
            public bool School { get; set; }
            public bool MilitaryStatus { get; set; }
            public bool TelNumber { get; set; }
            public bool FormOfStudy { get; set; }
            public bool ScholarshipStatus { get; set; }
            public bool Group { get; set; }

            public int? Order { get; set; }
        }

        public class FontProperty
        {
            public int Size { get; set; }

            public string Name { get; set; }
        }
    }
}
