using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class UpdateSignRequestPaymentStateSeeds
    {
        // Static valid GUID strings
        private static readonly string _validGuid = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string _anotherValidGuid = "0a190675-b674-46d3-8ad6-33736ff33bdc";
        private static readonly string IntegrationTest = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";

        // Invalid GUID strings
        private static readonly string _invalidGuid = "not-a-valid-guid";
        private static readonly string _emptyGuid = "";
        private static readonly string _nullGuid = null;
        private static readonly string _shortGuid = "123";
        private static readonly string _longGuid = "12345678-1234-1234-1234-1234567890123";

        // SignRequestNo values
        private static readonly string _validSignRequestNo = "SRN-2024-001";
        private static readonly string _anotherValidSignRequestNo = "SRN-2024-002";
        private static readonly string _emptySignRequestNo = "";
        private static readonly string _nullSignRequestNo = null;
        private static readonly string _longSignRequestNo = new string('A', 50);
        private static readonly string _shortSignRequestNo = "A";
        private static readonly string _whitespaceSignRequestNo = "   ";
        private static readonly string _specialCharsSignRequestNo = "SRN-2024-001@#$";

        // Standard valid payment state update request
        //مرحله3-1
        public static UpdateSignRequestPaymentStateCommand IntegrationTestRequest { get; } = new()
        {
            SignRequestId = IntegrationTest,
            SignRequestNo = "",
            InquiryMode = false
        };

        // Standard valid request with inquiry mode true
        public static UpdateSignRequestPaymentStateCommand InquiryModeRequest { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = true
        };

        // Null SignRequestId
        public static UpdateSignRequestPaymentStateCommand NullSignRequestId { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Empty SignRequestId
        public static UpdateSignRequestPaymentStateCommand EmptySignRequestId { get; } = new()
        {
            SignRequestId = _emptyGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Invalid GUID SignRequestId
        public static UpdateSignRequestPaymentStateCommand InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = _invalidGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Null SignRequestNo
        public static UpdateSignRequestPaymentStateCommand NullSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _nullSignRequestNo,
            InquiryMode = false
        };

        // Empty SignRequestNo
        public static UpdateSignRequestPaymentStateCommand EmptySignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _emptySignRequestNo,
            InquiryMode = false
        };

        // Both SignRequestId and SignRequestNo null
        public static UpdateSignRequestPaymentStateCommand BothNull { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _nullSignRequestNo,
            InquiryMode = false
        };

        // Both SignRequestId and SignRequestNo empty
        public static UpdateSignRequestPaymentStateCommand BothEmpty { get; } = new()
        {
            SignRequestId = _emptyGuid,
            SignRequestNo = _emptySignRequestNo,
            InquiryMode = false
        };

        // Valid GUID without dashes
        public static UpdateSignRequestPaymentStateCommand NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012",
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Valid GUID with uppercase
        public static UpdateSignRequestPaymentStateCommand UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc",
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Long SignRequestNo
        public static UpdateSignRequestPaymentStateCommand LongSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _longSignRequestNo,
            InquiryMode = false
        };

        // Short SignRequestNo
        public static UpdateSignRequestPaymentStateCommand ShortSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _shortSignRequestNo,
            InquiryMode = false
        };

        // Whitespace SignRequestNo
        public static UpdateSignRequestPaymentStateCommand WhitespaceSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _whitespaceSignRequestNo,
            InquiryMode = false
        };

        // Special characters in SignRequestNo
        public static UpdateSignRequestPaymentStateCommand SpecialCharsSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _specialCharsSignRequestNo,
            InquiryMode = false
        };

        // Different SignRequestNo
        public static UpdateSignRequestPaymentStateCommand DifferentSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _anotherValidSignRequestNo,
            InquiryMode = false
        };

        // Different GUID
        public static UpdateSignRequestPaymentStateCommand DifferentGuidRequest { get; } = new()
        {
            SignRequestId = _anotherValidGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Inquiry mode with invalid GUID
        public static UpdateSignRequestPaymentStateCommand InquiryModeWithInvalidGuid { get; } = new()
        {
            SignRequestId = _invalidGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = true
        };

        // Inquiry mode with null SignRequestNo
        public static UpdateSignRequestPaymentStateCommand InquiryModeWithNullSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _nullSignRequestNo,
            InquiryMode = true
        };

        // All properties valid with inquiry true
        public static UpdateSignRequestPaymentStateCommand AllValidWithInquiry { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = true
        };

        // All properties valid with inquiry false
        public static UpdateSignRequestPaymentStateCommand AllValidWithoutInquiry { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // GUID with braces (invalid)
        public static UpdateSignRequestPaymentStateCommand BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{62f720b6-c03b-4dfd-908e-cff35e8c6bed}",
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // GUID with non-hex characters (invalid)
        public static UpdateSignRequestPaymentStateCommand NonHexCharactersRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-12345678901G",
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Minimum valid GUID
        public static UpdateSignRequestPaymentStateCommand MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000",
            SignRequestNo = _validSignRequestNo,
            InquiryMode = false
        };

        // Numeric SignRequestNo
        public static UpdateSignRequestPaymentStateCommand NumericSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = "2024001",
            InquiryMode = false
        };

        // Alphanumeric SignRequestNo
        public static UpdateSignRequestPaymentStateCommand AlphanumericSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = "SRN2024001ABC",
            InquiryMode = false
        };

        // Edge case: very long string for both
        public static UpdateSignRequestPaymentStateCommand VeryLongStringsRequest { get; } = new()
        {
            SignRequestId = new string('X', 100),
            SignRequestNo = new string('Y', 100),
            InquiryMode = false
        };

        // Inquiry mode with empty SignRequestNo
        public static UpdateSignRequestPaymentStateCommand InquiryModeWithEmptySignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _emptySignRequestNo,
            InquiryMode = true
        };

        // Inquiry mode with whitespace SignRequestNo
        public static UpdateSignRequestPaymentStateCommand InquiryModeWithWhitespaceSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _whitespaceSignRequestNo,
            InquiryMode = true
        };
    }
}