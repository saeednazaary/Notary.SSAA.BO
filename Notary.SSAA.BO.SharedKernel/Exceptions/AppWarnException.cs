using System.ComponentModel;

namespace Notary.SSAA.BO.SharedKernel.Exceptions;

public class AppWarnException : WarningException
{
    public AppWarnException(string message) : base(message)
    {
    }
}