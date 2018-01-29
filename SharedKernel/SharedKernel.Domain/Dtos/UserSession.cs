namespace SharedKernel.Domain.Dtos
{
    public static class UserSession
    {
        public static string    AppToken    { get; set; }
        public static string    Token       { get; set; }
        public static string    UrlBaseApi  { get; set; }
        public static bool      IsLogged => !string.IsNullOrWhiteSpace(Token);
    }
}
