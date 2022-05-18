using System.Text.Json.Serialization;

namespace FCCodingChallenge.API.Data
{
    public class GenericResponse<T>
    {
        public bool IsSuccessful { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
        public T Data { get; set; }
        [JsonIgnore]
        public string Caller { get; set; }
    }
}
