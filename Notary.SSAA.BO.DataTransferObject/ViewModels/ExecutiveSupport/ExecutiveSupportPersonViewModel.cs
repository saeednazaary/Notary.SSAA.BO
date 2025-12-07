using Notary.SSAA.BO.DataTransferObject.Bases;

namespace Notary.SSAA.BO.DataTransferObject.ViewModels.ExecutiveSupport
{
    public class ExecutiveSupportPersonViewModel :  EntityState
    {
        public ExecutiveSupportPersonViewModel()
        {
            ScriptoriumId = new List<string>();
            PersonPostTypeId = new List<string>();
            PersonNationalityId = new List<string>();
            LegalPersonNatureId = new List<string>();
            LegalPersonTypeId = new List<string>();
        }
        [System.ComponentModel.DisplayName("رديف")]
        [System.ComponentModel.Description("رديف")]
        public int RowNo { get; set; }
        [System.ComponentModel.DisplayName("آيا استعلام ثبت احوال براي اين شخص انجام شده است؟")]
        [System.ComponentModel.Description("آيا استعلام ثبت احوال براي اين شخص انجام شده است؟")]
        public bool IsPersonSabteAhvalChecked { get; set; }
        [System.ComponentModel.DisplayName("آيا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟")]
        [System.ComponentModel.Description("آيا اطلاعات وارد شده با اطلاعات ثبت احوال تطابق دارد؟")]
        public bool IsPersonSabteAhvalCorrect { get; set; }
        [System.ComponentModel.DisplayName("شناسه شخص")]
        [System.ComponentModel.Description("شناسه شخص")]
        public string PersonId { get; set; }
        [System.ComponentModel.DisplayName("تاريخ مطابقت با ثبت احوال")]
        [System.ComponentModel.Description("تاريخ مطابقت با ثبت احوال")]
        public string PersonSabtahvalInquiryDate { get; set; }
        [System.ComponentModel.DisplayName("زمان مطابقت با ثبت احوال")]
        [System.ComponentModel.Description("زمان مطابقت با ثبت احوال")]
        public string PersonSabtahvalInquiryTime { get; set; }
        [System.ComponentModel.DisplayName("آيا تصوير اثر انگشت شخص گرفته شده است؟")]
        [System.ComponentModel.Description("آيا تصوير اثر انگشت شخص گرفته شده است؟")]
        public bool IsPersonFingerprintGotten { get; set; }
        [System.ComponentModel.DisplayName("آيا در سامانه ثنا حساب کاربري دارد؟")]
        [System.ComponentModel.Description("آيا در سامانه ثنا حساب کاربري دارد؟")]
        public bool PersonHasSana { get; set; }
        [System.ComponentModel.DisplayName("تاريخ استعلام ثنا")]
        [System.ComponentModel.Description("تاريخ استعلام ثنا")]
        public string PersonSanaInquiryDate { get; set; }
        [System.ComponentModel.DisplayName("زمان استعلام ثنا")]
        [System.ComponentModel.Description("زمان استعلام ثنا")]
        public string PersonSanaInquiryTime { get; set; }
        [System.ComponentModel.DisplayName("نام شخص حقوقي در سامانه ثنا")]
        [System.ComponentModel.Description("نام شخص حقوقي در سامانه ثنا")]
        public string PersonSanaOrganizationName { get; set; }
        [System.ComponentModel.DisplayName("کد شخص حقوقي در سامانه ثنا")]
        [System.ComponentModel.Description("کد شخص حقوقي در سامانه ثنا")]
        public string PersonSanaOrganizationCode { get; set; }
        [System.ComponentModel.DisplayName("شماره تلفن همراه")]
        [System.ComponentModel.Description("تلفن همراه")]
        public string PersonSanaMobileNo { get; set; }
        public int PersonSanaHasOrganizationChart { get; set; }
        [System.ComponentModel.DisplayName("شماره فکس")]
        [System.ComponentModel.Description("شماره فکس")]
        public string PersonFax { get; set; }
        [System.ComponentModel.DisplayName("شناسه درخواست تبعی اجرائیه")]
        [System.ComponentModel.Description("شناسه درخواست تبعی اجرائیه")]
        public string ExecutionSupportiveId { get; set; }
        [System.ComponentModel.DisplayName("شماره ملی/شناسه ملی")]
        [System.ComponentModel.Description("حقيقي است يا حقوقي؟")]
        public string PersonNationalNo { get; set; }
        [System.ComponentModel.DisplayName("تاريخ تولد")]
        [System.ComponentModel.Description("تاريخ تولد")]
        public string PersonBirthDate { get; set; }
        [System.ComponentModel.DisplayName("نام")]
        [System.ComponentModel.Description("نام")]
        public string PersonName { get; set; }
        [System.ComponentModel.DisplayName("نام خانوادگي")]
        [System.ComponentModel.Description("نام خانوادگي")]
        public string PersonFamily { get; set; }
        [System.ComponentModel.DisplayName("نام پدر")]
        [System.ComponentModel.Description("نام پدر")]
        public string PersonFatherName { get; set; }
        [System.ComponentModel.DisplayName("شماره شناسنامه")]
        [System.ComponentModel.Description("شماره شناسنامه")]
        public string PersonIdentityNo { get; set; }
        [System.ComponentModel.DisplayName("محل صدور شناسنامه")]
        [System.ComponentModel.Description("محل صدور")]
        public string PersonIdentityIssueLocation { get; set; }
        [System.ComponentModel.DisplayName("سري شناسنامه - بخش عددي")]
        [System.ComponentModel.Description("سريال شناسنامه")]
        public string PersonIdentitySeri { get; set; }
        [System.ComponentModel.DisplayName("سريال شناسنامه")]
        [System.ComponentModel.Description("سريال شناسنامه")]
        public string PersonIdentitySerial { get; set; }
        [System.ComponentModel.DisplayName("كد پستي")]
        [System.ComponentModel.Description("كد پستي")]
        public string PersonPostalCode { get; set; }
        public string PersonMobileNo { get; set; }
        [System.ComponentModel.DisplayName("نشاني")]
        [System.ComponentModel.Description("نشاني")]
        public string PersonAddress { get; set; }
        [System.ComponentModel.DisplayName("نوع نشاني")]
        [System.ComponentModel.Description("نوع نشاني")]
        public IList<string> PersonAddressTypeId { get; set; }
        [System.ComponentModel.DisplayName("عنوان نوع نشاني")]
        [System.ComponentModel.Description("عنوان نوع نشاني")]
        public string PersonAddressTypeTitle { get; set; }
        [System.ComponentModel.DisplayName("شماره تلفن ثابت")]
        [System.ComponentModel.Description("تلفن ثابت")]
        public string PersonTel { get; set; }
        [System.ComponentModel.DisplayName("آدرس پست الکترونيکي")]
        [System.ComponentModel.Description("آدرس پست الکترونيکي")]
        public string PersonEmail { get; set; }
        [System.ComponentModel.DisplayName("توضيحات")]
        [System.ComponentModel.Description("توضيحات")]
        public string PersonDescription { get; set; }
        [System.ComponentModel.DisplayName("شناسه دفترخانه")]
        [System.ComponentModel.Description("شناسه دفترخانه")]
        public IList<string> ScriptoriumId { get; set; }
        [System.ComponentModel.DisplayName("عنوان دفترخانه")]
        [System.ComponentModel.Description("عنوان دفترخانه")]
        public string ScriptoriumTitle { get; set; }
        [System.ComponentModel.DisplayName("جنسيت")]
        [System.ComponentModel.Description("جنسيت")]
        public string PersonSexType { get; set; }
        public string PersonBirthLocation { get; set; }
        [System.ComponentModel.DisplayName("شناسه سمت اشخاص در اجرائيات ثبت")]
        [System.ComponentModel.Description("سمت")]
        public IList<string> PersonPostTypeId { get; set; }
        [System.ComponentModel.DisplayName("عنوان سمت اشخاص در اجرائيات ثبت")]
        [System.ComponentModel.Description("سمت")]
        public string PersonPostTypeTitle { get; set; }
        [System.ComponentModel.DisplayName("حقيقي است يا حقوقي؟")]
        [System.ComponentModel.Description("حقیقی/حقوقی")]
        public string ExecutionSupportivePersonType { get; set; }
        [System.ComponentModel.DisplayName("آيا اين شخص وکيل است؟")]
        [System.ComponentModel.Description("وکیل، ولی، نماینده و ... اشخاصی است؟")]
        public bool IsPersonRelated { get; set; }
        public bool IsPersonSanaChecked { get; set; }
        public bool PersonMobileNoStat { get; set; }
        public bool IsPersonAlive { get; set; }
        [System.ComponentModel.DisplayName("آيا اين شخص اصيل است؟")]
        [System.ComponentModel.Description("اصیل است؟")]
        public bool IsPersonOriginal { get; set; }
        [System.ComponentModel.DisplayName("آيا اين شخص ايراني است؟")]
        [System.ComponentModel.Description("ایرانی است؟")]
        public bool IsPersonIranian { get; set; }
        public string PersonalImage { get; set; }
        [System.ComponentModel.DisplayName("سري شناسنامه - بخش حرفي")]
        [System.ComponentModel.Description("سریال شناسنامه")]
        public string PersonIdentityAlphabetSeri { get; set; }
        [System.ComponentModel.DisplayName("مليت")]
        [System.ComponentModel.Description("تابعیت")]
        public IList<string> PersonNationalityId { get; set; }
        [System.ComponentModel.DisplayName("مليت")]
        [System.ComponentModel.Description("تابعیت")]
        public string PersonNationalityTitle { get; set; }
        [System.ComponentModel.DisplayName("شماره گذرنامه")]
        [System.ComponentModel.Description("شماره گذرنامه")]
        public string PersonPassportNo { get; set; }
        [System.ComponentModel.DisplayName("نوع شرکت")]
        [System.ComponentModel.Description("نوع شرکت")]
        public IList<string> PersonCompanyType { get; set; }
        [System.ComponentModel.DisplayName("عنوان شرکت")]
        [System.ComponentModel.Description("عنوان شرکت")]
        public string PersonCompanyTypeTitle { get; set; }
        [System.ComponentModel.DisplayName("محل ثبت")]
        [System.ComponentModel.Description("محل ثبت")]
        public string PersonCompanyRegisterLocation { get; set; }
        [System.ComponentModel.DisplayName("شماره ثبت")]
        [System.ComponentModel.Description("شماره ثبت")]
        public string PersonCompanyRegisterNo { get; set; }
        [System.ComponentModel.DisplayName("تاريخ ثبت شخص حقوقي")]
        [System.ComponentModel.Description("تاريخ ثبت")]
        public string PersonCompanyRegisterDate { get; set; }
        [System.ComponentModel.DisplayName("شماره آخرين روزنامه رسمي/گواهي ثبت شرکت ها")]
        [System.ComponentModel.Description("شماره آخرین روزنامه رسمی")]
        public string PersonLastLegalPaperNo { get; set; }
        [System.ComponentModel.DisplayName("تاريخ آخرين روزنامه رسمي/گواهي ثبت شرکت ها")]
        [System.ComponentModel.Description("تاریخ آخرین روزنامه رسمی")]
        public string PersonLastLegalPaperDate { get; set; }
        [System.ComponentModel.DisplayName("شناسه ماهيت شخص حقوقي")]
        [System.ComponentModel.Description("ماهیت")]
        public IList<string> LegalPersonNatureId { get; set; }
        [System.ComponentModel.DisplayName("عنوان ماهيت شخص حقوقي")]
        [System.ComponentModel.Description("ماهیت")]
        public string LegalPersonNatureTitle { get; set; }
        [System.ComponentModel.DisplayName("شناسه نوع شخص حقوقي")]
        [System.ComponentModel.Description("نوع شخص حقوقی")]
        public IList<string> LegalPersonTypeId { get; set; }
        [System.ComponentModel.DisplayName("عنوان نوع شخص حقوقي")]
        [System.ComponentModel.Description("نوع شخص حقوقی")]
        public string LegalPersonTypeTitle { get; set; }

    }
}
