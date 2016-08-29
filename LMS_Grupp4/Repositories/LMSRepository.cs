using LMS_Grupp4.Controllers;
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

        //Error controller
        ErrorController error = new ErrorController();

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

		public void SaveChanges()
		{
			db.SaveChanges();
		}

        #region Assignment
        // Returns all assignments from the database
        public IEnumerable<Assignment> GetAllAssignments()
		{
            try
            {
                return db.Assignments.ToList();
            }
            catch(Exception e)
            {
                error.ExceptionHandler(e);
                return null;//Proper handling
            }
			
		}

		// Gets an assignment from the database with a specific id
		public Assignment GetAssignmentByID(int? id)
		{
            try
            {
                return db.Assignments.Find(id);
            }
            catch(Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
		}

		// Add assignment to the database
		public void AddAssignment(Assignment assignment)
		{
            try
            {
                db.Assignments.Add(assignment);
                db.SaveChanges();
            }
            catch(Exception e)
            {

                Console.WriteLine("An exception was thrown: {0}", e.ToString());                
                error.ExceptionHandler(e);
            }
		}

		// Edits an assignment in the database
		public void EditAssignment(Assignment assignment)
		{
            try
            {
                db.Entry(assignment).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}

		// Deletes an assignment from the database
		public void DeleteAssignment(int id)
		{
            try
            {
                Assignment assignment = db.Assignments.Find(id);
                db.Assignments.Remove(assignment);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}
        #endregion

        #region Course
        // Returns all courses from the database
        public IEnumerable<Course> GetAllCourses()
        {
            try
            {
                return db.Courses.ToList();
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

		// Gets a course from the database with a specific id
        public Course GetCourseByID(int id)
        {
            try
            {
                return db.Courses.Find(id);
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

        // Add course to the database
        public void AddCourse(Course course)
        {
            try
            {
                db.Courses.Add(course);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Edit a course in the database
        public void EditCourse(Course course)
        {
            try
            {
                db.Entry(course).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Delete a course from the database
        public void DeleteCourse(int id)
        {
            try
            {
                Course course = db.Courses.Find(id);
                db.Courses.Remove(course);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }
        #endregion

        #region File
		// Returns all files from the database
        public IEnumerable<LMSFile> GetAllFiles()
		{
            try
            {
                return db.Files.ToList();
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;                
            }
		}
		
		// Gets a file from the database with a specific id
		public LMSFile GetFileByID(int id)
		{
            try
            {
                return db.Files.Find(id);
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
		}

		// Add a file to the database
		public void AddFile(LMSFile file)
		{
            try
            {
                db.Files.Add(file);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}

		// Edit a file in the database
		public void EditFile(LMSFile file)
		{
            try
            {
                db.Entry(file).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}

		// Delete a file from the database
		public void DeleteFile(int id)
		{
            try
            {
                LMSFile file = db.Files.Find(id);
                db.Files.Remove(file);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}
		#endregion 

		#region ProgramClass
		// Returns all ProgramClasses from the database
		public IEnumerable<ProgramClass> GetAllProgramClasses()
		{
            try
            {
                return db.ProgramClasses.ToList();
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
		}

		// Gets a ProgramClass from the database with a specific id
		public ProgramClass GetProgramClassByID(int id)
		{
            try
            {
                return db.ProgramClasses.Find(id);
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
		}

		// Add a ProgramClass to the database
        public void AddProgramClass(ProgramClass programClass)
		{
            try
            {
                db.ProgramClasses.Add(programClass);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}

        // Edit a ProgramClass in the database
        public void EditProgramClass(ProgramClass programClass)
		{
            try
            {
            db.Entry(programClass).State = EntityState.Modified;
			db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}

        // Delete a ProgramClass from the database
        public void DeleteProgramClass(int id)
		{
            try
            {
            ProgramClass programClass = db.ProgramClasses.Find(id);
            db.ProgramClasses.Remove(programClass);
			db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
		}
		#endregion

		#region ClassSchema
        // Returns all ClassSchemas from the database
        public IEnumerable<ClassSchema> GetAllClassSchemas()
        {
            try
            {
                return db.ClassSchemas.ToList();
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

		// Gets a ClassSchema from the database with a specific id
        public ClassSchema GetClassSchemaByID(int id)
        {
            try
            {
                return db.ClassSchemas.Find(id);
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

        // Add a ClassSchemas to the database
        public void AddClassSchema(ClassSchema classSchema)
        {
            try
            { 
            db.ClassSchemas.Add(classSchema);
            db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Edit a ClassSchema in the database
        public void EditClassSchema(ClassSchema classSchema)
        {
            try
            { 
            db.Entry(classSchema).State = EntityState.Modified;
            db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Delete a ClassSchema from the database
        public void DeleteClassSchema(int id)
        {
            try
            {
                ClassSchema classSchema = db.ClassSchemas.Find(id);
                db.ClassSchemas.Remove(classSchema);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }
        #endregion

        #region CourseApplication
        // Returns all ClassSchemas from the database
        public IEnumerable<CourseApplication> GetAllCourseApplications()
        {
            try
            { 
            return db.CourseApplications.ToList();
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

        // Gets a ClassSchema from the database with a specific id
        public CourseApplication GetCourseApplicationID(int id)
        {
            try
            { 
            return db.CourseApplications.Find(id);
            }
            catch (Exception e)
            {
                error.ExceptionHandler(e);
                return null;
            }
        }

        // Add a ClassSchemas to the database
        public void AddCourseApplication(CourseApplication courseApplication)
        {
            try
            { 
            db.CourseApplications.Add(courseApplication);
            db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Edit a ClassSchema in the database
        public void EditCourseApplication(CourseApplication courseApplication)
        {
            try 
            { 
            db.Entry(courseApplication).State = EntityState.Modified;
            db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }

        // Delete a ClassSchema from the database
        public void DeleteCourseApplication(int id)
        {
            try
            { 
            CourseApplication courseApplication = db.CourseApplications.Find(id);
            db.CourseApplications.Remove(courseApplication);
            db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine("An exception was thrown: {0}", e.ToString());
                error.ExceptionHandler(e);
            }
        }
        #endregion

		#region Evaluation
		// Returns all evaluations from the database
		public IEnumerable<Evaluation> GetAllEvaluations()
		{
			return db.Evaluations.ToList();
		}

		// Gets a evaluation from the database with a specific id
		public Evaluation GetEvaluationByID(int id)
		{
			return db.Evaluations.Find(id);
		}

		// Add evaluation to the database
		public void AddEvaluation(Evaluation evaluation)
		{
			db.Evaluations.Add(evaluation);
			db.SaveChanges();
		}

		// Edit a evaluation in the database
		public void EditEvaluation(Evaluation evaluation)
		{
			db.Entry(evaluation).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Delete a evaluation from the database
		public void DeleteEvaluation(int id)
		{
			Evaluation evaluation = db.Evaluations.Find(id);
			db.Evaluations.Remove(evaluation);
			db.SaveChanges();
		}
		#endregion
    }
}
