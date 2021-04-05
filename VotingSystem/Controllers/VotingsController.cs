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

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteVotingGroup(int id)
        {
            var votingGroup= db.VotingGroups.Find(id);
            if (votingGroup != null)
            {

                db.VotingGroups.Remove(votingGroup);
                db.SaveChanges();
            }


            return RedirectToAction(string.Format("Details/{0}", votingGroup.votingId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteCandidate(int id)
        {
            var candidate = db.Candidates.Find(id);
            if (candidate != null)
            {

                db.Candidates.Remove(candidate);
                db.SaveChanges();
            }


            return RedirectToAction(string.Format("Details/{0}", id));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddGroup(int id)
        {
            ViewBag.groupId = new SelectList(db.Groups.OrderBy(g => g.description), "groupId", "description");

            var view = new AddGroupView
            {
                votingId = id,
            };
            return View(view);
        }

        [HttpPost]
        public ActionResult AddGroup(AddGroupView view)
        {
            ViewBag.groupId = new SelectList(db.Groups.OrderBy(g => g.description), "groupId", "description");

            if (!ModelState.IsValid)             
            {               
                return View(view);
            }
            var votingGroup = db.VotingGroups.Where(vg => vg.votingId == view.votingId && vg.groupId == view.groupId).FirstOrDefault();

            if (votingGroup != null)
            {
                ViewBag.Error = "The group already belongs to this voting";
                return View(view);
            }

            var group = new VotingGroup
            {
                votingId = view.votingId,
                groupId = view.groupId,
            };

            db.VotingGroups.Add(group);
            db.SaveChanges();

            return RedirectToAction(string.Format("Details/{0}", group.votingId));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public ActionResult AddCandidate(int id)
        {
            ViewBag.userId = new SelectList(db.Users.OrderBy(g => g.firtsName).ThenBy(g => g.lastName), "userId", "fullName");

            var view = new AddCandidateView
            {
                votingId = id,
            };
            return View(view);
        }

        [HttpPost]
        public ActionResult AddCandidate(AddCandidateView view)
        {
            ViewBag.userId = new SelectList(db.Users.OrderBy(g => g.firtsName).ThenBy(g => g.lastName), "userId", "fullName");

            if (!ModelState.IsValid)
            {
                return View(view);
            }
            var candidate = db.Candidates.Where(vg => vg.votingId == view.votingId && vg.userId == view.userId).FirstOrDefault();

            if (candidate != null)
            {
                ViewBag.Error = "The user already is candidate to this voting";
                return View(view);
            }

            var candidateUser = new Candidate
            {
                votingId = view.votingId,
                userId = view.userId,
            };

            db.Candidates.Add(candidateUser);
            db.SaveChanges();

            return RedirectToAction(string.Format("Details/{0}", view.votingId));
        }

        // GET: Votings/Details/5
        [Authorize(Roles = "Admin")]
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

            var detailsView = new DetailsVotingView
            {
                votingId = voting.votingId,
                description = voting.description,
                stateId = voting.stateId,
                remarks = voting.remarks,
                dateTimeEnd = voting.dateTimeEnd,
                dateTimeStart = voting.dateTimeStart,
                isEnabledBlankVotes = voting.isEnabledBlankVotes,
                isForAllUsers = voting.isForAllUsers,
                quantityBlankVotes = voting.quantityBlankVotes,
                quantityVotes = voting.quantityVotes,
                candidateWinId = voting.candidateWinId,
                Candidates = voting.Candidates.ToList(),
                VotingGroups = voting.VotingGroups.ToList(),

            };

            return View(detailsView);
        }

        // GET: Votings/Create
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
