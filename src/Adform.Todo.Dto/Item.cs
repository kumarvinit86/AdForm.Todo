using System.Text.Json.Serialization;

namespace Adform.Todo.Dto
{
    public class Item 
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LabelName { get; set; }
        [JsonIgnore]
        public int UserId { get; set; }
    }
}
