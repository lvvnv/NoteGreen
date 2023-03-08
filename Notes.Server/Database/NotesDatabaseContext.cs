using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Notes.Server.Entities;

namespace Notes.Server.Database
{
    public class NotesDatabaseContext : DbContext
    {
        public NotesDatabaseContext(DbContextOptions<NotesDatabaseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        
        public DbSet<Note> Notes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Note>().ToTable("Notes");
            base.OnModelCreating(modelBuilder);
        }
    }
}