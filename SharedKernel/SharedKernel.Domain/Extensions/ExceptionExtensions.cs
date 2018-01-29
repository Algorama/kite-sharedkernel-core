using System;

namespace SharedKernel.Domain.Extensions
{
    public static class ExceptionExtensions
    {
        public static string BuildExceptionMessage(this Exception exception)
        {
            if (exception == null) return null;

            var msg = "";
            while (true)
            {
                msg = msg + exception.Message + Environment.NewLine;
                if (exception.InnerException == null) return msg;
                exception = exception.InnerException;
            }
        }
    }
}
