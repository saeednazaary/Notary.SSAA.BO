using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Queries
{
    public static class ReportSignRequestSeeds
    {
        // Static valid GUID strings
        private static readonly string _validGuid = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string _anotherValidGuid = "0a190675-b674-46d3-8ad6-33736ff33bdc";

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

        // Standard valid report query with GUID
        public static ReportSignRequestQuery StandardReportWithGuid { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _nullSignRequestNo
        };

        // Standard valid report query with request number
        public static ReportSignRequestQuery StandardReportWithRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Both GUID and request number provided
        public static ReportSignRequestQuery BothGuidAndRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Null SignRequestId (only request number)
        public static ReportSignRequestQuery NullSignRequestId { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Empty SignRequestId (only request number)
        public static ReportSignRequestQuery EmptySignRequestId { get; } = new()
        {
            SignRequestId = _emptyGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Invalid GUID SignRequestId (only request number)
        public static ReportSignRequestQuery InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = _invalidGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Null SignRequestNo (only GUID)
        public static ReportSignRequestQuery NullSignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _nullSignRequestNo
        };

        // Empty SignRequestNo (only GUID)
        public static ReportSignRequestQuery EmptySignRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _emptySignRequestNo
        };

        // Both SignRequestId and SignRequestNo null
        public static ReportSignRequestQuery BothNull { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _nullSignRequestNo
        };

        // Both SignRequestId and SignRequestNo empty
        public static ReportSignRequestQuery BothEmpty { get; } = new()
        {
            SignRequestId = _emptyGuid,
            SignRequestNo = _emptySignRequestNo
        };

        // Valid GUID without dashes
        public static ReportSignRequestQuery NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012",
            SignRequestNo = _nullSignRequestNo
        };

        // Valid GUID with uppercase
        public static ReportSignRequestQuery UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "ABCDEFAB-1234-5678-90AB-CDEF12345678",
            SignRequestNo = _nullSignRequestNo
        };

        // Long SignRequestNo
        public static ReportSignRequestQuery LongSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _longSignRequestNo
        };

        // Short SignRequestNo
        public static ReportSignRequestQuery ShortSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _shortSignRequestNo
        };

        // Whitespace SignRequestNo
        public static ReportSignRequestQuery WhitespaceSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _whitespaceSignRequestNo
        };

        // Special characters in SignRequestNo
        public static ReportSignRequestQuery SpecialCharsSignRequestNoRequest { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _specialCharsSignRequestNo
        };

        // Different SignRequestNo
        public static ReportSignRequestQuery DifferentSignRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = _anotherValidSignRequestNo
        };

        // Different GUID
        public static ReportSignRequestQuery DifferentGuidRequest { get; } = new()
        {
            SignRequestId = _anotherValidGuid,
            SignRequestNo = _nullSignRequestNo
        };

        // GUID with braces (invalid)
        public static ReportSignRequestQuery BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{12345678-1234-1234-1234-123456789012}",
            SignRequestNo = _nullSignRequestNo
        };

        // GUID with non-hex characters (invalid)
        public static ReportSignRequestQuery NonHexCharactersRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-12345678901G",
            SignRequestNo = _nullSignRequestNo
        };

        // Minimum valid GUID
        public static ReportSignRequestQuery MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000",
            SignRequestNo = _nullSignRequestNo
        };

        // Numeric SignRequestNo
        public static ReportSignRequestQuery NumericSignRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "2024001"
        };

        // Alphanumeric SignRequestNo
        public static ReportSignRequestQuery AlphanumericSignRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "SRN2024001ABC"
        };

        // Invalid GUID with valid request number
        public static ReportSignRequestQuery InvalidGuidWithValidRequestNo { get; } = new()
        {
            SignRequestId = _invalidGuid,
            SignRequestNo = _validSignRequestNo
        };

        // Valid GUID with invalid request number
        public static ReportSignRequestQuery ValidGuidWithInvalidRequestNo { get; } = new()
        {
            SignRequestId = _validGuid,
            SignRequestNo = _specialCharsSignRequestNo
        };

        // Both invalid
        public static ReportSignRequestQuery BothInvalid { get; } = new()
        {
            SignRequestId = _invalidGuid,
            SignRequestNo = _specialCharsSignRequestNo
        };

        // Edge case: very long string for both
        public static ReportSignRequestQuery VeryLongStringsRequest { get; } = new()
        {
            SignRequestId = new string('X', 100),
            SignRequestNo = new string('Y', 100)
        };

        // GUID with request number containing only numbers
        public static ReportSignRequestQuery NumericOnlyRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "1234567890"
        };

        // GUID with request number containing only letters
        public static ReportSignRequestQuery LettersOnlyRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "ABCDEFGHIJ"
        };

        // GUID with request number containing hyphens
        public static ReportSignRequestQuery HyphenatedRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "SRN-2024-001-001"
        };

        // GUID with request number containing underscores
        public static ReportSignRequestQuery UnderscoredRequestNo { get; } = new()
        {
            SignRequestId = _nullGuid,
            SignRequestNo = "SRN_2024_001"
        };
    }
}