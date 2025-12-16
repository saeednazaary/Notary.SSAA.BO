
namespace Notary.SSAA.BO.SharedKernel.Constants
{
    public static class SystemMessagesConstant
    {

        public static readonly string Request_IsNew_Invalid =
            "نوع درخواست جدید نمی‌باشد.";

        public static readonly string Request_IsDelete_Invalid =
            "درخواست حذف مجاز نمی‌باشد.";

        public static readonly string SignRequest_IdInvalid =
            "شناسه گواهی امضا معتبر نمی‌باشد.";

        public static readonly string Request_IsDirty_Invalid =
            "نوع درخواست ویرایش نمیباشد";

        public static readonly string Subject_Required =
            "موضوع گواهی امضا الزامی است.";

        public static readonly string Subject_CountInvalid =
            "تعداد موضوعات گواهی امضا باید دقیقاً یک مورد باشد.";

        public static readonly string Subject_IdRequired =
            "شناسه موضوع گواهی امضا اجباری است.";


        public static readonly string Persons_ListInvalid =
            "لیست اشخاص گواهی امضا غیر مجاز است.";

        public static readonly string Person_NotNew =
            "نوع درخواست شخص جدید نمی‌باشد.";

        public static readonly string Person_DeleteNotAllowed =
            "درخواست حذف برای شخص نامعتبر است.";

        public static readonly string PersonName_Required =
            "فیلد نام اجباری است.";

        public static readonly string PersonName_MaxLength =
            "طول نام بیش از حد مجاز است.";

        public static readonly string PersonFamily_Required =
            "فیلد نام خانوادگی اجباری است.";

        public static readonly string PersonFamily_MaxLength =
            "طول نام خانوادگی بیش از حد مجاز است.";

        public static readonly string PersonFatherName_Required =
            "فیلد نام پدر اجباری است.";

        public static readonly string PersonFatherName_MaxLength =
            "طول نام پدر بیش از حد مجاز است.";

        public static readonly string PersonSex_Required =
            "جنسیت اجباری است.";

        public static readonly string PersonSex_Invalid =
            "مقدار جنسیت غیر مجاز است.";

        public static readonly string NationalNo_Required =
            "شماره ملی اجباری است.";

        public static readonly string NationalNo_LengthInvalid =
            "شماره ملی باید ۱۰ رقم باشد.";

        public static readonly string BirthDate_Required =
            "تاریخ تولد اجباری است.";

        public static readonly string BirthDate_InvalidFormat =
            "فرمت تاریخ تولد صحیح نیست.";

        public static readonly string IdentityNo_Required =
            "شماره شناسنامه اجباری است.";

        public static readonly string IdentityNo_MaxLength =
            "طول شماره شناسنامه بیش از حد مجاز است.";

        public static readonly string IdentityNo_Invalid =
            "فرمت شماره شناسنامه اشتباه است.";

        public static readonly string IdentityIssueLocation_Required =
            "محل صدور شناسنامه اجباری است.";

        public static readonly string IdentityIssueLocation_MaxLength =
            "طول محل صدور شناسنامه بیش از حد مجاز است.";


        public static readonly string Nationality_Required =
            "ملیت اجباری است.";

        public static readonly string Nationality_CountInvalid =
            "تعداد ملیت غیر مجاز است.";

        public static readonly string NationalityId_Required =
            "شناسه ملیت اجباری است.";

        public static readonly string NationalityId_Invalid =
            "مقدار شناسه ملیت غیر مجاز است.";


        public static readonly string Mobile_Required =
            "شماره موبایل اجباری است.";

        public static readonly string Mobile_Invalid =
            "فرمت شماره موبایل اشتباه است.";

        public static readonly string Tel_Invalid =
            "فرمت شماره تلفن اشتباه است.";

        public static readonly string Tel_MaxLength =
            "طول شماره تلفن بیش از حد مجاز است.";

        public static readonly string Email_Invalid =
            "فرمت ایمیل اشتباه است.";

        public static readonly string Email_MaxLength =
            "طول ایمیل بیش از حد مجاز است.";


        public static readonly string Address_Required =
            "آدرس اجباری است.";

        public static readonly string Address_TooShort =
            "طول آدرس بسیار کوتاه است.";

        public static readonly string Address_MaxLength =
            "طول آدرس بیش از حد مجاز است (حداکثر ۴۰۰۰ کاراکتر).";

        public static readonly string PostalCode_Required =
            "کد پستی اجباری است.";

        public static readonly string PostalCode_LengthInvalid =
            "کد پستی باید ۱۰ رقم باشد.";

        public static readonly string PostalCode_Invalid =
            "کد پستی باید فقط شامل عدد باشد.";

        public static readonly string Description_MaxLength =
            "مقدار توضیحات بیش از حد مجاز است.";

        public static readonly string RelatedPersons_ListInvalid =
            "لیست اشخاص وابسته گواهی امضا غیر مجاز است.";

        public static readonly string Related_DeleteNotAllowed =
            "نوع درخواست وابسته حذف غیر مجاز است.";

        public static readonly string MainPerson_Required =
            "شخص اصلی اجباری است.";

        public static readonly string MainPerson_CountInvalid =
            "تعداد اشخاص اصلی غیر مجاز است.";

        public static readonly string MainPersonId_Required =
            "شناسه شخص اصلی اجباری است.";

        public static readonly string AgentPerson_Required =
            "شخص نماینده اجباری است.";

        public static readonly string AgentPerson_CountInvalid =
            "تعداد اشخاص نماینده غیر مجاز است.";

        public static readonly string AgentPersonId_Required =
            "شناسه شخص نماینده اجباری است.";

        public static readonly string AgentType_Required =
            "نوع وابستگی اجباری است.";

        public static readonly string AgentType_CountInvalid =
            "تعداد انواع وابستگی غیر مجاز است.";

        public static readonly string ReliableReason_Required =
            "دلیل نیاز به معتمد اجباری است.";

        public static readonly string Operation_Successful = 
            "عملیات با موفقیت انجام شد";
        public static readonly string DocumentTemplate_State_Invalid =
            "مقدار فیلد شناسه غیر مجاز است";

        public static readonly string DocumentTemplate_Id_Invalid =
            "مقدار فیلد وضعیت غیر مجاز است";

        public static readonly string DocumentTemplate_Code_Invalid =
            "مقدار فیلد کد غیر مجاز است";

        public static readonly string DocumentTemplate_Code_MaxLength =
            "طول مقدار فیلد کد بیش از حد مجاز است";

        public static readonly string DocumentType_Count_Invalid =
            "تعداد نوع سند غیر مجاز است";

        public static readonly string DocumentType_Id_Invalid =
            "مقدار فیلد شناسه نوع سند غیر مجاز است";

        public static readonly string DocumentType_Id_MaxLength =
            "طول مقدار فیلد شناسه نوع سند بیش از حد مجاز است";

        public static readonly string DocumentTemplateTitle_Required =
            "فیلد عنوان اجباری است";

        public static readonly string DocumentTemplateTitle_MaxLength =
            "طول مقدار فیلد عنوان بیش از حد مجاز است";

        public static readonly string DocumentTemplate_Id_NotValid =
            "شناسه مورد نظر معتبر نمیباشد.";

        public static readonly string Grid_PageIndex_Invalid =
            "مقدار شماره صقحه غیر مجاز است";

        public static readonly string Grid_PageIndex_Required =
            "فیلد شماره صفحه اجباری است";

        public static readonly string Grid_PageSize_Invalid =
            "مقدار اندازه صقحه غیر مجاز است";

        public static readonly string Grid_PageSize_Required =
            "فیلد اندازه صفحه اجباری است";

        public static readonly string SignRequest_RequestNo_MaxLength =
            "طول شماره درخواست مجاز نمیباشد";

        public static readonly string SignRequest_NationalNo_MaxLength =
            "طول شناسه یکتا گواهی امضا مجاز نمیباشد";

        public static readonly string SignRequest_State_Invalid =
            "مقدار وضعیت مجاز نمیباشد";

        public static readonly string SignRequest_RequestDateFrom_Invalid =
            "فیلد تاریخ درخواست از معتبر نمیباشد";

        public static readonly string SignRequest_RequestDateTo_Invalid =
            "فیلد تاریخ درخواست تا معتبر نمیباشد";

        public static readonly string SignRequest_SignDateFrom_Invalid =
            "فیلد تاریخ امضا از معتبر نمیباشد";

        public static readonly string SignRequest_SignDateTo_Invalid =
            "فیلد تاریخ امضا تا معتبر نمیباشد";
        public static readonly string Person_NationalNo_MaxLength =
            "طول شماره ملی غیر مجاز است";

        public static readonly string Person_Name_MaxLength =
            "طول نام بیشتر از حد مجاز است";

        public static readonly string Person_Family_MaxLength =
            "طول نام خانوادگی بیشتر از حد مجاز است";

        public static readonly string Person_FatherName_MaxLength =
            "طول نام پدر بیشتر از حد مجاز است";

        public static readonly string Person_SignClassifyNo_Format_Invalid =
            "فرمت شناسه کلاسه صحیح نمیباشد";

        public static readonly string Person_SignClassifyNo_MaxLength =
            "طول شناسه کلاسه غیر مجاز است";

        public static readonly string Person_BirthDate_Invalid =
            "مقدار تاریخ تولد غیر مجاز است";

        public static readonly string Person_BirthDate_Required =
            "فیلد تاریخ تولد اجباری است";

        public static readonly string Person_IdentityNo_MaxLength =
            "طول شماره شناسنامه بیشتر از حد مجاز است";

        public static readonly string Person_IdentityNo_Format_Invalid =
            "فرمت شماره شناسنامه اشتباه است";

        public static readonly string Person_Seri_Format_Invalid =
            "سری شناسنامه صحیح نمیباشد";

        public static readonly string Person_Seri_MaxLength =
            "طول سری شخص صحیح نمیباشد";

        public static readonly string Person_Serial_Format_Invalid =
            "طول سریال شناسنامه بیشتر از حد مجاز است";

        public static readonly string Person_Serial_MaxLength =
            "طول سریال شناسنامه بیشتر از حد مجاز است";

        public static readonly string Person_PostalCode_Format_Invalid =
            "مقدار فیلد کد پستی غیر مجاز است";

        public static readonly string Person_PostalCode_MaxLength =
            "طول کد پستی غیر مجاز است";

        public static readonly string Person_Mobile_Format_Invalid =
            "فرمت شماره موبایل اشتباه است";

        public static readonly string Person_Mobile_MaxLength =
            "طول شماره موبایل غیر مجاز است";

        public static readonly string Person_Address_MaxLength =
            "طول آدرس بیشتر از حد مجاز است";

        public static readonly string Person_Address_Required =
            "فیلد آدرس اجباری است";

        public static readonly string Person_Tel_Format_Invalid =
            "فرمت شماره تلفن اشتباه است";
        public static readonly string Person_SexType_Invalid =
            "مقدار فیلد جنسیت غیر مجاز است";

        public static readonly string Person_AlphabetSeri_Invalid =
            "مقدار سری الفبایی شناسنامه غیر مجاز است";

        public static readonly string Person_Sex_Required =
            "جنسیت اجباری است";

        public static readonly string Person_Name_Required =
            "فیلد نام اجباری است";

        public static readonly string Person_Family_Required =
            "فیلد نام خانوادگی اجباری است";

        public static readonly string Person_FatherName_Required =
            "فیلد نام پدر اجباری است";

        public static readonly string Person_NationalNo_Required =
            "فیلد شماره ملی اجباری است";

        public static readonly string Person_IdentityNo_Required =
            "فیلد شماره شناسنامه اجباری است";

        public static readonly string Person_IdentityIssueLocation_Required =
            "فیلد محل صدور شناسنامه اجباری است";

        public static readonly string Person_Serial_Required =
            "فیلد سریال اجباری است";

        public static readonly string Person_PostalCode_Required =
            "فیلد کد پستی اجباری است";

        public static readonly string Person_Mobile_Required =
            "فیلد شماره موبایل اجباری است";

        public static readonly string Person_Email_MaxLength =
            "طول ایمیل بیش از حد مجاز است";

        public static readonly string Person_Email_Format_Invalid =
            "فرمت ایمیل اشتباه است";

        public static readonly string Person_Description_MaxLength =
            "مقدار توضیحات شخص بیش از حد مجاز است";

        public static readonly string Person_Nationality_Invalid =
            "مقدار فیلد ملیت غیر مجاز است";

        public static readonly string Person_Nationality_Count_Invalid =
            "تعداد اشخاص اصلی غیر مجاز میباشد";

        public static readonly string Person_Nationality_Required =
            "ملیت اجباری است";
        public static readonly string Company_RegisterNo_MaxLength =
            "طول شماره ثبت بیشتر از حد مجاز است";

        public static readonly string Company_RegisterNo_Required =
            "فیلد شماره ثبت اجباری است";

        public static readonly string Company_RegisterDate_Invalid =
            "مقدار تاریخ ثبت غیر مجاز است";

        public static readonly string Company_RegisterDate_Required =
            "فیلد تاریخ ثبت اجباری است";

        public static readonly string Company_RegisterLocation_MaxLength =
            "طول شماره ثبت بیشتر از حد مجاز است";

        public static readonly string Company_RegisterLocation_Required =
            "فیلد شماره ثبت اجباری است";
        public static readonly string Related_DocumentId_Invalid =
            "مقدار شناسه سند اجباری است";

        public static readonly string Related_DocumentId_Required =
            "شناسه سند اجباری است";

        public static readonly string Related_MainPerson_Count_Invalid =
            "تعداد اشخصاص اصلی شخص غیر مجاز میباشد";

        public static readonly string Related_MainPersonId_Invalid =
            "مقدار شناسه شخص اصلی غیر مجاز است";

        public static readonly string Related_MainPersonId_Required =
            "شناسه شخص اصلی اجباری است";

        public static readonly string Related_Agent_Count_Invalid =
            "تعداد اشخاص نماینده غیر مجاز میباشد";

        public static readonly string Related_AgentId_Invalid =
            "مقدار شناسه شخص نماینده غیر مجاز است";

        public static readonly string Related_AgentId_Required =
            "شناسه شخص نماینده اجباری است";

        public static readonly string Related_AgentType_Count_Invalid =
            "تعداد انواع وابستگی غیر مجاز میباشد";

        public static readonly string Related_AgentType_Required =
            "شناسه نوع وابستگی اجباری است";

        public static readonly string Related_ReliableReason_Invalid =
            "فیلد دلیل نیاز به معتمد غیر مجاز است";

        public static readonly string Related_Scriptorium_Invalid =
            "فیلد دفترخانه غیر مجاز است";
        public static readonly string Document_Type_Count_Invalid =
            "تعداد نوع سند غیر مجاز است";

        public static readonly string Document_TypeId_Required =
            "شناسه نوع سند اجباری است";

        public static readonly string Document_People_Invalid =
            "مقدار اشخاص سند غیر مجاز است";

        public static readonly string Document_RelatedPeople_Invalid =
            "مقدار اشخاص وابسته سند غیر مجاز است";

        public static readonly string Request_Invalid =
            "درخواست نامعتبر است";
        public static readonly string Judgment_Create_Required =
            "زمانیکه درخواست براساس حکم دادگاه است، ایجاد حکم دادگاه اجباری است";

        public static readonly string Judgment_Type_Required =
            "فیلد نوع حکم اجباری است";

        public static readonly string Judgment_IssueDate_Required =
            "فیلد تاریخ حکم اجباری است";

        public static readonly string Judgment_IssueNo_Required =
            "فیلد شماره حکم اجباری است";

        public static readonly string Judgment_IssuerName_Required =
            "فیلد صادرکننده حکم اجباری است";

        public static readonly string Judgment_CaseClassifyNo_Required =
            "فیلد شماره کلاسه حکم اجباری است";
        public static readonly string Related_ReliableReason_Required =
            "فیلد دلیل نیاز به معتمد اجباری است";

        public static readonly string Related_DocumentDate_Invalid =
            "مقدار تاریخ وکالتنامه غیر مجاز است";

        public static readonly string Related_DocumentNo_Required =
            "فیلد شماره وکالتنامه اجباری است";

        public static readonly string Related_DocumentNo_MaxLength =
            "طول شماره وکالتنامه نباید بیشتر از 50 کاراکتر باشد";

        public static readonly string Related_DocumentIssuer_Required =
            "فیلد مرجع صدور اجباری میباشد";

        public static readonly string Related_DocumentCountry_Required =
            "فیلد کشور اجباری میباشد";

        public static readonly string Related_Scriptorium_Required =
            "فیلد دفترخانه اجباری میباشد";
        public static readonly string Related_DocumentDate_SSAR_Invalid =
            "تاریخ وکالتنامه غیر مجاز است";
        public static readonly string Related_MainPerson_Deleted =
            "شخص اصیل شخص وابسته حذف شده است";

        public static readonly string Related_AgentPerson_Deleted =
            "شخص نماینده شخص وابسته حذف شده است";
        public static readonly string Document_NotFound =
            "سند مربوطه یافت نشد";
        public static readonly string Cadastre_Parts_Fetch_Failed =
            "خطا در دریافت قطعات تفکیکی از سامانه املاک رخ داده است";
        public static readonly string OrganizationRegistry_Service_Execution_Failed =
            "خطا در حین اجرای سرویس در سرویس دهنده سازمان ثبت رخ داد";

    }
}
