using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using Microsoft.AspNet.Identity.EntityFramework;
using ReadLater.Entities;

namespace ReadLater.Data {
	public class ReadLaterDataContext : IdentityDbContext<ApplicationUser>, IDbContext
    {
        static ReadLaterDataContext()
        {
            Database.SetInitializer(new CreateDatabaseIfNotExists<ReadLaterDataContext>());
        }

        public ReadLaterDataContext()
            : base("ReadLaterDataContext", throwIfV1Schema: false)
        {
        }

        public new IDbSet<T> Set<T>() where T : class
        {
            return base.Set<T>();
        }

        public override int SaveChanges()
        {
            this.ApplyStateChanges();
            return base.SaveChanges();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
			base.OnModelCreating(modelBuilder);

            EntityTypeConfiguration<Category> categoryMap = modelBuilder.Entity<Category>();
            EntityTypeConfiguration<Bookmark> bookmarkMap = modelBuilder.Entity<Bookmark>();
		}

		public static ReadLaterDataContext Create() {
			return new ReadLaterDataContext();
		}

		public DbSet<Category> Categories { get; set; }

		public DbSet<Bookmark> Bookmarks { get; set; }
    }
}
