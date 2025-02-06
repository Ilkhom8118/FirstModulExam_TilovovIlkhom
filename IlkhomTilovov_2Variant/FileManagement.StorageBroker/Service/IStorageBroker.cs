namespace FileManagement.StorageBroker.Service;

public interface IStorageBroker
{
    Task DeleteFile(string filePath);
    Task CreateFolder(string folderPath);
    Task DeleteFolder(string folderPath);
    Task<Stream> DownloadFile(string filePath);
    Task UploadFile(Stream file, string folderPath);
    Task<Stream> DownloadFolderAsZip(string folderPath);
    Task UploadFiles(List<Stream> files, string folderPath);
    Task<List<string>> GetAllInFolderPath(string folderPath);
    Task UploadFileWithChunks(Stream file, string folderPath);
    Task GetContentOfTxtFile(string filePAth, string newContent);
    Task UpdateContentOfTxtFile(string filePath, string oldText, string newContent);
}