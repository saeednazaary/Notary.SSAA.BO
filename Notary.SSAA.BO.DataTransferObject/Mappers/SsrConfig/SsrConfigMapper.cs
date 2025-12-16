using Mapster;
using Notary.SSAA.BO.DataTransferObject.Commands.SsrConfig;
using Notary.SSAA.BO.DataTransferObject.ViewModels.SsrConfig;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Mappers.SsrConfig
{
    public static class SsrConfigMapper
    {
        public static SsrConfigViewModel ToViewModel(Domain.Entities.SsrConfig entity)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<Domain.Entities.SsrConfig, SsrConfigViewModel>()
                // Booleans from string IsConfirmed
                .Map(dest => dest.IsValid, src => src.IsConfirmed == "Y")
                .Map(dest => dest.IsNew, src => false) // You can add logic
                .Map(dest => dest.IsDelete, src => false)
                .Map(dest => dest.IsDirty, src => false)
                .Map(dest => dest.ConfigId, src => src.Id.ToString())

                // GUIDs to string lists
                .Map(dest => dest.SsrConfigMainSubjectId, src => new[] { src.SsrConfigMainSubjectId.ToString() })
                .Map(dest => dest.SsrConfigSubjectId, src => new[] { src.SsrConfigSubjectId.ToString() })
                .Map(dest => dest.ConfigType, src => src.SsrConfigSubjectId != null && src.SsrConfigSubject != null ? src.SsrConfigSubject.ConfigType : null)
                // Dates/times
                .Map(dest => dest.ConfigStartDate, src => src.ConfigStartDate)
                .Map(dest => dest.ConfigStartTime, src => src.ConfigStartTime)
                .Map(dest => dest.ConfigEndTime, src => src.ConfigEndTime)
                .Map(dest => dest.ConfigEndDate, src => src.ConfigEndDate)

                // Values
                .Map(dest => dest.ConfigValue, src => src.Value)
                .Map(dest => dest.ConditionType, src => src.ConditionType)
                .Map(dest => dest.ActionType, src => src.ActionType)

                // Confirmation info
                .Map(dest => dest.ConfirmDate, src => src.ConfirmDate)
                .Map(dest => dest.ConfirmTime, src => src.ConfirmTime)
                .Map(dest => dest.Confirmer, src => src.Confirmer)
                .Map(dest => dest.IsConfirmed, src => src.IsConfirmed)
                .IgnoreNonMapped(true);

            config.Compile();

            return entity.Adapt<SsrConfigViewModel>(config);
        }
        public static void ToEntity(ref Domain.Entities.SsrConfig entity, CreateSsrConfigCommand command)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<CreateSsrConfigCommand, Domain.Entities.SsrConfig>()
                // Convert first string in list to Guid (null-safe)
                .Map(dest => dest.SsrConfigMainSubjectId,
                     src => src.SsrConfigMainSubjectId != null && src.SsrConfigMainSubjectId.Any()
                                ? Guid.Parse(src.SsrConfigMainSubjectId.First())
                                : Guid.Empty)
                .Map(dest => dest.SsrConfigSubjectId,
                     src => src.SsrConfigSubjectId != null && src.SsrConfigSubjectId.Any()
                                ? Guid.Parse(src.SsrConfigSubjectId.First())
                                : Guid.Empty)

                // Dates/times (stored as string in DB)
                .Map(dest => dest.ConfigStartDate, src => src.ConfigStartDate)
                .Map(dest => dest.ConfigStartTime, src => src.ConfigStartTime)
                .Map(dest => dest.ConfigEndDate, src => src.ConfigEndDate)
                .Map(dest => dest.ConfigEndTime, src => src.ConfigEndTime)

                // Values
                .Map(dest => dest.Value, src => src.ConfigValue)
                .Map(dest => dest.ConditionType, src => src.ConditionType)
                //.Map(dest => dest.ActionType, src => src.ActionType)
                .IgnoreNonMapped(true);

            config.Compile();
            command.Adapt(entity, config);
        }
        public static void ToEntity(ref Domain.Entities.SsrConfig entity, UpdateSsrConfigCommand command)
        {
            var config = new TypeAdapterConfig();

            config.NewConfig<UpdateSsrConfigCommand, Domain.Entities.SsrConfig>()
                // Convert first string in list to Guid (null-safe)
                .Map(dest => dest.SsrConfigMainSubjectId,
                     src => src.SsrConfigMainSubjectId != null && src.SsrConfigMainSubjectId.Any()
                                ? src.SsrConfigMainSubjectId.First().ToGuid()
                                : Guid.Empty)
                .Map(dest => dest.SsrConfigSubjectId,
                     src => src.SsrConfigSubjectId != null && src.SsrConfigSubjectId.Any()
                                ? src.SsrConfigSubjectId.First().ToGuid()
                                : Guid.Empty)

                // Dates/times (stored as string in DB)
                //.Map(dest => dest.ConfigStartDate, src => src.ConfigStartDate)
                //.Map(dest => dest.ConfigStartTime, src => src.ConfigStartTime)
                //.Map(dest => dest.ConfigEndDate, src => src.ConfigEndDate)
                //.Map(dest => dest.ConfigEndTime, src => src.ConfigEndTime)

                // Values
                .Map(dest => dest.Value, src => src.ConfigValue)
                .Map(dest => dest.ConditionType, src => src.ConditionType)
                //.Map(dest => dest.ActionType, src => src.ActionType)

                .IgnoreNonMapped(true);

            config.Compile();
            command.Adapt(entity, config);
        }
    }
}