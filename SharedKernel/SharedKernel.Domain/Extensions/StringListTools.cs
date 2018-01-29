using System.Collections.Generic;

namespace SharedKernel.Domain.Extensions
{
    public static class StringListTools
    {
        public static string ToMessageBoxString(this List<string> lista)
        {
            var msg = string.Empty;
            foreach (var m in lista)
            {
                msg += m + "<br/>";
            }
            return msg.Trim();
        }
    }
}
