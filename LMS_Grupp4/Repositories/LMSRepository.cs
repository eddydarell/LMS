using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LMS_Grupp4.Repositories
{
	// Repository for all assignment functions
	public class LMSRepository
	{
		/// Database connections
		private ApplicationDbContext db = new ApplicationDbContext();

        #region Assignment
		// Returns all assignments from the database
		public IEnumerable<Assignment> GetAllAssignments()
		{
			return db.Assignments.ToList();
		}

		public Assignment GetSpecificAssignment(int id)
		{			
			 return db.Assignments.Find(id);             
		}

		// Adds assignments to the database
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

		// Deletes a assignment from the database
		public void DeleteAssignment(int id)
		{
			Assignment assignment = db.Assignments.Find(id);
			db.Assignments.Remove(assignment);
			db.SaveChanges();
		}
        #endregion Assignment

        #region Course
        // Returns all courses from the database
        public IEnumerable<Course> GetAllCourses()
        {
            return db.Courses.ToList();
        }

        public Course GetSpecificCourse(int id)
        {
            return db.Courses.Find(id);
        }

        // Adds courses to the database
        public void AddCourse(Course course)
        {
            db.Courses.Add(course);
            db.SaveChanges();
        }

        // Edits a course in the database
        public void EditCourse(Course course)
        {
            db.Entry(course).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Deletes a course from the database
        public void DeleteCourse(int id)
        {
            Course course = db.Courses.Find(id);
            db.Courses.Remove(course);
            db.SaveChanges();
        }
        #endregion Course

        #region File
        public IEnumerable<File> GetAllFiles()
		{
			return db.Files.ToList();
		}

		public File GetSpecificFile(int id)
		{
			return db.Files.Find(id);
		}

		// Adds files to the database
		public void AddFile(File file)
		{
			db.Files.Add(file);
			db.SaveChanges();
		}

		// Edits an file in the database
		public void EditFile(File file)
		{
			db.Entry(file).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Deletes a file from the database
		public void DeleteFile(int id)
		{
			File file = db.Files.Find(id);
			db.Files.Remove(file);
			db.SaveChanges();
		}
		#endregion

        #region ClassSchema
        // Returns all classSchemas from the database
        public IEnumerable<ClassSchema> GetAllClassSchemas()
        {
            return db.ClassSchemas.ToList();
        }

        public ClassSchema GetSpecificClassSchema(int id)
        {
            return db.ClassSchemas.Find(id);
        }

        // Adds classSchemas to the database
        public void AddClassSchema(ClassSchema classSchema)
        {
            db.ClassSchemas.Add(classSchema);
            db.SaveChanges();
        }

        // Edits a classSchema in the database
        public void EditClassSchema(ClassSchema classSchema)
        {
            db.Entry(classSchema).State = EntityState.Modified;
            db.SaveChanges();
        }

        // Deletes a classSchema from the database
        public void DeleteClassSchema(int id)
        {
            ClassSchema classSchema = db.ClassSchemas.Find(id);
            db.ClassSchemas.Remove(classSchema);
            db.SaveChanges();
        }
        #endregion ClassSchema
    }
}