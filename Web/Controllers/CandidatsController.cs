﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsForCoders.Models;
using System.Net.Mail;
using System.IO;
using System.Threading.Tasks;
using WebGrease.Activities;

namespace JobsForCoders.Controllers
{
    public class CandidatsController : Controller
    {
        private jobsforcodersEntities db = new jobsforcodersEntities();

        // GET: JobSeekers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat jobSeeker = db.Candidats.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }






    

        // GET: JobSeekers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: JobSeekers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "JobSeekerID,Name,Surname,Birthdate,Gender,Address,City,Education,Objectives,Introduction,Links,Email,Password,Cellphone,Operator,Buzz_Words")] Candidat jobSeeker, HttpPostedFileBase Photo1, HttpPostedFileBase CV)
        {
            if (ModelState.IsValid)
            {
                if (CV != null && CV.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(CV.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    CV.SaveAs(path);

                    jobSeeker.CV = fileName;
                }

                if (Photo1 != null && Photo1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Photo1.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Photo1.SaveAs(path);

                    jobSeeker.Photo1 = fileName;
                }


                db.Candidats.Add(jobSeeker);
                db.SaveChanges();


                //Determine the Cellphone Operator
                var operatorValue = jobSeeker.Operator;
                if (operatorValue == "Virgin")
                {
                    jobSeeker.Cellphone = jobSeeker.Cellphone + "@vmobile.ca";
                }
                if (operatorValue == "Fido" || operatorValue == "Microcell")
                {
                    jobSeeker.Cellphone = jobSeeker.Cellphone + "@fido.ca";
                }
                if (operatorValue == "Rogers")
                {
                    jobSeeker.Cellphone = jobSeeker.Cellphone + "@fido.ca";
                }
                if (operatorValue == "Bell")
                {
                    jobSeeker.Cellphone = jobSeeker.Cellphone + "@txt.bell.ca";
                }
                if (operatorValue == "Telus")
                {
                    jobSeeker.Cellphone = jobSeeker.Cellphone + "@msg.telus.ca";
                }





                //Email
                //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                //var message = new MailMessage();
                //message.To.Add(new MailAddress(jobSeeker.Email));  // replace with valid value 
                //message.From = new MailAddress("jobforcoders@hotmail.com");  // replace with valid value
                //message.Subject = "Welcome to #JobForCoders!";
                //message.Body = string.Format(body, "JobForCoders", "no-reply@jobforcoders.com", "Registration completed.");
                //message.IsBodyHtml = true;

                //using (var smtp = new SmtpClient())
                //{
                //    var credential = new NetworkCredential
                //    {
                //        UserName = "jobforcoders@hotmail.com",  // replace with valid value
                //        Password = "555555Jc"  // replace with valid value
                //    };
                //    smtp.Credentials = credential;
                //    smtp.Host = "smtp-mail.outlook.com";
                //    smtp.Port = 587;
                //    smtp.EnableSsl = true;
                //    await smtp.SendMailAsync(message);
                //}


                ////SMS
                //var tbody = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                //var tmessage = new MailMessage();
                //tmessage.To.Add(new MailAddress(jobSeeker.Cellphone));  // replace with valid value 
                //tmessage.From = new MailAddress("jobforcoders@hotmail.com");  // replace with valid value
                //tmessage.Subject = "Welcome to #JobForCoders!";
                //tmessage.Body = string.Format(tbody, "JobForCoders", "no-reply@jobforcoders.com", "Registration completed.");
                //tmessage.IsBodyHtml = true;

                //using (var smtp = new SmtpClient())
                //{
                //    var credential = new NetworkCredential
                //    {
                //        UserName = "jobforcoders@hotmail.com",  // replace with valid value
                //        Password = "555555Jc"  // replace with valid value
                //    };
                //    smtp.Credentials = credential;
                //    smtp.Host = "smtp-mail.outlook.com";
                //    smtp.Port = 587;
                //    smtp.EnableSsl = true;
                //    await smtp.SendMailAsync(tmessage);
                //}


                //This will load all Employers where the Buzz_Words contains jobseekers Buzz_Words

                var buzzWords = jobSeeker.Buzz_Words.Split(',').ToList();
                //var EmployerList = db.Entreprises.Where(e => e.Buzz_Words.Contains(jobSeeker.Buzz_Words));


                //foreach (var employer in EmployerList)
                //{
                //    //Determine the Cellphone Operator
                //    var operatorValueRegister = employer.Operator;
                //    if (operatorValueRegister == "Virgin")
                //    {
                //        employer.Cellphone = employer.Cellphone + "@vmobile.ca";
                //    }
                //    if (operatorValueRegister == "Fido" || operatorValueRegister == "Microcell")
                //    {
                //        employer.Cellphone = employer.Cellphone + "@fido.ca";
                //    }
                //    if (operatorValueRegister == "Rogers")
                //    {
                //        employer.Cellphone = employer.Cellphone + "@fido.ca";
                //    }
                //    if (operatorValueRegister == "Bell")
                //    {
                //        employer.Cellphone = employer.Cellphone + "@txt.bell.ca";
                //    }
                //    if (operatorValueRegister == "Telus")
                //    {
                //        employer.Cellphone = employer.Cellphone + "@msg.telus.ca";
                //    }

                //    //Email
                //    var secondbody = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                //    var secondmessage = new MailMessage();
                //    secondmessage.To.Add(new MailAddress(employer.Email));  // replace with valid value 
                //    secondmessage.From = new MailAddress("jobforcoders@hotmail.com");  // replace with valid value
                //    secondmessage.Subject = "News from #JobForCoders!";
                //    secondmessage.Body = string.Format(secondbody, "JobForCoders", "no-reply@jobforcoders.com", "A new interesting applicand has just registered.");
                //    secondmessage.IsBodyHtml = true;

                //    using (var smtp = new SmtpClient())
                //    {
                //        var credential = new NetworkCredential
                //        {
                //            UserName = "jobforcoders@hotmail.com",  // replace with valid value
                //            Password = "555555Jc"  // replace with valid value
                //        };
                //        smtp.Credentials = credential;
                //        smtp.Host = "smtp-mail.outlook.com";
                //        smtp.Port = 587;
                //        smtp.EnableSsl = true;
                //        await smtp.SendMailAsync(secondmessage);
                //    }


                //    //SMS
                //    var tsecondbody = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                //    var tsecondmessage = new MailMessage();
                //    tsecondmessage.To.Add(new MailAddress(employer.Cellphone));  // replace with valid value 
                //    tsecondmessage.From = new MailAddress("jobforcoders@hotmail.com");  // replace with valid value
                //    tsecondmessage.Subject = "Welcome to #JobForCoders!";
                //    tsecondmessage.Body = string.Format(tsecondbody, "JobForCoders", "no-reply@jobforcoders.com", "A new interesting applicand has just registered.");
                //    tsecondmessage.IsBodyHtml = true;

                //    using (var smtp = new SmtpClient())
                //    {
                //        var credential = new NetworkCredential
                //        {
                //            UserName = "jobforcoders@hotmail.com",  // replace with valid value
                //            Password = "555555Jc"  // replace with valid value
                //        };
                //        smtp.Credentials = credential;
                //        smtp.Host = "smtp-mail.outlook.com";
                //        smtp.Port = 587;
                //        smtp.EnableSsl = true;
                //        await smtp.SendMailAsync(tsecondmessage);
                //    }
                //}


                return RedirectToAction("Login" , "Home");
            }

            return View(jobSeeker);
        }

        // GET: JobSeekers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat jobSeeker = db.Candidats.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeekers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "JobSeekerID,Name,Surname,Birthdate,Gender,Address,City,Education,Objectives,Introduction,Links,Email,Password,Cellphone,Operator,Buzz_Words")] Candidat jobSeeker, HttpPostedFileBase Photo1, HttpPostedFileBase CV)
        {
            if (ModelState.IsValid)
            {
                db.Entry(jobSeeker).State = EntityState.Modified;

                if (CV != null && CV.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(CV.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    CV.SaveAs(path);

                    jobSeeker.CV = fileName;
                }
                else
                {
                    db.Entry(jobSeeker).Property(m => m.CV).IsModified = false;
                }

                if (Photo1 != null && Photo1.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Photo1.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Photo1.SaveAs(path);

                    jobSeeker.Photo1 = fileName;
                }
                else
                {
                    db.Entry(jobSeeker).Property(m => m.Photo1).IsModified = false;
                }

                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            return View(jobSeeker);
        }

        // GET: JobSeekers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Candidat jobSeeker = db.Candidats.Find(id);
            if (jobSeeker == null)
            {
                return HttpNotFound();
            }
            return View(jobSeeker);
        }

        // POST: JobSeekers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Candidat jobSeeker = db.Candidats.Find(id);
            db.Candidats.Remove(jobSeeker);
            db.SaveChanges();
            return RedirectToAction("Index", "Home");
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
