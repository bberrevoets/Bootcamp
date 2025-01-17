﻿namespace GameStore.Api.Shared.FileUpload;

public class FileUploader(IWebHostEnvironment environment, IHttpContextAccessor httpContextAccessor)
{
    public async Task<FileUploadResult> UploadFileAsync(IFormFile file, string folder)
    {
        var result = new FileUploadResult();

        switch (file.Length)
        {
            case 0:
                result.IsSuccess = false;
                result.ErrorMessage = "No file uploaded";
                return result;
            case > 10 * 1024 * 1024:
                result.IsSuccess = false;
                result.ErrorMessage = "File is too large";
                return result;
        }

        var permittedExtensions = new[] { ".jpg", ".jpeg", ".png" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (string.IsNullOrEmpty(fileExtension) || !permittedExtensions.Contains(fileExtension))
        {
            result.IsSuccess = false;
            result.ErrorMessage = "Invalid file type.";
            return result;
        }

        var uploadFolder = Path.Combine(environment.WebRootPath, folder);
        if (!Directory.Exists(uploadFolder)) Directory.CreateDirectory(uploadFolder);

        var safeFileName = $"{Guid.NewGuid()}{fileExtension}";
        var fullPath = Path.Combine(uploadFolder, safeFileName);
        await using var stream = new FileStream(fullPath, FileMode.Create);
        await file.CopyToAsync(stream);

        var httpContext = httpContextAccessor.HttpContext;
        var fileUrl = $"{httpContext?.Request.Scheme}://{httpContext?.Request.Host}/{folder}/{safeFileName}";
        result.FileUrl = fileUrl;
        result.IsSuccess = true;

        return result;
    }
}