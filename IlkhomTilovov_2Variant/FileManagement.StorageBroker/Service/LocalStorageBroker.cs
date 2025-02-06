
using System.IO.Compression;

namespace FileManagement.StorageBroker.Service;

public class LocalStorageBroker : IStorageBroker
{
    private string _dataPath;
    public LocalStorageBroker()
    {
        _dataPath = Path.Combine(Directory.GetCurrentDirectory(), "Data");
        if (!Directory.Exists(_dataPath))
        {
            Directory.CreateDirectory(_dataPath);
        }
    }
    public async Task CreateFolder(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        if (Directory.Exists(folderPath))
        {
            throw new Exception("Folder has already created");
        }
        var parent = Directory.GetParent(folderPath);
        if (!Directory.Exists(parent?.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
        Directory.CreateDirectory(folderPath);
    }

    public async Task DeleteFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        if (!File.Exists(filePath))
        {
            throw new Exception("Not Found ");
        }
        File.Delete(filePath);
    }

    public async Task DeleteFolder(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        if (!Directory.Exists(folderPath))
        {
            throw new Exception("Not Found");
        }
        Directory.Delete(folderPath);
    }

    public async Task<Stream> DownloadFile(string filePath)
    {
        filePath = Path.Combine(_dataPath, filePath);
        if (!File.Exists(filePath))
        {
            throw new Exception("Not found");
        }
        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read);
        return stream;
    }

    public async Task<Stream> DownloadFolderAsZip(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        var parent = Directory.GetParent(folderPath);
        if (!Directory.Exists(parent?.FullName))
        {
            throw new Exception("Error");
        }
        var zip = folderPath + ".zip";
        ZipFile.CreateFromDirectory(folderPath, zip);
        var res = new FileStream(zip, FileMode.Open, FileAccess.Read);
        return res;
    }

    public async Task<List<string>> GetAllInFolderPath(string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        var parent = Directory.GetParent(folderPath);
        if (!Directory.Exists(parent?.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
        var all = Directory.GetFileSystemEntries(folderPath).ToList();
        all = all.Select(a => a.Remove(0, folderPath.Length + 1)).ToList();
        return all;
    }

    public async Task GetContentOfTxtFile(string filePAth, string newContent)
    {
        filePAth = Path.Combine(_dataPath, filePAth);
        if (!File.Exists(filePAth))
        {
            throw new Exception("Not found");
        }
        File.ReadAllText(filePAth);
    }

    public async Task UploadFile(Stream file, string folderPath)
    {
        folderPath = Path.Combine(_dataPath, folderPath);
        var parent = Directory.GetParent(folderPath);
        if (!Directory.Exists(parent?.FullName))
        {
            throw new Exception("Parent folder path not found");
        }
        using (var stream = new FileStream(folderPath, FileMode.Create, FileAccess.Write))
        {
            file.CopyTo(stream);
        }
    }

    public Task UploadFiles(List<Stream> files, string folderPath)
    {
        throw new NotImplementedException();
    }

    public async Task UploadFileWithChunks(Stream file, string folderPath)
    {
        var fileExtension = Path.GetExtension(folderPath);
        var destinationFilePath = Path.Combine(_dataPath, folderPath + fileExtension);

        var bytes = 1024 * 1024 * 10;
        byte[] buffer = new byte[bytes];
        int bytesRead;

        using (FileStream fileStreamPath = new FileStream(folderPath, FileMode.Open, FileAccess.Read))
        {
            using (FileStream fileDestination = new FileStream(destinationFilePath, FileMode.Create, FileAccess.Write))
            {
                while ((bytesRead = fileStreamPath.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fileDestination.Write(buffer, 0, bytesRead);
                }
            }
        }

    }
    public async Task UpdateContentOfTxtFile(string file, string oldText, string newText)
    {
        file = Path.Combine(_dataPath, file);
        if (!File.Exists(file))
        {
            throw new Exception("Not found");
        }
        var fileContaines = File.ReadAllText(file);
        string updateFile = "";
        if (fileContaines.Contains(oldText))
        {
            updateFile = fileContaines.Replace(oldText, newText);
        }
        else
        {
            File.AppendAllText(file, newText + Environment.NewLine);
        }
        File.WriteAllText(file, updateFile);
    }
}
