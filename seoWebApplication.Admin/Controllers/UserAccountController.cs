using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using seoWebApplication.Data;
using seoWebApplication.Common.Security;

namespace seoWebApplication.Controllers
{
    public class UserAccountController : Controller
    {
        private SeoWebAppEntities db = new SeoWebAppEntities();

        // GET: /UserAccount/
        public ActionResult Index()
        {
            return View(db.UserAccounts.ToList());
        }

        // GET: /UserAccount/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount useraccount = db.UserAccounts.Find(id);
            if (useraccount == null)
            {
                return HttpNotFound();
            }
            return View(useraccount);
        }

        // GET: /UserAccount/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /UserAccount/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="UserAccountId,IsActive,AccountName,FirstName,LastName,Email,Password,webstore_id,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] UserAccount useraccount)
        {
            string pw = phasher.Hash(useraccount.Password);

            if (ModelState.IsValid)
            {
                useraccount.Password = pw;
                db.UserAccounts.Add(useraccount);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(useraccount);
        }

        // GET: /UserAccount/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount useraccount = db.UserAccounts.Find(id);
            if (useraccount == null)
            {
                return HttpNotFound();
            }
            return View(useraccount);
        }

        // POST: /UserAccount/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="UserAccountId,IsActive,AccountName,FirstName,LastName,Email,Password,webstore_id,InsertDate,InsertENTUserAccountId,UpdateDate,UpdateENTUserAccountId,Version")] UserAccount useraccount)
        {
            string pw = phasher.Hash(useraccount.Password);
            if (pw != useraccount.Password)
            {            
                useraccount.Password = pw;
            }
            if (ModelState.IsValid)
            {
                db.Entry(useraccount).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(useraccount);
        }

        // GET: /UserAccount/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserAccount useraccount = db.UserAccounts.Find(id);
            if (useraccount == null)
            {
                return HttpNotFound();
            }
            return View(useraccount);
        }

        // POST: /UserAccount/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            UserAccount useraccount = db.UserAccounts.Find(id);
            db.UserAccounts.Remove(useraccount);
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
