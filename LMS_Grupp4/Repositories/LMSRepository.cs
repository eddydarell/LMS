using LMS_Grupp4.Models;
using LMS_Grupp4.Models.LMS_Models;
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
		private ApplicationDbContext db = new ApplicationDbContext();

        #region Assignment
		// Returns all assignments from the database
		public IEnumerable<Assignment> GetAllAssignments()
		{
			return db.Assignments.ToList();
		}

		public Assignment GetSpecificAssignment(int id)
		{
			if (id != null)
			{ return db.Assignments.Find(id); } else return null;
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
            if (id != null)
            { return db.Courses.Find(id); }
            else return null;
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
			if (id != null)
			{ return db.Files.Find(id); } else return null;
		}

		// Add file to the database
		public void AddFile(File file)
		{
			db.Files.Add(file);
			db.SaveChanges();
		}

		// Edit an file in the database
		public void EditFile(File file)
		{
			db.Entry(file).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Delete a file from the database
		public void DeleteFile(int id)
		{
			File file = db.Files.Find(id);
			db.Files.Remove(file);
			db.SaveChanges();
		}
		#endregion


		#region ProgramClass
		public IEnumerable<ProgramClass> GetAllProgramClasses()
		{
			return db.ProgramClasses.ToList();
		}

		public ProgramClass GetSpecificProgramClass(int id)
		{
			return db.ProgramClasses.Find(id); 
		}

		// Add ProgramClass to the database
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
	}
}