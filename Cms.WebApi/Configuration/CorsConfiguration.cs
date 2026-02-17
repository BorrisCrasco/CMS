namespace Cms.WebApi.Configuration
{
    public class CorsConfiguration
    {
        public string PolicyName { get; set; }
        public bool CorsEnabled { get; set; }
        public string[] AllowedOrigins { get; set; }
    }
}
