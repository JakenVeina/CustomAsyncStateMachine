using System.Text.Json.Serialization;

namespace CusomtAsyncStateMachine
{
    public class UsersDTO
    {
        [JsonPropertyName("page")]
        public int Page { get; set; }

        [JsonPropertyName("per_page")]
        public int PerPage { get; set; }

        [JsonPropertyName("total")]
        public int Total { get; set; }

        [JsonPropertyName("total_pages")]
        public int TotalPages { get; set; }

        [JsonPropertyName("data")]
        public UserDTO[] Data { get; set; }
            = null!;

        [JsonPropertyName("ad")]
        public AdDTO Ad { get; set; }
            = null!;
    }
}
