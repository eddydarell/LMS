﻿using LMS_Grupp4.Models;
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

	}
}