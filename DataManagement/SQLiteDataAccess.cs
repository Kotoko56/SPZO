using Microsoft.EntityFrameworkCore;
using SPZO.Model;

namespace SPZO.DataManagement
{
    public class SQLiteDataAccess : DbContext
    {
        //Class for main database connection and tables. 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Connection string
            optionsBuilder.UseSqlite("Data Source = database.db");
        }

        //DBSet(Table) for Clients
        public DbSet<Client> Clients { get; set; }

        //DBSet for Payments
        public DbSet<Payments> Payments { get; set; }

        //DBSet for Users
        public DbSet<Users> Users { get; set; }
    }
}
