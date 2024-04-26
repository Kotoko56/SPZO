using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using SPZO.Model;

namespace SPZO.DataManagement
{
    public class SQLiteDataAccess : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = database.db");
        }

        public DbSet<Client> Clients { get; set; }

        public DbSet<Payments> Payments { get; set; }
    }
}
