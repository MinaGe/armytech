using armytech.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace armytech.Controllers
{
    public class StudentController : Controller
    {
        ArmyTechTaskEntities dbcontext = new ArmyTechTaskEntities();

        public ActionResult Index()
        {
            List<Student> StudentList = dbcontext.Students.ToList();
            return View(StudentList);
        }

        public ActionResult Details(int id)
        {
            Student st = dbcontext.Students.Where(s => s.ID == id).FirstOrDefault();
            return View(st);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ViewBag.Governorates = dbcontext.Governorates.ToList();
            ViewBag.Neighborhoods = dbcontext.Neighborhoods.ToList();
            ViewBag.Fields = dbcontext.Fields.ToList();
            return View();
        }


        [HttpPost]
        public ActionResult Create(Student student)
        {
            if (!ModelState.IsValid)
            {
                return View(student);
            }
            else
                try
                {
                    // TODO: Add insert logic here
                    dbcontext.Students.Add(student);
                    dbcontext.SaveChanges();
                    return RedirectToAction("Details", new RouteValueDictionary(
    new { controller = "Student", action = "Details", Id = student.ID }));
                }
                catch (Exception ex)
                {
                    return View(ex.Message);
                }
        }


        public ActionResult Edit(int id)
        {
            Student st = dbcontext.Students.FirstOrDefault(s => s.ID == id);
            ViewBag.Governorates = dbcontext.Governorates.ToList();
            ViewBag.Neighborhoods = dbcontext.Neighborhoods.ToList();
            ViewBag.Fields = dbcontext.Fields.ToList();

            return View(st);
        }


        [HttpPost]
        public ActionResult Edit(int id, Student student)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var st = dbcontext.Students.FirstOrDefault(s => s.ID == id);
                    st.ID = student.ID;
                    st.Name = student.Name;
                    st.BirthDate = student.BirthDate;
                    st.GovernorateId = student.GovernorateId;
                    st.NeighborhoodId = student.NeighborhoodId;
                    st.FieldId = student.FieldId;
                    dbcontext.SaveChanges();
                    return RedirectToAction("Index");
                }
                return View();
            }


            catch (Exception ex)
            {

                ModelState.AddModelError("", ex.Message);
                return View(student);
            }
        }

        public ActionResult Delete(int id)
        {
            Student st = dbcontext.Students.FirstOrDefault(s => s.ID == id);




            dbcontext.Students.Remove(st);
            dbcontext.SaveChanges();

            return RedirectToAction("Index");



        }
    }
}