namespace MiniGigWebApi.Data
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity;
    using MiniGigWebApi.Domain;

    public class MiniGigContext: DbContext
    {
        public MiniGigContext(): base("name=MiniGigConnection"){
            //Database.SetInitializer(new MigrateDatabaseToLatestVersion<MiniGigContext, Migrations.Configuration>(useSuppliedContext:true));
        }
        public virtual DbSet<Gig> Gigs { get; set; }
        public virtual DbSet<MusicGenre> MusicGenres { get; set; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<Gig>()
                .HasKey(x=>x.Id);

            builder.Entity<Gig>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            builder.Entity<Gig>()
                .Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Gig>()
                .Property(g => g.GigDate)
                .IsRequired();

            builder.Entity<Gig>()
                .HasRequired(g => g.MusicGenre);

            builder.Entity<MusicGenre>()
               .HasKey(x => x.Id);

            builder.Entity<MusicGenre>()
                .Property(x => x.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            builder.Entity<MusicGenre>()
               .Property(x => x.Category)
               .HasMaxLength(100);
        }
    }
}