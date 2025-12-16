
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notary.SSAA.BO.SharedKernel.Contracts.Coordinator.Document
{
    public class AnnotationPack
    {
        /// <summary>
        /// Gets or sets the RelatedDocNationalNo
        /// </summary>
        public string RelatedDocNationalNo { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocObjectID
        /// </summary>
        public string RelatedDocObjectID { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocDate
        /// </summary>
        public string RelatedDocDate { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocLastModifyDateTime
        /// </summary>
        public string RelatedDocLastModifyDateTime { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocBriefDescription
        /// </summary>
        public string RelatedDocBriefDescription { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocTitle
        /// </summary>
        public string RelatedDocTitle { get; set; }

        /// <summary>
        /// Gets or sets the RelatedDocContext
        /// </summary>
        public string RelatedDocContext { get; set; }

        /// <summary>
        /// Gets or sets the EditFormURI
        /// </summary>
        public string EditFormURI { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether IsLocalDocument
        /// </summary>
        public bool IsLocalDocument { get; set; }
    }

}
