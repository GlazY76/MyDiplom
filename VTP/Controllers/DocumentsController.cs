using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VTP.Data;
using VTP.BL;
using View = VTP.Models.ViewModels;
using VTP.Models.ViewModels;

namespace VTP.WEB.Controllers
{
    [Authorize]
    public class DocumentsController : Controller
    {
        ApplicationDbContext db;
        WordHelper word;

        public DocumentsController(ApplicationDbContext _db)
        {
            db = _db;
            word = new WordHelper();
        }

        public IActionResult MyDocuments()
        {
            return View();
        }

        public IActionResult GetAll()
        {
            var docs = db.Documents.Where(x => !x.IsTemplate).ToList();
            return PartialView("PartialView/_GetAll", docs);
        }

        public ActionResult ListOfTemplate()
        {
            return View();
        }

        public ActionResult CreateTemplate()
        {
            return View();
        }      

        public IActionResult GetFile(int id)
        {
            var doc = db.Documents.FirstOrDefault(d => d.Id == id);
            return File(doc.File, "application/force-download", doc.Name);
        }

        [HttpGet]
        public IActionResult ChooseStudents()
        {
            var students = db.Students.ToList();
            return PartialView("PartialView/_ChooseStudents", students);
        }

        [HttpPost]
        public IActionResult CreateTemplate([FromBody]View.TemplateData template)
        {
            var tableProperties = CreateTableProperties(template);
            var doc = db.Documents.FirstOrDefault(d => d.Id == template.DocumentId && d.IsTemplate);
            var document = word.AddTable(doc, tableProperties);

            if (template.IsSave)
            {
                db.Documents.Add(document);
            }

            return File(document.File, "application/force-download", document.Name);
        }

        private TableProperties CreateTableProperties(View.TemplateData data)
        {
            TableProperties tableProperties = new TableProperties();

            tableProperties.NumColumns = data.Colums.Count();

            foreach (var colum in data.Colums) {
                var property = new TableProperties.StudentProperty();
                switch (colum.ColumName)
                {
                    case "Name":
                        property.Name = true;
                        property.Order = colum.Order;
                        break;
                    case "Group":
                        property.Group = true;
                        property.Order = colum.Order;
                        break;
                    case "FormOfStudy":
                        property.FormOfStudy = true;
                        property.Order = colum.Order;
                        break;
                    case "ScholarshipStatus":
                        property.ScholarshipStatus = true;
                        property.Order = colum.Order;
                        break;
                    case "FamilyStatus":
                        property.FamilyStatus = true;
                        property.Order = colum.Order;
                        break;
                    case "MilitaryStatus":
                        property.MilitaryStatus = true;
                        property.Order = colum.Order;
                        break;
                    case "School":
                        property.School = true;
                        property.Order = colum.Order;
                        break;
                    case "FatherName":
                        property.FatherName = true;
                        property.Order = colum.Order;
                        break;
                    case "MotherName":
                        property.MotherName = true;
                        property.Order = colum.Order;
                        break;
                    case "TelNumber":
                        property.TelNumber = true;
                        property.Order = colum.Order;
                        break;
                    case "BornDate":
                        property.BornDate = true;
                        property.Order = colum.Order;
                        break;
                    case "EntryDate":
                        property.EntryDate = true;
                        property.Order = colum.Order;
                        break;
                    case "GraduationDate":
                        property.GraduationDate = true;
                        property.Order = colum.Order;
                        break;
                }
                tableProperties.ColumnsProperty.Add(property);
            }

            foreach (var id in data.StudentIds)
            {
                var student = db.Students.FirstOrDefault(s => s.Id == id) ?? throw new NullReferenceException(nameof(id));
                tableProperties.Students.Add(student);
            }

            tableProperties.NumRows = tableProperties.Students.Count();

            return tableProperties;
        }
    }
}