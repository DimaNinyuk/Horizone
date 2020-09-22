using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HorizonSite.Models;
using System.IO;
using System.Threading.Tasks;

namespace HorizonSite.Controllers
{
    public class FilmsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Films
        public ActionResult Index()
        {
            var films = db.Films.Include(f => f.Genre);
            return View(films.ToList());
        }

        // GET: Films/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // GET: Films/Create
        public ActionResult Create()
        {
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "NameGenre");
            return View();
        }

        // POST: Films/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "FilmId,NameFilm,Duration,DateStart,DateEnd,Company,Picture,GenreId,IMDBRatffing,RottenRating,LinkTriller")] Film film, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid && upload != null)
            {
                //создания и загрузка файла
                string dirSourse = System.IO.Path.GetFileName(upload.FileName);
                string dirnewSourse = Server.MapPath("~/Resourses/Films" + dirSourse);
                string path = Server.MapPath("~/Resourses/Films");
                string NameFile;
                Directory.CreateDirectory(path);

                if (System.IO.File.Exists(dirnewSourse))
                {
                    int i = 1;
                    string newname;
                    do
                    {
                        i++;
                        newname = Path.GetFileNameWithoutExtension(dirnewSourse)
                               + i + Path.GetExtension(dirnewSourse); //Новое имя файла
                    }
                    while (System.IO.File.Exists(path + "/" + newname));

                    upload.SaveAs(path + "/" + newname);  //Сохранить файл с новым именем
                    NameFile = newname;
                }
                else
                {
                    //Такого файла не существует в конечной папке, можно копировать
                    upload.SaveAs(path + "/" + dirSourse); //Сохранить файл
                    NameFile = dirSourse;
                }

                film.Picture = NameFile;
                db.Films.Add(film);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

           
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "NameGenre", film.GenreId);
            return View(film);

         
        }

        // GET: Films/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "NameGenre", film.GenreId);
            return View(film);
        }

        // POST: Films/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "FilmId,NameFilm,Duration,DateStart,DateEnd,Company,Picture,GenreId,IMDBRatffing,RottenRating,LinkTriller")] Film film, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                var thisservice = db.Films.Find(film.FilmId);
                thisservice.NameFilm = film.NameFilm;
                thisservice.Duration = film.Duration;
                thisservice.DateStart = film.DateStart;
                thisservice.DateEnd = film.DateEnd;
                thisservice.Company = film.Company;
                //thisservice.Picture = film.Picture;
                thisservice.GenreId = film.GenreId;
                thisservice.IMDBRatffing = film.IMDBRatffing;
                thisservice.RottenRating = film.RottenRating;
                thisservice.LinkTriller = film.LinkTriller;

                if (upload != null)
                {
                    //создания и загрузка файла
                    string dirSourse = System.IO.Path.GetFileName(upload.FileName);
                    string dirnewSourse = Server.MapPath("~/Resourses/Films" + dirSourse);
                    string path = Server.MapPath("~/Resourses/Films");
                    string NameFile;
                    Directory.CreateDirectory(path);

                    if (System.IO.File.Exists(dirnewSourse))
                    {
                        int i = 1;
                        string newname;
                        do
                        {
                            i++;
                            newname = Path.GetFileNameWithoutExtension(dirnewSourse)
                                   + i + Path.GetExtension(dirnewSourse); //Новое имя файла
                        }
                        while (System.IO.File.Exists(path + "/" + newname));

                        upload.SaveAs(path + "/" + newname);  //Сохранить файл с новым именем
                        NameFile = newname;
                    }
                    else
                    {
                        //Такого файла не существует в конечной папке, можно копировать
                        upload.SaveAs(path + "/" + dirSourse); //Сохранить файл
                        NameFile = dirSourse;
                    }

                    thisservice.Picture = NameFile;
                }
                db.Entry(thisservice).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "NameGenre", film.GenreId);
            return View(film);
            
        }

        // GET: Films/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Film film = db.Films.Find(id);
            if (film == null)
            {
                return HttpNotFound();
            }
            return View(film);
        }

        // POST: Films/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Film film = db.Films.Find(id);
            db.Films.Remove(film);
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
