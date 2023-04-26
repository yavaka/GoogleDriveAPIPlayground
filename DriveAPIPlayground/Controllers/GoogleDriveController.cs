using DriveAPIPlayground.Services;
using Microsoft.AspNetCore.Mvc;

namespace DriveAPIPlayground.Controllers
{
    public class GoogleDriveController : Controller
    {
        private readonly IGoogleDriveService _driveService;

        public GoogleDriveController(IGoogleDriveService driveService)
            => this._driveService = driveService;

        public IActionResult Index()
        {
            var folders = this._driveService.GetFoldersByMainFolderId();

            return View(folders);
        }

        public async Task<IActionResult> GetFilesByFolderId(string folderId)
        {
            var files = await this._driveService.GetFilesByFolderIdAsync(folderId);

            return View(files);
        }

        public async Task<IActionResult> DownloadFileById(string fileId)
        {
            var file = await this._driveService.DownloadFileById(fileId);

            return File(file.Data, file.MimeType, file.Name);
        }

        public async Task<IActionResult> CreateFolder()
        {
            var createdFolderId = await this._driveService.CreateFolder("Yavor Yankov Folder");

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> UploadFileToFolder(IFormFile file, string folderId)
        {
            await this._driveService.UploadFile(file, folderId);

            return RedirectToAction(nameof(Index));
        }
    }
}
