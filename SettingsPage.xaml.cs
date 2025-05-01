using SHROH.Models;
using SHROH.Services;

namespace SHROH;

public partial class SettingsPage : ContentPage
{
    private SettingsModel _settings;

    private readonly Dictionary<string, double> _conversionRates = new()
    {
        { "USD", 1.0 },
        { "GBP", 0.8 },
        { "EUR", 0.9 }
    };

    public SettingsPage()
    {
        InitializeComponent();
        LoadSettings();
    }

    private async void LoadSettings()
    {
        await DatabaseService.Instance.Init();
        var settingsList = await DatabaseService.Instance.GetSettingsAsync();
        _settings = settingsList.FirstOrDefault() ?? new SettingsModel();

        foreach (var rb in CurrencyRadioGroup.Children.OfType<RadioButton>())
            rb.IsChecked = rb.Content.ToString() == _settings.Currency;

        foreach (var rb in ThemeRadioGroup.Children.OfType<RadioButton>())
            rb.IsChecked = rb.Content.ToString() == _settings.Theme;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        string selectedCurrency = CurrencyRadioGroup.Children.OfType<RadioButton>()
            .FirstOrDefault(r => r.IsChecked)?.Content.ToString();

        string selectedTheme = ThemeRadioGroup.Children.OfType<RadioButton>()
            .FirstOrDefault(r => r.IsChecked)?.Content.ToString();

        if (selectedCurrency != null) _settings.Currency = selectedCurrency;
        if (selectedTheme != null) _settings.Theme = selectedTheme;

        await DatabaseService.Instance.SaveSettingsAsync(_settings);

        ApplyTheme(_settings.Theme);

        await DisplayAlert("Settings", "Preferences saved and applied.", "OK");
    }

    private void ApplyTheme(string theme)
    {
        App.Current.UserAppTheme = theme == "Dark" ? AppTheme.Dark : AppTheme.Light;
    }
}
