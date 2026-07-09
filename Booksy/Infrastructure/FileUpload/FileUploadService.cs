using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Booksy.Core.Exceptions;

namespace Booksy.Infrastructure.FileUpload;

/// <summary>
/// File upload service for managing file storage
/// </summary>
public class FileUploadService : IFileUploadService
{
    private readonly string _basePath;
    private readonly ILogger<FileUploadService> _logger;
    private const long MAX_FILE_SIZE = 5 * 1024 * 1024; // 5MB
    private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".webp", ".gif" };

    public FileUploadService(IWebHostEnvironment environment, ILogger<FileUploadService> logger)
    {
        _basePath = Path.Combine(environment.WebRootPath, "images");
        _logger = logger;
        
        // Ensure base directory exists
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }
    }

    /// <summary>
    /// Upload a file to the specified folder
    /// </summary>
    public async Task<string> UploadFileAsync(IFormFile file, string folderPath, CancellationToken cancellationToken)
    {
        // Validate file
        if (!IsValidImage(file))
        {
            throw new BusinessException("Invalid image file. Only JPG, PNG, WebP, and GIF files are allowed.");
        }

        // Check file size
        if (file.Length > MAX_FILE_SIZE)
        {
            throw new BusinessException("File size exceeds 5MB limit.");
        }

        try
        {
            // Generate unique filename
            var safeFileName = GetSafeFileName(file.FileName);
            var uniqueName = $"{Guid.NewGuid()}_{safeFileName}";
            var fullPath = Path.Combine(_basePath, folderPath, uniqueName);

            // Create directory if it doesn't exist
            var directory = Path.GetDirectoryName(fullPath);
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            // Save file
            using (var stream = new FileStream(fullPath, FileMode.Create, FileAccess.Write))
            {
                await file.CopyToAsync(stream, cancellationToken);
            }

            // Return relative path (for database storage)
            var relativePath = Path.Combine("images", folderPath, uniqueName).Replace("\\", "/");
            _logger.LogInformation($"File uploaded successfully: {relativePath}");
            
            return relativePath;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error uploading file");
            throw new BusinessException("Failed to upload file. Please try again.");
        }
    }

    /// <summary>
    /// Delete a file by its relative path
    /// </summary>
    public async Task DeleteFileAsync(string filePath, CancellationToken cancellationToken)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            return;
        }

        try
        {
            // Don't delete placeholder image
            if (filePath.Contains("placeholder"))
            {
                return;
            }

            var fullPath = Path.Combine(_basePath, "..", filePath);
            var normalizedPath = Path.GetFullPath(fullPath);

            // Security check: ensure path is within wwwroot
            var normalizedBasePath = Path.GetFullPath(Path.Combine(_basePath, ".."));
            if (!normalizedPath.StartsWith(normalizedBasePath))
            {
                _logger.LogWarning($"Attempted to delete file outside wwwroot: {filePath}");
                return;
            }

            if (File.Exists(normalizedPath))
            {
                File.Delete(normalizedPath);
                _logger.LogInformation($"File deleted successfully: {filePath}");
            }

            await Task.CompletedTask;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting file: {filePath}");
            // Don't throw - deletion failure shouldn't block operations
        }
    }

    /// <summary>
    /// Check if the file is a valid image
    /// </summary>
    public bool IsValidImage(IFormFile file)
    {
        if (file?.Length == 0)
        {
            return false;
        }

        var extension = Path.GetExtension(file.FileName).ToLower();
        return AllowedExtensions.Contains(extension);
    }

    /// <summary>
    /// Get a safe file name by removing invalid characters
    /// </summary>
    public string GetSafeFileName(string fileName)
    {
        // Remove path information
        var name = Path.GetFileName(fileName);
        
        // Remove invalid characters
        var invalidChars = Path.GetInvalidFileNameChars();
        var safeName = new string(name
            .Where(c => !invalidChars.Contains(c))
            .ToArray());

        // Remove special characters
        safeName = safeName
            .Replace(" ", "_")
            .Replace("\"", "")
            .Replace("<", "")
            .Replace(">", "")
            .Replace("|", "")
            .Replace(":", "")
            .Replace("*", "")
            .Replace("?", "");

        return string.IsNullOrEmpty(safeName) ? $"file_{Guid.NewGuid()}" : safeName;
    }
}
