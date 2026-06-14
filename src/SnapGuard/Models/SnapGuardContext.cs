using Microsoft.EntityFrameworkCore;

namespace SnapGuard.Models;

public class SnapGuardContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<CameraModel> CameraModels { get; set; }

    public DbSet<Hub> Hubs { get; set; }

    public DbSet<MotionEvent> MotionEvents { get; set; }

    public DbSet<Picture> Pictures { get; set; }

    public DbSet<Station> Stations { get; set; }

    public DbSet<StationEvent> StationEvents { get; set; }

    public DbSet<StationEventType> StationEventTypes { get; set; }

    public DbSet<StationToken> StationTokens { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public DbSet<UserToken> UserTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<StationToken>(entity =>
        {
            entity.ToTable("station_tokens")
                .HasKey(e => e.TokenId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.TokenId)
                .HasColumnType("int(11)")
                .HasColumnName("token_id");
            entity.Property(e => e.StationId)
                .HasColumnType("int(11)")
                .HasColumnName("station_id");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .HasColumnName("token");
            entity.Property(e => e.ExpiresOn)
                .HasColumnType("datetime")
                .HasColumnName("expires_on");

            entity.HasOne(d => d.Station).WithMany(p => p.StationTokens)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("station_tokens_ibfk_1");
        });

        modelBuilder.Entity<CameraModel>(entity =>
        {
            entity.ToTable("camera_models")
                .HasKey(e => e.ModelCode)
                .HasName("PRIMARY");

            entity.Property(e => e.ModelCode)
                .HasMaxLength(8)
                .HasColumnName("model_code");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
            entity.Property(e => e.SupportsJpeg)
                .HasColumnType("tinyint(1)")
                .HasColumnName("supports_jpeg");

            entity.HasMany(d => d.Stations).WithOne(p => p.CameraModel)
                .HasForeignKey(p => p.CameraModelCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("camera_models_ibfk_1");
        });

        modelBuilder.Entity<Picture>(entity =>
        {
            entity.ToTable("pictures")
                .HasKey(p => p.PictureId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);
            entity.HasIndex(e => e.Format);
            entity.HasIndex(e => e.Resolution);

            entity.Property(e => e.PictureId)
                .HasColumnType("int(11)")
                .HasColumnName("picture_id");
            entity.Property(e => e.StationId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("int(11)")
                .HasColumnName("station_id");
            entity.Property(e => e.FileName)
                .HasMaxLength(48)
                .HasColumnName("file_name");
            entity.Property(e => e.Format)
                .HasColumnType("ENUM('RGB565','YUV422','YUV420','GRAYSCALE','JPEG','RGB888','RAW','RGB444','RGB555')")
                .HasColumnName("format")
                .HasConversion<string>();
            entity.Property(e => e.Resolution)
                .HasColumnType("ENUM('FS_96X96','FS_QQVGA','FS_128X128','FS_QCIF','FS_HQVGA','FS_240X240','FS_QVGA','FS_320X320','FS_CIF','FS_HVGA','FS_VGA','FS_SVGA','FS_XGA','FS_HD','FS_SXGA','FS_UXGA','FS_FHD','FS_P_HD','FS_P_3MP','FS_QXGA','FS_QHD','FS_WQXGA','FS_P_FHD','FS_QSXGA','FS_5MP','FS_INVALID')")
                .HasColumnName("resolution")
                .HasConversion<string>();
            entity.Property(e => e.MotionEventId)
                .HasDefaultValueSql("NULL")
                .HasColumnType("int(11)")
                .HasColumnName("motion_event_id");
            entity.Property(e => e.UploadedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("uploaded_at")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.MotionEvent).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.MotionEventId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("pictures_ibfk_1");

            entity.HasOne(d => d.Station).WithMany(p => p.Pictures)
                .HasForeignKey(d => d.StationId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("pictures_ibfk_2");
        });

        modelBuilder.Entity<StationEventType>(entity =>
        {
            entity.ToTable("station_event_types")
                .HasKey(e => e.TypeCode)
                .HasName("PRIMARY");

            entity.Property(e => e.TypeCode)
                .HasMaxLength(8)
                .HasColumnName("type_code");
            entity.Property(e => e.Description)
                .HasMaxLength(32)
                .HasColumnName("description");
            entity.Property(e => e.Severity)
                .HasColumnType("ENUM('INFO','WARNING','ERROR')")
                .HasColumnName("severity")
                .HasConversion<string>();

            entity.HasMany(d => d.StationEvents).WithOne(p => p.Type)
                .HasForeignKey(p => p.TypeCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("station_event_types_ibfk_1");
        });

        modelBuilder.Entity<StationEvent>(entity =>
        {
            entity.ToTable("station_events")
                .HasKey(e => e.EventId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);
            entity.HasIndex(e => e.TypeCode);

            entity.Property(e => e.EventId)
                .HasColumnType("int(11)")
                .HasColumnName("event_id");
            entity.Property(e => e.StationId)
                .HasColumnType("int(11)")
                .HasColumnName("station_id");
            entity.Property(e => e.TypeCode)
                .HasMaxLength(8)
                .HasColumnName("type_code");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("registered_at")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.Station).WithMany(p => p.Events)
                .HasForeignKey(p => p.EventId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("stations_ibfk_1");
        });

        modelBuilder.Entity<MotionEvent>(entity =>
        {
            entity.ToTable("motion_events")
                .HasKey(e => e.MotionEventId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.StationId);

            entity.Property(e => e.MotionEventId)
                .HasColumnType("int(11)")
                .HasColumnName("motion_event_id");
            entity.Property(e => e.StationId)
                .HasColumnType("int(11)")
                .HasColumnName("station_id");
            entity.Property(e => e.StartTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("start_timestamp");
            entity.Property(e => e.EndTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("end_timestamp");
            entity.Property(e => e.TriggerCount)
                .HasColumnType("int(11)")
                .HasColumnName("trigger_count");

            entity.HasOne(d => d.Station).WithMany(p => p.MotionEvents)
                .HasForeignKey(p => p.StationId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("motion_events_ibfk_1");
        });

        modelBuilder.Entity<Hub>(entity =>
        {
            entity.ToTable("hubs")
                .HasKey(e => e.HubId)
                .HasName("PRIMARY");

            entity.Property(e => e.HubId)
                .HasColumnType("int(11)")
                .HasColumnName("hub_id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("registered_at")
                .ValueGeneratedOnAdd();

            entity.HasMany(p => p.Stations).WithOne(d => d.Hub)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.HubId)
                .HasConstraintName("hubs_ibfk_1");

            entity.HasMany(p => p.Users).WithOne(d => d.Hub)
                .OnDelete(DeleteBehavior.Cascade)
                .HasForeignKey(p => p.HubId)
                .HasConstraintName("hubs_ibfk_2");
        });

        modelBuilder.Entity<Station>(entity =>
        {
            entity.ToTable("stations")
                .HasKey(e => e.StationId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.HubId);
            entity.HasIndex(e => e.CameraModelCode);

            entity.Property(e => e.StationId)
                .HasColumnType("int(11)")
                .HasColumnName("station_id");
            entity.Property(e => e.HubId)
                .HasColumnType("int(11)")
                .HasColumnName("hub_id");
            entity.Property(e => e.Name)
                .HasMaxLength(40)
                .HasColumnName("name");
            entity.Property(e => e.MacAddress)
                .HasMaxLength(20)
                .HasColumnName("mac_address");
            entity.Property(e => e.IsSolarPowered)
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("0")
                .HasColumnName("is_solar_powered");
            entity.Property(e => e.IsBatteryPowered)
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("0")
                .HasColumnName("is_battery_powered");
            entity.Property(e => e.HasCameraFlash)
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("0")
                .HasColumnName("has_camera_flash");
            entity.Property(e => e.HasPanTiltControl)
                .HasColumnType("tinyint(1)")
                .HasDefaultValueSql("0")
                .HasColumnName("has_pan_tilt_control");
            entity.Property(e => e.HasNightVision)
                .HasColumnType("tinyint(1)")
                .HasColumnName("has_night_vision");
            entity.Property(e => e.Version)
                .HasMaxLength(12)
                .HasColumnName("version");
            entity.Property(e => e.CoreVersion)
                .HasMaxLength(12)
                .HasColumnName("core_version");
            entity.Property(e => e.CameraModelCode)
                .HasMaxLength(8)
                .HasColumnName("camera_model_code");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("registered_at")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NULL")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at")
                .ValueGeneratedOnUpdate();
        });

        modelBuilder.Entity<UserRole>(entity =>
        {
            entity.ToTable("user_roles")
                .HasKey(e => e.RoleCode)
                .HasName("PRIMARY");

            entity.Property(e => e.RoleCode)
                .HasMaxLength(8)
                .HasColumnName("role_code");
            entity.Property(e => e.Name)
                .HasMaxLength(16)
                .HasColumnName("name");
            entity.Property(e => e.CanModifyHub)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_modify_hub");
            entity.Property(e => e.CanQueryLogs)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_query_logs");
            entity.Property(e => e.CanDeleteLogs)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_delete_logs");
            entity.Property(e => e.CanMakeRequests)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_make_requests");
            entity.Property(e => e.CanCreateUsers)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_create_users");
            entity.Property(e => e.CanDeleteUsers)
                .HasDefaultValueSql("0")
                .HasColumnType("tinyint(1)")
                .HasColumnName("can_delete_users");

            entity.HasMany(d => d.Users).WithOne(p => p.Role)
                .HasForeignKey(p => p.RoleCode)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("user_roles_ibfk_1");
        });

        modelBuilder.Entity<UserToken>(entity =>
        {
            entity.ToTable("user_tokens")
                .HasKey(e => e.TokenId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.UserId);

            entity.Property(e => e.TokenId)
                .HasColumnType("int(11)")
                .HasColumnName("token_id");
            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.Token)
                .HasMaxLength(200)
                .HasColumnName("token");
            entity.Property(e => e.ExpiresOn)
                .HasColumnType("datetime")
                .HasColumnName("expires_on");
            entity.Property(e => e.UserAgent)
                .HasColumnType("longtext")
                .HasColumnName("user_agent");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("created_at")
                .ValueGeneratedOnAdd();

            entity.HasOne(d => d.User).WithMany(p => p.UserTokens)
                .HasForeignKey(p => p.TokenId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("user_tokens_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("users")
                .HasKey(u => u.UserId)
                .HasName("PRIMARY");

            entity.HasIndex(e => e.HubId);
            entity.HasIndex(e => e.RoleCode);

            entity.Property(e => e.UserId)
                .HasColumnType("int(11)")
                .HasColumnName("user_id");
            entity.Property(e => e.UserName)
                .HasMaxLength(16)
                .HasColumnName("user_name");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(32)
                .HasColumnName("display_name");
            entity.Property(e => e.Password)
                .HasMaxLength(64)
                .HasColumnName("password");
            entity.Property(e => e.HubId)
                .HasColumnType("int(11)")
                .HasColumnName("hub_id");
            entity.Property(e => e.RoleCode)
                .HasMaxLength(8)
                .HasColumnName("role_code");
            entity.Property(e => e.RegisteredAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("timestamp")
                .HasColumnName("registered_at")
                .ValueGeneratedOnAdd();
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("NULL")
                .HasColumnType("timestamp")
                .HasColumnName("updated_at")
                .ValueGeneratedOnUpdate();
        });
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseAsyncSeeding(async (context, _, cancellationToken) =>
        {
            int modelCount = await context.Set<CameraModel>()
                .CountAsync(cancellationToken);

            if (modelCount == 0)
            {
                await context.Set<CameraModel>().AddRangeAsync([

                    new ()
                    {
                        ModelCode = "ov9650",
                        Name = "OV9650",
                        SupportsJpeg = false,
                    },
                    new ()
                    {
                        ModelCode = "ov7725",
                        Name = "OV7725",
                        SupportsJpeg = false,
                    },
                    new ()
                    {
                        ModelCode = "ov2640",
                        Name = "OV2640",
                        SupportsJpeg = true,
                    },
                    new ()
                    {
                        ModelCode = "ov5640",
                        Name = "OV5640",
                        SupportsJpeg = true,
                    }
                ], cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }

            int typeCount = await context.Set<StationEventType>()
                .CountAsync(cancellationToken);

            if (typeCount == 0)
            {
                await context.Set<StationEventType>().AddRangeAsync([
                    new ()
                    {
                        TypeCode = "rst_unkn",
                        Description = "Reset due to unknown reasons",
                        Severity = Enums.StationEventSeverity.WARNING
                    },
                    new ()
                    {
                        TypeCode = "rst_manl",
                        Description = "Reset requested",
                        Severity = Enums.StationEventSeverity.WARNING
                    },
                    new ()
                    {
                        TypeCode = "rst_excp",
                        Description = "Reset due to exception",
                        Severity = Enums.StationEventSeverity.ERROR
                    },
                    new ()
                    {
                        TypeCode = "rst_powr",
                        Description = "Reset due to power glitch",
                        Severity = Enums.StationEventSeverity.ERROR
                    },
                    new ()
                    {
                        TypeCode = "wl_desc",
                        Description = "Disconnected from Wi-Fi",
                        Severity = Enums.StationEventSeverity.ERROR
                    },
                    new ()
                    {
                        TypeCode = "wl_auth",
                        Description = "Wi-Fi Authentication failed",
                        Severity = Enums.StationEventSeverity.ERROR
                    },
                    new ()
                    {
                        TypeCode = "wl_conn",
                        Description = "Wi-Fi Connection failed",
                        Severity = Enums.StationEventSeverity.ERROR
                    },
                ], cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }

            int roleCount = await context.Set<UserRole>()
                .CountAsync(cancellationToken);

            if (roleCount == 0)
            {
                await context.Set<UserRole>().AddRangeAsync([
                    new ()
                    {
                        RoleCode = "admin",
                        Name = "Admistrator",
                        CanModifyHub = true,
                        CanQueryLogs = true,
                        CanDeleteLogs = true,
                        CanMakeRequests = true,
                        CanCreateUsers = true,
                        CanDeleteUsers = true
                    },
                    new()
                    {
                        RoleCode = "user",
                        Name = "User",
                        CanModifyHub = false,
                        CanQueryLogs = true,
                        CanDeleteLogs = false,
                        CanMakeRequests = true,
                        CanCreateUsers = false,
                        CanDeleteUsers = false
                    },
                    new()
                    {
                        RoleCode = "guest",
                        Name = "Guest",
                        CanModifyHub = false,
                        CanQueryLogs = false,
                        CanDeleteLogs = false,
                        CanMakeRequests = false,
                        CanCreateUsers = false,
                        CanDeleteUsers = false
                    }
                ], cancellationToken);

                await context.SaveChangesAsync(cancellationToken);
            }
        });
    }
}
