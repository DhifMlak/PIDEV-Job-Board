using JobsForCoders.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using System.Text;

namespace JobsForCoders.Controllers
{
    public class ReportsEmployerController : Controller
    {
        private jobsforcodersEntities db = new jobsforcodersEntities();

        // GET: ReportsEmployer
        public ActionResult Index()
        {
            return View();
        }
       public ActionResult Export(Report report)
        {
            if (report.ExportTo == "pdf")
            {
                return ExportToPdf(report);
            }

            return ExportToWord(report);
        }

        public ActionResult Report(Report report)
        {
            Report employerReport = GenerateDataReport(report);

            return View(employerReport);
        }

        //public Report GenerateDataReport(Report report)
        //{
        //    //This is the Method to grab the data.
        //    var employerId = (int)Session["LoggedUserID"];

        //    var employer = db.Entreprises.FirstOrDefault(e => e.EntrepriseID == employerId);

        //    var ApplicantsQuery = db.Applications
        //        .Include(a => a.Offre)
        //        .Include(a => a.Candidat)
        //        .Where(a => a.EntrepriseID == employerId);

        //    if (report.Candidat != null)
        //    {
        //        ApplicantsQuery = ApplicantsQuery.Where(a => a.Offre.Candidat.Name.Contains(report.Candidat));
        //    }
        //    if (report.Position != null)
        //    {
        //        ApplicantsQuery = ApplicantsQuery.Where(a => a.Offre.Position.Contains(report.Position));
        //    }
        //    if (report.DateFrom > DateTime.MinValue)
        //    {
        //        ApplicantsQuery = ApplicantsQuery.Where(a => a.Application_Date >= report.DateFrom);
        //    }
        //    if (report.DateTo > DateTime.MinValue)
        //    {
        //        ApplicantsQuery = ApplicantsQuery.Where(a => a.Application_Date <= report.DateTo);
        //    }
        //    var applicants = ApplicantsQuery.ToList();

        //    var reportData = new Report()
        //    {
        //        Applications = applicants,
        //        Entreprise = employer,
        //        DateFrom = report.DateFrom,
        //        DateTo = report.DateTo
        //    };
        //    return reportData;
        //}

        public Report GenerateDataReport(Report report)
        {
            //This is the Method to grab the data.
            var employerId = (int)Session["LoggedUserID"];

            var employer = db.Entreprises.FirstOrDefault(e => e.EntrepriseID == employerId);

            var ApplicantsQuery = db.Applications
            .Include(a => a.Offre)
            .Include(a => a.Offre)
            .Include(a => a.Offre.Entreprise)
            .Where(a => a.Offre.Entreprise.EntrepriseID == employerId);

            if (report.JobSeekerName != null)
            {
                ApplicantsQuery = ApplicantsQuery.Where(a => a.Candidat.Name.Contains(report.JobSeekerName));
            }
            if (report.Position != null)
            {
                ApplicantsQuery = ApplicantsQuery.Where(a => a.Offre.Position.Contains(report.Position));
            }
            if (report.DateFrom > DateTime.MinValue)
            {
                ApplicantsQuery = ApplicantsQuery.Where(a => a.Application_Date >= report.DateFrom);
            }
            if (report.DateTo > DateTime.MinValue)
            {
                ApplicantsQuery = ApplicantsQuery.Where(a => a.Application_Date <= report.DateTo);
            }
            var applicants = ApplicantsQuery.ToList();

            var reportData = new Report()
            {
                Applications = applicants,
                Entreprise = employer,
                DateFrom = report.DateFrom,
                DateTo = report.DateTo
            };
            return reportData;
        }

        //PDF and Word Generator
        public string RenderViewToString(string viewName, object model)
        {
            if (string.IsNullOrEmpty(viewName))
                viewName = ControllerContext.RouteData.GetRequiredString("action");

            ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                return sw.GetStringBuilder().ToString();
            }
        }

        public ActionResult ExportToPdf(Report report)
        {
            var htmlToConvert = RenderViewToString("Report", GenerateDataReport(report));
            var htmlToPdfConverter = new NReco.PdfGenerator.HtmlToPdfConverter();
            htmlToPdfConverter.PdfToolPath = Server.MapPath("~/Uploads/Reports/");
            var pdfBytes = htmlToPdfConverter.GeneratePdf(htmlToConvert);
            FileResult fileResult = new FileContentResult(pdfBytes, "application/pdf");
            fileResult.FileDownloadName = "Report.pdf";
            return fileResult;
        }

        public ActionResult ExportToWord(Report report)
        {
            var htmlToConvert = RenderViewToString("Report", GenerateDataReport(report));
            new List<string>().Add(htmlToConvert);
            HttpContext.Response.Clear();
            HttpContext.Response.Charset = "utf-8";
            HttpContext.Response.ContentType = "application/msword";
            const string strFileName = "Report.doc";
            HttpContext.Response.AddHeader("Content-Disposition", "inline;filename=" + strFileName);
            var strHtmlContent = new StringBuilder();
            strHtmlContent.Append(htmlToConvert);

            HttpContext.Response.Write(strHtmlContent);

            HttpContext.Response.End();
            HttpContext.Response.Flush();

            return null;
        }
    }
}