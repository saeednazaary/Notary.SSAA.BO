using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;


namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class UpdateSignRequestFingerprintStateSeeds
    {
        // Static valid GUID strings
        private static readonly string ValidGuid = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string AnotherValidGuid = "0a190675-b674-46d3-8ad6-33736ff33bdc";
        private static readonly string IntegrationTest = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";

        // Invalid GUID strings
        private static readonly string InvalidGuid = "not-a-valid-guid";
        private static readonly string EmptyGuid = "";
        private static readonly string NullGuid = null;
        private static readonly string ShortGuid = "123";
        private static readonly string LongGuid = "12345678-1234-1234-1234-1234567890123"; // Too long

        // Standard valid update fingerprint state request
        //مرحله2-1
        public static UpdateSignRequestFingerprintStateCommand IntegrationTestRequest { get; } = new()
        {
            SignRequestId = IntegrationTest
        };

        // Null SignRequestId
        public static UpdateSignRequestFingerprintStateCommand NullSignRequestId { get; } = new()
        {
            SignRequestId = NullGuid
        };

        // Empty SignRequestId (invalid GUID)
        public static UpdateSignRequestFingerprintStateCommand EmptySignRequestId { get; } = new()
        {
            SignRequestId = EmptyGuid
        };

        // Invalid GUID SignRequestId
        public static UpdateSignRequestFingerprintStateCommand InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = InvalidGuid
        };

        // Short invalid GUID
        public static UpdateSignRequestFingerprintStateCommand ShortGuidRequest { get; } = new()
        {
            SignRequestId = ShortGuid
        };

        // Long invalid GUID
        public static UpdateSignRequestFingerprintStateCommand LongGuidRequest { get; } = new()
        {
            SignRequestId = LongGuid
        };

        // Valid GUID without dashes (N format)
        public static UpdateSignRequestFingerprintStateCommand NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // Valid GUID with uppercase
        public static UpdateSignRequestFingerprintStateCommand UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc"
        };

        // GUID with braces (invalid format)
        public static UpdateSignRequestFingerprintStateCommand BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{62f720b6-c03b-4dfd-908e-cff35e8c6bed}"
        };

        // GUID with parentheses (invalid format)
        public static UpdateSignRequestFingerprintStateCommand ParenthesesGuidRequest { get; } = new()
        {
            SignRequestId = "(62f720b6-c03b-4dfd-908e-cff35e8c6bed)"
        };

        // Whitespace only SignRequestId
        public static UpdateSignRequestFingerprintStateCommand WhitespaceSignRequestId { get; } = new()
        {
            SignRequestId = "   "
        };

        // GUID with leading/trailing whitespace
        public static UpdateSignRequestFingerprintStateCommand WhitespaceAroundGuidRequest { get; } = new()
        {
            SignRequestId = "  62f720b6-c03b-4dfd-908e-cff35e8c6bed  "
        };

        // Another valid GUID for variety
        public static UpdateSignRequestFingerprintStateCommand AnotherValidGuidRequest { get; } = new()
        {
            SignRequestId = AnotherValidGuid
        };

        // GUID with mixed case
        public static UpdateSignRequestFingerprintStateCommand MixedCaseGuidRequest { get; } = new()
        {
            SignRequestId = "aBcDeF12-3456-7890-abCd-EF1234567890"
        };

        // GUID with special characters (invalid)
        public static UpdateSignRequestFingerprintStateCommand SpecialCharsGuidRequest { get; } = new()
        {
            SignRequestId = "guid-with-special!@#$%^&*()"
        };

        // Very long string that's not a GUID
        public static UpdateSignRequestFingerprintStateCommand LongStringRequest { get; } = new()
        {
            SignRequestId = new string('X', 100)
        };

        // GUID with extra characters appended (invalid)
        public static UpdateSignRequestFingerprintStateCommand ExtraCharsGuidRequest { get; } = new()
        {
            SignRequestId = ValidGuid + "extra"
        };

        // Minimum valid GUID (just the right length)
        public static UpdateSignRequestFingerprintStateCommand MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000"
        };

        // GUID with wrong hyphen positions (invalid)
        public static UpdateSignRequestFingerprintStateCommand WrongHyphenPositionRequest { get; } = new()
        {
            SignRequestId = "123456781-234-1234-1234-123456789012"
        };

        // GUID with missing hyphens (invalid)
        public static UpdateSignRequestFingerprintStateCommand MissingHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // GUID with extra hyphens (invalid)
        public static UpdateSignRequestFingerprintStateCommand ExtraHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-1234-56789012"
        };

        // GUID with non-hex characters (invalid)
        public static UpdateSignRequestFingerprintStateCommand NonHexCharactersRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-12345678901G" // Contains 'G' which is not hex
        };

        // GUID with only one section (invalid)
        public static UpdateSignRequestFingerprintStateCommand SingleSectionRequest { get; } = new()
        {
            SignRequestId = "12345678"
        };

        // GUID with partial sections (invalid)
        public static UpdateSignRequestFingerprintStateCommand PartialSectionsRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234"
        };

        // GUID with incorrect section lengths (invalid)
        public static UpdateSignRequestFingerprintStateCommand WrongSectionLengthRequest { get; } = new()
        {
            SignRequestId = "1234567-1234-1234-1234-123456789012" // First section too short
        };

        // GUID with all F's (valid but edge case)
        public static UpdateSignRequestFingerprintStateCommand AllFsGuidRequest { get; } = new()
        {
            SignRequestId = "FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"
        };

        // GUID with all 1's (valid)
        public static UpdateSignRequestFingerprintStateCommand AllOnesGuidRequest { get; } = new()
        {
            SignRequestId = "11111111-1111-1111-1111-111111111111"
        };

        // GUID with alternating pattern (valid)
        public static UpdateSignRequestFingerprintStateCommand AlternatingPatternRequest { get; } = new()
        {
            SignRequestId = "A1B2C3D4-E5F6-7890-ABCD-EF1234567890"
        };
    }
}