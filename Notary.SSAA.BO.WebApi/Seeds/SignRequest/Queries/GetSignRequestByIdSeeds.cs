using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Queries
{
    public static class GetSignRequestByIdSeeds
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

        // Standard valid get by ID query
        public static GetSignRequestByIdQuery StandardGetByIdQuery { get; } = new GetSignRequestByIdQuery(_validGuid);

        // Null SignRequestId
        public static GetSignRequestByIdQuery NullSignRequestId { get; } = new GetSignRequestByIdQuery(_nullGuid);

        // Empty SignRequestId (invalid GUID)
        public static GetSignRequestByIdQuery EmptySignRequestId { get; } = new GetSignRequestByIdQuery(_emptyGuid);

        // Invalid GUID SignRequestId
        public static GetSignRequestByIdQuery InvalidGuidSignRequestId { get; } = new GetSignRequestByIdQuery(_invalidGuid);

        // Short invalid GUID
        public static GetSignRequestByIdQuery ShortGuidQuery { get; } = new GetSignRequestByIdQuery(_shortGuid);

        // Long invalid GUID
        public static GetSignRequestByIdQuery LongGuidQuery { get; } = new GetSignRequestByIdQuery(_longGuid);

        // Valid GUID without dashes (N format)
        public static GetSignRequestByIdQuery NoDashesGuidQuery { get; } = new GetSignRequestByIdQuery("12345678123412341234123456789012");

        // Valid GUID with uppercase
        public static GetSignRequestByIdQuery UpperCaseGuidQuery { get; } = new GetSignRequestByIdQuery("ABCDEFAB-1234-5678-90AB-CDEF12345678");

        // GUID with braces (invalid format)
        public static GetSignRequestByIdQuery BracedGuidQuery { get; } = new GetSignRequestByIdQuery("{12345678-1234-1234-1234-123456789012}");

        // GUID with parentheses (invalid format)
        public static GetSignRequestByIdQuery ParenthesesGuidQuery { get; } = new GetSignRequestByIdQuery("(12345678-1234-1234-1234-123456789012)");

        // Whitespace only SignRequestId
        public static GetSignRequestByIdQuery WhitespaceSignRequestId { get; } = new GetSignRequestByIdQuery("   ");

        // GUID with leading/trailing whitespace
        public static GetSignRequestByIdQuery WhitespaceAroundGuidQuery { get; } = new GetSignRequestByIdQuery("  12345678-1234-1234-1234-123456789012  ");

        // Another valid GUID for variety
        public static GetSignRequestByIdQuery AnotherValidGuidQuery { get; } = new GetSignRequestByIdQuery(_anotherValidGuid);

        // GUID with mixed case
        public static GetSignRequestByIdQuery MixedCaseGuidQuery { get; } = new GetSignRequestByIdQuery("aBcDeF12-3456-7890-abCd-EF1234567890");

        // GUID with special characters (invalid)
        public static GetSignRequestByIdQuery SpecialCharsGuidQuery { get; } = new GetSignRequestByIdQuery("guid-with-special!@#$%^&*()");

        // Very long string that's not a GUID
        public static GetSignRequestByIdQuery LongStringQuery { get; } = new GetSignRequestByIdQuery(new string('X', 100));

        // GUID with extra characters appended (invalid)
        public static GetSignRequestByIdQuery ExtraCharsGuidQuery { get; } = new GetSignRequestByIdQuery(_validGuid + "extra");

        // Minimum valid GUID (just the right length)
        public static GetSignRequestByIdQuery MinimumValidGuidQuery { get; } = new GetSignRequestByIdQuery("00000000-0000-0000-0000-000000000000");

        // GUID with wrong hyphen positions (invalid)
        public static GetSignRequestByIdQuery WrongHyphenPositionQuery { get; } = new GetSignRequestByIdQuery("123456781-234-1234-1234-123456789012");

        // GUID with missing hyphens (invalid)
        public static GetSignRequestByIdQuery MissingHyphensQuery { get; } = new GetSignRequestByIdQuery("12345678123412341234123456789012");

        // GUID with extra hyphens (invalid)
        public static GetSignRequestByIdQuery ExtraHyphensQuery { get; } = new GetSignRequestByIdQuery("12345678-1234-1234-1234-1234-56789012");

        // GUID with non-hex characters (invalid)
        public static GetSignRequestByIdQuery NonHexCharactersQuery { get; } = new GetSignRequestByIdQuery("12345678-1234-1234-1234-12345678901G");

        // GUID with only one section (invalid)
        public static GetSignRequestByIdQuery SingleSectionQuery { get; } = new GetSignRequestByIdQuery("12345678");

        // GUID with partial sections (invalid)
        public static GetSignRequestByIdQuery PartialSectionsQuery { get; } = new GetSignRequestByIdQuery("12345678-1234-1234-1234");

        // GUID with incorrect section lengths (invalid)
        public static GetSignRequestByIdQuery WrongSectionLengthQuery { get; } = new GetSignRequestByIdQuery("1234567-1234-1234-1234-123456789012");

        // GUID with all F's (valid but edge case)
        public static GetSignRequestByIdQuery AllFsGuidQuery { get; } = new GetSignRequestByIdQuery("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");

        // GUID with all 1's (valid)
        public static GetSignRequestByIdQuery AllOnesGuidQuery { get; } = new GetSignRequestByIdQuery("11111111-1111-1111-1111-111111111111");

        // GUID with alternating pattern (valid)
        public static GetSignRequestByIdQuery AlternatingPatternQuery { get; } = new GetSignRequestByIdQuery("A1B2C3D4-E5F6-7890-ABCD-EF1234567890");
    }
}