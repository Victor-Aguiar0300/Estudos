using System.Text.Json.Serialization;

namespace Estudos.Models
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Rpgclass
    {
        Knight = 1,

        Mage = 2,

        Cleric = 3
    }
}
