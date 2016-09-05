using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using LMS_Grupp4.Repositories;
using LMS_Grupp4.Models.LMS_ViewModels;
using LMS_Grupp4.Models.LMS_Models;
using LMS_Grupp4.Models;

namespace LMS_Grupp4.Controllers
{
    public class AssignmentController : Controller
    {
        LMSRepository LMSRepo = new LMSRepository();

        [Authorize(Roles = "teacher, student")]
        public ActionResult IndexUser()
        {
            var userId = User.Identity.GetUserId();

            var user = LMSRepo.GetUserManager().FindById(userId);

            List<Assignment> assignmentModelList = new List<Assignment>();

            if (User.IsInRole("student"))
            {
                try
                {
                    assignmentModelList = user.Assignments.ToList();
                    //Creating an empty list representing the teacherlist that is not necessary in Student user index,
                    //but needed as parameter in the viewmodel.
                    List<IGrouping<string, Assignment>> emptyTeacherList = new List<IGrouping<string, Assignment>>();
                    var responds = assignmentModelList.OrderByDescending(a => a.Course.CourseName).GroupBy(a => a.Course.CourseName);
                    Assignment_IndexUserViewModel A_IUVW = new Assignment_IndexUserViewModel(emptyTeacherList, responds);

                    return View(A_IUVW);
                }
                catch (Exception)
                {
                    return RedirectToAction("ExceptionHandler", "Error");
                }
            }
            else
            {
                var teacherCourses = user.Courses;
                List<Assignment> teacherAssignments = new List<Assignment>();
                List<Assignment> respondAssignments = new List<Assignment>();

                try
                {
                    //Populates a list with all assignments a teacher got in his courses
                    foreach (Course course in teacherCourses)
                    {
                        foreach (Assignment assignment in course.Assignments)
                        {
                            teacherAssignments.Add(assignment);
                        }
                    }

                    var userManager = LMSRepo.GetUserManager();
                    //Populates a list with all assignments that has been confirmed by the student in 
                    //any course that the teacher got.
                    foreach (Course course in teacherCourses)
                    {
                        foreach (var courseUser in course.Users)
                        {
                            if (userManager.IsInRole(courseUser.Id, "student"))
                            {
                                foreach (Assignment assignment in courseUser.Assignments)
                                {
                                    if (!respondAssignments.Contains(assignment) && assignment.Course == course)
                                    {
                                        respondAssignments.Add(assignment);
                                    }
                                }
                            }
                        }
                    }

                    var assignments = teacherAssignments.OrderByDescending(a => a.Course.CourseName).GroupBy(a => a.Course.CourseName);
                    var responds = respondAssignments.OrderByDescending(a => a.Course.CourseName).GroupBy(a => a.Course.CourseName);
                    Assignment_IndexUserViewModel A_IUVW = new Assignment_IndexUserViewModel(assignments, responds);

                    return View(A_IUVW);
                }
                catch (NullReferenceException)
                {
                    return RedirectToAction("ExceptionHandler", "Error");
                }
            }
        }


        [Authorize(Roles = "teacher, student")]
        public ActionResult IndexCourse(int courseID = 0)
        {
            try
            {
				var userId = User.Identity.GetUserId();
				var user = LMSRepo.GetUserManager().FindById(userId);
                Course course = LMSRepo.GetCourseByID(courseID);
                string courseName = course.CourseName;
                var courseAssignmentList = course.Assignments;
				List<Assignment> studentUnconfirmedAssignments = new List<Assignment>(); 

				foreach (var assignment in courseAssignmentList)
				{
					if (!assignment.Evaluations.Any(e => e.Student.Id == user.Id))
					{
						studentUnconfirmedAssignments.Add(assignment);
					}
				}

                Assignment_IndexCourseViewModel aICVM = new Assignment_IndexCourseViewModel(courseName, courseAssignmentList, studentUnconfirmedAssignments);

                return View(aICVM);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("IndexUser");
            }
        }


        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Create(int courseID = 0)
        {
            try
            {
                //Contains code for populating a drop-down list in the view, which is not used right now.
                var roleManager = LMSRepo.GetRoleManager();
                var studentRole = roleManager.FindByName("student");
                var course = LMSRepo.GetCourseByID(courseID);
                var students = course.Users.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();

                //This viewmodel is not needed in this form, but remains from earlier test with dropdownlist.
                Assignment_CreateViewModel a_CVM = new Assignment_CreateViewModel(students, course);

                return View(a_CVM);
            }
            catch (NullReferenceException)
            {
                return RedirectToAction("IndexUser");
            }
        }


        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Create(int CourseID = 0, string Name = "", DateTime? DueDate = null, int MaxScore = 0)
        {
            Assignment assignment = new Assignment();
            Course course = LMSRepo.GetCourseByID(CourseID);

            assignment.Name = Name;
            assignment.DueDate = DueDate;
            assignment.MaxScore = MaxScore;
            assignment.IssueDate = DateTime.Now;
            assignment.IsExpired = false;
            //Connects the course to the assignment
            assignment.Course = course;
            assignment.Students = new List<ApplicationUser>();

            LMSRepo.AddAssignment(assignment);


            //var roleManager = LMSRepo.GetRoleManager();
            //var studentRole = roleManager.FindByName("student");
            //string id = User.Identity.GetUserId();
            //var teacher = LMSRepo.GetUserManager().FindById(id);

            //var students = course.Students.Where(stu => stu.Roles.FirstOrDefault(r => r.RoleId == studentRole.Id) != null).ToList();

            return RedirectToAction("IndexUser");
        }


        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult Edit(int ID = 0)
        {
            Assignment assignment = LMSRepo.GetAssignmentByID(ID);

            return View(assignment);
        }


        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult Edit(int ID = 0, string Name = "", DateTime? DueDate = null)
        {
            Assignment assignment = LMSRepo.GetAssignmentByID(ID);
            assignment.Name = Name;
            assignment.DueDate = DueDate;

            assignment.IsExpired = DateTime.Now >= assignment.DueDate;

            LMSRepo.EditAssignment(assignment);

            return RedirectToAction("IndexUser");
        }


        [Authorize(Roles = "teacher")]
        [HttpGet]
        public ActionResult EditResponse(int ID = 0)
        {
            Evaluation evaluation = LMSRepo.GetEvaluationByID(ID);

            return View(evaluation);
        }

        [Authorize(Roles = "teacher")]
        [HttpPost]
        public ActionResult EditResponse(int ID = 0, int Score = 0, string Message = "")
        {
            var evaluation = LMSRepo.GetEvaluationByID(ID);
            evaluation.Score = Score;
            evaluation.Message = Message;
            evaluation.Percentage = Math.Round(((double)evaluation.Score / (double)evaluation.Assignment.MaxScore) * 100, 2);

            if (evaluation.Percentage >= 50.00)
                evaluation.IsPassed = true;
            else
                evaluation.IsPassed = false;

            if (evaluation.Percentage >= 90)
            {
                evaluation.Mark = "A";
            }
            else if (evaluation.Percentage < 90 && evaluation.Percentage >= 80)
            {
                evaluation.Mark = "B";
            }
            else if (evaluation.Percentage < 80 && evaluation.Percentage >= 70)
            {
                evaluation.Mark = "C";
            }
            else if (evaluation.Percentage < 70 && evaluation.Percentage >= 60)
            {
                evaluation.Mark = "D";
            }
            else if (evaluation.Percentage < 60 && evaluation.Percentage >= 50)
            {
                evaluation.Mark = "E";
            }
            else if (evaluation.Percentage < 50 && evaluation.Percentage >= 40)
            {
                evaluation.Mark = "Fx";
            }
            else
            {
                evaluation.Mark = "F";
            }

            LMSRepo.EditEvaluation(evaluation);

            return RedirectToAction("IndexUser");
        }




        //[Authorize(Roles = "teacher")]
        //[HttpPost]
        //public ActionResult EditResponse(int ID = 0, int Score = 0, string Message = "")
        //{
        //    Evaluation evaluation = LMSRepo.GetEvaluationByID(ID), tmpEvaluation;
        //    var assignment = evaluation.Assignment;
        //    var course = assignment.Course;
        //    var user = evaluation.Student;
        //    List<Evaluation> studentCourseEvaluations = new List<Evaluation>();
        //    int assignmentCount = 0;
        //    double? totalPercentage = 0;

        //    foreach (Assignment iterateCourseAssignment in course.Assignments)
        //    {
        //        foreach (Assignment iterateStudentAssignment in user.Assignments)
        //        {
        //            if (iterateCourseAssignment.Equals(iterateStudentAssignment))
        //            {
        //                tmpEvaluation = iterateStudentAssignment.Evaluations.FirstOrDefault(eval => eval.Student.Id.Equals(user.Id));
        //                studentCourseEvaluations.Add(tmpEvaluation);
        //                assignmentCount++;
        //            }
        //        }
        //    }

        //    assignment.IsExpired = DateTime.Now >= assignment.DueDate;
        //    evaluation.Score = Score;
        //    evaluation.Percentage = (evaluation.Score / evaluation.Assignment.MaxScore) * 100;
        //    evaluation.Message = Message;

        //    if (evaluation.Percentage >= 50)
        //    {
        //        evaluation.IsPassed = true;
        //    }
        //    else
        //    {
        //        evaluation.IsPassed = false;
        //    }

        //    if (assignmentCount < 3)
        //    {
        //        evaluation.Mark = "ND";
        //        LMSRepo.EditAssignment(assignment);
        //    }
        //    else
        //    {
        //        foreach (Evaluation calcEvaluation in studentCourseEvaluations)
        //        {
        //            totalPercentage = totalPercentage + calcEvaluation.Percentage;
        //        }

        //        totalPercentage = totalPercentage / studentCourseEvaluations.Count;

        //        foreach (Evaluation markedEvaluation in studentCourseEvaluations)
        //        {
        //            if (totalPercentage >= 90)
        //            {
        //                markedEvaluation.Mark = "A";
        //            }
        //            else if (totalPercentage < 90 && totalPercentage >= 80)
        //            {
        //                markedEvaluation.Mark = "B";
        //            }
        //            else if (totalPercentage < 80 && totalPercentage >= 70)
        //            {
        //                markedEvaluation.Mark = "C";
        //            }
        //            else if (totalPercentage < 70 && totalPercentage >= 60)
        //            {
        //                markedEvaluation.Mark = "D";
        //            }
        //            else if (totalPercentage < 60 && totalPercentage >= 50)
        //            {
        //                markedEvaluation.Mark = "E";
        //            }
        //            else if (totalPercentage < 50 && totalPercentage >= 40)
        //            {
        //                markedEvaluation.Mark = "Fx";
        //            }
        //            else
        //            {
        //                markedEvaluation.Mark = "F";
        //            }

        //            LMSRepo.EditAssignment(markedEvaluation.Assignment);
        //        }
        //    }

        //    return RedirectToAction("IndexUser");
        //}

        [Authorize(Roles = "student")]
        public ActionResult AssignmentAcceptance(int id = 0)
        {
            var assignment = LMSRepo.GetAssignmentByID(id);
            return View("UploadFile", assignment);
        }

        [Authorize(Roles = "student")]
        public ActionResult ConfirmAssignment(int id = 0)
        {
            string userId = User.Identity.GetUserId();
            var userManager = LMSRepo.GetUserManager();
            var user = userManager.FindById(userId);
            
            Assignment assignment = LMSRepo.GetAssignmentByID(id);

            //Add file to evaluation
            var evaluation = new Evaluation();
            evaluation.LMSFile = null;
            evaluation.Student = user;
            evaluation.Score = 0;
            LMSRepo.AddEvaluation(evaluation);

            //Add evaluation to the assignment
            assignment.Evaluations.Add(evaluation);
            user.Assignments.Add(assignment);

            //assignment.Students.Add(uploader);
            userManager.Update(user);



            assignment.IsExpired = DateTime.Now >= assignment.DueDate;

            LMSRepo.EditAssignment(assignment);
            //user.Assignments.Add(assignment);

            return RedirectToAction("IndexUser");
        }


        public ActionResult Details_Basic(int id)
        {
            var assignment = LMSRepo.GetAssignmentByID(id);

            assignment.IsExpired = DateTime.Now >= assignment.DueDate;

            return View(assignment);
        }


        public ActionResult Details_Full(int id)
        {
            var evaluation = LMSRepo.GetEvaluationByID(id);

            evaluation.Assignment.IsExpired = DateTime.Now >= evaluation.Assignment.DueDate;

            return View(evaluation);
        }


        [Authorize(Roles = "teacher")]
        public ActionResult Delete(int id)
        {
            Assignment assignment = LMSRepo.GetAssignmentByID(id);

            return View(assignment);
        }


        [Authorize(Roles = "teacher")]
        public ActionResult DeleteConfirmed(int id)
        {
            var assignment = LMSRepo.GetAssignmentByID(id);
            List<Evaluation> tmpEvaluationList = new List<Evaluation>();

            foreach (Evaluation evaluation in assignment.Evaluations)
            {
                tmpEvaluationList.Add(evaluation);
            }

            for (int i = 0; i < tmpEvaluationList.Count; i++)
            {
                LMSRepo.DeleteEvaluation(tmpEvaluationList.ElementAt(i).ID);
            }

            LMSRepo.DeleteAssignment(id);

            return RedirectToAction("IndexUser");
        }
    }
}