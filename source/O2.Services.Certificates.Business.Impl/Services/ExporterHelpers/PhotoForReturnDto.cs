using Newtonsoft.Json;

namespace O2.Services.Certificates.Business.Impl.Services.ExporterHelpers
{
    public class PhotoForReturnDto
    {
        [JsonProperty(PropertyName = "url")]
        public string Url { get; set; }

        [JsonProperty(PropertyName = "dateAdded")]
        public long DateAdded { get; set; }

        [JsonProperty(PropertyName = "modifiedDate")]
        public long ModifiedDate { get; set; }
        
        [JsonProperty(PropertyName = "IsMain")]
        public bool IsMain { get; set; }
    }
}