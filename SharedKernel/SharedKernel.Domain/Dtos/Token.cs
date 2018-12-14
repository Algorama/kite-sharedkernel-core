using System;

namespace SharedKernel.Domain.Dtos
{
    public class Token
    {
        public long     UserId       { get; set; }
        public string   UserName     { get; set; }
        public string   Login        { get; set; }
        public DateTime ExpirateAt   { get; set; }
    }
}