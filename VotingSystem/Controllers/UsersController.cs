using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using VotingSystem.Database;
using VotingSystem.Models;

namespace VotingSystem.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private DBContext db = new DBContext();

        // GET: Users
        public ActionResult Index()
        {
            return View(db.Users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                return View(userView);

            }

            // upload image
            string path = string.Empty;
            string pic = string.Empty;

            if (userView.photo != null)
            {
                pic = Path.GetFileName(userView.photo.FileName);
                path = Path.Combine(Server.MapPath("/Content/Photos"), pic);
                userView.photo.SaveAs(path);

                using (MemoryStream ms = new MemoryStream()) {

                    userView.photo.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();

                }
            };


            // create user
            var user = new User
            {
                userName = userView.userName,
                firtsName = userView.firtsName,
                lastName = userView.lastName,
                phone = userView.phone,
                address = userView.address,
                grade = userView.grade,
                group = userView.group,
                photo = pic == string.Empty ? string.Empty : string.Format("/Content/Photos/{0}", pic),
            };

            // save record
            db.Users.Add(user);
            try
            {
                db.SaveChanges();
                this.CreateSAPUser(userView);

            } catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("UsernameIndex"))
                {
                    ViewBag.Error = "The email has been register";
                } else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(userView);
            }
            
            return RedirectToAction("Index");


        }

        private void CreateSAPUser(UserView userView)
        {
            // user managment
            var userContext = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(userContext));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(userContext));

            // create role
            string roleName = "User";

            // Check if the roleName already exists
            if (!roleManager.RoleExists(roleName))
            {
                roleManager.Create(new IdentityRole(roleName));
            }

            // create de ASP Net User
            var userASP = new ApplicationUser
            {
                UserName = userView.userName,
                PhoneNumber = userView.phone,
                Email = userView.userName,
            };

            userManager.Create(userASP, userASP.UserName);

            // add user to role
            userASP = userManager.FindByName(userView.userName);
            userManager.AddToRole(userASP.Id, "User");
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            // create userview
            var userView = new UserView
            {
                userId = user.userId,
                userName = user.userName,
                firtsName = user.firtsName,
                lastName = user.lastName,
                phone = user.phone,
                address = user.address,
                grade = user.grade,
                group = user.group,
            };

            return View(userView);
        }

        // POST: Users/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(UserView userView)
        {
            if (!ModelState.IsValid)
            {
                return View(userView);
                
            }

            // upload image
            string path = string.Empty;
            string pic = string.Empty;

            if (userView.photo != null)
            {
                pic = Path.GetFileName(userView.photo.FileName);
                path = Path.Combine(Server.MapPath("/Content/Photos"), pic);
                userView.photo.SaveAs(path);

                using (MemoryStream ms = new MemoryStream())
                {

                    userView.photo.InputStream.CopyTo(ms);
                    byte[] array = ms.GetBuffer();

                }
            };
            var user = db.Users.Find(userView.userId);

            // create user
            user.userName = userView.userName;
            user.firtsName = userView.firtsName;
            user.lastName = userView.lastName;
            user.phone = userView.phone;
            user.address = userView.address;
            user.grade = userView.grade;
            user.group = userView.group;
            if (!string.IsNullOrEmpty(pic))
            {
                user.photo = pic == string.Empty ? string.Empty : string.Format("/Content/Photos/{0}", pic);
            }
            

            db.Users.Add(user);

            try
            {
                db.SaveChanges();

            }
            catch (Exception ex)
            {
                if (ex.InnerException != null &&
                    ex.InnerException.InnerException != null &&
                    ex.InnerException.InnerException.Message.Contains("UsernameIndex"))
                {
                    ViewBag.Error = "The email has been register";
                }
                else
                {
                    ViewBag.Error = ex.Message;
                }
                return View(userView);
            }

            return RedirectToAction("Index");


        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
