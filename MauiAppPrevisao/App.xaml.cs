using MauiAppPrevisao.Helpers;
using System.Globalization;

namespace MauiAppPrevisao
{
    public partial class App : Application
    {
        
        public static int UsuarioLogadoId { get; set; } = 0; 

        static SQLiteDatabaseHelper _db;

        public static SQLiteDatabaseHelper Db
        {
            get
            {
                if (_db == null)
                {
                    string path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "banco_sqlite_PrevisaoTempo.db3");
                    _db = new SQLiteDatabaseHelper(path);
                }
                return _db;
            }
        }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new Views.Login());
        }
    }
}