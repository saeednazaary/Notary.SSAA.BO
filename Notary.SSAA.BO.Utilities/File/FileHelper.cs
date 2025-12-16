using SkiaSharp;
using System.Security;

namespace Notary.SSAA.BO.Utilities.File
{
    public static class FileHelper
    {
        private static readonly string[] AllowedImageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".webp" };
        private static readonly long MaxFileSize = 10 * 1024 * 1024; // 10MB

        public static byte[] GetByteArray(this string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            string sanitizedPath = SanitizePath(path);

            // Validate file extension
            ValidateFileExtension(sanitizedPath);

            if (!System.IO.File.Exists(sanitizedPath))
            {
                throw new FileNotFoundException("File not found", sanitizedPath);
            }

            FileInfo fileInfo = new(sanitizedPath);
            return fileInfo.Length > MaxFileSize
                ? throw new SecurityException($"File size exceeds maximum allowed size of {MaxFileSize} bytes")
                : System.IO.File.ReadAllBytes(sanitizedPath);
        }

        public static byte[] ResizeImage(this byte[] bytes, int width, int height, SKEncodedImageFormat imageFormat)
        {
            // Validate input parameters
            if (bytes == null || bytes.Length == 0)
            {
                throw new ArgumentException("Image bytes cannot be null or empty", nameof(bytes));
            }

            if (width <= 0 || height <= 0)
            {
                throw new ArgumentException("Width and height must be positive values");
            }

            if (bytes.Length > MaxFileSize)
            {
                throw new SecurityException($"Image size exceeds maximum allowed size of {MaxFileSize} bytes");
            }

            try
            {
                using SKMemoryStream stream = new(bytes);
                using SKBitmap bitmap = SKBitmap.Decode(stream);

                if (bitmap == null)
                {
                    throw new InvalidDataException("Invalid image format");
                }

                using SKBitmap resizedBitmap = bitmap.Resize(new SKImageInfo(width, height), SKFilterQuality.High);
                using SKDynamicMemoryWStream imageStream = new();
                _ = resizedBitmap.Encode(imageStream, imageFormat, 100);
                SKData skData = imageStream.DetachAsData();
                return skData.ToArray();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to resize image", ex);
            }
        }

        #region Security Helper Methods

        private static string SanitizePath(string inputPath)
        {
            string fullPath = Path.GetFullPath(inputPath).Trim();

            string allowedBaseDirectory = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

            return !fullPath.StartsWith(allowedBaseDirectory, StringComparison.OrdinalIgnoreCase)
                ? throw new SecurityException("Access to the specified path is not allowed")
                : fullPath;
        }

        private static void ValidateFileExtension(string filePath)
        {
            string extension = Path.GetExtension(filePath).ToLowerInvariant();

            if (string.IsNullOrEmpty(extension))
            {
                throw new SecurityException("File must have an extension");
            }

            if (!AllowedImageExtensions.Contains(extension))
            {
                throw new SecurityException($"File extension '{extension}' is not allowed. Allowed extensions: {string.Join(", ", AllowedImageExtensions)}");
            }
        }

        #endregion

        #region Additional Safe Methods

        public static byte[] GetByteArraySafe(this string path, string allowedDirectory = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new ArgumentException("Path cannot be null or empty", nameof(path));
            }

            string sanitizedPath = SanitizePathWithDirectory(path, allowedDirectory);
            ValidateFileExtension(sanitizedPath);

            return !System.IO.File.Exists(sanitizedPath)
                ? throw new FileNotFoundException("File not found", sanitizedPath)
                : System.IO.File.ReadAllBytes(sanitizedPath);
        }

        private static string SanitizePathWithDirectory(string inputPath, string allowedDirectory)
        {
            string fullPath = Path.GetFullPath(inputPath).Trim();

            if (!string.IsNullOrEmpty(allowedDirectory))
            {
                string allowedBaseDir = Path.GetFullPath(allowedDirectory);

                if (!fullPath.StartsWith(allowedBaseDir, StringComparison.OrdinalIgnoreCase))
                {
                    throw new SecurityException("Access to the specified path is not allowed");
                }
            }
            else
            {
                string appBaseDir = Path.GetFullPath(AppDomain.CurrentDomain.BaseDirectory);

                if (!fullPath.StartsWith(appBaseDir, StringComparison.OrdinalIgnoreCase))
                {
                    throw new SecurityException("Access to the specified path is not allowed");
                }
            }

            return fullPath;
        }

        #endregion
    }
}