using System;
using System.Collections.ObjectModel;

namespace SHROH
{
    public partial class MainPage : ContentPage
    {
        private decimal totalIncome = 0;
        private decimal totalExpenses = 0;
        private ObservableCollection<Transaction> transactions = new ObservableCollection<Transaction>();

        public MainPage()
        {
            InitializeComponent();
            TransactionsList.ItemsSource = transactions;
        }

        private void OnAddTransactionClicked(object sender, EventArgs e)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(DescriptionEntry.Text) ||
                !decimal.TryParse(AmountEntry.Text, out decimal amount) ||
                TransactionTypePicker.SelectedItem == null)
            {
                DisplayAlert("Error", "Please enter valid transaction details.", "OK");
                return;
            }

            string type = TransactionTypePicker.SelectedItem.ToString();
            if (type == "Income")
            {
                totalIncome += amount;
                IncomeLabel.Text = $"${totalIncome:F2}";
            }
            else if (type == "Expense")
            {
                totalExpenses += amount;
                ExpensesLabel.Text = $"${totalExpenses:F2}";
            }

            // Update balance
            decimal balance = totalIncome - totalExpenses;
            BalanceLabel.Text = $"Current Balance: ${balance:F2}";

            // Add transaction to the list
            transactions.Add(new Transaction
            {
                Description = DescriptionEntry.Text,
                Amount = type == "Expense" ? -amount : amount,
                AmountColor = type == "Expense" ? "#B91C1C" : "#059669"
            });

            // Clear input fields
            DescriptionEntry.Text = string.Empty;
            AmountEntry.Text = string.Empty;
            TransactionTypePicker.SelectedItem = null;
        }
    }

    public class Transaction
    {
        public string Description { get; set; }
        public decimal Amount { get; set; }
        public string AmountColor { get; set; }
    }
}
