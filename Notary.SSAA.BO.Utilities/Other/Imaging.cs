//using ImageMagick;


//namespace Notary.SSAA.BO.Utilities.Other
//{
//    public static class MagickImageArrayExtensions
//    {
//        public static async Task<byte[]> ToTiffBytesAsync(this IEnumerable<byte[]> imageList)
//        {
//            ArgumentNullException.ThrowIfNull(imageList);
//            var resultStream = new MemoryStream();

//            await Task.Run(() =>
//            {
//                using var coll = new MagickImageCollection();
//                foreach (var bytes in imageList)
//                    coll.Add(new MagickImage(bytes));
//                coll.Write(resultStream, MagickFormat.Tiff);
//            });

//            return resultStream.ToArray();
//        }

//        public static async Task<List<byte[]>> ToImageListAsync(this byte[] tiffBytes, MagickFormat outputFormat = MagickFormat.Jpeg)
//        {
//            ArgumentNullException.ThrowIfNull(tiffBytes);

//            var pages = new List<byte[]>();

//            await Task.Run(() =>
//            {
//                using var collection = new MagickImageCollection(tiffBytes);

//                foreach (var frame in collection)
//                {
//                    using var ms = new MemoryStream();
//                    frame.Write(ms, outputFormat);
//                    pages.Add(ms.ToArray());
//                }
//            });

//            return pages;
//        }

//        public static async Task<MagickFormat> DetectImageFormatAsync(this byte[] imageBytes)
//        {
//            try
//            {
//                ArgumentNullException.ThrowIfNull(imageBytes);

//                return await Task.Run(() =>
//                {
//                    var info = new MagickImageInfo(imageBytes);
//                    return info.Format;
//                });
//            }
//            catch (Exception)
//            {

//                return MagickFormat.Unknown;
//            }

//        }

//    }
//}
