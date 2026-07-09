using Microsoft.AspNetCore.Http;

namespace Booksy.Infrastructure.FileUpload;

/// <summary>
/// Service for handling file uploads and storage
/// </summary>
public interface IFileUploadService
{
    /// <summary>
    /// Upload a file to the specified folder
    /// </summary>
    /// <param name="file">The file to upload</param>
    /// <param name="folderPath">Target folder path (relative to wwwroot/images)</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The relative path to the uploaded file</returns>
    Task<string> UploadFileAsync(IFormFile file, string folderPath, CancellationToken cancellationToken);

    /// <summary>
    /// Delete a file by its relative path
    /// </summary>
    /// <param name="filePath">Relative path of the file to delete</param>
    /// <param name="cancellationToken">Cancellation token</param>
    Task DeleteFileAsync(string filePath, CancellationToken cancellationToken);

    /// <summary>
    /// Check if the file is a valid image
    /// </summary>
    /// <param name="file">The file to validate</param>
    /// <returns>True if valid, false otherwise</returns>
    bool IsValidImage(IFormFile file);

    /// <summary>
    /// Get a safe file name by removing invalid characters
    /// </summary>
    /// <param name="fileName">The original file name</param>
    /// <returns>Safe file name</returns>
    string GetSafeFileName(string fileName);
}
