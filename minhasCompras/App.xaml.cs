using minhasCompras.helpers;

namespace minhasCompras
{
    public partial class App : Application
    {
        static SQLiteDatabaseHelper? _db; // -- define a variável como nullable para evitar o aviso de inicialização --


        public static SQLiteDatabaseHelper Db
        {
            get
            {
                // -- vefifica se a instância do banco de dados já foi criada  --
                if (_db == null)
                {
                    string path = Path.Combine(
                        Environment.GetFolderPath
                        (Environment.SpecialFolder.LocalApplicationData), "banco_sqLite_minhasCompras.db3");

                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }

        public App()
        {
            InitializeComponent();

            //MainPage = new AppShell();
            MainPage = new NavigationPage (new Views.ListaProduto());
        }
        // Configura a janela do aplicativo
        protected override Window 
            CreateWindow(IActivationState? activationState)
        {
            var window = base.CreateWindow(activationState);
            window.Title = "Minhas Compras";

            window.Page?.SetValue(NavigationPage.BarBackgroundColorProperty, Color.FromArgb("#FF6200EE"));
            window.Width = 400;
            window.Height = 600;

            return window;
        }
    }
}
