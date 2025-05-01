using SHROH.Models;
using SHROH.Services;
using SHROH.Helpers;

namespace SHROH;

public partial class BudgetsPage : ContentPage
{
    public BudgetsPage()
    {
        InitializeComponent();
        LoadBudgets();
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadBudgets();
    }


    private async void LoadBudgets()
    {
        await DatabaseService.Instance.Init();

        var budgets = await DatabaseService.Instance.GetBudgetsAsync();
        var transactions = await DatabaseService.Instance.GetTransactionsAsync();
        var (currency, rate) = await SettingsHelper.GetCurrencyInfoAsync();

        double totalBudget = budgets.Sum(b => b.Limit);
        double totalSpent = transactions.Where(t => !t.IsIncome).Sum(t => t.Amount);

        var layout = new VerticalStackLayout { Spacing = 15 };

        foreach (var budget in budgets)
        {
            var spentInCategory = transactions
                .Where(t => !t.IsIncome && t.Category == budget.Category)
                .Sum(t => t.Amount);

            double progress = Math.Min(spentInCategory / budget.Limit, 1.0);

            var color = progress switch
            {
                >= 1 => Colors.Red,
                >= 0.8 => Colors.Orange,
                _ => Colors.Green
            };

            var frame = new Frame
            {
                BackgroundColor = Color.FromArgb("#f3f4f6"),
                CornerRadius = 12,
                Padding = 10,
                Content = new VerticalStackLayout
                {
                    Children =
                {
                    new Label
                    {
                        Text = $"{budget.Category} - {SettingsHelper.FormatAmount(spentInCategory, rate, currency)} / {SettingsHelper.FormatAmount(budget.Limit, rate, currency)}",
                        FontSize = 16,
                        FontAttributes = FontAttributes.Bold
                    },
                    new ProgressBar
                    {
                        Progress = progress,
                        ProgressColor = color,
                        HeightRequest = 6
                    }
                }
                }
            };

            layout.Children.Add(frame);
        }

        BudgetStack.Children.Clear();
        BudgetStack.Children.Add(layout);
    }

    private async void OnAddBudgetClicked(object sender, EventArgs e)
    {
        await Shell.Current.Navigation.PushAsync(new AddBudgetPage());
    }

}
