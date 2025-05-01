using SHROH.Models;
using SHROH.Services;

namespace SHROH
{
    public partial class AddBudgetPage : ContentPage
    {
        public AddBudgetPage()
        {
            InitializeComponent();
        }

        private async void OnSaveBudgetClicked(object sender, EventArgs e)
        {
            if (CategoryPicker.SelectedIndex == -1 || string.IsNullOrWhiteSpace(LimitEntry.Text))
            {
                await DisplayAlert("Error", "Please select a category and enter a valid limit.", "OK");
                return;
            }

            double limit = double.TryParse(LimitEntry.Text, out double result) ? result : 0;

            var budget = new BudgetModel
            {
                Category = CategoryPicker.SelectedItem.ToString(),
                Limit = limit
            };

            await DatabaseService.Instance.SaveBudgetAsync(budget);
            await Shell.Current.Navigation.PopAsync();
        }
    }
}
