using Google.Apis.Drive.v3;
using InCodeAuthentication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace InCodeAuthentication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DriveService _driveService;

        public HomeController(
            ILogger<HomeController> logger,
            DriveService driveService)
        {
            this._logger = logger;
            this._driveService = driveService;
        }

        public async Task<IActionResult> Index()
        {
            var request = this._driveService.Files.List();
            request.Q = $"'18Mk_aoN3NjMWGtmsFafWXwIbtMXX9WZq' in parents and mimeType = 'application/vnd.google-apps.folder'";
            request.Fields = "nextPageToken, files(id, name)";

            var results = new Dictionary<string, string>();
            do
            {
                var files = request.Execute();
                foreach (var f in files.Files)
                {
                    results.Add(f.Id, f.Name);
                }
                request.PageToken = files.NextPageToken;
            } while (!string.IsNullOrEmpty(request.PageToken));

            return View();
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