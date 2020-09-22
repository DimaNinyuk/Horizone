using HorizonSite.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using MimeKit;
using System.Security.Cryptography.Xml;
using PagedList;

namespace HorizonSite.Controllers
{
    public class HomeController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Index(int? page)
        {
            var model = new HomeIndexViewModels
            {
                LastThreeFilms = db.Films.Include(f => f.Genre).OrderByDescending(d => d.DateStart).Take(3).ToList(),
                RentedFilms = db.Films.Where(w => w.DateEnd > DateTime.Now).ToList(),
                LastTwoReviews = db.Reviews.OrderByDescending(d => d.Date).Take(2).ToPagedList(page ?? 1, 3),
                listNameGenre = db.Genres.ToList(),
                listDateSession = db.Sessions.ToList(),
            };
            
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string SNP, string Email, string Text)
        {
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true; //тело сообщения в формате HTML
            message.From = new MailAddress("2020petrpetrov2020@gmail.com", "Клиент " + SNP + ""); //отправитель сообщения
            message.To.Add("2020petrpetrov2020@gmail.com"); //адресат сообщения
            message.Subject = "Сообщение от " + Email + ""; //тема сообщения
            message.Body = "<div class=\"testimonial\"> <p>  " + Text + " </ p ></div>"; //тело сообщения
                                                                                                              //message.Attachments.Add(new Attachment("... путь к файлу ...")); //добавить вложение к письму при необходимости

            using (System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient("smtp.gmail.com")) //используем сервера Google
            {
                client.Credentials = new NetworkCredential("2020petrpetrov2020@gmail.com", "2020qwertyuiop"); //логин-пароль от аккаунта
                client.Port = 587; //порт 587 либо 465
                client.EnableSsl = true; //SSL обязательно
                client.Send(message);

            }
            Message sendM = new Message();
            sendM.SNP = SNP;
            sendM.Email = Email;
            sendM.Text = Text;

            if (ModelState.IsValid)
                {

                    db.Messages.Add(sendM);
                    db.SaveChanges();
                

                    return RedirectToAction("Index");

                }
            return View(sendM);
            
        }
        public ActionResult News()
        {
            var model = new HomeIndexViewModels
            {
                RentedFilms = db.Films.Where(w => w.DateEnd > DateTime.Now).Take(5).ToList(),
                CountReview = db.Reviews
            };
            return View(model);
        }
        public ActionResult Single(int? id, int? page)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var model = new HomeIndexViewModels
            {
                AllFilms = db.Films.Where(w=>w.FilmId == id),
                CountReview = db.Reviews,
                producersFilm = db.FilmsProducers,
                listNameGenre = db.Genres.ToList(),
                listDateSession = db.Sessions.ToList(),
                actorFilm = db.FilmsActors,
                LastTwoReviews = db.Reviews.Where(w=>w.FilmId == id).OrderByDescending(d => d.Date).ToPagedList(page ?? 1, 2)
                
            };
            if (model == null)
            {
                return HttpNotFound();
            }
            ViewBag.FilmId = id;
            return View(model);
            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Single([Bind(Include = "ReviewId,Email,Date,Text,FilmId,AdminId")] Review review)
        {
            review.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Reviews.Add(review);
                db.SaveChanges();
                return RedirectToAction("Single");
            }

            //ViewBag.AdminId = new SelectList(db.ApplicationUsers, "Id", "Email", review.AdminId);
            //ViewBag.FilmId = new SelectList(db.Films, "FilmId", "NameFilm", review.FilmId);
            return View(review);
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Admin()
        {
            return View();
        }

        public ActionResult Tickets(int idsession)
        {
            var session = db.Sessions.Where(s=>s.SessionId==idsession).First();
            return View(session);
        }

        // POST: Home/Buy
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Buy(string sessionid, string[] places)
        {
            List<Buying> newbuys = new List<Buying>();
          foreach (var p in places)
            {
                var row = p.Split(':')[0];
                var seat = p.Split(':')[1];

                Buying buying = new Buying();
                buying.Date = DateTime.Now;
                buying.SessionId = int.Parse(sessionid);
                buying.SeatId = int.Parse(seat);
                buying.Seat = db.Seats.Where(s => s.SeatId == buying.SeatId).First();
                buying.RowId = int.Parse(row);
                buying.Session = db.Sessions.Where(s=>s.SessionId == buying.SessionId).First();
                db.Buyings.Add(buying);
                db.SaveChanges();
                newbuys.Add(buying);
            }
            return View(newbuys);
        }
    }
}