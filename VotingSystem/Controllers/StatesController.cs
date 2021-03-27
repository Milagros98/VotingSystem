using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Database;

namespace VotingSystem.Controllers
{
    public class StatesController : Controller
    {

        private DBContext db = new DBContext();

        // GET: States
        public ActionResult Index()
        {
            return View();
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