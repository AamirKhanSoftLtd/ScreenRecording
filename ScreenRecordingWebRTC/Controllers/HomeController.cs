using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ScreenRecordingWebRTC.Models;
using System;
using System.Diagnostics;
using System.IO;

namespace ScreenRecordingWebRTC.Controllers
{
    public class HomeController : Controller
    {


        private readonly IWebHostEnvironment _environment;

      

        private readonly ILogger<HomeController> _logger;



        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment environment)
        {
            _environment = environment ?? throw new ArgumentNullException(nameof(environment));
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }


        [RequestFormLimits(MultipartBodyLengthLimit =409715200)]
        [RequestSizeLimit(409715200)]
        [HttpPost]
        public ActionResult PostRecordedAudioVideo()
        {

            //foreach (string upload in Request.Files)
            //{
            //    var path = AppDomain.CurrentDomain.BaseDirectory + @"uploads\";
            //    var file = Request.Files[upload];
            //    if (file == null) continue;
            //    file.SaveAs(Path.Combine(path, Request.Form[0]));
            //}
            //return Json(Request.Form[0]);

           // IFormFile file = Request.Form.Files[0];
            string ext = ".mp4";
            string uno ="ClassRecording" ;
            int counter = 0;
           
                foreach (var upload in Request.Form.Files)
                {

                string filepath = $"uploads/{uno}{ext}";
                var uploads = Path.Combine(_environment.WebRootPath, filepath);
                var file = Request.Form.Files[0];
                using (var stream = new FileStream(uploads, FileMode.Create))
                    {
                        file.CopyToAsync(stream);
                    } 
                }
            return Json(Request.Form.Files[0]);


        }

        // ---/RecordRTC/DeleteFile
        [HttpPost]
        public ActionResult DeleteFile()
        {
            var fileUrl = AppDomain.CurrentDomain.BaseDirectory + "uploads/" + Request.Form["delete-file"] + ".webm";
            new FileInfo(fileUrl).Delete();
            return Json(true);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
