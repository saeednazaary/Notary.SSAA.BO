

namespace Notary.SSAA.BO.Utilities.Fingerprint
{
    public class FingerprintRAWToJPEGInput
    {
        public int Width { get; set; }
        public FingerDevice DeviceType { get; set; }
        public int Height { get; set; }
        public string Data { get; set; }
    }
    public enum FingerDevice
    {
        Suprima = 0,
        Hongda = 1,
        Fotronic = 2
    }
}
