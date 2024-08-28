using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Asp.Versioning;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Text;
using static Org.BouncyCastle.Asn1.Cmp.Challenge;
using Microsoft.AspNetCore.Http.HttpResults;
using Administration.Configs.Image;
using Google.Protobuf;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using Microsoft.Extensions.Options;

public class ImageUploadController : Controller
{
    private readonly IWebHostEnvironment _hostingEnvironment;
    private static readonly Random random = new Random();
    public static string storageDir = "media";
    private readonly ImageConfig _imageConfig;
    public ImageUploadController(IWebHostEnvironment hostingEnvironment, IOptions<ImageConfig> imageConfig)
    {
        _hostingEnvironment = hostingEnvironment;
        _imageConfig = imageConfig.Value;
    }

    public string GetNewFileName(string fname)
    {
        if (string.IsNullOrEmpty(fname))
        {
            // Вернуть значение по умолчанию или сгенерировать новое имя файла
            return $"default_{DateTime.Now}.ext"; // Пример
        }

        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, fname);
        if (!System.IO.File.Exists(filePath))
        {
            return fname;
        }

        // Генерация нового уникального имени файла
        string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fname);
        string extension = Path.GetExtension(fname);
        string newFileName;
        int counter = 1;

        do
        {
            newFileName = $"{fileNameWithoutExtension}_{counter}{extension}";
            filePath = Path.Combine(_hostingEnvironment.WebRootPath, newFileName);
            counter++;
        } while (System.IO.File.Exists(filePath));

        return newFileName;
    }

    [HttpPost("upload-image"), ApiVersion("1")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file.");

        var targetDirectory = Path.Combine(_hostingEnvironment.WebRootPath, "uploaded-images");
        var newFName = GetNewFileName(file.FileName);
        var filePath = Path.Combine(targetDirectory, newFName);

        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        var fileUrl = $"{Request.Scheme}://{Request.Host}/uploaded-images/{newFName}";
        return Ok(new { imageUrl = fileUrl });
    }
    [HttpPost("test-file"), ApiVersion("1")]
    public IActionResult TestName(string name = null)
    {
        string path = GetStoragePath(name);
        string result = GenerateSubdir(path);
        return Ok(new { result = result });
    }

    public static string GenerateName(string ext, string suffix = null)
    {
        string randomString = GenerateRandomString(5);
        string timeStamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString();
        if (string.IsNullOrEmpty(suffix))
        {
            return $"{timeStamp}{randomString}.{ext}";
        }
        return $"{timeStamp}{randomString}-{suffix}.{ext}";
    }
    public static string GenerateSubdir(string path, bool check = true)
    {
        if (check && !Directory.Exists(path)) 
        {
            Directory.CreateDirectory(path);
        }

        string subdir = GenerateRandomString(2);
        if (subdir.Equals("ad", StringComparison.OrdinalIgnoreCase))
        {
            subdir = "da";
        }

        string subdirPath = Path.Combine(path, subdir);
        if (check && !Directory.Exists(subdirPath)) 
        {
            Directory.CreateDirectory(subdirPath);
        }

        return subdir;
    }
    private static string GenerateRandomString(int length)
    {
        const string chars = "abcdefghijklmnopqrstuvwxyz0123456789";
        return new string(Enumerable.Repeat(chars, length)
            .Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public static string GetStoragePath(string name = null)
    {
        string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", storageDir);
        

        if (string.IsNullOrEmpty(name))
        {
            return path;
        }
        else
        {
            return Path.Combine(path, name.Trim('/'));
        }
    }
    [HttpPost("test-uploaded"), ApiVersion("1")]

    public async Task<IActionResult> SaveUploadedAsync(IFormFile file, string path, bool subdir = true)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Invalid file!");

        string storagePath = GetStoragePath(path);

        if (subdir)
        {
            string subdirName = GenerateSubdir(storagePath);
            storagePath = Path.Combine(storagePath, subdirName);
        }
        else
        {
            string parentDirectory = Path.GetDirectoryName(storagePath);
            if (!Directory.Exists(parentDirectory))
            {
                Directory.CreateDirectory(parentDirectory);
            }

            if (!Directory.Exists(storagePath))
            {
                Directory.CreateDirectory(storagePath);
            }
        }

        string newFileName = GenerateName(Path.GetExtension(file.FileName).TrimStart('.'));
        string fullPath = Path.Combine(storagePath, newFileName);

        try
        {
            using (var image = await Image.LoadAsync(file.OpenReadStream()))
            {
                // Сохраняем оригинал
                await image.SaveAsync(fullPath);

                // Сохраняем обрезанные версии
                if (_imageConfig.Blogs?.Image != null)
                {
                    foreach (var size in _imageConfig.Blogs.Image)
                    {
                        var resizedImage = image.Clone(ctx => ctx.Resize(size[0], size[1]));
                        string resizedFileName = GenerateName(Path.GetExtension(file.FileName).TrimStart('.'), $"{size[0]}x{size[1]}");
                        string resizedFullPath = Path.Combine(storagePath, resizedFileName);
                        await resizedImage.SaveAsync(resizedFullPath);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, $"Could not save uploaded file to path {fullPath}: {ex.Message}");
        }

        var result = Path.Combine(Path.GetFileName(storagePath), newFileName);
        return Ok(new { result = result });
    }


}
