namespace SHROH
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
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

        private async void OnReportsClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ReportsPage());
        }

    }

}
