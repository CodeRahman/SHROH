using SHROH;
using SHROH.Services;
using SHROH.Models;
using SHROH.Helpers;

namespace SHROH
{
    public partial class TransactionsPage : ContentPage
    {
        private string _filter = "All";
        private List<TransactionModel> _allTransactions;
        private bool _showAll = false;

        public TransactionsPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            LoadTransactions();
        }

        private async void LoadTransactions()
        {
            await DatabaseService.Instance.Init();

            _allTransactions = await DatabaseService.Instance.GetTransactionsAsync();
            var (currency, rate) = await SettingsHelper.GetCurrencyInfoAsync();

            var filtered = _filter switch
            {
                "Income" => _allTransactions.Where(t => t.IsIncome).ToList(),
                "Expenses" => _allTransactions.Where(t => !t.IsIncome).ToList(),
                _ => _allTransactions
            };

            var transactions = _showAll
                ? filtered.OrderByDescending(t => t.Date).ToList()
                : filtered.OrderByDescending(t => t.Date).Take(10).ToList();

            MoreButton.IsVisible = !_showAll && filtered.Count > 10;

            TransactionGrid.Children.Clear();
            TransactionGrid.RowDefinitions.Clear();

            TransactionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            void AddCell(View view, int col, int row)
            {
                Grid.SetRow(view, row);
                Grid.SetColumn(view, col);
                TransactionGrid.Children.Add(view);
            }

            AddCell(new Label { Text = "Date", FontAttributes = FontAttributes.Bold }, 0, 0);
            AddCell(new Label { Text = "Category", FontAttributes = FontAttributes.Bold }, 2, 0);
            AddCell(new Label { Text = "Description", FontAttributes = FontAttributes.Bold }, 1, 0);
            AddCell(new Label { Text = "Amount", FontAttributes = FontAttributes.Bold }, 3, 0);

            int row = 1;
            foreach (var tx in transactions)
            {
                TransactionGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

                AddCell(new Label { Text = tx.Date.ToString("MM/dd") }, 0, row);
                AddCell(new Label { Text = tx.Category }, 2, row);

                var descLabel = new Label
                {
                    Text = tx.Description,
                    TextDecorations = TextDecorations.Underline,
                    TextColor = Colors.Blue,
                    FontAttributes = FontAttributes.Bold
                };

                var tapGesture = new TapGestureRecognizer();
                tapGesture.Tapped += async (s, e) =>
                {
                    await Shell.Current.Navigation.PushAsync(new EditTransactionPage(tx));
                };
                descLabel.GestureRecognizers.Add(tapGesture);

                AddCell(descLabel, 1, row);

                AddCell(new Label
                {
                    Text = SettingsHelper.FormatAmount(tx.Amount, rate, currency),
                    TextColor = tx.IsIncome ? Colors.Green : Colors.Red
                }, 3, row);

                row++;
            }
        }


        private void OnMoreClicked(object sender, EventArgs e)
        {
            _showAll = true;
            LoadTransactions();
        }

        private void OnFilterClicked(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.CommandParameter is string filterType)
            {
                _filter = filterType;
                LoadTransactions();
            }
        }

        private async void OnAddTransactionClicked(object sender, EventArgs e)
        {
            await Shell.Current.Navigation.PushAsync(new AddTransactionPage());
        }
    }
}
