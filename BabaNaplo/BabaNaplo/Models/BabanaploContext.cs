using Microsoft.EntityFrameworkCore;

namespace BabaNaplo.Models;

public partial class BabanaploContext : DbContext
{
    public BabanaploContext()
    {

    }

    public BabanaploContext(DbContextOptions<BabanaploContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Esemenyek> Esemenyeks { get; set; }

    public virtual DbSet<Kedvencek> Kedvenceks { get; set; }

    public virtual DbSet<Novekedes> Novekedes { get; set; }

    public virtual DbSet<Szuletes> Szuletes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=babanaplo;user=root;sslmode=none", Microsoft.EntityFrameworkCore.ServerVersion.Parse("10.4.20-mariadb"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_hungarian_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Esemenyek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity
                .ToTable("esemenyek")
                .HasCharSet("utf8")
                .UseCollation("utf8_hungarian_ci");

            entity.HasIndex(e => e.BabaId, "BabaId");

            
            entity.Property(e => e.Tortenet).HasColumnType("text");
            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.BabaId).HasColumnType("int(11)");
            entity.Property(e => e.Datum).HasColumnType("date");
            entity.Property(e => e.Megnevezes).HasMaxLength(64);

            entity.HasOne(d => d.Baba).WithMany(p => p.Esemenyeks)
                .HasForeignKey(d => d.BabaId)
                .HasConstraintName("esemenyek_ibfk_2");
        });

        modelBuilder.Entity<Kedvencek>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("kedvencek");

            entity.HasIndex(e => e.BabaId, "babaid");

            entity.Property(e => e.Id).HasColumnType("int(11)");
            entity.Property(e => e.BabaId).HasColumnType("int(11)");
            entity.Property(e => e.Etel).HasColumnType("text");
            entity.Property(e => e.Ital).HasColumnType("text");
            entity.Property(e => e.Jatek).HasColumnType("text");
            entity.Property(e => e.Mese).HasColumnType("text");
            entity.Property(e => e.Mondoka).HasColumnType("text");

            entity.HasOne(d => d.Baba).WithMany(p => p.Kedvenceks)
                .HasForeignKey(d => d.BabaId)
                .HasConstraintName("kedvencek_ibfk_1");
        });

        modelBuilder.Entity<Novekedes>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("novekedes");

            entity.HasIndex(e => e.BabaId, "BabaId");

            entity.Property(e => e.Id).HasColumnType("int(32)");
            entity.Property(e => e.BabaId).HasColumnType("int(16)");
            entity.Property(e => e.Datum).HasColumnType("date");

            entity.HasOne(d => d.Baba).WithMany(p => p.Novekedess)
                .HasForeignKey(d => d.BabaId)
                .HasConstraintName("novekedes_ibfk_1");
        });

        modelBuilder.Entity<Szuletes>(entity =>
        {
            entity.HasKey(e => e.BabaId).HasName("PRIMARY");

            entity.ToTable("szuletes");

            entity.Property(e => e.BabaId).HasColumnType("int(64)");
            entity.Property(e => e.Csillagjegy).HasMaxLength(32);
            entity.Property(e => e.FelhasznaloId).HasColumnType("int(64)");
            entity.Property(e => e.Hajszin).HasMaxLength(64);
            entity.Property(e => e.Hely).HasMaxLength(64);
            entity.Property(e => e.Idopont).HasColumnType("datetime");
            entity.Property(e => e.Nev).HasMaxLength(32);
            entity.Property(e => e.Suly).HasColumnType("int(4)");
            entity.Property(e => e.Szemszin).HasMaxLength(64);
            entity.Property(e => e.Vercsoport).HasMaxLength(10);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
