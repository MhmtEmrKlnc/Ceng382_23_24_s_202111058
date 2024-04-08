using System.Text.Json.Serialization;

public class Room
{
    [JsonPropertyName("roomId")]
    public string? roomId { get; set; }

    [JsonPropertyName("roomName")]
    public string? roomName { get; set; }

    [JsonPropertyName("capacity")]
    public int? capacity { get; set; }


}