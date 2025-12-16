using Notary.SSAA.BO.DataTransferObject.Bases;
using Notary.SSAA.BO.DataTransferObject.ViewModels.ExternalServices.Estate;
using Notary.SSAA.BO.SharedKernel.Result;

namespace Notary.SSAA.BO.DataTransferObject.ServiceInputs.ExternalServices.Estate
{
    public class SendEstateTaxInquiryInput : BaseExternalRequest<ApiResult<EstateTaxInquiyServiceResultObject>>
    {
        public SendEstateTaxInquiryInput()
        {
            ClientId = "SSAR";
        }
        public string ClientId { get; set; }
        public string ServiceID { get; set; }
        public string NationalID { get; set; }
        public int FacilityLawYear { get; set; }
        public string FacilityLawNumber { get; set; }
        public TaxAffairsInquiryObject AmlakObject { get; set; }
    }

    public partial class TaxAffairsInquiryObject
    {

        private _Specific_Information specific_InformationField;

        private _Notary_Public_Information notary_Public_InformationField;

        private _Information_Related_To_Property_Transfer_Right_Of_Object_Of_Transaction information_Related_To_Property_Transfer_Right_Of_Object_Of_TransactionField;

        private _Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction specifications_Of_Property_Transfer_Right_Of_Object_Of_TransactionField;

        private _Ownership_Deed_Specification ownership_Deed_SpecificationField;

        private _Information_Related_To_Renovation information_Related_To_RenovationField;

        private _Building_Specification building_SpecificationField;

        private _Specification_Of_Property_Sale_Contract_Transfer_Right specification_Of_Property_Sale_Contract_Transfer_RightField;

        private _Rescission_Cancellation_Deed rescission_Cancellation_DeedField;

        private _Mortgage_Deed mortgage_DeedField;

        private _Calculation_Of_Taxable_Basis_And_Applicable_Tax calculation_Of_Taxable_Basis_And_Applicable_TaxField;

        private _Calculation_Of_Transfer_Share_Coefficient calculation_Of_Transfer_Share_CoefficientField;

        private _Calculation_Of_Building_Share_Coefficient calculation_Of_Building_Share_CoefficientField;

        private _Calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_Building calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_BuildingField;

        private _Calculation_Of_Land_Transactional_Value calculation_Of_Land_Transactional_ValueField;

        private _Calculation_Of_Building_Transactional_Value calculation_Of_Building_Transactional_ValueField;

        private _Specifications_Of_Transfer_Right specifications_Of_Transfer_RightField;

        private _Contract_Lease_Contract_Specification contract_Lease_Contract_SpecificationField;

        private _Facilities facilitiesField;

        private _Footer footerField;

        private _Owner_Specification owner_SpecificationField;

        private _Owner_Specification[] owner_Specification_ListField;

        private _Buyers_Information buyers_InformationField;

        private _Buyers_Information[] buyers_Information_ListField;

        private _Payment_Related_To_This_Return payment_Related_To_This_ReturnField;

        private _RegistrationAddressStructure registrationAddressStructureField;

        public _Specific_Information Specific_Information
        {
            get
            {
                return specific_InformationField;
            }
            set
            {
                specific_InformationField = value;

            }
        }


        public _Notary_Public_Information Notary_Public_Information
        {
            get
            {
                return notary_Public_InformationField;
            }
            set
            {
                notary_Public_InformationField = value;

            }
        }

        public _Information_Related_To_Property_Transfer_Right_Of_Object_Of_Transaction Information_Related_To_Property_Transfer_Right_Of_Object_Of_Transaction
        {
            get
            {
                return information_Related_To_Property_Transfer_Right_Of_Object_Of_TransactionField;
            }
            set
            {
                information_Related_To_Property_Transfer_Right_Of_Object_Of_TransactionField = value;

            }
        }

        public _Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction
        {
            get
            {
                return specifications_Of_Property_Transfer_Right_Of_Object_Of_TransactionField;
            }
            set
            {
                specifications_Of_Property_Transfer_Right_Of_Object_Of_TransactionField = value;

            }
        }


        public _Ownership_Deed_Specification Ownership_Deed_Specification
        {
            get
            {
                return ownership_Deed_SpecificationField;
            }
            set
            {
                ownership_Deed_SpecificationField = value;

            }
        }

        public _Information_Related_To_Renovation Information_Related_To_Renovation
        {
            get
            {
                return information_Related_To_RenovationField;
            }
            set
            {
                information_Related_To_RenovationField = value;

            }
        }


        public _Building_Specification Building_Specification
        {
            get
            {
                return building_SpecificationField;
            }
            set
            {
                building_SpecificationField = value;

            }
        }


        public _Specification_Of_Property_Sale_Contract_Transfer_Right Specification_Of_Property_Sale_Contract_Transfer_Right
        {
            get
            {
                return specification_Of_Property_Sale_Contract_Transfer_RightField;
            }
            set
            {
                specification_Of_Property_Sale_Contract_Transfer_RightField = value;

            }
        }


        public _Rescission_Cancellation_Deed Rescission_Cancellation_Deed
        {
            get
            {
                return rescission_Cancellation_DeedField;
            }
            set
            {
                rescission_Cancellation_DeedField = value;

            }
        }


        public _Mortgage_Deed Mortgage_Deed
        {
            get
            {
                return mortgage_DeedField;
            }
            set
            {
                mortgage_DeedField = value;

            }
        }

        public _Calculation_Of_Taxable_Basis_And_Applicable_Tax Calculation_Of_Taxable_Basis_And_Applicable_Tax
        {
            get
            {
                return calculation_Of_Taxable_Basis_And_Applicable_TaxField;
            }
            set
            {
                calculation_Of_Taxable_Basis_And_Applicable_TaxField = value;

            }
        }

        public _Calculation_Of_Transfer_Share_Coefficient Calculation_Of_Transfer_Share_Coefficient
        {
            get
            {
                return calculation_Of_Transfer_Share_CoefficientField;
            }
            set
            {
                calculation_Of_Transfer_Share_CoefficientField = value;

            }
        }

        public _Calculation_Of_Building_Share_Coefficient Calculation_Of_Building_Share_Coefficient
        {
            get
            {
                return calculation_Of_Building_Share_CoefficientField;
            }
            set
            {
                calculation_Of_Building_Share_CoefficientField = value;

            }
        }

        public _Calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_Building Calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_Building
        {
            get
            {
                return calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_BuildingField;
            }
            set
            {
                calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_BuildingField = value;

            }
        }

        public _Calculation_Of_Land_Transactional_Value Calculation_Of_Land_Transactional_Value
        {
            get
            {
                return calculation_Of_Land_Transactional_ValueField;
            }
            set
            {
                calculation_Of_Land_Transactional_ValueField = value;

            }
        }

        public _Calculation_Of_Building_Transactional_Value Calculation_Of_Building_Transactional_Value
        {
            get
            {
                return calculation_Of_Building_Transactional_ValueField;
            }
            set
            {
                calculation_Of_Building_Transactional_ValueField = value;

            }
        }


        public _Specifications_Of_Transfer_Right Specifications_Of_Transfer_Right
        {
            get
            {
                return specifications_Of_Transfer_RightField;
            }
            set
            {
                specifications_Of_Transfer_RightField = value;

            }
        }

        public _Contract_Lease_Contract_Specification Contract_Lease_Contract_Specification
        {
            get
            {
                return contract_Lease_Contract_SpecificationField;
            }
            set
            {
                contract_Lease_Contract_SpecificationField = value;

            }
        }

        public _Facilities Facilities
        {
            get
            {
                return facilitiesField;
            }
            set
            {
                facilitiesField = value;

            }
        }

        public _Footer Footer
        {
            get
            {
                return footerField;
            }
            set
            {
                footerField = value;

            }
        }

        public _Owner_Specification Owner_Specification
        {
            get
            {
                return owner_SpecificationField;
            }
            set
            {
                owner_SpecificationField = value;

            }
        }

        public _Owner_Specification[] Owner_Specification_List
        {
            get
            {
                return owner_Specification_ListField;
            }
            set
            {
                owner_Specification_ListField = value;

            }
        }

        public _Buyers_Information Buyers_Information
        {
            get
            {
                return buyers_InformationField;
            }
            set
            {
                buyers_InformationField = value;

            }
        }

        public _Buyers_Information[] Buyers_Information_List
        {
            get
            {
                return buyers_Information_ListField;
            }
            set
            {
                buyers_Information_ListField = value;

            }
        }

        public _Payment_Related_To_This_Return Payment_Related_To_This_Return
        {
            get
            {
                return payment_Related_To_This_ReturnField;
            }
            set
            {
                payment_Related_To_This_ReturnField = value;

            }
        }

        public _RegistrationAddressStructure RegistrationAddressStructure
        {
            get
            {
                return registrationAddressStructureField;
            }
            set
            {
                registrationAddressStructureField = value;

            }
        }


    }

    public partial class _Specific_Information : object
    {

        private string sa_Estate_Transfer_TypeField;

        private string sa_Is_It_The_First_TransferField;

        private string followCodeField;

        private string sa_Transfer_DateField;

        private string sa_Previous_Transactions_Based_Facilitate_LawField;

        private string sa_Assessor_OfficeField;

        private string sa_Has_Transfered_Estate_More_One_Property_Usage_And_Dose_Not_Seperated_DeedField;

        private string mobileField;

        private string requestTypeField;

        public string Sa_Estate_Transfer_Type
        {
            get
            {
                return sa_Estate_Transfer_TypeField;
            }
            set
            {
                sa_Estate_Transfer_TypeField = value;

            }
        }

        public string Sa_Is_It_The_First_Transfer
        {
            get
            {
                return sa_Is_It_The_First_TransferField;
            }
            set
            {
                sa_Is_It_The_First_TransferField = value;

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


        public string Sa_Transfer_Date
        {
            get
            {
                return sa_Transfer_DateField;
            }
            set
            {
                sa_Transfer_DateField = value;

            }
        }

        public string Sa_Previous_Transactions_Based_Facilitate_Law
        {
            get
            {
                return sa_Previous_Transactions_Based_Facilitate_LawField;
            }
            set
            {
                sa_Previous_Transactions_Based_Facilitate_LawField = value;

            }
        }

        public string Sa_Assessor_Office
        {
            get
            {
                return sa_Assessor_OfficeField;
            }
            set
            {
                sa_Assessor_OfficeField = value;

            }
        }

        public string Sa_Has_Transfered_Estate_More_One_Property_Usage_And_Dose_Not_Seperated_Deed
        {
            get
            {
                return sa_Has_Transfered_Estate_More_One_Property_Usage_And_Dose_Not_Seperated_DeedField;
            }
            set
            {
                sa_Has_Transfered_Estate_More_One_Property_Usage_And_Dose_Not_Seperated_DeedField = value;

            }
        }

        public string Mobile
        {
            get
            {
                return mobileField;
            }
            set
            {
                mobileField = value;

            }
        }

        public string RequestType
        {
            get
            {
                return requestTypeField;
            }
            set
            {
                requestTypeField = value;

            }
        }

    }

    public partial class LocationResult : object
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




    public partial class _RegistrationAddressStructure : object
    {


    }

    public partial class _Payment_Related_To_This_Return : object
    {


    }

    public partial class _Buyers_Information : object
    {

        private string nameField;

        private string surnameField;

        private string national_Identifier_National_ID_CodeField;

        private string national_IDField;

        private string birthDateField;

        private string purchased_SharesField;

        private string total_ShareField;

        private string fatherNameField;

        private string registerNumberField;

        public string Name
        {
            get
            {
                return nameField;
            }
            set
            {
                nameField = value;

            }
        }

        public string Surname
        {
            get
            {
                return surnameField;
            }
            set
            {
                surnameField = value;

            }
        }

        public string National_Identifier_National_ID_Code
        {
            get
            {
                return national_Identifier_National_ID_CodeField;
            }
            set
            {
                national_Identifier_National_ID_CodeField = value;

            }
        }

        public string National_ID
        {
            get
            {
                return national_IDField;
            }
            set
            {
                national_IDField = value;

            }
        }

        public string BirthDate
        {
            get
            {
                return birthDateField;
            }
            set
            {
                birthDateField = value;

            }
        }

        public string Purchased_Shares
        {
            get
            {
                return purchased_SharesField;
            }
            set
            {
                purchased_SharesField = value;

            }
        }

        public string Total_Share
        {
            get
            {
                return total_ShareField;
            }
            set
            {
                total_ShareField = value;

            }
        }

        public string FatherName
        {
            get
            {
                return fatherNameField;
            }
            set
            {
                fatherNameField = value;

            }
        }

        public string RegisterNumber
        {
            get
            {
                return registerNumberField;
            }
            set
            {
                registerNumberField = value;

            }
        }


    }

    public partial class _Owner_Specification : object
    {

        private string citizenship_TypeField;

        private string landlord_NameField;

        private string surnameField;

        private string national_NoField;

        private string national_ID_National_CodeField;

        private string foreign_National_CodeField;

        private string birthDateField;

        private string fatherNameField;

        private string registerNumberField;

        private string addressField;

        private string provinceField;

        private string countyField;

        private string cityField;

        private string postalCodeField;

        public string Citizenship_Type
        {
            get
            {
                return citizenship_TypeField;
            }
            set
            {
                citizenship_TypeField = value;

            }
        }


        public string Landlord_Name
        {
            get
            {
                return landlord_NameField;
            }
            set
            {
                landlord_NameField = value;

            }
        }

        public string Surname
        {
            get
            {
                return surnameField;
            }
            set
            {
                surnameField = value;

            }
        }

        public string National_No
        {
            get
            {
                return national_NoField;
            }
            set
            {
                national_NoField = value;

            }
        }

        public string National_ID_National_Code
        {
            get
            {
                return national_ID_National_CodeField;
            }
            set
            {
                national_ID_National_CodeField = value;

            }
        }

        public string Foreign_National_Code
        {
            get
            {
                return foreign_National_CodeField;
            }
            set
            {
                foreign_National_CodeField = value;

            }
        }

        public string BirthDate
        {
            get
            {
                return birthDateField;
            }
            set
            {
                birthDateField = value;

            }
        }

        public string FatherName
        {
            get
            {
                return fatherNameField;
            }
            set
            {
                fatherNameField = value;

            }
        }

        public string RegisterNumber
        {
            get
            {
                return registerNumberField;
            }
            set
            {
                registerNumberField = value;

            }
        }

        public string Address
        {
            get
            {
                return addressField;
            }
            set
            {
                addressField = value;

            }
        }

        public string Province
        {
            get
            {
                return provinceField;
            }
            set
            {
                provinceField = value;

            }
        }

        public string County
        {
            get
            {
                return countyField;
            }
            set
            {
                countyField = value;

            }
        }

        public string City
        {
            get
            {
                return cityField;
            }
            set
            {
                cityField = value;

            }
        }

        public string PostalCode
        {
            get
            {
                return postalCodeField;
            }
            set
            {
                postalCodeField = value;

            }
        }


    }

    public partial class _Footer : object
    {


    }

    public partial class _Facilities : object
    {

        private string goodwill_Application_TypeField;

        private string transferred_Goodwill_Value_Based_ContractField;

        public string Goodwill_Application_Type
        {
            get
            {
                return goodwill_Application_TypeField;
            }
            set
            {
                goodwill_Application_TypeField = value;

            }
        }

        public string Transferred_Goodwill_Value_Based_Contract
        {
            get
            {
                return transferred_Goodwill_Value_Based_ContractField;
            }
            set
            {
                transferred_Goodwill_Value_Based_ContractField = value;

            }
        }


    }

    public partial class _Contract_Lease_Contract_Specification : object
    {

    }

    public partial class _Specifications_Of_Transfer_Right : object
    {

        private string goodwill_Ownership_TypeField;

        public string Goodwill_Ownership_Type
        {
            get
            {
                return goodwill_Ownership_TypeField;
            }
            set
            {
                goodwill_Ownership_TypeField = value;

            }
        }


    }

    public partial class _Calculation_Of_Building_Transactional_Value : object
    {

        private string structure_TypeField;

        private string building_StatusField;

        private string construction_StageField;

        private string application_Type_Completed_BuildingsField;

        private string is_it_the_enfeebled_restrictrField;

        public string Structure_Type
        {
            get
            {
                return structure_TypeField;
            }
            set
            {
                structure_TypeField = value;

            }
        }

        public string Building_Status
        {
            get
            {
                return building_StatusField;
            }
            set
            {
                building_StatusField = value;

            }
        }

        public string Construction_Stage
        {
            get
            {
                return construction_StageField;
            }
            set
            {
                construction_StageField = value;

            }
        }

        public string Application_Type_Completed_Buildings
        {
            get
            {
                return application_Type_Completed_BuildingsField;
            }
            set
            {
                application_Type_Completed_BuildingsField = value;

            }
        }

        public string is_it_the_enfeebled_restrictr
        {
            get
            {
                return is_it_the_enfeebled_restrictrField;
            }
            set
            {
                is_it_the_enfeebled_restrictrField = value;

            }
        }


    }

    public partial class _Calculation_Of_Land_Transactional_Value : object
    {

        private string block_Number_Based_Transactional_Value_BookletField;

        private string row_Number_Based_On_Transactional_Value_BookletField;

        private string property_Application_TypeField;

        private string total_Area_Of_The_LandField;

        private string private_PasswaysField;

        private string land_SpecificationField;

        private string land_Passway_WidthField;

        private string does_Property_Have_Determined_Transactional_ValueField;

        public string Block_Number_Based_Transactional_Value_Booklet
        {
            get
            {
                return block_Number_Based_Transactional_Value_BookletField;
            }
            set
            {
                block_Number_Based_Transactional_Value_BookletField = value;

            }
        }

        public string Row_Number_Based_On_Transactional_Value_Booklet
        {
            get
            {
                return row_Number_Based_On_Transactional_Value_BookletField;
            }
            set
            {
                row_Number_Based_On_Transactional_Value_BookletField = value;

            }
        }

        public string Property_Application_Type
        {
            get
            {
                return property_Application_TypeField;
            }
            set
            {
                property_Application_TypeField = value;

            }
        }

        public string Total_Area_Of_The_Land
        {
            get
            {
                return total_Area_Of_The_LandField;
            }
            set
            {
                total_Area_Of_The_LandField = value;

            }
        }

        public string Private_Passways
        {
            get
            {
                return private_PasswaysField;
            }
            set
            {
                private_PasswaysField = value;

            }
        }

        public string Land_Specification
        {
            get
            {
                return land_SpecificationField;
            }
            set
            {
                land_SpecificationField = value;

            }
        }

        public string Land_Passway_Width
        {
            get
            {
                return land_Passway_WidthField;
            }
            set
            {
                land_Passway_WidthField = value;

            }
        }


        public string Does_Property_Have_Determined_Transactional_Value
        {
            get
            {
                return does_Property_Have_Determined_Transactional_ValueField;
            }
            set
            {
                does_Property_Have_Determined_Transactional_ValueField = value;

            }
        }


    }

    public partial class _Calculation_Of_Taxable_Transactional_Value_And_Applicable_Tax_Of_Land_And_Building : object
    {

    }

    public partial class _Calculation_Of_Building_Share_Coefficient : object
    {

        private string applicable_Area_BuildingField;

        private string warehouse_AreaField;

        private string area_Of_The_ParkingField;

        private string total_Applicable_Areas_Whole_BuildingField;

        public string Applicable_Area_Building
        {
            get
            {
                return applicable_Area_BuildingField;
            }
            set
            {
                applicable_Area_BuildingField = value;

            }
        }

        public string Warehouse_Area
        {
            get
            {
                return warehouse_AreaField;
            }
            set
            {
                warehouse_AreaField = value;

            }
        }

        public string Area_Of_The_Parking
        {
            get
            {
                return area_Of_The_ParkingField;
            }
            set
            {
                area_Of_The_ParkingField = value;

            }
        }

        public string Total_Applicable_Areas_Whole_Building
        {
            get
            {
                return total_Applicable_Areas_Whole_BuildingField;
            }
            set
            {
                total_Applicable_Areas_Whole_BuildingField = value;

            }
        }

    }

    public partial class _Calculation_Of_Transfer_Share_Coefficient : object
    {

        private string share_From_OwnershipField;

        private string share_From_The_Whole_OwnershipField;

        private string transfered_Share_Amount_Per_Part_From_6_PartsField;

        public string Share_From_Ownership
        {
            get
            {
                return share_From_OwnershipField;
            }
            set
            {
                share_From_OwnershipField = value;

            }
        }

        public string Share_From_The_Whole_Ownership
        {
            get
            {
                return share_From_The_Whole_OwnershipField;
            }
            set
            {
                share_From_The_Whole_OwnershipField = value;

            }
        }

        public string Transfered_Share_Amount_Per_Part_From_6_Parts
        {
            get
            {
                return transfered_Share_Amount_Per_Part_From_6_PartsField;
            }
            set
            {
                transfered_Share_Amount_Per_Part_From_6_PartsField = value;

            }
        }

    }

    public partial class _Calculation_Of_Taxable_Basis_And_Applicable_Tax : object
    {

    }

    public partial class _Mortgage_Deed : object
    {


    }

    public partial class _Rescission_Cancellation_Deed : object
    {

    }

    public partial class _Specification_Of_Property_Sale_Contract_Transfer_Right : object
    {

        private string value_Of_The_Property_To_Be_Transferred_According_To_The_Sales_Contract_Or_ContractField;

        public string Value_Of_The_Property_To_Be_Transferred_According_To_The_Sales_Contract_Or_Contract
        {
            get
            {
                return value_Of_The_Property_To_Be_Transferred_According_To_The_Sales_Contract_Or_ContractField;
            }
            set
            {
                value_Of_The_Property_To_Be_Transferred_According_To_The_Sales_Contract_Or_ContractField = value;

            }
        }


    }

    public partial class _Building_Specification : object
    {

        private string contsraction_Certificate_DateField;

        private string issue_Date_Building_Termination_CertificateField;

        private string seperated_Minutes_RefrenceField;

        private string groundField;

        private string floor_NumberField;

        private string warehouse_NoField;

        private string parking_NoField;

        private string age_BuidlingField;

        public string Contsraction_Certificate_Date
        {
            get
            {
                return contsraction_Certificate_DateField;
            }
            set
            {
                contsraction_Certificate_DateField = value;

            }
        }

        public string Issue_Date_Building_Termination_Certificate
        {
            get
            {
                return issue_Date_Building_Termination_CertificateField;
            }
            set
            {
                issue_Date_Building_Termination_CertificateField = value;

            }
        }

        public string Seperated_Minutes_Refrence
        {
            get
            {
                return seperated_Minutes_RefrenceField;
            }
            set
            {
                seperated_Minutes_RefrenceField = value;

            }
        }

        public string Ground
        {
            get
            {
                return groundField;
            }
            set
            {
                groundField = value;

            }
        }

        public string Floor_Number
        {
            get
            {
                return floor_NumberField;
            }
            set
            {
                floor_NumberField = value;

            }
        }

        public string Warehouse_No
        {
            get
            {
                return warehouse_NoField;
            }
            set
            {
                warehouse_NoField = value;

            }
        }

        public string Parking_No
        {
            get
            {
                return parking_NoField;
            }
            set
            {
                parking_NoField = value;

            }
        }

        public string Age_Buidling
        {
            get
            {
                return age_BuidlingField;
            }
            set
            {
                age_BuidlingField = value;

            }
        }

    }

    public partial class _Information_Related_To_Renovation : object
    {

        private string rowField;

        private string real_EstateField;

        private string blockField;

        public string Row
        {
            get
            {
                return rowField;
            }
            set
            {
                rowField = value;

            }
        }

        public string Real_Estate
        {
            get
            {
                return real_EstateField;
            }
            set
            {
                real_EstateField = value;

            }
        }

        public string Block
        {
            get
            {
                return blockField;
            }
            set
            {
                blockField = value;

            }
        }

    }

    public partial class _Ownership_Deed_Specification : object
    {

        private string primary_Registered_NoField;

        private string secondary_Registered_NoField;

        private string land_Lot_NumberField;

        private string registry_DistrictField;

        public string Primary_Registered_No
        {
            get
            {
                return primary_Registered_NoField;
            }
            set
            {
                primary_Registered_NoField = value;

            }
        }

        public string Secondary_Registered_No
        {
            get
            {
                return secondary_Registered_NoField;
            }
            set
            {
                secondary_Registered_NoField = value;

            }
        }

        public string Land_Lot_Number
        {
            get
            {
                return land_Lot_NumberField;
            }
            set
            {
                land_Lot_NumberField = value;

            }
        }

        public string Registry_District
        {
            get
            {
                return registry_DistrictField;
            }
            set
            {
                registry_DistrictField = value;

            }
        }

    }

    public partial class _Specifications_Of_Property_Transfer_Right_Of_Object_Of_Transaction : object
    {

        private string provinceField;

        private string plate_NoField;

        private string postal_CodeField;

        private string townshipField;

        private string regionField;

        private string street_NameField;

        private string latitudeField;

        private string longitudeField;

        public string Province
        {
            get
            {
                return provinceField;
            }
            set
            {
                provinceField = value;

            }
        }

        public string Plate_No
        {
            get
            {
                return plate_NoField;
            }
            set
            {
                plate_NoField = value;

            }
        }

        public string Postal_Code
        {
            get
            {
                return postal_CodeField;
            }
            set
            {
                postal_CodeField = value;

            }
        }

        public string Township
        {
            get
            {
                return townshipField;
            }
            set
            {
                townshipField = value;

            }
        }

        public string Region
        {
            get
            {
                return regionField;
            }
            set
            {
                regionField = value;

            }
        }

        public string Street_Name
        {
            get
            {
                return street_NameField;
            }
            set
            {
                street_NameField = value;

            }
        }

        public string Latitude
        {
            get
            {
                return latitudeField;
            }
            set
            {
                latitudeField = value;

            }
        }

        public string Longitude
        {
            get
            {
                return longitudeField;
            }
            set
            {
                longitudeField = value;

            }
        }


    }

    public partial class _Information_Related_To_Property_Transfer_Right_Of_Object_Of_Transaction : object
    {


    }

    public partial class _Notary_Public_Information : object
    {

        private string notary_Office_NumberField;

        private string notary_Office_CityField;

        private string notary_Office_Enquiry_RefrenceField;

        private string notary_Office_Enquiry_DateField;

        private string type_Of_Request_For_Document_DraftingField;

        public string Notary_Office_Number
        {
            get
            {
                return notary_Office_NumberField;
            }
            set
            {
                notary_Office_NumberField = value;

            }
        }

        public string Notary_Office_City
        {
            get
            {
                return notary_Office_CityField;
            }
            set
            {
                notary_Office_CityField = value;

            }
        }

        public string Notary_Office_Enquiry_Refrence
        {
            get
            {
                return notary_Office_Enquiry_RefrenceField;
            }
            set
            {
                notary_Office_Enquiry_RefrenceField = value;

            }
        }

        public string Notary_Office_Enquiry_Date
        {
            get
            {
                return notary_Office_Enquiry_DateField;
            }
            set
            {
                notary_Office_Enquiry_DateField = value;

            }
        }

        public string Type_Of_Request_For_Document_Drafting
        {
            get
            {
                return type_Of_Request_For_Document_DraftingField;
            }
            set
            {
                type_Of_Request_For_Document_DraftingField = value;

            }
        }


    }
}
