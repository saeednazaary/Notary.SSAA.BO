using Notary.SSAA.BO.DataTransferObject.Queries.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Queries
{
    public static class SignRequestElectronicBookPageSeeds
    {
        // National number values
        private static readonly string _validNationalNo = "140402157999000001";
        private static readonly string _anotherValidNationalNo = "140402157999000002";
        private static readonly string _emptyNationalNo = "";
        private static readonly string _nullNationalNo = null;
        private static readonly string _longNationalNo = new string('1', 20);
        private static readonly string _shortNationalNo = "123";
        private static readonly string _whitespaceNationalNo = "   ";
        private static readonly string _nonNumericNationalNo = "21600A4284";
        private static readonly string _specialCharsNationalNo = "21600@4284!";

        // Person sign classify number values
        private static readonly string _validClassifyNo = "72876";
        private static readonly string _anotherValidClassifyNo = "72877";
        private static readonly string _emptyClassifyNo = "";
        private static readonly string _nullClassifyNo = null;
        private static readonly string _longClassifyNo = new string('0', 10);
        private static readonly string _shortClassifyNo = "1";
        private static readonly string _whitespaceClassifyNo = "   ";
        private static readonly string _nonNumericClassifyNo = "00A";
        private static readonly string _specialCharsClassifyNo = "00@";

        // Page number values
        private const int _validPageNumber = 1;
        private const int _anotherValidPageNumber = 2;
        private const int _zeroPageNumber = 0;
        private const int _negativePageNumber = -1;
        private const int _largePageNumber = 1000;
        private const int _maxPageNumber = int.MaxValue;
        private const int _minPageNumber = int.MinValue;

        // Standard valid query
        public static SignRequestElectronicBookPageQuery StandardQuery { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Query with different page number
        public static SignRequestElectronicBookPageQuery DifferentPageNumber { get; } = new()
        {
            PageNumber = _anotherValidPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Query with different national number
        public static SignRequestElectronicBookPageQuery DifferentNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _anotherValidNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Query with different classify number
        public static SignRequestElectronicBookPageQuery DifferentClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _anotherValidClassifyNo
        };

        // Null national number
        public static SignRequestElectronicBookPageQuery NullNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _nullNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Empty national number
        public static SignRequestElectronicBookPageQuery EmptyNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _emptyNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Null classify number
        public static SignRequestElectronicBookPageQuery NullClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _nullClassifyNo
        };

        // Empty classify number
        public static SignRequestElectronicBookPageQuery EmptyClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _emptyClassifyNo
        };

        // Both national number and classify number null
        public static SignRequestElectronicBookPageQuery BothNull { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _nullNationalNo,
            PersonSignClassifyNo = _nullClassifyNo
        };

        // Both national number and classify number empty
        public static SignRequestElectronicBookPageQuery BothEmpty { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _emptyNationalNo,
            PersonSignClassifyNo = _emptyClassifyNo
        };

        // Long national number
        public static SignRequestElectronicBookPageQuery LongNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _longNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Short national number
        public static SignRequestElectronicBookPageQuery ShortNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _shortNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Long classify number
        public static SignRequestElectronicBookPageQuery LongClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _longClassifyNo
        };

        // Short classify number
        public static SignRequestElectronicBookPageQuery ShortClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _shortClassifyNo
        };

        // Whitespace national number
        public static SignRequestElectronicBookPageQuery WhitespaceNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _whitespaceNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Whitespace classify number
        public static SignRequestElectronicBookPageQuery WhitespaceClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _whitespaceClassifyNo
        };

        // Non-numeric national number
        public static SignRequestElectronicBookPageQuery NonNumericNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _nonNumericNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Non-numeric classify number
        public static SignRequestElectronicBookPageQuery NonNumericClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _nonNumericClassifyNo
        };

        // Special characters in national number
        public static SignRequestElectronicBookPageQuery SpecialCharsNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _specialCharsNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Special characters in classify number
        public static SignRequestElectronicBookPageQuery SpecialCharsClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _specialCharsClassifyNo
        };

        // Zero page number
        public static SignRequestElectronicBookPageQuery ZeroPageNumber { get; } = new()
        {
            PageNumber = _zeroPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Negative page number
        public static SignRequestElectronicBookPageQuery NegativePageNumber { get; } = new()
        {
            PageNumber = _negativePageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Large page number
        public static SignRequestElectronicBookPageQuery LargePageNumber { get; } = new()
        {
            PageNumber = _largePageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Maximum page number
        public static SignRequestElectronicBookPageQuery MaxPageNumber { get; } = new()
        {
            PageNumber = _maxPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // Minimum page number
        public static SignRequestElectronicBookPageQuery MinPageNumber { get; } = new()
        {
            PageNumber = _minPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // All properties with edge values
        public static SignRequestElectronicBookPageQuery AllEdgeValues { get; } = new()
        {
            PageNumber = _largePageNumber,
            SignRequestNationalNo = _longNationalNo,
            PersonSignClassifyNo = _longClassifyNo
        };

        // Only page number provided (both strings null)
        public static SignRequestElectronicBookPageQuery OnlyPageNumber { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _nullNationalNo,
            PersonSignClassifyNo = _nullClassifyNo
        };

        // Only national number provided
        public static SignRequestElectronicBookPageQuery OnlyNationalNo { get; } = new()
        {
            PageNumber = 1,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = _nullClassifyNo
        };

        // Only classify number provided
        public static SignRequestElectronicBookPageQuery OnlyClassifyNo { get; } = new()
        {
            PageNumber = 1,
            SignRequestNationalNo = _nullNationalNo,
            PersonSignClassifyNo = _validClassifyNo
        };

        // National number with leading zeros
        public static SignRequestElectronicBookPageQuery LeadingZerosNationalNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = "0021600742",
            PersonSignClassifyNo = _validClassifyNo
        };

        // Classify number with leading zeros
        public static SignRequestElectronicBookPageQuery LeadingZerosClassifyNo { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = _validNationalNo,
            PersonSignClassifyNo = "0001"
        };

        // Very long strings for both text fields
        public static SignRequestElectronicBookPageQuery VeryLongStrings { get; } = new()
        {
            PageNumber = _validPageNumber,
            SignRequestNationalNo = new string('1', 100),
            PersonSignClassifyNo = new string('0', 100)
        };
    }
}