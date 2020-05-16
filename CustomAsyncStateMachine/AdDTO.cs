using System.Text.Json.Serialization;

namespace CusomtAsyncStateMachine
{
    public class AdDTO
    {
        [JsonPropertyName("company")]
        public string Company { get; set; }
            = null!;

        [JsonPropertyName("url")]
        public string Url { get; set; }
            = null!;

        [JsonPropertyName("text")]
        public string Text { get; set; }
            = null!;
    }
}
