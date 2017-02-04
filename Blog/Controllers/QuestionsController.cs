using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Extensions;
using Blog.Models;


namespace Blog.Controllers
{
    [ValidateInput(false)]

    public class QuestionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Metal()
        {
            return View();
        }

        public ActionResult Rock()
        {
            return View();
        }

        public ActionResult ChooseStyle()
        {
            return View();
        }

        // GET: Questions
        //        public ActionResult Index()
        //        {
        //            var postsWithAuthors =
        //               db.Questions
        //               .Include(p => p.Author)
        //               .ToList();
        //            return View(postsWithAuthors);
        //        }

        public ViewResult Index(string sortOrder)
        {
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewBag.DateSortParm = sortOrder == "Author" ? "date_desc" : "Author";
            var question = from s in db.Questions.Include(p => p.Author)
                           select s;

            switch (sortOrder)
            {
                case "category_desc":
                    question = question.OrderByDescending(s => s.Category);
                    break;
                case "date_desc":
                    question = question.OrderByDescending(s => s.Author);
                    break;
                default:
                    question = question.OrderBy(s => s.Category);
                    break;
            }

            return View(question.ToList());
        }

        // GET: Questions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // GET: Questions/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Questions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Category")] Question question)
        {
            if (ModelState.IsValid)
            {
                question.Author = db.Users.FirstOrDefault(u => u.UserName == User.Identity.Name);
                db.Questions.Add(question);
                db.SaveChanges();
                this.AddNotification("Question Asked.", NotificationType.INFO);
                return RedirectToAction("Index");
            }

            return View(question);
        }

        // GET: Questions/Edit/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Administrators")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date,Category")] Question question)
        {
            if (ModelState.IsValid)
            {
                db.Entry(question).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(question);
        }

        // GET: Questions/Delete/5
        [Authorize(Roles = "Administrators")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Question question = db.Questions.Find(id);
            if (question == null)
            {
                return HttpNotFound();
            }
            return View(question);
        }

        // POST: Questions/Delete/5
        [Authorize(Roles = "Administrators")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Question question = db.Questions.Find(id);
            db.Questions.Remove(question);
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
