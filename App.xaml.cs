using Microsoft.EntityFrameworkCore.Infrastructure;
using SPZO.DataManagement;
using System.Windows;

namespace SPZO
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            //Override on startup allows me to define myDbContext, also known as my database
            SQLiteDataAccess myDbContext = new SQLiteDataAccess();

            DatabaseFacade facadeClients = new DatabaseFacade(myDbContext);
            facadeClients.EnsureCreated(); //This check if tables for Clients exists. If not it will be created based on Client class construction.

            DatabaseFacade facadePayments = new DatabaseFacade(myDbContext);
            facadePayments.EnsureCreated(); //This check if tables for Payments exists. If not it will be created based on Payment class construction.
        }
    }
}
