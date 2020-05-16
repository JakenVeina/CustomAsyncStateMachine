using System.Text.Json.Serialization;

namespace CusomtAsyncStateMachine
{
    public class UserDTO
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }
            = null!;

        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
            = null!;

        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
            = null!;

        [JsonPropertyName("avatar")]
        public string Avatar { get; set; }
            = null!;
    }
}
