using Notary.SSAA.BO.DataTransferObject.Commands.SignRequest;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SignRequest;
using System;
using System.Collections.Generic;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Commands
{
    public static class ConfirmSignRequestSeeds
    {
        // Static valid GUID strings
        private static readonly string _validGuid = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string _anotherValidGuid = "0a190675-b674-46d3-8ad6-33736ff33bdc";
        private static readonly string IntegrationTest = "62f720b6-c03b-4dfd-908e-cff35e8c6bed";
        private static readonly string IntegrationTestElectronicBook = "0824ba77-86a2-4380-9450-e2ecb182710d";

        // Invalid GUID strings
        private static readonly string _invalidGuid = "not-a-valid-guid";
        private static readonly string _emptyGuid = "";
        private static readonly string _nullGuid = null;

        // Sample signature strings
        private static readonly string _validSignature = "base64encodedsignature1234567890ABCDEF";
        private static readonly string _anotherValidSignature = "base64encodedsignatureABCDEF1234567890";
        private static readonly string _emptySignature = "";
        private static readonly string _nullSignature = null;
        private static readonly string _longSignature = new string('S', 1000);
        private static readonly string _shortSignature = "S";

        // Sample certificate DN strings
        private static readonly string _validCertificateDN = "CN=Test User, OU=IT, O=Company, C=IR";
        private static readonly string _anotherValidCertificateDN = "CN=Another User, OU=Finance, O=Company, C=IR";
        private static readonly string _emptyCertificateDN = "";
        private static readonly string _nullCertificateDN = null;
        private static readonly string _longCertificateDN = new string('C', 500);
        private static readonly string _invalidCertificateDN = "Invalid DN Format";

        // Standard valid confirm request with one electronic book
        //مرحله6-1
        public static ConfirmSignRequestCommand StandardConfirmRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = IntegrationTestElectronicBook,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = IntegrationTest,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Confirm request with multiple electronic books
        public static ConfirmSignRequestCommand MultipleElectronicBooksRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                },
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _anotherValidGuid,
                    SignElectronicBookSign = _anotherValidSignature,
                    SignElectronicBookCertificateDN = _anotherValidCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Empty electronic books list
        public static ConfirmSignRequestCommand EmptyElectronicBooksRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>(),
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Null electronic books list
        public static ConfirmSignRequestCommand NullElectronicBooksRequest { get; } = new()
        {
            ElectronicBookSignedObjects = null,
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Null SignRequestSignedObject
        public static ConfirmSignRequestCommand NullSignRequestSignedObject { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = null
        };

        // Both ElectronicBookSignedObjects and SignRequestSignedObject null
        public static ConfirmSignRequestCommand BothNullRequest { get; } = new()
        {
            ElectronicBookSignedObjects = null,
            SignRequestSignedObject = null
        };

        // Invalid GUID in electronic book
        public static ConfirmSignRequestCommand InvalidGuidInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _invalidGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Invalid GUID in sign request
        public static ConfirmSignRequestCommand InvalidGuidInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _invalidGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Empty signature in electronic book
        public static ConfirmSignRequestCommand EmptySignatureInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _emptySignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Empty signature in sign request
        public static ConfirmSignRequestCommand EmptySignatureInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _emptySignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Null signature in electronic book
        public static ConfirmSignRequestCommand NullSignatureInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _nullSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Null signature in sign request
        public static ConfirmSignRequestCommand NullSignatureInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _nullSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Empty certificate DN in electronic book
        public static ConfirmSignRequestCommand EmptyCertificateDNInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _emptyCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Empty certificate DN in sign request
        public static ConfirmSignRequestCommand EmptyCertificateDNInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _emptyCertificateDN
            }
        };

        // Null certificate DN in electronic book
        public static ConfirmSignRequestCommand NullCertificateDNInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _nullCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // Null certificate DN in sign request
        public static ConfirmSignRequestCommand NullCertificateDNInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _nullCertificateDN
            }
        };

        // Long strings in all properties
        public static ConfirmSignRequestCommand LongStringsRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _longSignature,
                    SignElectronicBookCertificateDN = _longCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _longSignature,
                SignCertificateDN = _longCertificateDN
            }
        };

        // Short strings in all properties
        public static ConfirmSignRequestCommand ShortStringsRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _shortSignature,
                    SignElectronicBookCertificateDN = "CN=T"
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _shortSignature,
                SignCertificateDN = "CN=T"
            }
        };

        // Invalid certificate DN format
        public static ConfirmSignRequestCommand InvalidCertificateDNRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _invalidCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _invalidCertificateDN
            }
        };

        // Mixed valid/invalid electronic books
        public static ConfirmSignRequestCommand MixedElectronicBooksRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel // Valid
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                },
                new SignElectronicBookSignedViewModel // Invalid GUID
                {
                    SignElectronicBookId = _invalidGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                },
                new SignElectronicBookSignedViewModel // Empty signature
                {
                    SignElectronicBookId = _anotherValidGuid,
                    SignElectronicBookSign = _emptySignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // All properties null in electronic book
        public static ConfirmSignRequestCommand AllNullInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _nullGuid,
                    SignElectronicBookSign = _nullSignature,
                    SignElectronicBookCertificateDN = _nullCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // All properties empty in electronic book
        public static ConfirmSignRequestCommand AllEmptyInElectronicBook { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _emptyGuid,
                    SignElectronicBookSign = _emptySignature,
                    SignElectronicBookCertificateDN = _emptyCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };

        // All properties null in sign request
        public static ConfirmSignRequestCommand AllNullInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _nullGuid,
                SignRequestSign = _nullSignature,
                SignCertificateDN = _nullCertificateDN
            }
        };

        // All properties empty in sign request
        public static ConfirmSignRequestCommand AllEmptyInSignRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel
                {
                    SignElectronicBookId = _validGuid,
                    SignElectronicBookSign = _validSignature,
                    SignElectronicBookCertificateDN = _validCertificateDN
                }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _emptyGuid,
                SignRequestSign = _emptySignature,
                SignCertificateDN = _emptyCertificateDN
            }
        };

        // Maximum number of electronic books (edge case)
        public static ConfirmSignRequestCommand MaxElectronicBooksRequest { get; } = new()
        {
            ElectronicBookSignedObjects = new List<SignElectronicBookSignedViewModel>
            {
                new SignElectronicBookSignedViewModel { SignElectronicBookId = _validGuid, SignElectronicBookSign = _validSignature, SignElectronicBookCertificateDN = _validCertificateDN },
                new SignElectronicBookSignedViewModel { SignElectronicBookId = _anotherValidGuid, SignElectronicBookSign = _anotherValidSignature, SignElectronicBookCertificateDN = _anotherValidCertificateDN },
                new SignElectronicBookSignedViewModel { SignElectronicBookId = "11111111-1111-1111-1111-111111111111", SignElectronicBookSign = _validSignature, SignElectronicBookCertificateDN = _validCertificateDN },
                new SignElectronicBookSignedViewModel { SignElectronicBookId = "22222222-2222-2222-2222-222222222222", SignElectronicBookSign = _anotherValidSignature, SignElectronicBookCertificateDN = _anotherValidCertificateDN },
                new SignElectronicBookSignedViewModel { SignElectronicBookId = "33333333-3333-3333-3333-333333333333", SignElectronicBookSign = _validSignature, SignElectronicBookCertificateDN = _validCertificateDN }
            },
            SignRequestSignedObject = new SignRequestSignedViewModel
            {
                SignRequestId = _validGuid,
                SignRequestSign = _validSignature,
                SignCertificateDN = _validCertificateDN
            }
        };
    }
}