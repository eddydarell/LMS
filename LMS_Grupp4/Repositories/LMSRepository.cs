using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Repositories
{
	// Repository for all Db-sets
	public class LMSRepository
	{
		/// Database connections
		ApplicationDbContext db = null;
        RoleStore<IdentityRole> roleStore = null;
        RoleManager<IdentityRole> roleManager = null;
        UserStore<ApplicationUser> userStore = null;
        UserManager<ApplicationUser> userManager = null;

        public LMSRepository()
		{
			db = new ApplicationDbContext();
            roleStore = new RoleStore<IdentityRole>(db);
            roleManager = new RoleManager<IdentityRole>(roleStore);
            userStore = new UserStore<ApplicationUser>(db);
            userManager = new UserManager<ApplicationUser>(userStore);
		}

        //Returns Identity managers
        public RoleManager<IdentityRole> GetRoleManager()
        {
            return roleManager;
        }

        public UserManager<ApplicationUser> GetUserManager()
        {
            return userManager;
        }

        #region Assignment
        // Returns all assignments from the database
        public IEnumerable<Assignment> GetAllAssignments()
		{
            try
            {
                return db.Assignments.ToList();
            }
            catch(Exception)
            {
                return null;//Proper handling
            }
			
		}

		// Gets an assignment from the database with a specific id
		public Assignment GetAssignmentByID(int? id)
		{
			 return db.Assignments.Find(id);             
		}

		// Add assignment to the database
		public void AddAssignment(Assignment assignment)
		{
			db.Assignments.Add(assignment);
			db.SaveChanges();
		}

		// Edits an assignment in the database
		public void EditAssignment(Assignment assignment)
		{
			db.Entry(assignment).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Deletes an assignment from the database
		public void DeleteAssignment(int id)
		{
			Assignment assignment = db.Assignments.Find(id);
			db.Assignments.Remove(assignment);
			db.SaveChanges();
		}
        #endregion

        #region Course
        // Returns all courses from the database
        public IEnumerable<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }

		// Gets a course from the database with a specific id
        public Course GetCourseByID(int id)
        {
            return db.Courses.Find(id);
        }

        // Add course to the database
        public void AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        // Edit a course in the database
        public void EditCourse(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Delete a course from the database
        public void DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
        }
        #endregion

        #region File
		// Returns all files from the database
        public IEnumerable<LMSFile> GetAllFiles()
		{
			return db.Files.ToList();
		}
		
		// Gets a file from the database with a specific id
		public LMSFile GetFileByID(int id)
		{
			return db.Files.Find(id);
		}

		// Add a file to the database
		public void AddFile(LMSFile file)
		{
			db.Files.Add(file);
			db.SaveChanges();
		}

		// Edit a file in the database
		public void EditFile(LMSFile file)
		{
			db.Entry(file).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Delete a file from the database
		public void DeleteFile(int id)
		{
			LMSFile file = db.Files.Find(id);
			db.Files.Remove(file);
			db.SaveChanges();
		}
		#endregion 

		#region ProgramClass
		// Returns all ProgramClasses from the database
		public IEnumerable<ProgramClass> GetAllProgramClasses()
		{
			return db.ProgramClasses.ToList();
		}

		// Gets a ProgramClass from the database with a specific id
		public ProgramClass GetProgramClassByID(int id)
		{
			return db.ProgramClasses.Find(id); 
		}

		// Add a ProgramClass to the database
        public void AddProgramClass(ProgramClass programClass)
		{
			db.ProgramClasses.Add(programClass);
			db.SaveChanges();
		}

        // Edit a ProgramClass in the database
        public void EditProgramClass(ProgramClass programClass)
		{
            db.Entry(programClass).State = EntityState.Modified;
			db.SaveChanges();
		}

        // Delete a ProgramClass from the database
        public void DeleteProgramClass(int id)
		{
            ProgramClass programClass = db.ProgramClasses.Find(id);
            db.ProgramClasses.Remove(programClass);
			db.SaveChanges();
		}
		#endregion

		#region ClassSchema
        // Returns all ClassSchemas from the database
        public IEnumerable<ClassSchema> GetAllClassSchemas()
        {
            return db.ClassSchemas.ToList();
        }

		// Gets a ClassSchema from the database with a specific id
        public ClassSchema GetClassSchemaByID(int id)
        {
            return db.ClassSchemas.Find(id);
        }

        // Add a ClassSchemas to the database
        public void AddClassSchema(ClassSchema classSchema)
        {
            db.ClassSchemas.Add(classSchema);
            db.SaveChanges();
        }

        // Edit a ClassSchema in the database
        public void EditClassSchema(ClassSchema classSchema)
        {
            db.Entry(classSchema).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Delete a ClassSchema from the database
        public void DeleteClassSchema(int id)
        {
            ClassSchema classSchema = db.ClassSchemas.Find(id);
            db.ClassSchemas.Remove(classSchema);
            db.SaveChanges();
        }
        #endregion

        #region CourseApplication
        // Returns all ClassSchemas from the database
        public IEnumerable<CourseApplication> GetAllCourseApplications()
        {
            return db.CourseApplications.ToList();
        }

        // Gets a ClassSchema from the database with a specific id
        public CourseApplication GetCourseApplicationID(int id)
        {
            return db.CourseApplications.Find(id);
        }

        // Add a ClassSchemas to the database
        public void AddCourseApplication(CourseApplication courseApplication)
        {
            db.CourseApplications.Add(courseApplication);
            db.SaveChanges();
        }

        // Edit a ClassSchema in the database
        public void EditCourseApplication(CourseApplication courseApplication)
        {
            db.Entry(courseApplication).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Delete a ClassSchema from the database
        public void DeleteCourseApplication(int id)
        {
            CourseApplication courseApplication = db.CourseApplications.Find(id);
            db.CourseApplications.Remove(courseApplication);
            db.SaveChanges();
        }
        #endregion
    }
}
