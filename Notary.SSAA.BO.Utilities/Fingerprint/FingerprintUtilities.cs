using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;


namespace Notary.SSAA.BO.Utilities.Fingerprint
{
    public static class FingerprintUtilities
    {
        private static byte[] ConvertToBMP(FingerDevice fingerDevice, int width, int height, byte[] rawImage)
        {
            using var image = CreateImage(fingerDevice, width, height, rawImage);
            using var ms = new MemoryStream();
            image.SaveAsBmp(ms);
            return ms.ToArray();
        }

        public static byte[] ConvertToJPG(FingerDevice fingerDevice, int width, int height, byte[] rawImage)
        {
            using var image = CreateImage(fingerDevice, width, height, rawImage);
            using var ms = new MemoryStream();
            image.SaveAsJpeg(ms);
            return ms.ToArray();
        }

        public static string ConvertToJPEGBase64(FingerprintRAWToJPEGInput input)
        {
            byte[] imageData = Convert.FromBase64String(input.Data);
            var result = ConvertToJPG(input.DeviceType, input.Width, input.Height, imageData);
            return Convert.ToBase64String(result);
        }

        private static Image<L8> CreateImage(FingerDevice fingerDevice, int width, int height, byte[] rawImage)
        {
            var image = Image.LoadPixelData<L8>(rawImage, width, height);

            switch (fingerDevice)
            {
                case FingerDevice.Fotronic:
                    InvertImage(image);
                    break;
                case FingerDevice.Hongda:
                case FingerDevice.Suprima:
                    // No processing needed as image is already in correct format
                    break;
            }

            return image;
        }

        private static void InvertImage(Image<L8> image)
        {
            image.ProcessPixelRows(accessor =>
            {
                for (int y = 0; y < accessor.Height; y++)
                {
                    var row = accessor.GetRowSpan(y);
                    for (int x = 0; x < row.Length; x++)
                    {
                        row[x].PackedValue = (byte)(255 - row[x].PackedValue);
                    }
                }
            });
        }
    }
}
