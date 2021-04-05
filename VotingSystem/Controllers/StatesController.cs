using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Authorize(Roles = "Admin")]
    public class StatesController : Controller
    {

        private DBContext db = new DBContext();

        // GET: States
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.States.ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(State state)
        {
            if (!ModelState.IsValid)
            {
                return View(state);
            }

            try
            {
                db.States.Add(state);
                db.SaveChanges();

            } catch 
            {
                return View();

            }
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        [HttpPost]
        public ActionResult Edit(State state)
        {

            if (!ModelState.IsValid)
            {
                return View(state);
            }

            try
            {
                db.Entry(state).State = EntityState.Modified;
                db.SaveChanges();

            } catch
            {
                throw;
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var state = db.States.Find(id);

            if (state == null)
            {
                return HttpNotFound();
            }

            return View(state);
        }

        [HttpPost]
        public ActionResult Delete(int id, State state)
        {

            state = db.States.Find(id);
            db.States.Remove(state);

            try
            {               
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                   ex.InnerException.InnerException != null &&
                   ex.InnerException.InnerException.Message.Contains("REFERENCE"))
                {
                    ViewBag.Error = "This field has relationed records";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
            }
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }
    }
}