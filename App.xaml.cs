using Microsoft.EntityFrameworkCore.Infrastructure;
using SPZO.DataManagement;
using System.Windows;

namespace SPZO
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DatabaseFacade facadeClients = new DatabaseFacade(new SQLiteDataAccess());
            facadeClients.EnsureCreated();

            DatabaseFacade facadePayments = new DatabaseFacade(new SQLiteDataAccess());
            facadePayments.EnsureCreated();
        }
    }
}
