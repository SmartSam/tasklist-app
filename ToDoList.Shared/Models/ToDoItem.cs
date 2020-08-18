using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ToDoList.Shared.Models
{
    public class ToDoItem
    {
        [Key]
        [JsonPropertyName("id")]
        public long Id { get; set; }

        [Required]
        [JsonPropertyName("title")]
        public string Title { get; set; }

        [Required]
        [JsonPropertyName("status")]
        public string Status { get; set; }
        
        //TODO: Link with Users table
        //public int UserId { get; set; }
    }
}
