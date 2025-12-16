using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{


    public static class DismissSignRequestSeeds
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

        // Standard valid dismiss request
        public static DismissSignRequestCommand StandardDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = AnotherValidGuid
        };

        // Null modifier (what you requested)
        public static DismissSignRequestCommand NullModifierDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = NullGuid
        };

        // Empty modifier (invalid GUID - what you requested)
        public static DismissSignRequestCommand EmptyModifierDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = EmptyGuid
        };

        // Invalid GUID modifier (what you requested)
        public static DismissSignRequestCommand InvalidGuidModifierDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = InvalidGuid
        };

        // Additional test cases for comprehensive coverage

        // Null SignRequestId
        public static DismissSignRequestCommand NullSignRequestIdDismissRequest { get; } = new()
        {
            SignRequestId = NullGuid,
            Modifier = ValidGuid
        };

        // Empty SignRequestId
        public static DismissSignRequestCommand EmptySignRequestIdDismissRequest { get; } = new()
        {
            SignRequestId = EmptyGuid,
            Modifier = ValidGuid
        };

        // Invalid GUID SignRequestId
        public static DismissSignRequestCommand InvalidGuidSignRequestIdDismissRequest { get; } = new()
        {
            SignRequestId = InvalidGuid,
            Modifier = ValidGuid
        };

        // Short invalid GUID
        public static DismissSignRequestCommand ShortGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = ShortGuid
        };

        // Long invalid GUID
        public static DismissSignRequestCommand LongGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = LongGuid
        };

        // Both properties null
        public static DismissSignRequestCommand BothNullDismissRequest { get; } = new()
        {
            SignRequestId = NullGuid,
            Modifier = NullGuid
        };

        // Both properties empty
        public static DismissSignRequestCommand BothEmptyDismissRequest { get; } = new()
        {
            SignRequestId = EmptyGuid,
            Modifier = EmptyGuid
        };

        // Both properties invalid GUID
        public static DismissSignRequestCommand BothInvalidGuidDismissRequest { get; } = new()
        {
            SignRequestId = InvalidGuid,
            Modifier = InvalidGuid
        };

        // Valid GUID without dashes (N format)
        public static DismissSignRequestCommand NoDashesGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "12345678123412341234123456789012"
        };

        // Valid GUID with uppercase
        public static DismissSignRequestCommand UpperCaseGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "0a190675-b674-46d3-8ad6-33736ff33bdc"
        };

        // GUID with braces (invalid format)
        public static DismissSignRequestCommand BracedGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "{62f720b6-c03b-4dfd-908e-cff35e8c6bed}"
        };

        // GUID with parentheses (invalid format)
        public static DismissSignRequestCommand ParenthesesGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "(62f720b6-c03b-4dfd-908e-cff35e8c6bed)"
        };

        // Whitespace only modifier
        public static DismissSignRequestCommand WhitespaceModifierDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "   "
        };

        // GUID with leading/trailing whitespace
        public static DismissSignRequestCommand WhitespaceAroundGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = "  62f720b6-c03b-4dfd-908e-cff35e8c6bed  "
        };

        // Same GUID for both properties
        public static DismissSignRequestCommand SameGuidDismissRequest { get; } = new()
        {
            SignRequestId = ValidGuid,
            Modifier = ValidGuid
        };
    }
}

