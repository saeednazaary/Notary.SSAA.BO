namespace Notary.SSAA.BO.Domain.RepositoryObjects.ExternalServices.SabtAhvalService
{
    public class Estelam3TPersonInfoResult
    {
        public string DeathDate { get; set; }
        public bool IsDead { get; set; }
        public string Name { get; set; }
        public string Family { get; set; }
        public string FatherName { get; set; }
        public string MotherName { get; set; }
        public bool IsMan { get; set; }
        public int ShenasnameNo { get; set; }
        public string ShenasnameSeri { get; set; }
        public int ShenasnameSerial { get; set; }
        public int BirthDate { get; set; }
        public int BookNo { get; set; }
        public int BookRow { get; set; }
        public int OfficeCode { get; set; }
        public string OfficeName { get; set; }
        public long Nin { get; set; }
        public byte[] Image { get; set; }
        public string PostCode { get; set; }
        public string PostAddress { get; set; }
        public bool SmartCard { get; set; }
    }
}
