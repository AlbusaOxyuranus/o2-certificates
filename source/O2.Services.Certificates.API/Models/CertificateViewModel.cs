namespace O2.Services.Certificates.API.Models
{
    public class CertificateViewModel
    {
        private string _name;
        private long _id;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public long Id
        {
            get { return _id; }
            set { _id = value; }
        }
    }
}