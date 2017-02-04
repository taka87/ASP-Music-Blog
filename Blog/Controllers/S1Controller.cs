using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Blog.Models;

namespace Blog.Controllers
{
    public class S1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: S1
        public ActionResult Index()
        {
            var postsWithAuthors =
               db.Posts
               .Include(p => p.Author)
               .ToList();
            return View(db.S1.ToList());
        }

        // GET: S1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S1 s1 = db.S1.Find(id);
            if (s1 == null)
            {
                return HttpNotFound();
            }
            return View(s1);
        }

        // GET: S1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: S1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] S1 s1)
        {
            if (ModelState.IsValid)
            {
                db.S1.Add(s1);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(s1);
        }

        // GET: S1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S1 s1 = db.S1.Find(id);
            if (s1 == null)
            {
                return HttpNotFound();
            }
            return View(s1);
        }

        // POST: S1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date")] S1 s1)
        {
            if (ModelState.IsValid)
            {
                db.Entry(s1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(s1);
        }

        // GET: S1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            S1 s1 = db.S1.Find(id);
            if (s1 == null)
            {
                return HttpNotFound();
            }
            return View(s1);
        }

        // POST: S1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            S1 s1 = db.S1.Find(id);
            db.S1.Remove(s1);
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
