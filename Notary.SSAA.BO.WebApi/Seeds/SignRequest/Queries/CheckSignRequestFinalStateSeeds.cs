using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;


namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Queries
{
    public static class CheckSignRequestFinalStateSeeds
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

        // Standard valid check final state query
        //مرحله4-1
        public static CheckSignRequestFinalStateQuery IntegrationTestRequest { get; } = new()
        {
            SignRequestId = IntegrationTest
        };

        // Null SignRequestId
        public static CheckSignRequestFinalStateQuery NullSignRequestId { get; } = new()
        {
            SignRequestId = _nullGuid
        };

        // Empty SignRequestId (invalid GUID)
        public static CheckSignRequestFinalStateQuery EmptySignRequestId { get; } = new()
        {
            SignRequestId = _emptyGuid
        };

        // Invalid GUID SignRequestId
        public static CheckSignRequestFinalStateQuery InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = _invalidGuid
        };

        // Short invalid GUID
        public static CheckSignRequestFinalStateQuery ShortGuidRequest { get; } = new()
        {
            SignRequestId = _shortGuid
        };

        // Long invalid GUID
        public static CheckSignRequestFinalStateQuery LongGuidRequest { get; } = new()
        {
            SignRequestId = _longGuid
        };

        // Valid GUID without dashes (N format)
        public static CheckSignRequestFinalStateQuery NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // Valid GUID with uppercase
        public static CheckSignRequestFinalStateQuery UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "ABCDEFAB-1234-5678-90AB-CDEF12345678"
        };

        // GUID with braces (invalid format)
        public static CheckSignRequestFinalStateQuery BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{12345678-1234-1234-1234-123456789012}"
        };

        // GUID with parentheses (invalid format)
        public static CheckSignRequestFinalStateQuery ParenthesesGuidRequest { get; } = new()
        {
            SignRequestId = "(12345678-1234-1234-1234-123456789012)"
        };

        // Whitespace only SignRequestId
        public static CheckSignRequestFinalStateQuery WhitespaceSignRequestId { get; } = new()
        {
            SignRequestId = "   "
        };

        // GUID with leading/trailing whitespace
        public static CheckSignRequestFinalStateQuery WhitespaceAroundGuidRequest { get; } = new()
        {
            SignRequestId = "  12345678-1234-1234-1234-123456789012  "
        };

        // Another valid GUID for variety
        public static CheckSignRequestFinalStateQuery AnotherValidGuidRequest { get; } = new()
        {
            SignRequestId = _anotherValidGuid
        };

        // GUID with mixed case
        public static CheckSignRequestFinalStateQuery MixedCaseGuidRequest { get; } = new()
        {
            SignRequestId = "aBcDeF12-3456-7890-abCd-EF1234567890"
        };

        // GUID with special characters (invalid)
        public static CheckSignRequestFinalStateQuery SpecialCharsGuidRequest { get; } = new()
        {
            SignRequestId = "guid-with-special!@#$%^&*()"
        };

        // Very long string that's not a GUID
        public static CheckSignRequestFinalStateQuery LongStringRequest { get; } = new()
        {
            SignRequestId = new string('X', 100)
        };

        // GUID with extra characters appended (invalid)
        public static CheckSignRequestFinalStateQuery ExtraCharsGuidRequest { get; } = new()
        {
            SignRequestId = _validGuid + "extra"
        };

        // Minimum valid GUID (just the right length)
        public static CheckSignRequestFinalStateQuery MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000"
        };

        // GUID with wrong hyphen positions (invalid)
        public static CheckSignRequestFinalStateQuery WrongHyphenPositionRequest { get; } = new()
        {
            SignRequestId = "123456781-234-1234-1234-123456789012"
        };

        // GUID with missing hyphens (invalid)
        public static CheckSignRequestFinalStateQuery MissingHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // GUID with extra hyphens (invalid)
        public static CheckSignRequestFinalStateQuery ExtraHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-1234-56789012"
        };

        // GUID with non-hex characters (invalid)
        public static CheckSignRequestFinalStateQuery NonHexCharactersRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-12345678901G" // Contains 'G' which is not hex
        };

        // GUID with only one section (invalid)
        public static CheckSignRequestFinalStateQuery SingleSectionRequest { get; } = new()
        {
            SignRequestId = "12345678"
        };

        // GUID with partial sections (invalid)
        public static CheckSignRequestFinalStateQuery PartialSectionsRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234"
        };

        // GUID with incorrect section lengths (invalid)
        public static CheckSignRequestFinalStateQuery WrongSectionLengthRequest { get; } = new()
        {
            SignRequestId = "1234567-1234-1234-1234-123456789012" // First section too short
        };

        // GUID with all F's (valid but edge case)
        public static CheckSignRequestFinalStateQuery AllFsGuidRequest { get; } = new()
        {
            SignRequestId = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
        };

        // GUID with all 1's (valid)
        public static CheckSignRequestFinalStateQuery AllOnesGuidRequest { get; } = new()
        {
            SignRequestId = "11111111-1111-1111-1111-111111111111"
        };

        // GUID with alternating pattern (valid)
        public static CheckSignRequestFinalStateQuery AlternatingPatternRequest { get; } = new()
        {
            SignRequestId = "A1B2C3D4-E5F6-7890-ABCD-EF1234567890"
        };
    }
}