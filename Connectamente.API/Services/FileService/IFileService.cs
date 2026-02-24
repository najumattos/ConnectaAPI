namespace Connectamente.API.Services.FileService;

public interface IFileService
{
    //posso usar essa Interface para o pdf de RPD?
    Task<string> SaveFileAsync(IFormFile file, string subDirectory = "uploads");
    Task<bool> DeleteFileAsync(string filePath);
    string GetFileUrl(string fileName);
}
