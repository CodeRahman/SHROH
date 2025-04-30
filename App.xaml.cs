
namespace SHROH
{
    public partial class App : Application
    {
        public App()

        {
            

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(BudgetsPage), typeof(BudgetsPage));
            Routing.RegisterRoute(nameof(TransactionsPage), typeof(TransactionsPage));
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}