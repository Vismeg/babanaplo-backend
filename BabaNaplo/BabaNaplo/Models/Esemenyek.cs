using System.Text.Json.Serialization;

namespace BabaNaplo.Models;

public partial class Esemenyek
{
    public int Id { get; set; }

    public int BabaId { get; set; }

    public string Megnevezes { get; set; } = null!;

    public bool Elsoalkalom { get; set; }

    public byte[]? Kep { get; set; }

    public string? Tortenet { get; set; }

    public DateOnly Datum { get; set; }
    [JsonIgnore]
    public virtual Szuletes? Baba { get; set; } = null!;
}
