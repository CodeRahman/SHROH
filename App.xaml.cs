namespace SHROH
{
    public partial class App : Application
    {
        public App()

        {
            InitializeComponent();
            InitializeDatabase();
        }

        private async void InitializeDatabase()
        {
            await Services.DatabaseService.Instance.Init();
        }
        
       

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new AppShell());
        }
    }
}