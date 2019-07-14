using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobsForCoders.Models;
using System.Threading.Tasks;
using System.Net.Mail;
using System.IO;
using WebGrease.Activities;

namespace JobsForCoders.Controllers
{
    public class EntreprisesController : Controller
    {
        private jobsforcodersEntities db = new jobsforcodersEntities();

        // GET: Entreprise/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprises.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // GET: Entreprise/Create
        public ActionResult Create()
        {
            return View();
        }

        public enum FileType
        {
            Avatar = 1, Photo
        }

        // POST: Entreprise/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "EntrepriseID,Name,Address,City,Email,Password,Cellphone,Website")] Entreprise entreprise, HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                

                if (Logo != null && Logo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Logo.SaveAs(path);

                    entreprise.Logo = fileName;
                }

                db.Entreprises.Add(entreprise);
                db.SaveChanges();

                ////Determine the Cellphone Operator
                //var operatorValue = entreprise.Operator;
                //if (operatorValue == "Virgin")
                //{
                //    entreprise.Cellphone = entreprise.Cellphone + "@vmobile.ca";
                //}
                //if (operatorValue == "Fido" || operatorValue == "Microcell")
                //{
                //    entreprise.Cellphone = entreprise.Cellphone + "@fido.ca";
                //}
                //if (operatorValue == "Rogers")
                //{
                //    entreprise.Cellphone = entreprise.Cellphone + "@fido.ca";
                //}
                //if (operatorValue == "Bell")
                //{
                //    entreprise.Cellphone = entreprise.Cellphone + "@txt.bell.ca";
                //}
                //if (operatorValue == "Telus")
                //{
                //    entreprise.Cellphone = entreprise.Cellphone + "@msg.telus.ca";
                //}

                ////Email
                //var body = "<p>Email From: {0} ({1})</p><p>Message:</p><p>{2}</p>";
                //var message = new MailMessage();
                //message.To.Add(new MailAddress(entreprise.Email));  // replace with valid value 
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
                //tmessage.To.Add(new MailAddress(entreprise.Cellphone));  // replace with valid value 
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


                return RedirectToAction("EntrepriseLogin", "Home");
            }

            return View(entreprise);
        }

        // GET: Entreprise/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprises.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }



        public ActionResult Follow(int? EntrepriseID)
        {
            var jobSeekerId = (int)Session["LoggedUserID"];

            var candidat = db.Candidats.Find(jobSeekerId);
            var entreprise = db.Entreprises.Find(EntrepriseID);
            candidat.Entreprises.Add(entreprise);
            entreprise.Candidats.Add(candidat);
            db.SaveChanges();

            return RedirectToAction("Index", "Home");

        }


        // POST: Entreprise/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EntrepriseID,Name,Address,City,Email,Password,Cellphone,Website")] Entreprise entreprise, HttpPostedFileBase Logo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(entreprise).State = EntityState.Modified;


                if (Logo != null && Logo.ContentLength > 0)
                {
                    var fileName = Path.GetFileName(Logo.FileName);
                    var path = Path.Combine(Server.MapPath("~/Uploads/"), fileName);
                    Logo.SaveAs(path);

                    entreprise.Logo = fileName;
                }
                else
                {
                    db.Entry(entreprise).Property(m => m.Logo).IsModified = false;
                }

                ModelState.Clear();

                db.SaveChanges();

                return RedirectToAction("Index", "Home");
            }
            return View(entreprise);


        }



        // GET: Entreprise/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Entreprise entreprise = db.Entreprises.Find(id);
            if (entreprise == null)
            {
                return HttpNotFound();
            }
            return View(entreprise);
        }

        // POST: Entreprise/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Entreprise entreprise = db.Entreprises.Find(id);
            db.Entreprises.Remove(entreprise);
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
