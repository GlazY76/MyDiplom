using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Word = Microsoft.Office.Interop.Word;
using System.Reflection;
using System.IO;
using VTP.Models;
using VTP.Models.ViewModels;

namespace VTP.BL
{
    public class WordHelper
    {
        private Word.Application application;
        private Word.Document document;
        private string tmpFile;

        //https://stackoverflow.com/questions/13227828/how-to-create-microsoft-office-interop-word-document-object-from-byte-array-wit
        //https://docs.microsoft.com/ru-ru/visualstudio/vsto/how-to-programmatically-create-word-tables?redirectedfrom=MSDN&view=vs-2019

        private Word.Document OpenDocument(Document document)
        {
            Word.Application app = new Word.Application();
            tmpFile = Path.GetTempFileName();
            var tmpFileStream = File.OpenWrite(tmpFile);
            tmpFileStream.Write(document.File, 0, document.File.Length);
            tmpFileStream.Close();

            var doc = app.Documents.Open(tmpFile);
            return doc;
        }
        
        public Document AddTable(Document document, TableProperties properties)
        {
            if (properties.Students.Count() == 0)
            {
                return null;
            }

            this.document = OpenDocument(document);
            Word.Range range;

            if (!string.IsNullOrEmpty(properties.Bookmark)) {
                Word.Bookmark bm = this.document.Bookmarks[properties.Bookmark];
                range = bm.Range;
            }
            else
            {
                range = this.document.Range(0, 0);
            }
             
            this.document.Tables.Add(range, properties.NumRows, properties.NumColumns);
            CreateTable(properties);
            this.document.Save();
            document.File = SaveDoc();
            return document;
        }

        private void CreateTable(TableProperties properties)
        {
            Word.Table tbl = this.document.Tables[1];
            tbl.Range.Font.Name = properties.Font.Name;
            tbl.Range.Font.Size = properties.Font.Size;

            int x = 1;
            //foreach(var student in properties.Students)
            //{
            //    for (int y = 1; y < properties.NumColumns; y++)
            //    {
            //        #region Dates
            //        if (properties.ColumnsProperty.BornDate && properties.ColumnsProperty.Order.BornDate == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.BornDate.ToString("dd-mm-yyyy");
            //        }

            //        if (properties.ColumnsProperty.IsEntryDate && properties.ColumnsProperty.Order.EntryDate == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.EntryDate.ToString("dd-mm-yyyy");
            //        }

            //        if (properties.ColumnsProperty.IsGraduationDate && properties.ColumnsProperty.Order.GraduationDate == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.GraduationDate.ToString("dd-mm-yyyy");
            //        }
            //        #endregion

            //        if (properties.ColumnsProperty.IsName && properties.ColumnsProperty.Order.Name == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.Name;
            //        }

            //        if (properties.ColumnsProperty.IsFamilyStatus && properties.ColumnsProperty.Order.FamilyStatus == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.FamilyStatus;
            //        }

            //        if (properties.ColumnsProperty.IsFatherName && properties.ColumnsProperty.Order.FatherName == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.FatherName;
            //        }

            //        if (properties.ColumnsProperty.IsMotherName && properties.ColumnsProperty.Order.MotherName == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.MotherName;
            //        }

            //        if (properties.ColumnsProperty.IsSchool && properties.ColumnsProperty.Order.School == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.School;
            //        }

            //        if (properties.ColumnsProperty.IsMilitaryStatus && properties.ColumnsProperty.Order.MilitaryStatus == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.MilitaryStatus;
            //        }

            //        if (properties.ColumnsProperty.IsTelNumber && properties.ColumnsProperty.Order.TelNumber == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.TelNumber;
            //        }

            //        if (properties.ColumnsProperty.IsFormOfStudy && properties.ColumnsProperty.Order.FormOfStudy == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.FormOfStudy;
            //        }

            //        if (properties.ColumnsProperty.IsScholarshipStatus && properties.ColumnsProperty.Order.ScholarshipStatus == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.ScholarshipStatus;
            //        }

            //        if (properties.ColumnsProperty.IsGroup && properties.ColumnsProperty.Order.Group == y)
            //        {
            //            tbl.Cell(x, y).Range.Text = student.Group;
            //        }

            //    }
            //    x++;
            //}
        }

        private byte[] SaveDoc()
        {
            using (FileStream fs = new FileStream(tmpFile, FileMode.Open, FileAccess.Read))
            {
                // Create a byte array of file stream length
                byte[] bytes = System.IO.File.ReadAllBytes(tmpFile);
                //Read block of bytes from stream into the byte array
                fs.Read(bytes, 0, System.Convert.ToInt32(fs.Length));
                //Close the File Stream
                fs.Close();
                return bytes; //return the byte data
            }
        }
    }
}
