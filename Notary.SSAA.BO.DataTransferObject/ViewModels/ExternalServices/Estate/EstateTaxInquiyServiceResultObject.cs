
namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    public partial class EstateTaxInquiyServiceResultObject : object
    {

        private bool isDataPassedField;

        private int errorIDField;

        private string errorMessageField;

        private string nationalIDField;

        private string followCodeField;

        private string oldFollowCodeField;

        private int statusField;

        private string statusDescriptionField;

        private object[] statusHistoryField;

        private string slipIDField;

        private string paymentIDField;

        private bool isLicenceReadyField;

        private string licenceNumberField;

        private byte[] licenceFileField;

        private string licenceHTMLField;

        private byte[] licenceHTMLBytesField;

        private string taxAmountField;

        private string lastRequestTypeField;

        private int lastRequestRfrenceIDField;

        private DateTime lastRequestDateField;

        private int fileRefrenceNumberField;


        public bool IsDataPassed
        {
            get
            {
                return isDataPassedField;
            }
            set
            {
                isDataPassedField = value;

            }
        }

        public int ErrorID
        {
            get
            {
                return errorIDField;
            }
            set
            {
                errorIDField = value;

            }
        }

        public string ErrorMessage
        {
            get
            {
                return errorMessageField;
            }
            set
            {
                errorMessageField = value;

            }
        }

        public string NationalID
        {
            get
            {
                return nationalIDField;
            }
            set
            {
                nationalIDField = value;

            }
        }

        public string FollowCode
        {
            get
            {
                return followCodeField;
            }
            set
            {
                followCodeField = value;

            }
        }

        public string OldFollowCode
        {
            get
            {
                return oldFollowCodeField;
            }
            set
            {
                oldFollowCodeField = value;

            }
        }

        public int Status
        {
            get
            {
                return statusField;
            }
            set
            {
                statusField = value;

            }
        }

        public string StatusDescription
        {
            get
            {
                return statusDescriptionField;
            }
            set
            {
                statusDescriptionField = value;

            }
        }

        public object[] StatusHistory
        {
            get
            {
                return statusHistoryField;
            }
            set
            {
                statusHistoryField = value;

            }
        }

        public string SlipID
        {
            get
            {
                return slipIDField;
            }
            set
            {
                slipIDField = value;

            }
        }

        public string PaymentID
        {
            get
            {
                return paymentIDField;
            }
            set
            {
                paymentIDField = value;

            }
        }

        public bool IsLicenceReady
        {
            get
            {
                return isLicenceReadyField;
            }
            set
            {
                isLicenceReadyField = value;

            }
        }

        public string LicenceNumber
        {
            get
            {
                return licenceNumberField;
            }
            set
            {
                licenceNumberField = value;

            }
        }


        public byte[] LicenceFile
        {
            get
            {
                return licenceFileField;
            }
            set
            {
                licenceFileField = value;

            }
        }

        public string LicenceHTML
        {
            get
            {
                return licenceHTMLField;
            }
            set
            {
                licenceHTMLField = value;

            }
        }

        public byte[] LicenceHTMLBytes
        {
            get
            {
                return licenceHTMLBytesField;
            }
            set
            {
                licenceHTMLBytesField = value;

            }
        }

        public string TaxAmount
        {
            get
            {
                return taxAmountField;
            }
            set
            {
                taxAmountField = value;

            }
        }

        public string LastRequestType
        {
            get
            {
                return lastRequestTypeField;
            }
            set
            {
                lastRequestTypeField = value;

            }
        }

        public int LastRequestRfrenceID
        {
            get
            {
                return lastRequestRfrenceIDField;
            }
            set
            {
                lastRequestRfrenceIDField = value;

            }
        }

        public DateTime LastRequestDate
        {
            get
            {
                return lastRequestDateField;
            }
            set
            {
                lastRequestDateField = value;

            }
        }

        public int FileRefrenceNumber
        {
            get
            {
                return fileRefrenceNumberField;
            }
            set
            {
                fileRefrenceNumberField = value;

            }
        }

        public string BuildingValue { get; set; }
        public string LandValue { get; set; }
        public string GoodWillValue { get; set; }
        public string TaxGoodWillValue { get; set; }
    }
}
