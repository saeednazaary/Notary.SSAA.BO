using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Notary.SSAA.BO.Configuration;
using Notary.SSAA.BO.Domain.Abstractions;
using Notary.SSAA.BO.Domain.Entities;
using Notary.SSAA.BO.SharedKernel.Enumerations;
using Notary.SSAA.BO.SharedKernel.Interfaces;
using Notary.SSAA.BO.Utilities.Extensions;
using Notary.SSAA.BO.Coordinator.NotaryDocument.Core.Commons;
using Stimulsoft.System.Windows.Forms;
using Microsoft.EntityFrameworkCore;

namespace Notary.SSAA.BO.Coordinator.NotaryDocument.Core.ENoteBook
{
    public class ENoteBookServerController
    {
        private readonly IDocumentElectronicBookRepository _documentElectronicBookRepository;
        private readonly IDocumentElectronicBookBaseInfoRepository _documentElectronicBookBaseInfoRepository;
        private readonly IDocumentRepository _documentRepository;
        private readonly IUserService _userService;
        private readonly ClientConfiguration _clientConfiguration;
        private readonly IDateTimeService _dateTimeService;
        private readonly SignProvider _signProvider;

        public ENoteBookServerController(IDocumentElectronicBookRepository documentElectronicBookRepository,
            IDocumentElectronicBookBaseInfoRepository documentElectronicBookBaseInfoRepository
            , IDocumentRepository documentRepository, IUserService userService,
            ClientConfiguration clientConfiguration, IDateTimeService dateTimeService, SignProvider signProvider)
        {
            _documentElectronicBookRepository = documentElectronicBookRepository;
            _documentElectronicBookBaseInfoRepository = documentElectronicBookBaseInfoRepository;
            _documentRepository = documentRepository;
            _userService = userService;
            _clientConfiguration = clientConfiguration;
            _dateTimeService = dateTimeService;
            _signProvider = signProvider;
        }

        public async Task<(decimal?, string)> ProvideNextClassifyNo(Document theCurrentRegisterServiceReq, CancellationToken cancellationToken, string messages)
        {



            string messagesRef = messages;
            decimal? lastClassifyNo = null;


            if (!string.IsNullOrWhiteSpace(theCurrentRegisterServiceReq.NationalNo))
            {
                var theDigitalBook =
                    await _documentElectronicBookRepository.GetDocumentElectronicBook(theCurrentRegisterServiceReq.NationalNo,
                        cancellationToken); //  Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalDocumentsBook>(theCurrentRegisterServiceReq.NationalNo, NotaryOfficeQuery.ONotaryDigitalDocumentsBook.NationalNo);
                if (theDigitalBook != null && theDigitalBook.ClassifyNo != null)
                {
                    lastClassifyNo = theDigitalBook.ClassifyNo;
                    return (lastClassifyNo, messagesRef);
                }
            }

            lastClassifyNo = await this.GetLastClassifyNoFromDigitalBook(theCurrentRegisterServiceReq.ScriptoriumId, cancellationToken);
            if (lastClassifyNo.HasValue)
                return (lastClassifyNo + 1, messagesRef);

            lastClassifyNo = await this.GetLastClassifyNoFromDigitalBookBaseInfo(theCurrentRegisterServiceReq.ScriptoriumId, cancellationToken);
            if (lastClassifyNo.HasValue)
                return (++lastClassifyNo, messagesRef);

            return (null, messagesRef);
        }

        private async Task<long?> GetLastClassifyNoFromDigitalBook(string scriptoriumId, CancellationToken cancellationToken)
        {
            return await _documentElectronicBookRepository.GetLastClassifyNoFromDigitalBook(scriptoriumId,
                cancellationToken);
        }
        private async Task<long?> GetLastClassifyNoFromDigitalBookBaseInfo(string scriptoriumId, CancellationToken cancellationToken)
        {
            var documentElectronicBookBaseInfo =
                  await _documentElectronicBookBaseInfoRepository.GetAsync(t => t.ScriptoriumId == scriptoriumId,
                    cancellationToken);
            return documentElectronicBookBaseInfo?.LastClassifyNo;
        }

        /// <summary>
        /// این تابع صفحات دفتر الکترونیک را ایجاد می نماید.
        /// </summary>
        /// <param name="currentRegisterServiceReq">سندی که صفحه دفتر برای آن ایجاد می گردد.</param>
        /// <param name="documentChainIndex">جایگاه سند در زنجیره اسناد</param>
        /// <param name="messages">پیام در صورت عدم ایجاد دفتر الکترونیک</param>
        /// <returns>لیستی از صفحات دفتر الکترونیک بر می گرداند</returns>
        public async Task<(List<DocumentElectronicBook>, int, string, bool)> ProvideDigitalBookEntity(Document currentRegisterServiceReq, CancellationToken cancellationToken, int documentChainIndex, string messages)
        {
            string messagesRef = messages;
            int documentChainIndexRef = documentChainIndex;
            bool relatedDocumentIsInSsar = false;
            bool isExistRelatedDoc = false;
            bool isexistClassifyNo = false;


            documentChainIndexRef++;
            var masterCriteria =
                _documentElectronicBookRepository.TableNoTracking.Where(t =>
                    t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId);

            if (currentRegisterServiceReq.DocumentType.IsSupportive == YesNo.Yes.GetString())
            {
                if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                {
                    if (string.IsNullOrWhiteSpace(currentRegisterServiceReq.RelatedDocumentNo))
                        return (null, documentChainIndexRef, messagesRef, false);

                    Document theRelatedDoc;
                    (theRelatedDoc, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.RelatedDocumentNo, currentRegisterServiceReq.ScriptoriumId, cancellationToken, messagesRef);
                    if (theRelatedDoc == null)
                        return (null, documentChainIndexRef, messagesRef, false);
                    else
                        masterCriteria = masterCriteria
                            .Where(t => (t.NationalNo == currentRegisterServiceReq.RelatedDocumentNo) || (t.ClassifyNo == theRelatedDoc.ClassifyNo));

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(currentRegisterServiceReq.RelatedDocumentNo))
                        masterCriteria = masterCriteria
                            .Where(t => t.ClassifyNo.ToString() == currentRegisterServiceReq.RelatedDocumentNo);

                }
            }
            else
            {
                if (currentRegisterServiceReq.ClassifyNo.HasValue)
                {
                    masterCriteria = masterCriteria
                        .Where(t => t.NationalNo == currentRegisterServiceReq.NationalNo || t.ClassifyNo == currentRegisterServiceReq.ClassifyNo);

                }
                else
                {
                    masterCriteria = masterCriteria
                        .Where(t => t.NationalNo == currentRegisterServiceReq.NationalNo);


                }



            }

            //long? nextClassifyNo = this.ProvideNextClassifyNo(currentRegisterServiceReq, ref messages);
            //slaveCriteria.AddOrEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryDigitalDocumentsBook.ClassifyNo, nextClassifyNo);


            DocumentElectronicBook? theDigitalBookEntity = await masterCriteria.FirstOrDefaultAsync(cancellationToken);
            if (theDigitalBookEntity != null)
                return (new List<DocumentElectronicBook>() { theDigitalBookEntity }, documentChainIndexRef, messagesRef, true); ;

            List<DocumentElectronicBook> generatedDigitalBooks = null;

            theDigitalBookEntity = new DocumentElectronicBook
            {
                Ilm = "1",
                Id = Guid.NewGuid(),
                ScriptoriumId = currentRegisterServiceReq.ScriptoriumId,
                SignDate = currentRegisterServiceReq.SignDate,
                DocumentDate = _dateTimeService.CurrentPersianDate,
                EnterBookDateTime = _dateTimeService.CurrentPersianDateTime,
                RecordDate = _dateTimeService.CurrentDateTime,
                HashOfPdf = await _signProvider.ProvideDocumentImageHash(currentRegisterServiceReq.Id.ToString(), cancellationToken),
                HashOfFingerprints = await _signProvider.ProvideFingerPrintsHash(currentRegisterServiceReq.Id.ToString(), cancellationToken)
            };

            ///تولید رکورد دفتر الکترونیک در 3 حالت زیر می باشد.
            ///1. سند جاری از نوع اسناد اصلی می باشد.
            ///2. سند جاری از نوع خدمت تبعی می باشد و سند وابسته در سامانه می باشد.
            ///3. سند جاری از نوع خدمت تبعی می باشد و سند وابسته در سامانه نمی باشد.
            #region 1. MainDocumentTypes-(Is4RegisterService = NO)
            if (currentRegisterServiceReq.DocumentType.IsSupportive == YesNo.No.GetString())
            {
                theDigitalBookEntity.NationalNo = currentRegisterServiceReq.NationalNo;
                theDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.DocumentTypeId;

                ///در صورتی که به صورت زنجیره ای در حال ساخت صفحه دفتر باشیم، نباید برای اسناد دوم به بعد از دیتابیس برای تخصیص شماره ترتیب بعدی استفاده نماییم.
                ///بدین منظور باید تشخیص بدیم که سند چندم در زنجیره هستیم و بر این اساس سیاست تخصیص شماره ترتیب را انتخاب نماییم 
                if (documentChainIndexRef == 1)
                {

                    decimal? classifyNo;
                    (classifyNo, messagesRef) = await this.ProvideNextClassifyNo(currentRegisterServiceReq, cancellationToken, messagesRef);
                    theDigitalBookEntity.ClassifyNo = int.Parse(classifyNo.ToString());
                }
                else
                {
                    (Document currentDocument, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.NationalNo, currentRegisterServiceReq.ScriptoriumId, cancellationToken, messagesRef);

                    if (currentDocument == null)
                    {
                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        messagesRef = "خطا در فراخوانی سند وابسته به منظور ایجاد صفحه دفتر الکترونیک.";
                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.DocumentDate = currentDocument.DocumentDate;
                    DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                        await _documentElectronicBookBaseInfoRepository.GetAsync(t => t.ScriptoriumId ==
                             currentRegisterServiceReq.ScriptoriumId, cancellationToken);//Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalBookBaseInfo>(currentRegisterServiceReq.ScriptoriumId, NotaryOfficeQuery.ONotaryDigitalBookBaseInfo.ScriptoriumId);



                    if (currentDocument.ClassifyNo > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                    {
                        messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.ClassifyNo = currentDocument.ClassifyNo;
                }

                ///اگر سند وابسته داشته باشیم و دفترخانه صادر کننده آن همین دفترخانه جاری باشد دو حالت خواهیم داشت.
                ///1. سند وابسته در سامانه تنظیم شده است که در این حالت از اطلاعات کامل سند وابسته برای ایجاد صفحه دفتر الکترونیک استفاده می نماییم
                ///2. سند وابسته قبل از سامانه تنظیم شده است که در این صورت باید صفحه دفتر را مظابق اطلاعات وارد شده در خصوص سند وابسته ایجاد نماییم
                ///
                #region RelatedDocDigitalBookGenerating
                if (
                    currentRegisterServiceReq.RelatedScriptoriumId != null &&
                    currentRegisterServiceReq.RelatedScriptoriumId == currentRegisterServiceReq.ScriptoriumId &&
                    documentChainIndexRef <= 1
                    )
                {
                    //1. سند وابسته در سامانه تنظیم شده است.
                    #region 1. سند وابسته در سامانه تنظیم شده است.
                    if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                    {
                        //این شرط به این منظور در نظر گرفته شده است که اسنادی که بصورت پیوسته و زنجیره ای قرار می گیرند نباید همگی در فرایند ساخت دفتر قرار گیرند/
                        //دفتر الکترونیک فقط برای سند اصلی و سند وابسته ایجاد می گردد.                 
                      
                            Document theRelatedReq = await _documentRepository.GetAsync(t =>
                                t.NationalNo == currentRegisterServiceReq.RelatedDocumentNo
                                && t.ScriptoriumId == currentRegisterServiceReq.RelatedScriptoriumId, cancellationToken);
                            bool isExistElectronicbook;
                            if (theRelatedReq != null)
                            {
                                (List<DocumentElectronicBook> theRelatedDigitalBooks, documentChainIndexRef, messagesRef, isExistElectronicbook) = await this.ProvideDigitalBookEntity(theRelatedReq, cancellationToken, documentChainIndexRef, messagesRef);

                                if (theRelatedDigitalBooks == null && !string.IsNullOrEmpty(messagesRef))
                                    return (null, documentChainIndexRef, messagesRef, isExistElectronicbook); ;

                                    generatedDigitalBooks = [.. theRelatedDigitalBooks];
                            }
                        
                    }
                    #endregion

                    //2. سند وابسته قبل از سامانه تنظیم شده است.
                    #region سند وابسته قبل از سامانه تنظیم شده است.
                    else if (currentRegisterServiceReq.IsRelatedDocAbroad == YesNo.No.GetString() &&
                        currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.No.GetString())
                    {

                        DocumentElectronicBook theRelatedDigitalBookEntity = await _documentElectronicBookRepository
                            .GetAsync(
                                t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId &&
                                     t.ClassifyNo == currentRegisterServiceReq.ClassifyNo, cancellationToken);


                        if (theRelatedDigitalBookEntity == null)
                        {
                            theRelatedDigitalBookEntity = new DocumentElectronicBook();
                            theRelatedDigitalBookEntity.ScriptoriumId = currentRegisterServiceReq.ScriptoriumId;
                            theRelatedDigitalBookEntity.SignDate = currentRegisterServiceReq.RelatedDocumentDate;
                            theRelatedDigitalBookEntity.DocumentDate = currentRegisterServiceReq.RelatedDocumentDate;
                            theRelatedDigitalBookEntity.RecordDate = _dateTimeService.CurrentDateTime;
                            ;
                            //تاریخ درج در دفتر همیشه تاریخ جاری می باشد. بدون هیچونه استثنایی
                            theRelatedDigitalBookEntity.EnterBookDateTime = currentRegisterServiceReq.RelatedDocumentDate;
                            theRelatedDigitalBookEntity.NationalNo = "-";

                            DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                                await _documentElectronicBookBaseInfoRepository.GetAsync(
                                    t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId, cancellationToken);

                            if (long.Parse(currentRegisterServiceReq.RelatedDocumentNo) > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                            {
                                messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                                //if ( theRelatedDigitalBookEntity.IsNew )
                                //    theRelatedDigitalBookEntity.MarkForDelete ();

                                return (null, documentChainIndexRef, messagesRef, false); ;
                            }

                            theRelatedDigitalBookEntity.ClassifyNo = int.Parse(currentRegisterServiceReq.RelatedDocumentNo);
                            theRelatedDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.RelatedDocumentTypeId;

                                generatedDigitalBooks = new List<DocumentElectronicBook>();
                                generatedDigitalBooks.Add(theRelatedDigitalBookEntity);
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region 2. RelatedDocumentTypes-(Is4RegisterService = YES)
            else
            {
                if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                {
                    (Document relatedDocument, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.RelatedDocumentNo, currentRegisterServiceReq.RelatedScriptoriumId, cancellationToken, messagesRef);

                    if (relatedDocument == null)
                    {
                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.NationalNo = relatedDocument.NationalNo;
                    theDigitalBookEntity.DocumentDate = relatedDocument.DocumentDate;
                    theDigitalBookEntity.ClassifyNo = relatedDocument.ClassifyNo;
                    theDigitalBookEntity.DocumentTypeId = relatedDocument.DocumentTypeId;
                }
                else
                {
                    theDigitalBookEntity.NationalNo = "-";
                    theDigitalBookEntity.DocumentDate = currentRegisterServiceReq.RelatedDocumentDate;

                    if (!currentRegisterServiceReq.RelatedDocumentNo.IsDigit())
                    {
                        messagesRef = "شماره سند وابسته حتماً باید مقدار عددی باشد. لطفاً اطلاعات سند وابسته را در فرم اصلاح نموده و مجدداً تلاش نمایید.";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                        await _documentElectronicBookBaseInfoRepository.GetAsync(
                            t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId, cancellationToken);

                    if (long.Parse(currentRegisterServiceReq.RelatedDocumentNo) > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                    {
                        messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.ClassifyNo = int.Parse(currentRegisterServiceReq.RelatedDocumentNo);
                    theDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.RelatedDocumentTypeId;
                }
            }
            #endregion

            if (generatedDigitalBooks == null)
                generatedDigitalBooks = new List<DocumentElectronicBook>();

            generatedDigitalBooks.Add(theDigitalBookEntity);

            return (generatedDigitalBooks, documentChainIndexRef, messagesRef, false); ;
        }

        public async Task<(List<DocumentElectronicBook>, int, string, bool)> ProvideDigitalBookEntityWithReservedClassifyNo(Document currentRegisterServiceReq, CancellationToken cancellationToken, int documentChainIndex, string messages)
        {
            string messagesRef = messages;
            int documentChainIndexRef = documentChainIndex;
            bool relatedDocumentIsInSsar = false;
            bool isExistRelatedDoc = false;
            bool isexistClassifyNo = false;


            documentChainIndexRef++;
            var masterCriteria =
                _documentElectronicBookRepository.TableNoTracking.Where(t =>
                    t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId);

            if (currentRegisterServiceReq.DocumentType.IsSupportive == YesNo.Yes.GetString())
            {
                if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                {
                    if (string.IsNullOrWhiteSpace(currentRegisterServiceReq.RelatedDocumentNo))
                        return (null, documentChainIndexRef, messagesRef, false);

                    Document theRelatedDoc;
                    (theRelatedDoc, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.RelatedDocumentNo, currentRegisterServiceReq.ScriptoriumId, cancellationToken, messagesRef);
                    if (theRelatedDoc == null)
                        return (null, documentChainIndexRef, messagesRef, false);
                    else
                        masterCriteria = masterCriteria
                            .Where(t => (t.NationalNo == currentRegisterServiceReq.RelatedDocumentNo) || (t.ClassifyNo == theRelatedDoc.ClassifyNo));

                }
                else
                {
                    if (!string.IsNullOrWhiteSpace(currentRegisterServiceReq.RelatedDocumentNo))
                        masterCriteria = masterCriteria
                            .Where(t => t.ClassifyNo.ToString() == currentRegisterServiceReq.RelatedDocumentNo);

                }
            }
            else
            {
                if (currentRegisterServiceReq.ClassifyNo.HasValue)
                {
                    masterCriteria = masterCriteria
                        .Where(t => t.NationalNo == currentRegisterServiceReq.NationalNo || t.ClassifyNo == currentRegisterServiceReq.ClassifyNo);

                }
                else
                {
                    masterCriteria = masterCriteria
                        .Where(t => t.NationalNo == currentRegisterServiceReq.NationalNo);


                }



            }

            //long? nextClassifyNo = this.ProvideNextClassifyNo(currentRegisterServiceReq, ref messages);
            //slaveCriteria.AddOrEqualTo(Rad.CMS.NotaryOfficeQuery.ONotaryDigitalDocumentsBook.ClassifyNo, nextClassifyNo);


            DocumentElectronicBook? theDigitalBookEntity = await masterCriteria.FirstOrDefaultAsync(cancellationToken);
            if (theDigitalBookEntity != null)
                return (new List<DocumentElectronicBook>() { theDigitalBookEntity }, documentChainIndexRef, messagesRef, true); ;

            List<DocumentElectronicBook> generatedDigitalBooks = null;

            theDigitalBookEntity = new DocumentElectronicBook
            {
                Ilm = "1",
                Id = Guid.NewGuid(),
                ScriptoriumId = currentRegisterServiceReq.ScriptoriumId,
                SignDate = currentRegisterServiceReq.SignDate,
                DocumentDate = _dateTimeService.CurrentPersianDate,
                EnterBookDateTime = _dateTimeService.CurrentPersianDateTime,
                RecordDate = _dateTimeService.CurrentDateTime,

                HashOfPdf = await _signProvider.ProvideDocumentImageHash(currentRegisterServiceReq.Id.ToString(), cancellationToken),
                HashOfFingerprints = await _signProvider.ProvideFingerPrintsHash(currentRegisterServiceReq.Id.ToString(), cancellationToken)
            };

            ///تولید رکورد دفتر الکترونیک در 3 حالت زیر می باشد.
            ///1. سند جاری از نوع اسناد اصلی می باشد.
            ///2. سند جاری از نوع خدمت تبعی می باشد و سند وابسته در سامانه می باشد.
            ///3. سند جاری از نوع خدمت تبعی می باشد و سند وابسته در سامانه نمی باشد.
            #region 1. MainDocumentTypes-(Is4RegisterService = NO)
            if (currentRegisterServiceReq.DocumentType.IsSupportive == YesNo.No.GetString())
            {
                theDigitalBookEntity.NationalNo = currentRegisterServiceReq.NationalNo;
                theDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.DocumentTypeId;

                ///در صورتی که به صورت زنجیره ای در حال ساخت صفحه دفتر باشیم، نباید برای اسناد دوم به بعد از دیتابیس برای تخصیص شماره ترتیب بعدی استفاده نماییم.
                ///بدین منظور باید تشخیص بدیم که سند چندم در زنجیره هستیم و بر این اساس سیاست تخصیص شماره ترتیب را انتخاب نماییم 
                if (documentChainIndexRef == 1)
                {

                    decimal? classifyNo;
                    (classifyNo, messagesRef) = await this.ProvideNextClassifyNo(currentRegisterServiceReq, cancellationToken, messagesRef);
                    theDigitalBookEntity.ClassifyNoReserved = int.Parse(classifyNo.ToString());
                }
                else
                {
                    (Document currentDocument, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.NationalNo, currentRegisterServiceReq.ScriptoriumId, cancellationToken, messagesRef);

                    if (currentDocument == null)
                    {
                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        messagesRef = "خطا در فراخوانی سند وابسته به منظور ایجاد صفحه دفتر الکترونیک.";
                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.DocumentDate = currentDocument.DocumentDate;
                    DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                        await _documentElectronicBookBaseInfoRepository.GetAsync(t => t.ScriptoriumId ==
                             currentRegisterServiceReq.ScriptoriumId, cancellationToken);//Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalBookBaseInfo>(currentRegisterServiceReq.ScriptoriumId, NotaryOfficeQuery.ONotaryDigitalBookBaseInfo.ScriptoriumId);



                    if (currentDocument.ClassifyNo > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                    {
                        messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.ClassifyNoReserved = int.Parse(currentDocument.ClassifyNo?.ToString());
                }

                ///اگر سند وابسته داشته باشیم و دفترخانه صادر کننده آن همین دفترخانه جاری باشد دو حالت خواهیم داشت.
                ///1. سند وابسته در سامانه تنظیم شده است که در این حالت از اطلاعات کامل سند وابسته برای ایجاد صفحه دفتر الکترونیک استفاده می نماییم
                ///2. سند وابسته قبل از سامانه تنظیم شده است که در این صورت باید صفحه دفتر را مظابق اطلاعات وارد شده در خصوص سند وابسته ایجاد نماییم
                ///
                #region RelatedDocDigitalBookGenerating
                if (
                    currentRegisterServiceReq.RelatedScriptoriumId != null &&
                    currentRegisterServiceReq.RelatedScriptoriumId == currentRegisterServiceReq.ScriptoriumId &&
                    documentChainIndexRef <= 1)
                {
                    //1. سند وابسته در سامانه تنظیم شده است.
                    #region 1. سند وابسته در سامانه تنظیم شده است.
                    if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                    {
                        //این شرط به این منظور در نظر گرفته شده است که اسنادی که بصورت پیوسته و زنجیره ای قرار می گیرند نباید همگی در فرایند ساخت دفتر قرار گیرند/
                        //دفتر الکترونیک فقط برای سند اصلی و سند وابسته ایجاد می گردد.                 
                        if (documentChainIndexRef <= 1)
                        {
                            Document theRelatedReq = await _documentRepository.GetAsync(t =>
                                t.NationalNo == currentRegisterServiceReq.RelatedDocumentNo
                                && t.ScriptoriumId == currentRegisterServiceReq.RelatedScriptoriumId, cancellationToken);
                            bool isExistElectronicbook;
                            if (theRelatedReq != null)
                            {
                                (List<DocumentElectronicBook> theRelatedDigitalBooks, documentChainIndexRef, messagesRef, isExistElectronicbook) = await this.ProvideDigitalBookEntityWithReservedClassifyNo(theRelatedReq, cancellationToken, documentChainIndexRef, messagesRef);

                                if (theRelatedDigitalBooks == null && !string.IsNullOrEmpty(messagesRef))
                                    return (null, documentChainIndexRef, messagesRef, isExistElectronicbook); ;

                                if (generatedDigitalBooks == null)
                                    generatedDigitalBooks = new List<DocumentElectronicBook>();

                                generatedDigitalBooks.AddRange(theRelatedDigitalBooks);
                            }
                        }
                    }
                    #endregion

                    //2. سند وابسته قبل از سامانه تنظیم شده است.
                    #region سند وابسته قبل از سامانه تنظیم شده است.
                    else if (currentRegisterServiceReq.IsRelatedDocAbroad == YesNo.No.GetString() &&
                        currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.No.GetString())
                    {

                        DocumentElectronicBook theRelatedDigitalBookEntity = await _documentElectronicBookRepository
                            .GetAsync(
                                t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId &&
                                     t.ClassifyNo == currentRegisterServiceReq.ClassifyNo, cancellationToken);


                        if (theRelatedDigitalBookEntity == null)
                        {
                            theRelatedDigitalBookEntity = new DocumentElectronicBook();
                            theRelatedDigitalBookEntity.ScriptoriumId = currentRegisterServiceReq.ScriptoriumId;
                            theRelatedDigitalBookEntity.RecordDate = _dateTimeService.CurrentDateTime;
                            theRelatedDigitalBookEntity.SignDate = currentRegisterServiceReq.RelatedDocumentDate;
                            theRelatedDigitalBookEntity.DocumentDate = currentRegisterServiceReq.RelatedDocumentDate;
                            //تاریخ درج در دفتر همیشه تاریخ جاری می باشد. بدون هیچونه استثنایی
                            theRelatedDigitalBookEntity.EnterBookDateTime = currentRegisterServiceReq.RelatedDocumentDate;
                            theRelatedDigitalBookEntity.NationalNo = "-";

                            DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                                await _documentElectronicBookBaseInfoRepository.GetAsync(
                                    t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId, cancellationToken);

                            if (long.Parse(currentRegisterServiceReq.RelatedDocumentNo) > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                            {
                                messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                                //if ( theRelatedDigitalBookEntity.IsNew )
                                //    theRelatedDigitalBookEntity.MarkForDelete ();

                                return (null, documentChainIndexRef, messagesRef, false); ;
                            }

                            theRelatedDigitalBookEntity.ClassifyNo = int.Parse(currentRegisterServiceReq.RelatedDocumentNo);
                            theRelatedDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.RelatedDocumentTypeId;

                            if (generatedDigitalBooks == null)
                                generatedDigitalBooks = new List<DocumentElectronicBook>();

                            generatedDigitalBooks.Add(theRelatedDigitalBookEntity);
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region 2. RelatedDocumentTypes-(Is4RegisterService = YES)
            else
            {
                if (currentRegisterServiceReq.RelatedDocumentIsInSsar == YesNo.Yes.GetString())
                {
                    (Document relatedDocument, messagesRef) = await this.GetRelatedDocFromView(currentRegisterServiceReq.RelatedDocumentNo, currentRegisterServiceReq.RelatedScriptoriumId, cancellationToken, messagesRef);

                    if (relatedDocument == null)
                    {
                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.NationalNo = relatedDocument.NationalNo;
                    theDigitalBookEntity.DocumentDate = relatedDocument.DocumentDate;
                    theDigitalBookEntity.ClassifyNo = int.Parse(relatedDocument.ClassifyNo?.ToString()); //long.Parse ( relatedDocument [ "ClassifyNo" ].ToString () );
                    theDigitalBookEntity.DocumentTypeId = relatedDocument.DocumentTypeId;
                    theDigitalBookEntity.RecordDate = _dateTimeService.CurrentDateTime;
                }
                else
                {
                    theDigitalBookEntity.NationalNo = "-";
                    theDigitalBookEntity.DocumentDate = currentRegisterServiceReq.RelatedDocumentDate;

                    if (!currentRegisterServiceReq.RelatedDocumentNo.IsDigit())
                    {
                        messagesRef = "شماره سند وابسته حتماً باید مقدار عددی باشد. لطفاً اطلاعات سند وابسته را در فرم اصلاح نموده و مجدداً تلاش نمایید.";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    DocumentElectronicBookBaseinfo theCurrentDigitalBookBaseInfo =
                        await _documentElectronicBookBaseInfoRepository.GetAsync(
                            t => t.ScriptoriumId == currentRegisterServiceReq.ScriptoriumId, cancellationToken);

                    if (long.Parse(currentRegisterServiceReq.RelatedDocumentNo) > theCurrentDigitalBookBaseInfo.LastClassifyNo)
                    {
                        messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";

                        //if ( theDigitalBookEntity.IsNew )
                        //    theDigitalBookEntity.MarkForDelete ();

                        return (null, documentChainIndexRef, messagesRef, false); ;
                    }

                    theDigitalBookEntity.ClassifyNo = int.Parse(currentRegisterServiceReq.RelatedDocumentNo);
                    theDigitalBookEntity.DocumentTypeId = currentRegisterServiceReq.RelatedDocumentTypeId;
                }
            }
            #endregion

            if (generatedDigitalBooks == null)
                generatedDigitalBooks = new List<DocumentElectronicBook>();

            generatedDigitalBooks.Add(theDigitalBookEntity);

            return (generatedDigitalBooks, documentChainIndexRef, messagesRef, false); ;
        }


        public async Task<(Document, string)> GetRelatedDocFromView(string relatedDocumentNo, string relatedScriptoriumId, CancellationToken cancellationToken, string messages)
        {
            string messagesRef = messages;
            Document document = await _documentRepository.GetAsync(t =>
                t.ScriptoriumId == relatedScriptoriumId && t.NationalNo == relatedDocumentNo && t.State == "6", cancellationToken);

            var currentScriptoriumDigitalBookBaseInfo = await _documentElectronicBookBaseInfoRepository
                .GetAsync(t => t.ScriptoriumId == _userService.UserApplicationContext.ScriptoriumInformation.Id,
                    cancellationToken); // Rad.CMS.InstanceBuilder.GetEntityByCode<IONotaryDigitalBookBaseInfo>(((Rad.BaseInfo.SystemConfiguration.ICMSOrganization)BaseInfoContext.Instance.CurrentCMSOrganization).ScriptoriumId, NotaryOfficeQuery.ONotaryDigitalBookBaseInfo.ScriptoriumId);

            if (document == null || document.ClassifyNo == null)
            {
                messagesRef = "سند وابسته یافت نشد. لطفاً اطلاعات سند وابسته را مجدداً بررسی نمایید.";
                return (null, messagesRef);
            }

            long relatedDocClassifyNo = long.Parse(document.ClassifyNo.ToString());

            if (
                relatedDocClassifyNo > currentScriptoriumDigitalBookBaseInfo.LastClassifyNo &&
                string.Compare(document.DocumentDate.ToString(), _clientConfiguration.ENoteBookEnabledDate) < 0
            )
            {
                messagesRef = "شماره سند وابسته در مورد اسناد سابق، نمی تواند بزرگتر از اولین شماره ترتیب دفترالکترونیک باشد. ";
                return (null, messagesRef);
            }

            return (document, messagesRef);
        }
    }
}
