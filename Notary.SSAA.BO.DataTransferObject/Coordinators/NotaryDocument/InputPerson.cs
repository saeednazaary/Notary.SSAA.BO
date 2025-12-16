using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mapster.Utils;
using Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.Utilities.Extensions;
using Stimulsoft.Blockly.Model;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Notary.SSAA.BO.DataTransferObject.Coordinators.NotaryDocument
{
    public class InputPerson
    {
        public string NationalCode
        {
            get { return _NationalCode; }
            set { _NationalCode = value; }
        }
        
        public string BirthDate
        {
            get { return _BirthDate; }
            set { _BirthDate = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public string Family
        {
            get { return _Family; }
            set { _Family = value; }
        }

        public string FatherName
        {
            get { return _FatherName; }
            set { _FatherName = value; }
        }

        public string BackSerialNo
        {
            get { return _BackSerialNo; }
            set { _BackSerialNo = value; }
        }

        public long shenasnameNo
        {
            get { return _shenasnameNo; }
            set { _shenasnameNo = value; }
        }

        public NotaryAlphabetLetter shenasnamehLetter 
        {
            get { return _shenasnamehLetter; }
            set { _shenasnamehLetter = value; }
        }
        public string shenasnamehSeri
        {
            get { return _shenasnamehSeri; }
            set { _shenasnamehSeri = value; }
        }
        public int shenasnamehSerial 
        {
            get { return _shenasnamehSerial; }
            set { _shenasnamehSerial = value; }
        }

        private string _NationalCode;
        private string _BirthDate;
        private string _Name;
        private string _Family;
        private string _FatherName;
        private string _BackSerialNo;
        private long _shenasnameNo;
        private NotaryAlphabetLetter _shenasnamehLetter;
        private string _shenasnamehSeri;
        private int _shenasnamehSerial;

        public InputPerson(DocumentPerson theOneDocPerson)
        {
            if (theOneDocPerson == null)
                return;
            NationalCode = theOneDocPerson.NationalNo;
            BirthDate = (!string.IsNullOrWhiteSpace(theOneDocPerson.BirthDate)) ? theOneDocPerson.BirthDate : theOneDocPerson.BirthYear.ToString();
            Name = theOneDocPerson.Name;
            Family = theOneDocPerson.Family;
            FatherName = theOneDocPerson.FatherName;

            if (!string.IsNullOrEmpty(theOneDocPerson.IdentityNo))
            {
                Int64.TryParse(theOneDocPerson.IdentityNo, out _shenasnameNo);
            }

            if (!string.IsNullOrEmpty(theOneDocPerson.Serial))
            {
                Int32.TryParse(theOneDocPerson.Serial, out _shenasnamehSerial);
            }
            shenasnamehLetter = ( NotaryAlphabetLetter ) Enum.Parse ( typeof ( NotaryAlphabetLetter ), theOneDocPerson.SeriAlpha );
            shenasnamehSeri = theOneDocPerson.Seri;
        }

        public InputPerson(EstelamPersonInputMessage input)
        {
            if (input == null)
                return;

            NationalCode = input.NationalCode;
            BirthDate = input.BirthDate;
            Name = input.Name;
            Family = input.Family;
            FatherName = input.fatherName;
            shenasnameNo = input.shenasnameNo;
            shenasnamehSerial = input.shenasnamehSerial;
            shenasnamehLetter = input.shenasnamehLetter;
            shenasnamehSeri = input.shenasnamehSeri;
            BackSerialNo = input.nationalCardSerialNo;
        }

        public InputPerson(SabtAhvalRequestPacket input)
        {
            if (input == null)
                return;

            NationalCode = input.NationalCode;
            BirthDate = input.BirthDate;
            Name = input.Name;
            Family = input.Family;
            FatherName = input.FatherName;
            shenasnameNo = input.ShenasnameNo;
            shenasnamehSerial = input.ShenasnamehSerial;
            shenasnamehLetter = input.ShenasnamehLetter;
            shenasnamehSeri = input.ShenasnamehSeri;
            BackSerialNo = input.BackSerialNo;
        }

        public InputPerson()
        {
        }
    }
}
