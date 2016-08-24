using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Repositories;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
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

        public ActionResult Upload(int id = 0)
        {
            var course = LMSRepo.GetCourseByID(id);
            return View(course);
        }

        //To-Do: polish this method if we have time
        [HttpPost]
        public ActionResult Upload(int courseID = 0, bool isPublic = false)
        {
            UserManager<ApplicationUser> userManager = LMSRepo.GetUserManager();
            LMSFile dbFile = new LMSFile();
            ApplicationUser uploader = userManager.FindById(User.Identity.GetUserId());
            string uploaderName = uploader.RealName;
            string courseName = LMSRepo.GetCourseByID(courseID).CourseName;
            string location = "~/Content/Uploads/Shared/";

            //Creates a slugs for the course name and the uploader name
            courseName = ExtensionClass.GenerateSlug(courseName);
            uploaderName = ExtensionClass.GenerateSlug(uploaderName); 

            //If the file is not public, set it in a user personal folder
            if (!isPublic)
            {
                location = "~/Content/Uploads/" + courseName + "/" + uploaderName + "/";
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

                    //Slugify the file name and trim length
                    fileName = ExtensionClass.GenerateSlug(fileName);
                    var path = Path.Combine(Server.MapPath(location), fileName);

                    //Verifies if the file has already been uploaded in the same directory
                    var existingFilesWithSameName = LMSRepo.GetAllFiles().Where(f => f.Name == fileName || f.Name.Replace(" - copy", "") == fileName && f.URL == path).ToList();
                    if (existingFilesWithSameName.Count > 0)
                    {
                        var existingFilesWithSameNameAndSize = existingFilesWithSameName.Where(ef => ef.Size == fileSize).ToList();
                        if(existingFilesWithSameNameAndSize.Count > 0)//Found file with same name and size
                        {
                            var existingFilesWithSameNameAndSizeAndFormat = existingFilesWithSameNameAndSize.Where(ef => ef.Format == fileFormat).ToList();
                            if(existingFilesWithSameNameAndSizeAndFormat.Count > 0)//Exact same file found
                            {
                                return null;//To-Do: proper handling
                            }
                            else//File with same name and size found but different format: Change name
                            {
                                fileName += " - copy";
                            }
                        }
                        else//If same names but different sizes, Change upload name, add - copy
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
                    dbFile.Course = new Course();

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

        [HttpGet]
        public ActionResult Delete(int id = 0)
        {
            var file = LMSRepo.GetFileByID(id);
            string fileURL = file.URL;

            // If the file exists on the server
            if (System.IO.File.Exists(fileURL))
            {
                System.IO.File.Delete(fileURL);
            }

            LMSRepo.DeleteFile(id);

            return RedirectToAction("Index");
        }
    }

    //Extension class
    public static class ExtensionClass
    {
        //Private methods
        //Helps create slugs
        private static string RemoveAccent(this string txt)
        {
            byte[] bytes = System.Text.Encoding.GetEncoding("Cyrillic").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }

        public static string GenerateSlug(this string phrase)
        {
            string str = RemoveAccent(phrase).ToLower();
            // invalid chars           
            str = Regex.Replace(str, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space   
            str = Regex.Replace(str, @"\s+", " ").Trim();
            // cut and trim 
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim();
            str = Regex.Replace(str, @"\s", "-"); // hyphens   
            return str;
        }
    }
}