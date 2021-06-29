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
        //[RequestFormLimits(MultipartBodyLengthLimit = 409715200)]
        //[RequestSizeLimit(409715200)]
        [HttpPost]
        public IActionResult PostRecordedAudioVideo()
        {

            foreach (var fromFile in Request.Form.Files)
            {
                var file = Request.Form.Files[0];
                var actualFileName = Request.Form["actualFileName"];
                string filepath = @"uploads\" + actualFileName;
                var uploads = Path.Combine(_environment.WebRootPath, filepath);

                using (var stream = System.IO.File.Create(uploads))
                {
                    file.CopyTo(stream);
                }
            }
            return Json(Request.Form.Files[0]);

        }
        // ---/RecordRTC/DeleteFile
        [HttpPost]
        public ActionResult DeleteFile()
        {
            var fileUrl = AppDomain.CurrentDomain.BaseDirectory + "uploads/" + Request.Form["delete-file"] + ".webm";
            //new FileInfo(fileUrl).Delete();
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
