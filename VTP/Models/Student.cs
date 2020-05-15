using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VTP.Models
{
    public class Student
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public DateTime BornDate { get; set; }
        public string FamilyStatus { get; set; }
        public DateTime EntryDate { get; set; }
        public DateTime GraduationDate { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public string School { get; set; }
        public string MilitaryStatus { get; set; }
        public string TelNumber { get; set; }
        public string FormOfStudy { get; set; }
        public string ScholarshipStatus { get; set; }
        public string Group { get; set; }
    }
}
