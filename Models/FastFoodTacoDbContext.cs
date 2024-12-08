using Microsoft.EntityFrameworkCore;

namespace TacoFastFoodAPI.Models;

public partial class FastFoodTacoDbContext : DbContext
{
    public FastFoodTacoDbContext()
    {
    }

    public FastFoodTacoDbContext(DbContextOptions<FastFoodTacoDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Combo> Combos { get; set; }

    public virtual DbSet<Drink> Drinks { get; set; }

    public virtual DbSet<Taco> Tacos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=SWIZZ\\SQLEXPRESS;Initial Catalog=FastFoodTacoDB;Integrated Security=SSPI;Encrypt=false;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Combo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Combo__3214EC27ECA8D20C");

            entity.ToTable("Combo");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);

            entity.HasOne(d => d.Drink).WithMany()
                .HasForeignKey(d => d.DrinkId)
                .HasConstraintName("FK__Combo__DrinkId__5FB337D6");

            entity.HasOne(d => d.Taco).WithMany()
                .HasForeignKey(d => d.TacoId)
                .HasConstraintName("FK__Combo__TacoId__5EBF139D");
        });

        modelBuilder.Entity<Drink>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Drink__3214EC2792DFA77B");

            entity.ToTable("Drink");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Taco>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Taco__3214EC27E5C9C7FE");

            entity.ToTable("Taco");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
