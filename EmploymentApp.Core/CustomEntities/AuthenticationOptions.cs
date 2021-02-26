namespace EmploymentApp.Core.CustomEntities
{
    public class AuthenticationOptions
    {
        public string SecretKey { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int Minutes { get; set; }
    }
}
