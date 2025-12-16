using FluentValidation;
using Notary.SSAA.BO.DataTransferObject.Commands.Fingerprint;
using Notary.SSAA.BO.Utilities.Extensions;

namespace Notary.SSAA.BO.DataTransferObject.Validators.Fingerprint
{
    public class TakePersonFingerprintValidator : AbstractValidator<TakePersonFingerprintCommand>
    {
        public TakePersonFingerprintValidator()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageFile)
                .NotEmpty().WithMessage("فایل اثرانگشت وجود ندارد . ");

            RuleFor(v => v.PersonFingerTypeId)
                .NotEmpty().WithMessage("نوع انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageHeight)
                .NotEmpty().WithMessage("طول تصویر انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageWidth)
                .NotEmpty().WithMessage("عرض تصویر اثرانگشت اجباری است .");

            RuleFor(v => v.FingerprintScannerDeviceType)
                .NotEmpty().WithMessage("نوع دستگاه اثرانگشت اجباری است . ");
        }
    }
    public class TakePersonFingerprintValidator_V2 : AbstractValidator<TakePersonFingerprintV2Command>
    {
        public TakePersonFingerprintValidator_V2()
        {
            RuleFor(v => v.FingerprintId)
                 .Must(ValidatorHelper.BeValidGuid).WithMessage("شناسه اثر انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageFile)
                .NotEmpty().WithMessage("فایل اثرانگشت وجود ندارد . ");

            RuleFor(v => v.PersonFingerTypeId)
                .NotEmpty().WithMessage("نوع انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageHeight)
                .NotEmpty().WithMessage("طول تصویر انگشت اجباری است .");

            RuleFor(v => v.FingerprintImageWidth)
                .NotEmpty().WithMessage("عرض تصویر اثرانگشت اجباری است .");

            RuleFor(v => v.FingerprintScannerDeviceType)
                .NotEmpty().WithMessage("نوع دستگاه اثرانگشت اجباری است . ");
        }
    }
}
