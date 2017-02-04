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
    public class LatinPagesController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        
        public ActionResult Index()
        {
            var discussions = db.LatinPages.Include(d => d.Author).OrderByDescending(d => d.Date).Take(2);
            return View(discussions.ToList());
        }

        public ActionResult Discussion()
        {
            var discussionsWithAuthors = db.LatinPages.Include(d => d.Author).ToList();
            return View(discussionsWithAuthors);
        }

        public ActionResult Singers()
        {
            return View();
        }
        
        [Authorize]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatinPage latinPage = db.LatinPages.Find(id);
            if (latinPage == null)
            {
                return HttpNotFound();
            }
            return View(latinPage);
        }
        
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: LatinPages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Body,Date")] LatinPage latinPage)
        {
            if (ModelState.IsValid)
            {
                db.LatinPages.Add(latinPage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(latinPage);
        }
        
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatinPage latinPage = db.LatinPages.Find(id);
            if (latinPage == null)
            {
                return HttpNotFound();
            }
            return View(latinPage);
        }

        // POST: LatinPages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Body,Date")] LatinPage latinPage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(latinPage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(latinPage);
        }

        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            LatinPage latinPage = db.LatinPages.Find(id);
            if (latinPage == null)
            {
                return HttpNotFound();
            }
            return View(latinPage);
        }

        // POST: LatinPages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            LatinPage latinPage = db.LatinPages.Find(id);
            db.LatinPages.Remove(latinPage);
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
