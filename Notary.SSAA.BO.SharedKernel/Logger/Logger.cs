using Serilog;
using System.Net;
using System.Runtime.CompilerServices;
public static class Logger
{
    private static readonly Serilog.ILogger _logger = Log.Logger;

    public static void Error(Exception ex,[CallerLineNumber] int? line = 0, [CallerMemberName] string? caller = null)
    {
        _logger.Error($" ErrorMessage: {ex.ToString()}, Line : {line}, CallerMemberName : {caller}");
    }



}