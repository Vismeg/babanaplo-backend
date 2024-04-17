using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace BabaNaplo.Models;

public partial class Novekedes
{
    public int Id { get; set; }

    public int BabaId { get; set; }

    public DateOnly Datum {  get; set; }

    public float? Suly { get; set; }

    public float? Magassag { get; set; }

    public byte[]? Kep { get; set; }


    [JsonIgnore] 
    public virtual Szuletes? Baba { get; set; } = null!;
}
