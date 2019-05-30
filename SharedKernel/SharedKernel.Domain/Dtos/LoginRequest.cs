namespace SharedKernel.Domain.Dtos
{
    public class LoginRequest
    {
        public string Login              { get; set; }
        public string Password           { get; set; }
        public string CelularNumeroSerie { get; set; }
        public double Latitude           { get; set; }
        public double Longitude          { get; set; }
    }
}