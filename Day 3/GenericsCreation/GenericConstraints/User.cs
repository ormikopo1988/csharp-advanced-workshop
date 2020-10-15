using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace GenericConstraints
{
    [DataContract]
    public class User
    {
        [DataMember]
        [JsonProperty("id")]
        public string Id { get; set; }

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}