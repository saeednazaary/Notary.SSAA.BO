namespace Notary.SSAA.BO.Domain.RichDomain.Document
{
    /// <summary>
    /// Defines the <see cref="RichDocumentCase" />
    /// </summary>
    public static class RichDocumentCase
    {
        /// <summary>
        /// The RegCaseText
        /// </summary>
        /// <param name="documentCase">The documentCase<see cref="Domain.Entities.DocumentCase"/></param>
        /// <returns>The <see cref="string"/></returns>
        public static string RegCaseText ( this Domain.Entities.DocumentCase documentCase )
        {
            if ( documentCase != null )
            {
                return documentCase.Title;

            }
            return null;
        }
    }
}
