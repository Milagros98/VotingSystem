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
    public class GroupsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Groups
        [HttpGet]
        public ActionResult Index()
        {
            return View(db.Groups.ToList());
        }

        [HttpGet]
        public ActionResult DeleteMember(int id)
        {
            var member = db.GroupMembers.Find(id);
            if (member != null)
            {

                db.GroupMembers.Remove(member);
                db.SaveChanges();
            }


            return RedirectToAction(string.Format("Details/{0}", member.groupId));
        }

        [HttpGet]
        public ActionResult AddMembers(int id)
        {
            ViewBag.userId = new SelectList(db.Users.OrderBy(u => u.firtsName).ThenBy(u => u.lastName), 
                "userId", 
                "fullName");

            var view = new AddMemberView { 
                groupId = id,
            };

            return View(view);
        }

        [HttpPost]
        public ActionResult AddMembers(AddMemberView view)
        {
            ViewBag.userId = new SelectList(db.Users.OrderBy(u => u.firtsName).ThenBy(u => u.lastName),
                "userId",
                "fullName");
            //var view = new AddMemberView();

            if (!ModelState.IsValid)
            {
                return View(view);
            }

            var members = db.GroupMembers
                .Where(gm => gm.groupId == view.groupId 
                 && gm.userId == view.userId)
                .FirstOrDefault();

            if(members != null){

                ViewBag.Error = ("The members already belongs to group");
            }

            var member = new GroupMembers
            {
                groupId = view.groupId,
                userId = view.userId,
            };

            db.GroupMembers.Add(member);

            try
            {
                
                db.SaveChanges();

            }
            catch
            {
                return View(view);

            }

            return RedirectToAction(string.Format("Details/{0}", view.groupId));
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Group group)
        {
            if (!ModelState.IsValid)
            {
                return View(group);
            }

            try
            {
                db.Groups.Add(group);
                db.SaveChanges();

            }
            catch
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

            var group = db.Groups.Find(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        [HttpPost]
        public ActionResult Edit(Group group)
        {

            if (!ModelState.IsValid)
            {
                return View(group);
            }

            try
            {
                db.Entry(group).State = EntityState.Modified;
                db.SaveChanges();

            }
            catch
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

            var group = db.Groups.Find(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            var view = new GroupDetailsView
            {
                groupId = group.groupId,
                description = group.description,
                members = group.GroupMembers.ToList(),
            };

            return View(view);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var group = db.Groups.Find(id);

            if (group == null)
            {
                return HttpNotFound();
            }

            return View(group);
        }

        [HttpPost]
        public ActionResult Delete(int id, Group group)
        {

            group = db.Groups.Find(id);

            try
            {
                db.Groups.Remove(group);
                db.SaveChanges();

            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
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