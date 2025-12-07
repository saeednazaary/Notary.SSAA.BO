using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Queries
{
    public static class GetSignRequestPersonFingerprintSeeds
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

        // Standard valid get person fingerprint query
        public static GetSignRequestPersonFingerprintQuery StandardGetFingerprintQuery { get; } = new GetSignRequestPersonFingerprintQuery(_validGuid);

        // Null SignRequestId
        public static GetSignRequestPersonFingerprintQuery NullSignRequestId { get; } = new GetSignRequestPersonFingerprintQuery(_nullGuid);

        // Empty SignRequestId (invalid GUID)
        public static GetSignRequestPersonFingerprintQuery EmptySignRequestId { get; } = new GetSignRequestPersonFingerprintQuery(_emptyGuid);

        // Invalid GUID SignRequestId
        public static GetSignRequestPersonFingerprintQuery InvalidGuidSignRequestId { get; } = new GetSignRequestPersonFingerprintQuery(_invalidGuid);

        // Short invalid GUID
        public static GetSignRequestPersonFingerprintQuery ShortGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery(_shortGuid);

        // Long invalid GUID
        public static GetSignRequestPersonFingerprintQuery LongGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery(_longGuid);

        // Valid GUID without dashes (N format)
        public static GetSignRequestPersonFingerprintQuery NoDashesGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678123412341234123456789012");

        // Valid GUID with uppercase
        public static GetSignRequestPersonFingerprintQuery UpperCaseGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("ABCDEFAB-1234-5678-90AB-CDEF12345678");

        // GUID with braces (invalid format)
        public static GetSignRequestPersonFingerprintQuery BracedGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("{12345678-1234-1234-1234-123456789012}");

        // GUID with parentheses (invalid format)
        public static GetSignRequestPersonFingerprintQuery ParenthesesGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("(12345678-1234-1234-1234-123456789012)");

        // Whitespace only SignRequestId
        public static GetSignRequestPersonFingerprintQuery WhitespaceSignRequestId { get; } = new GetSignRequestPersonFingerprintQuery("   ");

        // GUID with leading/trailing whitespace
        public static GetSignRequestPersonFingerprintQuery WhitespaceAroundGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("  12345678-1234-1234-1234-123456789012  ");

        // Another valid GUID for variety
        public static GetSignRequestPersonFingerprintQuery AnotherValidGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery(_anotherValidGuid);

        // GUID with mixed case
        public static GetSignRequestPersonFingerprintQuery MixedCaseGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("aBcDeF12-3456-7890-abCd-EF1234567890");

        // GUID with special characters (invalid)
        public static GetSignRequestPersonFingerprintQuery SpecialCharsGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("guid-with-special!@#$%^&*()");

        // Very long string that's not a GUID
        public static GetSignRequestPersonFingerprintQuery LongStringQuery { get; } = new GetSignRequestPersonFingerprintQuery(new string('X', 100));

        // GUID with extra characters appended (invalid)
        public static GetSignRequestPersonFingerprintQuery ExtraCharsGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery(_validGuid + "extra");

        // Minimum valid GUID (just the right length)
        public static GetSignRequestPersonFingerprintQuery MinimumValidGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("00000000-0000-0000-0000-000000000000");

        // GUID with wrong hyphen positions (invalid)
        public static GetSignRequestPersonFingerprintQuery WrongHyphenPositionQuery { get; } = new GetSignRequestPersonFingerprintQuery("123456781-234-1234-1234-123456789012");

        // GUID with missing hyphens (invalid)
        public static GetSignRequestPersonFingerprintQuery MissingHyphensQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678123412341234123456789012");

        // GUID with extra hyphens (invalid)
        public static GetSignRequestPersonFingerprintQuery ExtraHyphensQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678-1234-1234-1234-1234-56789012");

        // GUID with non-hex characters (invalid)
        public static GetSignRequestPersonFingerprintQuery NonHexCharactersQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678-1234-1234-1234-12345678901G");

        // GUID with only one section (invalid)
        public static GetSignRequestPersonFingerprintQuery SingleSectionQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678");

        // GUID with partial sections (invalid)
        public static GetSignRequestPersonFingerprintQuery PartialSectionsQuery { get; } = new GetSignRequestPersonFingerprintQuery("12345678-1234-1234-1234");

        // GUID with incorrect section lengths (invalid)
        public static GetSignRequestPersonFingerprintQuery WrongSectionLengthQuery { get; } = new GetSignRequestPersonFingerprintQuery("1234567-1234-1234-1234-123456789012");

        // GUID with all F's (valid but edge case)
        public static GetSignRequestPersonFingerprintQuery AllFsGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF");

        // GUID with all 1's (valid)
        public static GetSignRequestPersonFingerprintQuery AllOnesGuidQuery { get; } = new GetSignRequestPersonFingerprintQuery("11111111-1111-1111-1111-111111111111");

        // GUID with alternating pattern (valid)
        public static GetSignRequestPersonFingerprintQuery AlternatingPatternQuery { get; } = new GetSignRequestPersonFingerprintQuery("A1B2C3D4-E5F6-7890-ABCD-EF1234567890");
    }
}