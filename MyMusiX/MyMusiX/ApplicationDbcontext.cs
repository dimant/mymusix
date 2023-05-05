namespace MyMusiX
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Data.Sqlite;

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Record> Records { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=records.db");
        }

        public async Task ImportRecordsAsync(IEnumerable<Record> records)
        {
            this.Database.EnsureCreated();

            foreach (var record in records)
            {
                this.Records.Add(record);
            }

            await this.SaveChangesAsync();
        }
    }
}
