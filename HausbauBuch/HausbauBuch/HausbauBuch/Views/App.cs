using System.Xml.Linq;
using HausbauBuch.Business;
using HausbauBuch.Views.Home;
using SQLite;
using Xamarin.Forms;

namespace HausbauBuch.Views
{
    public class App : Application
    {
        public static SQLiteAsyncConnection SqlConnection;

        public static ActivitiesController ActivityController;

        public static AppointmentsController AppointmentsController;

        public static ContactsController ContactsController;

        public static DocumentsController DocumentsController;

        public static EnviromentsController EnviromentsController;

        public static GardenController GardenController;

        public App()
        {
            var dataBaseAccess = DependencyService.Get<IDatabaseAccess>();
            SqlConnection = dataBaseAccess.GetConnection();

            InitControllers();

            MainPage = new NavigationPage(new Dashboard());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        private static void InitControllers()
        {
            ActivityController = new ActivitiesController();
            AppointmentsController = new AppointmentsController();
            ContactsController = new ContactsController();
            DocumentsController = new DocumentsController();
            EnviromentsController = new EnviromentsController();
            GardenController = new GardenController();
        }
    }
}