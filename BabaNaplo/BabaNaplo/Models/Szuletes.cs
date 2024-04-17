using System.Text.Json.Serialization;

namespace BabaNaplo.Models;

public partial class Szuletes
{
    public int BabaId { get; set; }

    public int FelhasznaloId { get; set; }

    public string Nev { get; set; } = null!;

    public DateTime Idopont { get; set; }

    public string Hely { get; set; } = null!;

    public int Suly { get; set; }

    public double Hossz { get; set; }

    public string? Hajszin { get; set; }

    public string? Szemszin { get; set; }

    public string? Vercsoport { get; set; }

    public string? Csillagjegy { get; set; }

    public string? Szuletestort { get; set; }

    public byte[]? Babafoto { get; set; }

    [JsonIgnore]
    public virtual ICollection<Esemenyek>? Esemenyeks { get; set; } = new List<Esemenyek>();
    [JsonIgnore]
    public virtual ICollection<Kedvencek>? Kedvenceks { get; set; } = new List<Kedvencek>();
    [JsonIgnore]
    public virtual ICollection<Novekedes>? Novekedess { get; set; } = new List<Novekedes>();
}
