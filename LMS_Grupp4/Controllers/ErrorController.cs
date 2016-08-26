using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LMS_Grupp4.Controllers
{
    public class ErrorController : Controller
    {
     
        // GET: Custom Errors      
        public ActionResult ErrorHandler400()
        {
            return View();
        }

        public ActionResult ErrorHandler401()
        {
            return View();
        }

        public ActionResult ErrorHandler403()
        {
            return View();
        }

        public ActionResult ErrorHandler404()
        {
            return View();
        }

        public ActionResult ErrorHandler500()
        {
            return View();
        }

        public ActionResult ExceptionHandler(Exception e)
        {
            if(e.GetType() == typeof(NullReferenceException))
            {
                ViewBag.message = "No result found.";
            }
            else if (e.GetType() == typeof(OutOfMemoryException))
            {
                ViewBag.message = "An attempt to allocate memory failed.";
            }
            else if (e.GetType() == typeof(OverflowException))
            {
                ViewBag.message = "An arithmetic operation in a checked context overflowed.";
            }
            else if (e.GetType() == typeof(StackOverflowException))
            {
                ViewBag.message = "The execution stack is exhausted by having too many pending method calls.";
            }
            else if (e.GetType() == typeof(TypeInitializationException))
            {
                ViewBag.message = "A static constructor throwed an exception, and no catch clauses existed to catch it.";
            }
            else if (e.GetType() == typeof(InvalidCastException))
            {
                ViewBag.message = "An explicit conversion from a base type or interface to a derived type failed at run time.";
            }
            else if (e.GetType() == typeof(IndexOutOfRangeException))
            {
                ViewBag.message = "An attempt to index an array via an index that was less than zero or outside the bounds of the array.";
            }
            else if (e.GetType() == typeof(DivideByZeroException))
            {
                ViewBag.message = "An attempt to divided an integral value by zero occurs.";
            }
            else if (e.GetType() == typeof(ArgumentException))
            {
                ViewBag.message = "Bad arguments are passed to a member.";
            }
            else if (e.GetType() == typeof(InvalidOperationException))
            {
                ViewBag.message = "The object was in an inappropriate state.";
            }
            return View();
        }
    }

}