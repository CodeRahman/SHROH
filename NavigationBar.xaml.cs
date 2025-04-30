namespace SHROH;

public partial class NavigationBar : ContentView
{
    public NavigationBar()
    {
        InitializeComponent();
    }

    private async void OnDashboardClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new MainPage());
    }

    private async void OnTransactionsClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new TransactionsPage());
    }

    private async void OnBudgetsClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new BudgetsPage());
    }

    private async void OnSettingsClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new SettingsPage());
    }

   
}
