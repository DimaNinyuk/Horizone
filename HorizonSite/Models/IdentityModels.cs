using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace HorizonSite.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Review> Reviews { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Message> Messages { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            Messages = new HashSet<Message>();
            Reviews = new HashSet<Review>();
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        public virtual DbSet<Actor> Actors { get; set; }
        public virtual DbSet<Buying> Buyings { get; set; }
        public virtual DbSet<Film> Films { get; set; }
        public virtual DbSet<FilmsActor> FilmsActors { get; set; }
        public virtual DbSet<FilmsProducer> FilmsProducers { get; set; }
        public virtual DbSet<Genre> Genres { get; set; }
        public virtual DbSet<Hall> Halls { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<Producer> Producers { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<Row> Rows { get; set; }
        public virtual DbSet<Seat> Seats { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }
        public virtual DbSet<Type> Types { get; set; }
        public IEnumerable ApplicationUsers { get; internal set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Actor>()
                .Property(e => e.SNP)
                .IsUnicode(false);

           modelBuilder.Entity<Film>()
                .Property(e => e.NameFilm)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Company)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.Picture)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .Property(e => e.IMDBRatffing)
                .HasPrecision(19, 1);

            modelBuilder.Entity<Film>()
                .Property(e => e.LinkTriller)
                .IsUnicode(false);

            modelBuilder.Entity<Film>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Film)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Genre>()
                .Property(e => e.NameGenre)
                .IsUnicode(false);

            modelBuilder.Entity<Hall>()
                .Property(e => e.NameHall)
                .IsUnicode(false);

            modelBuilder.Entity<Hall>()
                .Property(e => e.Information)
                .IsUnicode(false);

            modelBuilder.Entity<Hall>()
                .HasMany(e => e.Sessions)
                .WithRequired(e => e.Hall)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.SNP)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Message>()
                .Property(e => e.CallTime)
                .IsUnicode(false);

            modelBuilder.Entity<Producer>()
                .Property(e => e.SNP)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Email)
                .IsUnicode(false);

            modelBuilder.Entity<Review>()
                .Property(e => e.Text)
                .IsUnicode(false);

            modelBuilder.Entity<Row>()
                .HasMany(e => e.Seats)
                .WithRequired(e => e.Row)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Seat>()
                .HasMany(e => e.Buyings)
                .WithOptional(e => e.Seat)
                .HasForeignKey(e => new { e.SeatId, e.RowId });

            modelBuilder.Entity<Session>()
                .HasMany(e => e.Buyings)
                .WithOptional(e => e.Session)
                .HasForeignKey(e => new { e.SessionId, e.HallId, e.FilmId });

            modelBuilder.Entity<Type>()
                .Property(e => e.NameType)
                .IsUnicode(false);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Messages)
                .WithOptional(e => e.Admin)
                .HasForeignKey(e => e.AdminId);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(e => e.Reviews)
                .WithOptional(e => e.Admin)
                .HasForeignKey(e => e.AdminId);
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}