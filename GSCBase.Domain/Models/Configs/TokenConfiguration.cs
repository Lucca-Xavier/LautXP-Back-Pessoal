namespace GSCBase.Domain.Models.Configs
{
    public class TokenConfiguration
    {
        public string Audience { get; set; }
        public string Issuer { get; set; }
        public int Seconds { get; set; }
        public string SecretKey { get; set; }
        public int FinalExpiration { get; set; }
    }
}