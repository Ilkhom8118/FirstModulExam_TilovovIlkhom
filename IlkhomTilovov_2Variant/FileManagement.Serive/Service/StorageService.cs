
using FileManagement.StorageBroker.Service;

namespace FileManagement.Serive.Service;

public class StorageService : IStorageService
{
    private readonly IStorageBroker _storageBroker;

    public StorageService(IStorageBroker storageBroker)
    {
        _storageBroker = storageBroker;
    }

    public async Task CreateFolder(string folderPath)
    {
        await _storageBroker.CreateFolder(folderPath);
    }

    public async Task DeleteFile(string filePath)
    {
        await _storageBroker.DeleteFile(filePath);
    }

    public async Task DeleteFolder(string folderPath)
    {
        await _storageBroker.DeleteFolder(folderPath);
    }

    public async Task<Stream> DownloadFile(string filePath)
    {
        return await _storageBroker.DownloadFile(filePath);
    }

    public async Task<Stream> DownloadFolderAsZip(string folderPath)
    {
        return await _storageBroker.DownloadFolderAsZip(folderPath);
    }

    public async Task<List<string>> GetAllInFolderPath(string folderPath)
    {
        return await _storageBroker.GetAllInFolderPath(folderPath);
    }

    public async Task GetContentOfTxtFile(string filePAth, string newContent)
    {
        await _storageBroker.GetContentOfTxtFile(filePAth, newContent);
    }

    public async Task UpdateContentOfTxtFile(string filePath, string oldText, string newContent)
    {
        await _storageBroker.UpdateContentOfTxtFile(filePath, oldText, newContent);
    }

    public async Task UploadFile(Stream file, string folderPath)
    {
        await _storageBroker.UploadFile(file, folderPath);
    }

    public async Task UploadFiles(List<Stream> files, string folderPath)
    {
        await _storageBroker.UploadFiles(files, folderPath);
    }

    public async Task UploadFileWithChunks(Stream file, string folderPath)
    {
        await _storageBroker.UploadFileWithChunks(file, folderPath);
    }
}
