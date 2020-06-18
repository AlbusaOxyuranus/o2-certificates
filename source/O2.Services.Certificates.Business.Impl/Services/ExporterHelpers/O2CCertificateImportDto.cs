using Newtonsoft.Json;

namespace O2.Services.Certificates.Business.Impl.Services.ExporterHelpers
{
    public class ImportDto
    {
        [JsonProperty(PropertyName = "cleanData")]
        public bool CleanData { get; set; } = false;
    }
    public class O2CCertificateImportDto : ImportDto
    {
    }
}