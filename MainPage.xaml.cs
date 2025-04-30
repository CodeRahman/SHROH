namespace SHROH
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
        }

        private async void OnTransactionsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new TransactionsPage());
        }

        private async void OnSettingsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SettingsPage());
        }

        private async void OnBudgetsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new BudgetsPage());


        }

        private async void OnDashboardClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DashboardPage());
        }

    }

}
