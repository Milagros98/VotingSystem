using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Authorize]
    public class VotingsController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Votings
        public ActionResult Index()
        {
            var votings = db.Votings.Include(v => v.State);
            return View(votings.ToList());
        }

        // GET: Votings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Voting voting = db.Votings.Find(id);

            if (voting == null)
            {
                return HttpNotFound();
            }

            return View(voting);
        }

        // GET: Votings/Create
        public ActionResult Create()
        {
            ViewBag.stateId = new SelectList(db.States, "stateId", "description");

            var votingView = new VotingView
            {
                dateStart = DateTime.Now,
                dateEnd = DateTime.Now,
                
            };
            return View(votingView);
        }

        // POST: Votings/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(VotingView votingView)
        {
            ViewBag.stateId = new SelectList(db.States, "stateId", "description", votingView.stateId);

            if (!ModelState.IsValid)
            {          
                return View(votingView);               
            }

            var voting = new Voting
            {
                dateTimeEnd = votingView.dateEnd.AddHours(votingView.timeEnd.Hour).AddMinutes(votingView.timeEnd.Minute),
                dateTimeStart = votingView.dateStart.AddHours(votingView.dateStart.Hour).AddMinutes(votingView.dateStart.Minute),
                description = votingView.description,
                stateId = votingView.stateId,
                remarks = votingView.remarks,
                isEnabledBlankVotes = votingView.isEnabledBlankVotes,
                isForAllUsers = votingView.isForAllUsers,
                           
            };

            db.Votings.Add(voting);

            try
            {
                db.SaveChanges();
            }
            catch
            {
                ViewBag.Error = "Something has been wrong";
            }
            
            return RedirectToAction("Index");


        }

        // GET: Votings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Voting voting = db.Votings.Find(id);

            if (voting == null)
            {
                return HttpNotFound();
            }
            ViewBag.stateId = new SelectList(db.States, "stateId", "description", voting.stateId);

            var votingView = new VotingView
            {
                votingId = voting.votingId,
                dateStart = voting.dateTimeStart,
                dateEnd = voting.dateTimeEnd,
                timeStart = voting.dateTimeStart,
                timeEnd = voting.dateTimeEnd,
                description = voting.description,
                stateId = voting.stateId,
                remarks = voting.remarks,
                isEnabledBlankVotes = voting.isEnabledBlankVotes,
                isForAllUsers = voting.isForAllUsers,
            };

            return View(votingView);
        }

        // POST: Votings/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(VotingView votingView)
        {
            ViewBag.stateId = new SelectList(db.States, "stateId", "description", votingView.stateId);

            if (!ModelState.IsValid)
            {               
                return View(votingView);
            }

            var voting = new Voting
            {
                votingId = votingView.votingId,
                dateTimeEnd = votingView.dateEnd.AddHours(votingView.timeEnd.Hour).AddMinutes(votingView.timeEnd.Minute),
                dateTimeStart = votingView.dateStart.AddHours(votingView.dateStart.Hour).AddMinutes(votingView.dateStart.Minute),
                description = votingView.description,
                stateId = votingView.stateId,
                remarks = votingView.remarks,
                isEnabledBlankVotes = votingView.isEnabledBlankVotes,
                isForAllUsers = votingView.isForAllUsers,
            };

            db.Entry(voting).State = EntityState.Modified;
            try
            {
                db.SaveChanges();
            }
            catch
            {
                ViewBag.Error = "Something has been wrong";
            }
            return RedirectToAction("Index");

        }

        // GET: Votings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Voting voting = db.Votings.Find(id);
            if (voting == null)
            {
                return HttpNotFound();
            }
            return View(voting);
        }

        // POST: Votings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Voting voting = db.Votings.Find(id);
            db.Votings.Remove(voting);
            db.SaveChanges();
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
