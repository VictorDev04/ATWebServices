namespace ATCUDVALIDATOR.API.Classes
{
    public class AppSettings
    {
        public string PublicKeyPath { get; set; }

        public string CertificatePath { get; set; }

        public string CertificatePassword { get; set; }

        public string EFaturaUser { get; set; }

        public string EFaturaPassword { get; set; }

        public WebServiceUrls WebServiceUrl { get; set; } = new ();

        public class WebServiceUrls
        {
            public string Series { get; set; }
        }
    }
}
