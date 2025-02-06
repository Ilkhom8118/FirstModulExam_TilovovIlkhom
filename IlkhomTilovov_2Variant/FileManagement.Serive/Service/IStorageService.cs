namespace FileManagement.Serive.Service;

public interface IStorageService
{
    Task CreateFolder(string folderPath);
    Task UploadFile(Stream file, string folderPath);
    Task UploadFileWithChunks(Stream file, string folderPath);
    Task UploadFiles(List<Stream> files, string folderPath);
    Task DeleteFile(string filePath);
    Task DeleteFolder(string folderPath);
    Task<Stream> DownloadFile(string filePath);
    Task<Stream> DownloadFolderAsZip(string folderPath);
    Task GetContentOfTxtFile(string filePAth, string newContent);
    Task<List<string>> GetAllInFolderPath(string folderPath);
    Task UpdateContentOfTxtFile(string filePath, string oldText, string newContent);
}