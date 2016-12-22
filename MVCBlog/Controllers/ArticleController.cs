using MVCBlog.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MVCBlog.Controllers
{
    public class ArticleController : Controller
    {
        // GET: Article
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()

        {
            using (var database = new BlogDbContext())

            {
                var articles = database.Articles
                .Include(a => a.Author)
                .ToList();

                return View(articles);
            }                         
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var database = new BlogDbContext();

            var article = database.Articles
                .Where(a => a.Id == id)
                .First();

            return View(article);
        }

        //Get
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Article article)
        {
            using (var db = new BlogDbContext())
            {
                var user = db.Users.FirstOrDefault(u => u.UserName.Equals(this.User.Identity.Name));

                article.AuthorId = user.Id;
                db.Articles.Add(article);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }

        //get
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);

                if (article == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View(article);
            } 
                       
        }

        //post
        [HttpPost]
        public ActionResult Edit(Article article)
        {
            if (ModelState.IsValid)

            {
                using (var db = new BlogDbContext())
                {
                    db.Entry(article).State = EntityState.Modified;
                    db.SaveChanges();

                    return RedirectToAction("List");
                }
            }

            return View(article);
        }


        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);

                if (article == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                return View(article);
            }            
        }

        //post
        [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            using (var db = new BlogDbContext())
            {
                var article = db.Articles.FirstOrDefault(a => a.Id == id);

                if (article == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }

                db.Articles.Remove(article);
                db.SaveChanges();

                return RedirectToAction("List");
            }
        }

    }
}