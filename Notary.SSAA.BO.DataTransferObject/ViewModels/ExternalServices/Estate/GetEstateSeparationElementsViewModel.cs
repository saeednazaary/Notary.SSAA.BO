using Notary.SSAA.BO.DataTransferObject.Queries.ExternalServices.Estate;
using Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public class GetEstateSeparationElementsViewModel
    {
        public Separation Separation { get; set; }
        public string ErrorMessage { get; set; }
    }

    public class Separation
    {
        public Separation()
        {
            TheSeparationElements = new List<SeparationElement>();
        }
        private string _Agent;


        public virtual string Agent
        {
            get
            {
                return _Agent;
            }
            set
            {
                _Agent = value;
            }
        }




        private string _Description;


        public virtual string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }


        private string _EEstateId;


        public virtual string EEstateId
        {
            get
            {
                return _EEstateId;
            }
            set
            {
                _EEstateId = value;
            }
        }




        private string _EngineerLicenseNo;


        public virtual string EngineerLicenseNo
        {
            get
            {
                return _EngineerLicenseNo;
            }
            set
            {
                _EngineerLicenseNo = value;
            }
        }


        private string _EngineerName;


        public virtual string EngineerName
        {
            get
            {
                return _EngineerName;
            }
            set
            {
                _EngineerName = value;
            }
        }




        private int _FunctionType;


        public virtual int FunctionType
        {
            get
            {
                return _FunctionType;
            }
            set
            {
                _FunctionType = value;
            }
        }

        string _Id;

        public string Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private string _MappingDate;


        public virtual string MappingDate
        {
            get
            {
                return _MappingDate;
            }
            set
            {
                _MappingDate = value;
            }
        }


        private string _NatureDate;


        public virtual string NatureDate
        {
            get
            {
                return _NatureDate;
            }
            set
            {
                _NatureDate = value;
            }
        }


        private string _NatureNum;

        public virtual string NatureNum
        {
            get
            {
                return _NatureNum;
            }
            set
            {
                _NatureNum = value;
            }
        }





        private string _PrintedText;


        public virtual string PrintedText
        {
            get
            {
                return _PrintedText;
            }
            set
            {
                _PrintedText = value;
            }
        }


        public List<SeparationElement> TheSeparationElements { get; set; }


        public string UnitId { get; set; }
    }


}
