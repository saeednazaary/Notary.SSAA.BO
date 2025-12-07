using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate
{
    
    public partial class EstateTaxInquiryServiceBlockRadifObject : object
    {

        private int errorIDField;

        private string errorMessageField;

        private string blockNumberField;

        private string radifNumberField;

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

        public string BlockNumber
        {
            get
            {
                return blockNumberField;
            }
            set
            {
                blockNumberField = value;

            }
        }

        public string RadifNumber
        {
            get
            {
                return radifNumberField;
            }
            set
            {
                radifNumberField = value;

            }
        }


    }
}
