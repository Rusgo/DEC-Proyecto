using Microsoft.Maui.ApplicationModel;

namespace AppTp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            Application.Current.UserAppTheme = AppTheme.Light;
            MainPage = new NavigationPage(new Pantallas.Inicio());
        }
    }
}
