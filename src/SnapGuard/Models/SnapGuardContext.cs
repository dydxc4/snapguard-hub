using Microsoft.EntityFrameworkCore;
using SnapGuard.Enums;
//using SnapGuard.Extensions;

namespace SnapGuard.Models;

public class SnapGuardContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }

    public DbSet<HubUser> HubUsers { get; set; }

    public DbSet<Hub> Hubs { get; set; }

    public DbSet<StationModel> StationModels { get; set; }

    public DbSet<Station> Stations { get; set; }

    public DbSet<StationEvent> StationEvents { get; set; }

    public DbSet<MotionEvent> MotionEvents { get; set; }

    public DbSet<Picture> Pictures { get; set; }

    public DbSet<StationToken> StationTokens { get; set; }

    public DbSet<OutstandingToken> OutstandingTokens { get; set; }

    public DbSet<UserNotification> Notifications { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StationToken>(entity =>
        {
            entity.ToTable("StationTokens")
                .HasKey(e => e.TokenId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.Token)
                .HasMaxLength(200);
            entity.Property(e => e.IsBlocked)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.Station)
                .WithMany(p => p.StationTokens)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.ToTable("Pictures")
                .HasKey(p => p.PictureId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.FileName)
                .HasMaxLength(48);
            entity.Property(e => e.Format)
                .HasConversion<string>()
                .HasColumnType("ENUM('RGB565','YUV422','YUV420','GRAYSCALE','JPEG','RGB888','RAW','RGB444','RGB555')");
            entity.Property(e => e.Resolution)
                .HasConversion<string>()
                .HasColumnType("ENUM('R_96X96','R_QQVGA','R_128X128','R_QCIF','R_HQVGA','R_240X240','R_QVGA','R_320X320','R_CIF','R_HVGA','R_VGA','R_SVGA','R_XGA','R_HD','R_SXGA','R_UXGA','R_FHD','R_P_HD','R_P_3MP','R_QXGA','R_QHD','R_WQXGA','R_P_FHD','R_QSXGA','R_5MP','R_INVALID')");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.MotionEvent)
                .WithMany(p => p.Pictures)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.Station)
                .WithMany(p => p.Pictures)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<StationEvent>(entity =>
        {
            entity.ToTable("StationEvents")
                .HasKey(e => e.EventId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasColumnType("ENUM('SYSTEM_RESTARTED','SYSTEM_WOKE_UP','SYSTEM_FAILED','BROWNOUT_DETECTED','WIFI_RECONNECTED','WIFI_AUTH_FAILED','CAMERA_FAILED','TAMPERING_DETECTED')");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Station)
                .WithMany(p => p.Events)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<MotionEvent>(entity =>
        {
            entity.ToTable("MotionEvents")
                .HasKey(e => e.MotionEventId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.StartedAt)
                .HasColumnType("DATETIME");
            entity.Property(e => e.EndedAt)
                .HasColumnType("DATETIME");

            entity.HasOne(d => d.Station)
                .WithMany(p => p.MotionEvents)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Hub>(entity =>
        {
            entity.ToTable("Hubs")
                .HasKey(e => e.HubId)
                .HasName("PRIMARY");

            entity.Property(e => e.Name)
                .HasMaxLength(40);
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasMany(p => p.Stations).WithOne(d => d.Hub)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<StationModel>(entity =>
        {
            entity.ToTable("StationModels")
                .HasKey(e => e.StationModelId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.Name)
                .IsUnique();

            entity.Property(e => e.IsSolarPowered)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.IsBatteryPowered)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.HasCameraFlash)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.HasPanTiltControl)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.HasNightVision)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.CameraModel)
                .HasConversion<string>()
                .HasColumnType("ENUM('OV2640','OV3660','OV5640','OV7725','OV9650')");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasMany(p => p.Stations).WithOne(d => d.StationModel)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.ToTable("Stations")
                .HasKey(e => e.StationId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.HubId);
            entity.HasIndex(e => e.StationModelId);

            entity.Property(e => e.Label)
                .HasMaxLength(40);
            entity.Property(e => e.MacAddress)
                .HasMaxLength(20);
            entity.Property(e => e.Version)
                .HasMaxLength(12);
            entity.Property(e => e.CoreVersion)
                .HasMaxLength(12);
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<OutstandingToken>(entity =>
        {
            entity.ToTable("OutstandingTokens")
                .HasKey(e => e.TokenId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.UserId);

            entity.Property(e => e.Token)
                .HasColumnType("LONGTEXT");
            entity.Property(e => e.CreateAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.ExpiresAt)
                .HasColumnType("DATETIME");
            entity.Property(e => e.BlockedAt)
                .HasColumnType("DATETIME");
            entity.Property(e => e.Jti)
                .HasMaxLength(255);
            entity.HasOne(d => d.User)
                .WithMany(p => p.OutstandingTokens)
                .OnDelete(DeleteBehavior.SetNull);
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Users")
                .HasKey(u => u.UserId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.UserName)
                .IsUnique();
            entity.HasIndex(e => e.Email)
                .IsUnique();
            entity.HasIndex(e => e.DisplayName);

            entity.Property(e => e.Email)
                .HasMaxLength(254);
            entity.Property(e => e.UserName)
                .HasMaxLength(32);
            entity.Property(e => e.DisplayName)
                .HasMaxLength(64);
            entity.Property(e => e.Password)
                .HasMaxLength(128);
            entity.Property(e => e.IsActive)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.IsStaff)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.RegisteredAt)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NULL ON UPDATE CURRENT_TIMESTAMP")
                .HasColumnType("TIMESTAMP")
                .ValueGeneratedOnUpdate();
            entity.Property(e => e.LastLoggedAt)
                .HasColumnType("TIMESTAMP");

            entity.HasMany(e => e.Hubs)
                .WithMany(e => e.Users)
                .UsingEntity<HubUser>(
                    r => r.HasOne(e => e.Hub)
                        .WithMany(e => e.HubUsers)
                        .OnDelete(DeleteBehavior.Cascade),
                    l => l.HasOne(e => e.User)
                        .WithMany(e => e.HubUsers)
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.ToTable("HubUsers");
                        j.Property(e => e.JoinedAt)
                            .HasColumnType("TIMESTAMP")
                            .HasDefaultValueSql("CURRENT_TIMESTAMP")
                            .ValueGeneratedOnAdd();
                        j.Property(e => e.Role)
                            .HasConversion<string>()
                            .HasColumnType("ENUM('OWNER','EDITOR','GUEST')");
                    }
                );
        });

        modelBuilder.Entity<UserNotification>(entity =>
        {
            entity.ToTable("UserNotifications")
                .HasKey(e => e.NotificationId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.UserId);

            entity.Property(e => e.Title)
                .HasMaxLength(64);
            entity.Property(e => e.Content)
                .HasMaxLength(256);
            entity.Property(e => e.Type)
                .HasConversion<string>()
                .HasColumnType("ENUM('SYSTEM','ACCOUNT','ALERT','REMINDER','HUB','STATION','MOTION','CAMERA','OTHER')");
            entity.Property(e => e.IsRead)
                .HasColumnType("TINYINT(1)");
            entity.Property(e => e.ReceivedAt)
                .HasColumnType("TIMESTAMP")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.User)
                .WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {

        });
    }
}
