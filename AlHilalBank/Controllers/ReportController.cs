using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AlHilalBank.Models;
using ExcelDataReader;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AlHilalBank.Controllers
{
    public class ReportController : Controller
    {
        private ApplicationContext db;
        public ReportController(ApplicationContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Index(List<Report> reports = null)
        {
          

            return View(reports);
        }
        [HttpPost]
        public IActionResult Index(IFormFile file, [FromServices] IHostingEnvironment hostingEnviroment)
        {

            string fileName = $"{hostingEnviroment.WebRootPath }\\files\\{file.FileName}";
            using (FileStream fileStream = System.IO.File.Create(fileName))
            {
                file.CopyTo(fileStream);
                fileStream.Flush();
            }
            var reports = GetReportList(file.FileName);
            db.ReportsDB.AddRange(reports);
    
            db.SaveChanges();
            return Index(reports);

        }
        private List<Report> GetReportList(string fName)
        {
            List<Report> reports = new List<Report>();
            var fileName = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\files"}" + "\\" + fName;
            System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
            using (var stream = System.IO.File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    CultureInfo provider = CultureInfo.InvariantCulture;
                    while (reader.Read())
                    {

                        reports.Add(new Report()
                        {
                            RNUM = Convert.ToInt32(reader.GetValue(0).ToString()),
                            REPORT_DATE = reader.GetValue(1).ToString(),
                            CREADITOR_ID = Convert.ToInt32(reader.GetValue(2).ToString()),
                            GL_NUMBER = reader.GetValue(3).ToString(),
                            DESC = reader.GetValue(4).ToString(),
                            RESIDENT_CODE = reader.GetValue(5).ToString(),
                            SECTOR_ECO = reader.GetValue(6).ToString(),
                            CURR_CODE = reader.GetValue(7).ToString(),
                            AMOUNT_LCY = Convert.ToInt32(reader.GetValue(8).ToString())

                        });

                    }

                }
            }

            return reports;
        }
    }
}
