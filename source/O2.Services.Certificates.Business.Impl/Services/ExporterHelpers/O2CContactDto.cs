using System.Runtime.Serialization;
using Newtonsoft.Json;

namespace O2.Services.Certificates.Business.Impl.Services.ExporterHelpers
{
    [DataContract]
    public class O2CContactDto
    {
        [DataMember(Name="key")] 
        [JsonProperty(PropertyName = "key")]
        public string Key { get; set; }

        [DataMember(Name="value")]
        [JsonProperty(PropertyName = "value")]
        public string Value { get; set; }
    }
}