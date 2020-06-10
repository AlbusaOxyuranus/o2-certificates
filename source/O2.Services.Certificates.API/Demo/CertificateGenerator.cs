namespace O2.Services.Certificates.API.Demo
{
    public class CertificateGenerator:ICertificateGenerator
    {
        private long _currentId = 1;
        public long Next()
        {
            return ++_currentId;
        }
    }
}