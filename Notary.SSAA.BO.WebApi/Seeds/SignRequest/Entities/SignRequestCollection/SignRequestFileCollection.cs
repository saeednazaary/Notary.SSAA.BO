using Notary.SSAA.BO.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Notary.SSAA.BO.WebApi.Seeds.SignRequest.Entities.SignRequestCollection
{
    public class SignRequestFileSeed
    {
        public static List<SignRequestFile> GetSeedData()
        {
            return new List<SignRequestFile>
            {
                new SignRequestFile
                {
                    Id = Guid.Parse("a1b2c3d4-e5f6-7890-abcd-ef1234567890"),
                    SignRequestId = Guid.Parse("87c69e83-ad82-449c-a88d-815ceb340b33"),
                    ScanFile = new byte[] { 0x01, 0x02, 0x03, 0x04, 0x05 },
                    ScanFileCreateDate = "1403/01/15",
                    ScanFileCreateTime = "10:30:45",
                    LastFile = new byte[] { 0x06, 0x07, 0x08, 0x09, 0x0A },
                    LastFileCreateDate = "1403/01/15",
                    LastFileCreateTime = "11:45:30",
                    ScanLegacyId = "SCAN_LEGACY_001",
                    LastLegacyId = "LAST_LEGACY_001",
                    ScriptoriumId = "12345",
                    RecordDate = new DateTime(2024, 4, 5),
                    Ilm = "1",
                    EdmId = "EDM_001",
                    EdmVersion = "1.0"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("c3d4e5f6-a7b8-9012-cdef-345678901234"),
                    SignRequestId = Guid.Parse("62f720b6-c03b-4dfd-908e-cff35e8c6bed"),
                    ScanFile = new byte[] { 0x0B, 0x0C, 0x0D, 0x0E, 0x0F },
                    ScanFileCreateDate = "1403/02/20",
                    ScanFileCreateTime = "09:15:20",
                    LastFile = new byte[] { 0x10, 0x11, 0x12, 0x13, 0x14 },
                    LastFileCreateDate = "1403/02/20",
                    LastFileCreateTime = "10:25:35",
                    ScanLegacyId = "SCAN_LEGACY_002",
                    LastLegacyId = "LAST_LEGACY_002",
                    ScriptoriumId = "12345",
                    RecordDate = new DateTime(2024, 5, 10),
                    Ilm = "1",
                    EdmId = "EDM_002",
                    EdmVersion = "1.1"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("e5f6a7b8-c9d0-1234-efgh-567890123456"),
                    SignRequestId = Guid.Parse("0a190675-b674-46d3-8ad6-33736ff33bdc"),
                    ScanFile = new byte[] { 0x15, 0x16, 0x17, 0x18, 0x19 },
                    ScanFileCreateDate = "1403/03/25",
                    ScanFileCreateTime = "14:20:10",
                    LastFile = new byte[] { 0x1A, 0x1B, 0x1C, 0x1D, 0x1E },
                    LastFileCreateDate = "1403/03/25",
                    LastFileCreateTime = "15:40:25",
                    ScanLegacyId = "SCAN_LEGACY_003",
                    LastLegacyId = "LAST_LEGACY_003",
                    ScriptoriumId = "67890",
                    RecordDate = new DateTime(2024, 6, 15),
                    Ilm = "2",
                    EdmId = "EDM_003",
                    EdmVersion = "2.0"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("a7b8c9d0-e1f2-3456-ghij-789012345678"),
                    SignRequestId = Guid.Parse("7050d603-d96e-452f-9277-13ba0afb8000"),
                    ScanFile = new byte[] { 0x1F, 0x20, 0x21, 0x22, 0x23 },
                    ScanFileCreateDate = "1403/04/10",
                    ScanFileCreateTime = "08:45:55",
                    LastFile = new byte[] { 0x24, 0x25, 0x26, 0x27, 0x28 },
                    LastFileCreateDate = "1403/04/10",
                    LastFileCreateTime = "09:55:40",
                    ScanLegacyId = "SCAN_LEGACY_004",
                    LastLegacyId = "LAST_LEGACY_004",
                    ScriptoriumId = "67890",
                    RecordDate = new DateTime(2024, 7, 1),
                    Ilm = "1",
                    EdmId = "EDM_004",
                    EdmVersion = "1.5"
                },
                new SignRequestFile
                {
                    Id = Guid.Parse("c9d0e1f2-g3h4-5678-ijkl-901234567890"),
                    SignRequestId = Guid.Parse("597e2af5-43bc-40bc-83ec-6d233f04df26"),
                    ScanFile = new byte[] { 0x29, 0x2A, 0x2B, 0x2C, 0x2D },
                    ScanFileCreateDate = "1403/05/05",
                    ScanFileCreateTime = "16:10:30",
                    LastFile = new byte[] { 0x2E, 0x2F, 0x30, 0x31, 0x32 },
                    LastFileCreateDate = "1403/05/05",
                    LastFileCreateTime = "17:20:15",
                    ScanLegacyId = "SCAN_LEGACY_005",
                    LastLegacyId = "LAST_LEGACY_005",
                    ScriptoriumId = "54321",
                    RecordDate = new DateTime(2024, 8, 20),
                    Ilm = "3",
                    EdmId = null,
                    EdmVersion = null
                }
            };
        }
    }
}