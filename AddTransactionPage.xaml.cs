using SHROH.Models;
using SHROH.Services;

namespace SHROH
{
    public partial class AddTransactionPage : ContentPage
    {
        public AddTransactionPage()
        {
            InitializeComponent();
        }

        private async void OnSaveTransactionClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(DescriptionEntry.Text) || string.IsNullOrWhiteSpace(AmountEntry.Text))
            {
                await DisplayAlert("Error", "Please enter description and amount.", "OK");
                return;
            }

            var transaction = new TransactionModel
            {
                Description = DescriptionEntry.Text,
                Category = CategoryPicker.SelectedItem?.ToString() ?? "Uncategorized",
                Amount = double.TryParse(AmountEntry.Text, out double amt) ? amt : 0,
                Date = DatePicker.Date, 
                IsIncome = IsIncomeSwitch.IsToggled
            };


            await DatabaseService.Instance.SaveTransactionAsync(transaction);
            await Shell.Current.Navigation.PopAsync(); // Go back to TransactionsPage
        }
    }
}
