using SHROH.Helpers;
using SHROH.Models;
using SHROH.Services;

namespace SHROH;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadDashboard();
    }

    private async void LoadDashboard()
    {
        await DatabaseService.Instance.Init();
        var transactions = await DatabaseService.Instance.GetTransactionsAsync();

        double totalIncome = transactions.Where(t => t.IsIncome).Sum(t => t.Amount);
        double totalExpenses = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);
        double balance = totalIncome - totalExpenses;

        var (currency, rate) = await SettingsHelper.GetCurrencyInfoAsync();

        IncomeAmountLabel.Text = SettingsHelper.FormatAmount(totalIncome, rate, currency);
        ExpensesAmountLabel.Text = SettingsHelper.FormatAmount(totalExpenses, rate, currency);
        BalanceLabel.Text = $"Current Balance: {SettingsHelper.FormatAmount(balance, rate, currency)}";

        var recent = transactions.OrderByDescending(t => t.Date).Take(5).ToList();
        RenderRecentTransactions(recent, rate, currency);
    }


    private void RenderRecentTransactions(List<TransactionModel> transactions, double rate, string currency)

    {
        DashboardTransactionGrid.Children.Clear();
        DashboardTransactionGrid.RowDefinitions.Clear();

        void AddCell(View view, int col, int row)
        {
            Grid.SetRow(view, row);
            Grid.SetColumn(view, col);
            DashboardTransactionGrid.Children.Add(view);
        }

        DashboardTransactionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

        AddCell(new Label { Text = "Date", FontAttributes = FontAttributes.Bold }, 0, 0);
        AddCell(new Label { Text = "Category", FontAttributes = FontAttributes.Bold }, 1, 0);
        AddCell(new Label { Text = "Description", FontAttributes = FontAttributes.Bold }, 2, 0);
        AddCell(new Label { Text = "Amount", FontAttributes = FontAttributes.Bold }, 3, 0);

        int row = 1;
        foreach (var tx in transactions)
        {
            DashboardTransactionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            AddCell(new Label { Text = tx.Date.ToString("MM/dd") }, 0, row);
            AddCell(new Label { Text = tx.Category }, 1, row);
            AddCell(new Label { Text = tx.Description }, 2, row);
            AddCell(new Label
            {
                Text = SettingsHelper.FormatAmount(tx.Amount, rate, currency),
                TextColor = tx.IsIncome ? Colors.Green : Colors.Red
            }, 3, row);


            row++;
        }
    }

    private async void OnAddTransactionClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new AddTransactionPage());
    }

    private async void OnViewAllClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new TransactionsPage());
    }
}
