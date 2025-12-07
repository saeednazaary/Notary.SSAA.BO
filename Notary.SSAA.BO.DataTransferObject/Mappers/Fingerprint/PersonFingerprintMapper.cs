using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.DataTransferObject.ViewModels.Fingerprint;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.Domain.RepositoryObjects.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.Fingerprint
{
    public static class PersonFingerprintMapper
    {

        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, CreatePersonFingerprintCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<CreatePersonFingerprintCommand, Domain.Entities.PersonFingerprint>()

                .Map(dest => dest.PersonFingerprintUseCaseId, src => src.ClientId)
                .Map(dest => dest.TfaIsRequired, src => src.IsTFARequired?"1":"2")
                .Map(dest => dest.PersonNationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.TfaMobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.NameFamily, src => src.PersonNameFamily)
                .Map(dest => dest.UseCasePersonObjectId, src => src.PersonObjectId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, CreatePersonFingerprintV2Command viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<CreatePersonFingerprintV2Command, Domain.Entities.PersonFingerprint>()

                .Map(dest => dest.PersonFingerprintUseCaseId, src => src.ClientId)
                .Map(dest => dest.TfaIsRequired, src => src.IsTFARequired ? "1" : "2")
                .Map(dest => dest.PersonNationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.TfaMobileNo, src => src.PersonMobileNo)
                .Map(dest => dest.NameFamily, src => src.PersonNameFamily)
                .Map(dest => dest.UseCasePersonObjectId, src => src.PersonObjectId)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, TakePersonFingerprintCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<TakePersonFingerprintCommand, Domain.Entities.PersonFingerprint>()

                .Map(dest => dest.PersonFingerTypeId, src => src.PersonFingerTypeId)
                .Map(dest => dest.FingerprintImageHeight, src => src.FingerprintImageHeight)
                .Map(dest => dest.FingerprintImageWidth, src => src.FingerprintImageWidth)
                .Map(dest => dest.FingerprintScannerDeviceType, src => src.FingerprintScannerDeviceType)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, TakePersonFingerprintV2Command viewModel)
        {
            //var config = new TypeAdapterConfig();

            //// Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            //config.NewConfig<TakePersonFingerprintCommand, Domain.Entities.PersonFingerprint>()

            //    .Map(dest => dest.PersonFingerTypeId, src => src.PersonFingerTypeId)
            //    .Map(dest => dest.FingerprintImageHeight, src => !string.IsNullOrWhiteSpace(src.FingerprintImageHeight) ? Convert.ToInt32(src.FingerprintImageHeight) : 0)
            //    .Map(dest => dest.FingerprintImageWidth, src => !string.IsNullOrWhiteSpace(src.FingerprintImageWidth) ? Convert.ToInt32(src.FingerprintImageWidth) : 0)
            //    .Map(dest => dest.FingerprintScannerDeviceType, src => src.FingerprintScannerDeviceType)
            //    .Map(dest => dest.FingerprintImageFile, src => Convert.FromBase64String(src.FingerprintImageFile))
            //    .IgnoreNonMapped(true);

            //// Apply the configuration
            //config.Compile();

            // Perform the mapping
            //viewModel.Adapt(entity, config);

            entity.PersonFingerTypeId=viewModel.PersonFingerTypeId;
            entity.FingerprintImageHeight = !string.IsNullOrWhiteSpace(viewModel.FingerprintImageHeight) ? Convert.ToInt32(viewModel.FingerprintImageHeight) : 0;
            entity.FingerprintImageWidth = !string.IsNullOrWhiteSpace(viewModel.FingerprintImageWidth) ? Convert.ToInt32(viewModel.FingerprintImageWidth) : 0;
            entity.FingerprintScannerDeviceType = viewModel.FingerprintScannerDeviceType;
            entity.FingerprintImageFile = Convert.FromBase64String(viewModel.FingerprintImageFile);

        }
        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, MatchPersonFingerprintCommand viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<TakePersonFingerprintCommand, Domain.Entities.PersonFingerprint>()

                .Map(dest => dest.PersonFingerTypeId, src => src.PersonFingerTypeId)
                .Map(dest => dest.FingerprintImageHeight, src => src.FingerprintImageHeight)
                .Map(dest => dest.FingerprintImageWidth, src => src.FingerprintImageWidth)
                .Map(dest => dest.FingerprintScannerDeviceType, src => src.FingerprintScannerDeviceType)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static void ToEntity(ref Domain.Entities.PersonFingerprint entity, MatchPersonFingerprintV2Command viewModel)
        {
            var config = new TypeAdapterConfig();

            // Configure the reverse mapping from CreateSignRequestViewModel to SignRequest
            config.NewConfig<TakePersonFingerprintV2Command, Domain.Entities.PersonFingerprint>()

                .Map(dest => dest.PersonFingerTypeId, src => src.PersonFingerTypeId)
                .Map(dest => dest.FingerprintImageHeight, src => src.FingerprintImageHeight)
                .Map(dest => dest.FingerprintImageWidth, src => src.FingerprintImageWidth)
                .Map(dest => dest.FingerprintScannerDeviceType, src => src.FingerprintScannerDeviceType)
                .IgnoreNonMapped(true);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            viewModel.Adapt(entity, config);

        }
        public static GetInquiryPersonFingerprintRepositoryObject ToViewModel(Domain.Entities.PersonFingerprint entity)
        {
            GetInquiryPersonFingerprintRepositoryObject inquiryFingerprintResultViewModel = new();
            TypeAdapterConfig config = new();

            _ = config.NewConfig<Domain.Entities.PersonFingerprint, GetInquiryPersonFingerprintRepositoryObject>()
                .Map(dest => dest.FingerprintId, src => src.Id.ToString())
                .Map(dest => dest.PersonObjectId, src => src.UseCasePersonObjectId)
                .Map(dest => dest.MainObjectId, src => src.UseCaseMainObjectId)
                .Map(dest => dest.PersonNationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.PersonFingerTypeId, src => src.PersonFingerTypeId)
                .Map(dest => dest.TFAState, src => src.TfaState)
                .Map(dest => dest.MOCState, src => src.MocState)
                .Map(dest => dest.PersonNationalNo, src => src.PersonNationalNo)
                .Map(dest => dest.IsDeleted, src => src.IsDeleted.ToBoolean())
                .Map(dest => dest.FingerprintDateTime, src => src.FingerprintGetDate+"-"+src.FingerprintGetTime);

            // Apply the configuration
            config.Compile();

            // Perform the mapping
            _ = entity.Adapt(inquiryFingerprintResultViewModel, config);

            return inquiryFingerprintResultViewModel;
        }
    }
}
