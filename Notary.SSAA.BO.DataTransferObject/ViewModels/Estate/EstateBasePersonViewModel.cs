using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.Estate
{
    public class EstateBasePersonViewModel : EntityState
    {
        public string PersonId { get; set; }


        public string PersonName { get; set; } = null!;


        public string PersonFamily { get; set; }


        public string PersonFatherName { get; set; }


        public string PersonIdentityNo { get; set; }


        public string PersonBirthDate { get; set; }

        public string PersonNationalityCode { get; set; }

        public string PersonSeri { get; set; }

        public string PersonSerialNo { get; set; }




        public string PersonIssuePlaceText { get; set; }



        public string PersonForiegnIssuePlace { get; set; }

        public string PersonSexType { get; set; }

        public string PersonType { get; set; }


        public string PersonPostalCode { get; set; } = null!;


        public string PersonAddress { get; set; } = null!;







        public IList<string> PersonIssuePlaceId { get; set; }


        public IList<string> PersonNationalityId { get; set; }


        public IList<string> PersonCityId { get; set; }

        public decimal PersonTimestamp { get; set; }


        //public string PersonIlm { get; set; }

        public bool PersonIsIrani { get; set; }

        public string PersonSeriAlpha { get; set; }

        public bool PersonIsSabtahvalCorrect { get; set; }
        public bool PersonIsSabtahvalChecked { get; set; }
        public bool PersonIsForeignerssysCorrect { get; set; }
        public bool PersonIsForeignerssysChecked { get; set; }

        public bool PersonIsIlencCorrect { get; set; }
        public bool PersonIsIlencChecked { get; set; }

        public string PersonalImage { get; set; }
        public string PersonMobileNo { get; set; }
        public bool? PersonMobileNoState { get; set; }

        public bool? PersonSanaState { get; set; }
        public string PersonSanaMoileNo { get; set; }
        public bool? PersonIsAlive { get; set; }
        public string PersonEmail { get; set; }
        public string PersonFax { get; set; }
        public string PersonDescription { get; set; }
        public bool PersonIsOriginal { get; set; }
        public bool PersonIsRelated { get; set; }
        public string PersonTel { get; set; }
    }
}
