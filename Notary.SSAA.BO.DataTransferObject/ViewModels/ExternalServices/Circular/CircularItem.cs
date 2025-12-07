

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Circular
{
    public class CircularItem
    {
        public CircularDetail CircularDetail { get; set; } = new();
        public CircularItemPerson CircularItemPerson { get; set; } = new();
        public CircularPersonStatus CircularPersonStatus { get; set; } = new();
    }

    public class CircularPersonStatus
    {
        public bool HasSana { get; set; }
        public bool HasSabtAhval { get; set; }
        public bool HasIlenc { get; set; }
        public bool HasDataValid { get; set; }
    }


    public class CircularItemPerson
    {
        public string CircularItemId { get; set; }
        public string CircularId { get; set; }
        public string RowNo { get; set; }
        public string PersonType { get; set; }
        public string NationalNo { get; set; }
        public string Name { get; set; }
        public string FatherName { get; set; }
        public string BirthDate { get; set; }
        public string IdentityNo { get; set; }
        public string Description { get; set; }
        public string BirthLocation { get; set; }
        public string FamilyIncluded { get; set; }
    }

    public class CircularDetail
    {
        public string No { get; set; }
        public string IssueDate { get; set; }
        public string CourtNo { get; set; }
        public string CourtDate { get; set; }
        public string CourtTitle { get; set; }
        public string CourtText { get; set; }
        public string BaseClaimerDate { get; set; }
        public string BaseClaimerNo { get; set; }
        public string BaseClaimerTitle { get; set; }
        public string FollowingNo { get; set; }
        public string FollowingDate { get; set; }
    }
}
