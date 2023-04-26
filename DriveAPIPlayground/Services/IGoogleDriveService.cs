using DriveAPIPlayground.ServiceModels;
using Google.Apis.Drive.v3;

namespace DriveAPIPlayground.Services
{
    public interface IGoogleDriveService
    {
        Task<string> CreateFolder(string folderName);

        IDictionary<string, string> GetFoldersByMainFolderId();

        Task<IEnumerable<DriveFileServiceModel>> GetFilesByFolderIdAsync(string folderId);

        Task<DriveFileServiceModel> DownloadFileById(string fileId);

        Task UploadFile(IFormFile file, string folderId);
    }
}
