namespace Notary.SSAA.BO.DataTransferObject.Mappers.Document
{
    using Mapster;
    using Notary.SSAA.BO.DataTransferObject.ViewModels.Document;
    using Notary.SSAA.BO.Domain.Entities;
    using Notary.SSAA.BO.Utilities.Extensions;

    /// <summary>
    /// Defines the <see cref="DocumentFingerPrintMapper" />
    /// </summary>
    public static class DocumentFingerPrintMapper
    {
        /// <summary>
        /// The ToFingerprintViewModel
        /// </summary>
        /// <param name="documentPerson">The documentPerson<see cref="DocumentPerson"/></param>
        /// <returns>The <see cref="DocumentPersonFingerprintViewModel"/></returns>
        public static DocumentPersonFingerprintViewModel ToFingerprintViewModel ( DocumentPerson documentPerson )
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<DocumentPerson, DocumentPersonFingerprintViewModel> ().IgnoreNullValues ( true )

                .Map ( dest => dest.PersonId, src => src.Id.ToString () )
                .Map ( dest => dest.MainObjectId, src => src.DocumentId.ToString () )
                .Map ( dest => dest.PersonNationalNo, src => src.NationalNo )
                .Map ( dest => dest.IsPersonOriginal, src => src.IsOriginal.ToBoolean () )
                .Map ( dest => dest.IsPersian, src => src.IsIranian.ToBoolean () )
                .Map ( dest => dest.PersonName, src => src.Name )
                .Map ( dest => dest.PersonFamily, src => src.Family )
                .Map ( dest => dest.PersonBirthDate, src => src.BirthDate )
                .Map ( dest => dest.PersonSexType, src => src.SexType )
                .Map ( dest => dest.PersonFatherName, src => src.FatherName )
                .Map ( dest => dest.IsPersonAlive, src => src.IsAlive.ToBoolean () )
                .Map ( dest => dest.PersonMobileNo, src => src.MobileNo )
                .Map ( dest => dest.IsTFARequired, src => src.IsTfaRequired.ToNullabbleBoolean () )
                .Map ( dest => dest.IsFingerprintGotten, src => src.IsFingerprintGotten.ToNullabbleBoolean () )
                .Map ( dest => dest.TFAState, src => src.TfaState );
            config.Compile ();

            var documentPersonViewModel = documentPerson.Adapt<DocumentPersonFingerprintViewModel>(config);
            return documentPersonViewModel;
        }
    }
}
