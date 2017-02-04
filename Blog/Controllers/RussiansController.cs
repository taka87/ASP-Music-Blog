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
    public class RussiansController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Russians
        public ActionResult Index()
        {
            var postsWithAuthors =
                db.Posts
               .Include(p => p.Author)
               .ToList();
            return View(db.Russians.ToList());
        }

        // GET: Russians/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Russian russian = db.Russians.Find(id);
            if (russian == null)
            {
                return HttpNotFound();
            }
            return View(russian);
        }

        // GET: Russians/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Russians/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] Russian russian)
        {
            if (ModelState.IsValid)
            {
                db.Russians.Add(russian);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(russian);
        }

        // GET: Russians/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Russian russian = db.Russians.Find(id);
            if (russian == null)
            {
                return HttpNotFound();
            }
            return View(russian);
        }

        // POST: Russians/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date")] Russian russian)
        {
            if (ModelState.IsValid)
            {
                db.Entry(russian).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(russian);
        }

        // GET: Russians/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Russian russian = db.Russians.Find(id);
            if (russian == null)
            {
                return HttpNotFound();
            }
            return View(russian);
        }

        // POST: Russians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Russian russian = db.Russians.Find(id);
            db.Russians.Remove(russian);
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
