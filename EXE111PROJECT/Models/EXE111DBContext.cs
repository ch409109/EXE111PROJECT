using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace EXE111PROJECT.Models
{
    public partial class EXE111DBContext : DbContext
    {
        public EXE111DBContext()
        {
        }

        public EXE111DBContext(DbContextOptions<EXE111DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Conversation> Conversations { get; set; } = null!;
        public virtual DbSet<Interest> Interests { get; set; } = null!;
        public virtual DbSet<Message> Messages { get; set; } = null!;
        public virtual DbSet<ProposedInterest> ProposedInterests { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Conversation>(entity =>
            {
                entity.Property(e => e.StartedAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.User1)
                    .WithMany(p => p.ConversationUser1s)
                    .HasForeignKey(d => d.User1Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__User1__5441852A");

                entity.HasOne(d => d.User2)
                    .WithMany(p => p.ConversationUser2s)
                    .HasForeignKey(d => d.User2Id)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Conversat__User2__5535A963");
            });

            modelBuilder.Entity<Interest>(entity =>
            {
                entity.HasIndex(e => e.InterestName, "UQ__Interest__D2704B36A24E026D")
                    .IsUnique();

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.InterestName).HasMaxLength(100);

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.Property(e => e.SendAt).HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Conversation)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ConversationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Conver__59063A47");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Receiv__5AEE82B9");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Messages__Sender__59FA5E80");
            });

            modelBuilder.Entity<ProposedInterest>(entity =>
            {
                entity.Property(e => e.AdminId).HasColumnName("AdminID");

                entity.Property(e => e.InterestName).HasMaxLength(100);

                entity.Property(e => e.ProposedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Pending')");

                entity.HasOne(d => d.Admin)
                    .WithMany(p => p.ProposedInterestAdmins)
                    .HasForeignKey(d => d.AdminId)
                    .HasConstraintName("FK__ProposedI__Admin__5165187F");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.ProposedInterestUsers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__ProposedI__UserI__4D94879B");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.RoleName, "UQ__Roles__8A2B61601DD697B4")
                    .IsUnique();

                entity.Property(e => e.RoleName).HasMaxLength(50);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Username, "UQ__Users__536C85E461827C70")
                    .IsUnique();

                entity.HasIndex(e => e.Email, "UQ__Users__A9D105345C265D84")
                    .IsUnique();

                entity.Property(e => e.Avatar).HasMaxLength(255);

                entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(500);

                entity.Property(e => e.Email).HasMaxLength(100);

                entity.Property(e => e.FullName).HasMaxLength(100);

                entity.Property(e => e.PasswordHash).HasMaxLength(255);

                entity.Property(e => e.PhoneNumber).HasMaxLength(20);

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('Active')");

                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username).HasMaxLength(50);

                entity.HasMany(d => d.Interests)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserInterest",
                        l => l.HasOne<Interest>().WithMany().HasForeignKey("InterestId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserInter__Inter__4AB81AF0"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").HasConstraintName("FK__UserInter__UserI__49C3F6B7"),
                        j =>
                        {
                            j.HasKey("UserId", "InterestId").HasName("PK__UserInte__7580FE8A04D27DD9");

                            j.ToTable("UserInterests");
                        });

                entity.HasMany(d => d.Roles)
                    .WithMany(p => p.Users)
                    .UsingEntity<Dictionary<string, object>>(
                        "UserRole",
                        l => l.HasOne<Role>().WithMany().HasForeignKey("RoleId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("FK__UserRoles__RoleI__4222D4EF"),
                        r => r.HasOne<User>().WithMany().HasForeignKey("UserId").HasConstraintName("FK__UserRoles__UserI__412EB0B6"),
                        j =>
                        {
                            j.HasKey("UserId", "RoleId").HasName("PK__UserRole__AF2760ADA13E044C");

                            j.ToTable("UserRoles");
                        });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
