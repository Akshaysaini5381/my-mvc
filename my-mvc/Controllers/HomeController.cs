using my_mvc.db;
using my_mvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace my_mvc.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        public ActionResult mytable()
        {

            newprojectEntities obj = new newprojectEntities();
            List<Class1> clobj = new List<Class1>();
            var res = obj.newtables.ToList();
            foreach (var item in res)
            {
                clobj.Add(new Class1
                {
                    id = item.id,
                    name = item.name,
                    email = item.email,
                    city = item.city,
                    college = item.college
                });
            }

            return View(clobj);
        }


        [HttpGet]

        public ActionResult myform()
        {
            return View();
        }



        [HttpPost]

        public ActionResult myform(Class1 objnew)
        {
            newprojectEntities obj = new newprojectEntities();
            newtable obj1 = new newtable();

            obj1.id = objnew.id;
            obj1.name = objnew.name;
            obj1.email = objnew.email;
            obj1.city = objnew.city;
            obj1.college = objnew.college;

            if (objnew.id == 0)
            {
                obj.newtables.Add(obj1);
                obj.SaveChanges();
            }
            else
            {
                obj.Entry(obj1).State = System.Data.Entity.EntityState.Modified;
                obj.SaveChanges();
            }


            return RedirectToAction("mytable");
            // return View();

        }

        public ActionResult delete(int id)
        {
            newprojectEntities obj = new newprojectEntities();
            newtable deleteitem = obj.newtables.Where(x => x.id == id).First();
            obj.newtables.Remove(deleteitem);
            obj.SaveChanges();

            return RedirectToAction("mytable");
        }
        [Authorize]
        public ActionResult edit(int id)
        {
            Class1 obj2 = new Class1();
            newprojectEntities obj = new newprojectEntities();
            var edititem = obj.newtables.Where(x => x.id == id).First();

            obj2.id = edititem.id;
            obj2.name = edititem.name;
            obj2.email = edititem.email;
            obj2.city = edititem.city;
            obj2.college = edititem.college;

            ViewBag.id = edititem.id;

            return View("myform", obj2);
        }

    }
}  