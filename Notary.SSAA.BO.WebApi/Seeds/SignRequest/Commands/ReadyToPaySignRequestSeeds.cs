
using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class ReadyToPaySignRequestSeeds
    {
        // Static valid GUID strings
        private static readonly string ValidGuid = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string AnotherValidGuid = "0a190675-b674-46d3-8ad6-33736ff33bdc";

        // Invalid GUID strings
        private static readonly string InvalidGuid = "not-a-valid-guid";
        private static readonly string EmptyGuid = "";
        private static readonly string NullGuid = null;
        private static readonly string ShortGuid = "123";
        private static readonly string LongGuid = "12345678-1234-1234-1234-1234567890123"; // Too long

        // Standard valid ready to pay request
        public static ReadyToPaySignRequestCommand StandardReadyToPayRequest { get; } = new()
        {
            SignRequestId = ValidGuid
        };

        // Null SignRequestId
        public static ReadyToPaySignRequestCommand NullSignRequestId { get; } = new()
        {
            SignRequestId = NullGuid
        };

        // Empty SignRequestId (invalid GUID)
        public static ReadyToPaySignRequestCommand EmptySignRequestId { get; } = new()
        {
            SignRequestId = EmptyGuid
        };

        // Invalid GUID SignRequestId
        public static ReadyToPaySignRequestCommand InvalidGuidSignRequestId { get; } = new()
        {
            SignRequestId = InvalidGuid
        };

        // Short invalid GUID
        public static ReadyToPaySignRequestCommand ShortGuidRequest { get; } = new()
        {
            SignRequestId = ShortGuid
        };

        // Long invalid GUID
        public static ReadyToPaySignRequestCommand LongGuidRequest { get; } = new()
        {
            SignRequestId = LongGuid
        };

        // Valid GUID without dashes (N format)
        public static ReadyToPaySignRequestCommand NoDashesGuidRequest { get; } = new()
        {
            SignRequestId = "12345678123412341234123456789012"
        };

        // Valid GUID with uppercase
        public static ReadyToPaySignRequestCommand UpperCaseGuidRequest { get; } = new()
        {
            SignRequestId = "0a190675-b674-46d3-8ad6-33736ff33bdc"
        };

        // GUID with braces (invalid format)
        public static ReadyToPaySignRequestCommand BracedGuidRequest { get; } = new()
        {
            SignRequestId = "{62f720b6-c03b-4dfd-908e-cff35e8c6bed}"
        };

        // GUID with parentheses (invalid format)
        public static ReadyToPaySignRequestCommand ParenthesesGuidRequest { get; } = new()
        {
            SignRequestId = "(62f720b6-c03b-4dfd-908e-cff35e8c6bed)"
        };

        // Whitespace only SignRequestId
        public static ReadyToPaySignRequestCommand WhitespaceSignRequestId { get; } = new()
        {
            SignRequestId = "   "
        };

        // GUID with leading/trailing whitespace
        public static ReadyToPaySignRequestCommand WhitespaceAroundGuidRequest { get; } = new()
        {
            SignRequestId = "  62f720b6-c03b-4dfd-908e-cff35e8c6bed  "
        };

        // Another valid GUID for variety
        public static ReadyToPaySignRequestCommand AnotherValidGuidRequest { get; } = new()
        {
            SignRequestId = AnotherValidGuid
        };

        // GUID with mixed case
        public static ReadyToPaySignRequestCommand MixedCaseGuidRequest { get; } = new()
        {
            SignRequestId = "aBcDeF12-3456-7890-abCd-EF1234567890"
        };

        // GUID with special characters (invalid)
        public static ReadyToPaySignRequestCommand SpecialCharsGuidRequest { get; } = new()
        {
            SignRequestId = "guid-with-special!@#$%^&*()"
        };

        // Very long string that's not a GUID
        public static ReadyToPaySignRequestCommand LongStringRequest { get; } = new()
        {
            SignRequestId = new string('X', 100)
        };

        // GUID with extra characters appended (invalid)
        public static ReadyToPaySignRequestCommand ExtraCharsGuidRequest { get; } = new()
        {
            SignRequestId = ValidGuid + "extra"
        };

        // Minimum valid GUID (just the right length)
        public static ReadyToPaySignRequestCommand MinimumValidGuidRequest { get; } = new()
        {
            SignRequestId = "00000000-0000-0000-0000-000000000000"
        };
    }
}