using System.Text.Json.Serialization;

namespace BabaNaplo.Models;

public class Kedvencek
{
    public int Id { get; set; }

    public int BabaId { get; set; }

    public string? Ital { get; set; }

    public string? Jatek { get; set; }

    public string? Mese { get; set; }

    public string? Mondoka { get; set; }

    public string? Etel { get; set; }
    [JsonIgnore]
    public virtual Szuletes? Baba { get; set; } = null!;
}
