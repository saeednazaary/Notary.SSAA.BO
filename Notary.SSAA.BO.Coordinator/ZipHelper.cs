using System.IO.Compression;

namespace Notary.SSAA.BO.Coordinator
{
    public class ZipHelper
    {
        public static async Task<byte[]> CreateZipFile(List<FileToZip> files,CancellationToken cancellationToken)
        {
            using var memoryStream = new MemoryStream();
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                foreach (var file in files)
                {
                    var entry = archive.CreateEntry(file.FileName, CompressionLevel.Optimal);
                    using var entryStream = entry.Open();
                    await entryStream.WriteAsync(file.Content, 0, file.Content.Length, cancellationToken);
                }
            }
            memoryStream.Seek(0, SeekOrigin.Begin);
            return memoryStream.ToArray();
        }
    }

    public class FileToZip
    {
        public string FileName { get; set; }
        public byte[] Content { get; set; }
    }
}
