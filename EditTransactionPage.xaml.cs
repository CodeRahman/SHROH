using SHROH.Models;
using SHROH.Services;

namespace SHROH
{
    public partial class EditTransactionPage : ContentPage
    {
        private TransactionModel _tx;

        public EditTransactionPage(TransactionModel tx)
        {
            InitializeComponent();
            _tx = tx;

            DescriptionEntry.Text = tx.Description;
            AmountEntry.Text = tx.Amount.ToString();
            CategoryPicker.SelectedItem = tx.Category;
            IsIncomeSwitch.IsToggled = tx.IsIncome;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            _tx.Description = DescriptionEntry.Text;
            _tx.Amount = double.TryParse(AmountEntry.Text, out double amt) ? amt : 0;
            _tx.Category = CategoryPicker.SelectedItem?.ToString();
            _tx.IsIncome = IsIncomeSwitch.IsToggled;
            _tx.Date = DatePicker.Date;


            await DatabaseService.Instance.SaveTransactionAsync(_tx);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnDeleteClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Confirm", "Delete this transaction?", "Yes", "No");
            if (confirm)
            {
                await DatabaseService.Instance.DeleteTransactionAsync(_tx);
                await Shell.Current.Navigation.PopAsync();
            }
        }
    }
}
