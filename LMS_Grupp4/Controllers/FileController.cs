using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        [HttpGet]
        public ActionResult Index()
        {
            List<LMSFile> files = LMSRepo.GetAllFiles().ToList();
            return View(files);
        }

        public ActionResult Upload()
        {
            return View();
        }

        //To-Do: polish this method if we have time
        [HttpPost]
        public ActionResult Upload(string uploaderID = "", bool isPublic = false)
        {
            UserManager<ApplicationUser> userManager = LMSRepo.GetUserManager();
            LMSFile dbFile = new LMSFile();
            ApplicationUser uploader = userManager.FindById(uploaderID);
            string location = "~/Content/Uploads/Shared/";

            //If the file is not public, set it in a user personal folder
            if (!isPublic)
            {
                location = "~/Content/Uploads/" + uploaderID + "/";
                var directoryPath = Path.Combine(Server.MapPath(location), "");//Converts the folder location in absolute path

                //if the folder does not exist, create a new one
                if (!Directory.Exists(directoryPath))
                {
                    //Try if the location is writable
                    try
                    {
                        DirectoryInfo di = Directory.CreateDirectory(directoryPath);
                    }
                   catch(Exception)
                    {
                        //To-Do: Set proper catch block
                        ViewBag.ErrorMessage = "Unable to create folder at this location. Make sure the location is writable.";
                    }
                }
            }

            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    var fileSize = file.ContentLength;
                    var fileFormat = file.ContentType;
                    var fileName = Path.GetFileName(file.FileName);
                    var path = Path.Combine(Server.MapPath(location), fileName);

                    //Verifies if the file has already been uploaded
                    var existingFilesWithSameName = LMSRepo.GetAllFiles().Where(f => f.Name == fileName);
                    var existingFilesWithSameNameCopy = LMSRepo.GetAllFiles().Where(f => f.Name.Replace(" - copy", "") == fileName);
                    if (existingFilesWithSameName != null || existingFilesWithSameNameCopy != null)
                    {
                        var existingFilesWithSameNameAndSize = existingFilesWithSameName.Where(ef => ef.Size == fileSize);
                        if(existingFilesWithSameNameAndSize != null)//Found file with same name and size
                        {
                            var existingFilesWithSameNameAndSizeAndFormat = existingFilesWithSameNameAndSize.Where(ef => ef.Format == fileFormat);
                            if(existingFilesWithSameNameAndSizeAndFormat != null)//Exact same file found
                            {
                                return null;//To-Do: proper handling
                            }
                            else//File with same name and size found but different format: Change name
                            {
                                fileName += " - copy";
                            }
                        }
                        else//If same names but different sizes, Change upload name, add current date
                        {
                            fileName += " - copy";
                        }
                    }

                    //Add file informations in the file object to be saved in the database
                    dbFile.Name = fileName;
                    dbFile.Size = fileSize;
                    dbFile.Uploader = uploader;
                    dbFile.Format = fileFormat;
                    dbFile.IsPublicVisible = isPublic;
                    dbFile.UploadDate = DateTime.Now;
                    dbFile.URL = path;
                    dbFile.Courses = new List<Course>();

                    //Add file to the database
                    LMSRepo.AddFile(dbFile);

                    //Moves the file to the server
                    file.SaveAs(path);
                }
            }

            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Download(int id = 0)
        {
            var file = LMSRepo.GetFileByID(id);
            var fileURL = file.URL;
            string fileName = file.Name;
            if(System.IO.File.Exists(fileURL))
            {
                byte[] fileBytes = System.IO.File.ReadAllBytes(@fileURL);
                return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);
            }
            else
            {
                LMSRepo.DeleteFile(id);
                return RedirectToAction("Index", new { Error = "Broken Link!\nThis file: '" + file.Name + "' does not exist on the server."});
            }
        }
    }
}