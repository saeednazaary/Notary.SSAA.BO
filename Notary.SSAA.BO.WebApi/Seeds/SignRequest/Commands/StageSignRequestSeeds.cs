using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class StageSignRequestSeeds
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

        // Standard valid stage request
        //مرحله5-1
        public static StageSignRequestCommand IntegrationTestRequest { get; } = new()
        {
            SignRequestId = IntegrationTest
        };

        // Null SignRequestId
        public static StageSignRequestCommand NullSignRequestId { get; } = new()
        {
            SignRequestId = NullGuid
        };

        // Empty SignRequestId (invalid GUID)
        public static StageSignRequestCommand EmptySignRequestId { get; } = new()
        {
            SignRequestId = EmptyGuid
        };

        // Invalid GUID SignRequestId
        public static StageSignRequestCommand InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = InvalidGuid
        };

        // Short invalid GUID
        public static StageSignRequestCommand ShortGuidRequest { get; } = new()
        {
            SignRequestId = ShortGuid
        };

        // Long invalid GUID
        public static StageSignRequestCommand LongGuidRequest { get; } = new()
        {
            SignRequestId = LongGuid
        };

        // Valid GUID without dashes (N format)
        public static StageSignRequestCommand NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // Valid GUID with uppercase
        public static StageSignRequestCommand UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc"
        };

        // GUID with braces (invalid format)
        public static StageSignRequestCommand BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{62f720b6-c03b-4dfd-908e-cff35e8c6bed}"
        };

        // GUID with parentheses (invalid format)
        public static StageSignRequestCommand ParenthesesGuidRequest { get; } = new()
        {
            SignRequestId = "(62f720b6-c03b-4dfd-908e-cff35e8c6bed)"
        };

        // Whitespace only SignRequestId
        public static StageSignRequestCommand WhitespaceSignRequestId { get; } = new()
        {
            SignRequestId = "   "
        };

        // GUID with leading/trailing whitespace
        public static StageSignRequestCommand WhitespaceAroundGuidRequest { get; } = new()
        {
            SignRequestId = "  62f720b6-c03b-4dfd-908e-cff35e8c6bed  "
        };

        // Another valid GUID for variety
        public static StageSignRequestCommand AnotherValidGuidRequest { get; } = new()
        {
            SignRequestId = AnotherValidGuid
        };

        // GUID with mixed case
        public static StageSignRequestCommand MixedCaseGuidRequest { get; } = new()
        {
            SignRequestId = "aBcDeF12-3456-7890-abCd-EF1234567890"
        };

        // GUID with special characters (invalid)
        public static StageSignRequestCommand SpecialCharsGuidRequest { get; } = new()
        {
            SignRequestId = "guid-with-special!@#$%^&*()"
        };

        // Very long string that's not a GUID
        public static StageSignRequestCommand LongStringRequest { get; } = new()
        {
            SignRequestId = new string('X', 100)
        };

        // GUID with extra characters appended (invalid)
        public static StageSignRequestCommand ExtraCharsGuidRequest { get; } = new()
        {
            SignRequestId = ValidGuid + "extra"
        };

        // Minimum valid GUID (just the right length)
        public static StageSignRequestCommand MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000"
        };

        // GUID with wrong hyphen positions (invalid)
        public static StageSignRequestCommand WrongHyphenPositionRequest { get; } = new()
        {
            SignRequestId = "123456781-234-1234-1234-123456789012"
        };

        // GUID with missing hyphens (invalid)
        public static StageSignRequestCommand MissingHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // GUID with extra hyphens (invalid)
        public static StageSignRequestCommand ExtraHyphensRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-1234-56789012"
        };

        // GUID with non-hex characters (invalid)
        public static StageSignRequestCommand NonHexCharactersRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234-12345678901G" // Contains 'G' which is not hex
        };

        // GUID with only one section (invalid)
        public static StageSignRequestCommand SingleSectionRequest { get; } = new()
        {
            SignRequestId = "12345678"
        };

        // GUID with partial sections (invalid)
        public static StageSignRequestCommand PartialSectionsRequest { get; } = new()
        {
            SignRequestId = "12345678-1234-1234-1234"
        };

        // GUID with incorrect section lengths (invalid)
        public static StageSignRequestCommand WrongSectionLengthRequest { get; } = new()
        {
            SignRequestId = "1234567-1234-1234-1234-123456789012" // First section too short
        };
    }
}