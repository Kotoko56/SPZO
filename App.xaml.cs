using Microsoft.EntityFrameworkCore.Infrastructure;
using SPZO.DataManagement;
using System.Windows;

namespace SPZO
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            SQLiteDataAccess myDbContext = new SQLiteDataAccess();

            DatabaseFacade facadeClients = new DatabaseFacade(myDbContext);
            facadeClients.EnsureCreated();

            DatabaseFacade facadePayments = new DatabaseFacade(myDbContext);
            facadePayments.EnsureCreated();
        }
    }
}
