using _0.Framework.Application;

namespace TopLearn.Web;

public class FileUploader : IFileUploader
{
    #region constructor injection

    private readonly IWebHostEnvironment _webHostEnvironment;
    public FileUploader(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    #endregion

    public string Upload(IFormFile file, string path)
    {
        if (file == null)
        {
            return "";
        }

        var savePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", path);

        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }

        var filename = $"{DateTime.Now.ToFileName()}-{file.FileName}";

        var fullPath = Path.Combine(savePath, filename);

        using var stream = new FileStream(fullPath, FileMode.Create);

        file.CopyTo(stream);

        return filename;
    }

    public void Delete(string path, string imageName)
    {
        var deletePath = Path.Combine(_webHostEnvironment.WebRootPath, "Files", path, imageName);

        if (File.Exists(deletePath))
        {
            File.Delete(deletePath);
        }
    }
}