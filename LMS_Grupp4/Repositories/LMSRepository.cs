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
		public void Add(Assignment assignment)
		{
			db.Assignments.Add(assignment);
			db.SaveChanges();
		}

		// Edits an assignment in the database
		public void Edit(Assignment assignment)
		{
			db.Entry(assignment).State = EntityState.Modified;
			db.SaveChanges();
		}

		// Deletes an assignments from the database
		public void Delete(int id)
		{
			Assignment assignment = db.Assignments.Find(id);
			db.Assignments.Remove(assignment);
			db.SaveChanges();
		}

	}
}