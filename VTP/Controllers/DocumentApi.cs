using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using VTP.Data;
using VTP.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace VTP.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    public class DocumentApi : Controller
    {
        ApplicationDbContext db;

        public DocumentApi(ApplicationDbContext _db)
        {
            db = _db;
        }

        [HttpPut]
        [Route("template")]
        public int SaveTemplate()
        {
            var file = HttpContext.Request.Form.Files["template"];

            if (file != null)
            {
                throw new NullReferenceException();
            }

            Document doc = new Document();
            byte[] fileData = null;
            using (var binaryReader = new BinaryReader(file.OpenReadStream()))
            {
                fileData = binaryReader.ReadBytes((int)file.Length);
            }

            doc.AppUserName = User.Identity.Name;
            doc.File = fileData;
            doc.Name = file.FileName;
            doc.CreationTime = DateTime.Now;
            doc.IsTemplate = true;
            doc.DocumentType = doc.Name.Split('.').Last();
            db.Documents.Add(doc);
            db.SaveChanges();
            return doc.Id;
        }

        [HttpPut]
        [Route("")]
        public void SaveDocs()
        {
            var file = HttpContext.Request.Form.Files["doc"];
            if (file != null)
            {
                Document doc = new Document();
                byte[] fileData = null;
                using (var binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    fileData = binaryReader.ReadBytes((int)file.Length);
                }

                doc.AppUserName = User.Identity.Name;
                doc.File = fileData;
                doc.Name = file.FileName;
                doc.CreationTime = DateTime.Now;
                doc.IsTemplate = false;
                doc.DocumentType = doc.Name.Split('.').Last();
                db.Documents.Add(doc);
                db.SaveChanges();
            }
        }

        [HttpDelete]
        public void DeleteDoc([FromBody]int id)
        {
            var doc = db.Documents.FirstOrDefault(d => d.Id == id);
            if (doc != null)
            {
                db.Documents.Remove(doc);
                db.SaveChanges();
            }
        }
    }
}
