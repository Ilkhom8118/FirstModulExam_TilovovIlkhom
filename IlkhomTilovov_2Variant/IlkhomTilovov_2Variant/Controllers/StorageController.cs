using FileManagement.Serive.Service;
using Microsoft.AspNetCore.Mvc;

namespace IlkhomTilovov_2Variant.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private IStorageService _storageService;
        public StorageController(IStorageService storageService)
        {
            _storageService = storageService;
        }
        [HttpPost("createFolder")]
        public async Task CreateFolder(string folder)
        {
            await _storageService.CreateFolder(folder);
        }
        [HttpPost("uploadFile")]
        public async Task UploadFile(IFormFile file, string? folderPath)
        {
            folderPath = folderPath ?? string.Empty;
            folderPath = Path.Combine(folderPath, file.FileName);
            using (var stream = file.OpenReadStream())
            {
                await _storageService.UploadFile(stream, folderPath);
            }
        }
        [HttpDelete("deleteFolder")]
        public async Task DeleteFolder(string directory)
        {
            await _storageService.DeleteFolder(directory);
        }
        [HttpDelete("deleteFile")]
        public async Task DeleteFile(string file)
        {
            await _storageService.DeleteFile(file);
        }
        [HttpGet("downloadFile")]
        public async Task<FileStreamResult> DownloadFile(string? file)
        {
            file = file ?? string.Empty;
            var nameFile = Path.GetFileName(file);
            var stream = await _storageService.DownloadFile(file);
            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = nameFile
            };
            return res;
        }
        [HttpGet("downloadFolderAsZip")]
        public async Task<FileStreamResult> DownloadFolderAsZip(string directory)
        {
            if (Path.GetExtension(directory) != string.Empty)
            {
                throw new Exception("Error");
            }
            var folderName = Path.GetFileName(directory);
            var stream = await _storageService.DownloadFolderAsZip(directory);
            var res = new FileStreamResult(stream, "application/octet-stream")
            {
                FileDownloadName = folderName + ".zip"
            };
            return res;
        }
        [HttpPost("uploadFileWithChunks")]
        public async Task UploadFileWithChunks(string filePath, IFormFile newFileName)
        {
            using (var stream = newFileName.OpenReadStream())
            {
                await _storageService.UploadFileWithChunks(stream, filePath);
            }
        }
        [HttpGet("getAllInFolderPath")]
        public async Task<List<string>> GetAllInFolderPath(string? folder)
        {
            folder = folder ?? string.Empty;
            return await _storageService.GetAllInFolderPath(folder);
        }
        [HttpGet("getContentOfTxtFile")]
        public async Task GetContentOfTxtFile(string filePath, string newContent)
        {
            await _storageService.GetContentOfTxtFile(filePath, newContent);
        }
        [HttpGet("updateContentOfTxtFile")]
        public async Task UpdateContentOfTxtFile(string filePath, string oldText, string newContent)
        {
            await _storageService.UpdateContentOfTxtFile(filePath, oldText, newContent);
        }
    }
}
