using PfeShell.Repositories;
using PfeShell.Views;
using Rg.Plugins.Popup.Extensions;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using YesS;
using static Xamarin.Essentials.Permissions;

namespace PfeShell
{
    public partial class App : Application
    {
        private string dbPath = Path.Combine(FileSystem.AppDataDirectory, "CalendarDB.db3");
        public static EventTable eventTable { get; set; }
        public static YesService yesService = new YesService("http://197.14.10.23/YesS_X/", new System.Net.Http.HttpClient());
      //  public static string chemin = "http://192.168.87.250:5158/api/Reclamations";
         public static string chemin = "http://192.168.100.135:5158/api/Reclamations";
        public static string cheminCat = "http://192.168.100.135:5158/api/CategorieRecs";
        public static string cheminClient = "http://192.168.100.135:5158/api/Clients";
      
      
        public App()
        {
            
            InitializeComponent();
            MainPage = new AppShell();
            Device.SetFlags(new[] { "Shapes_Experimental", "MediaElement_Experimental" });
            Device.SetFlags(new[] { "Expander_Experimental" });
            eventTable = new EventTable(dbPath);
            Sharpnado.CollectionView.Initializer.Initialize(true, false);
            


        }

        protected override void OnStart()
        {
           
                if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.iOS)
                {
                    // AskForRelevantPermissionsAsync();
                }
                else if (Xamarin.Forms.Device.RuntimePlatform == Xamarin.Forms.Device.Android)
                {
                    //AskForRelevantPermissionsAsync();
                    MainThread.BeginInvokeOnMainThread(async () =>
                    {
                        await AskForRelevantPermissionsAsync();
                    });
                }
                 
        }
        private async System.Threading.Tasks.Task AskForRelevantPermissionsAsync()
        {
            //await AskForPermissionAsync<Permissions.LocationAlways>();
            await AskForPermissionAsync<Permissions.Camera>();
            await AskForPermissionAsync<Permissions.Media>();
            await AskForPermissionAsync<Permissions.StorageRead>();
            await AskForPermissionAsync<Permissions.StorageWrite>();
            await AskForPermissionAsync<Permissions.Phone>();
            await AskForPermissionAsync<Permissions.NetworkState>();
            //await AskForPermissionAsync<Permissions.Maps>();


        }
        private async System.Threading.Tasks.Task AskForPermissionAsync<TPermission>()
              where TPermission : BasePermission, new()
        {
            var result = await CheckStatusAsync<TPermission>();
            if (result != PermissionStatus.Granted)
                await RequestAsync<TPermission>();
        }

        public async Task<PermissionStatus> CheckAndRequestPermissionAsync<T>(T permission)
            where T : BasePermission
        {
            var status = await permission.CheckStatusAsync();
            if (status != PermissionStatus.Granted)
            {
                status = await permission.RequestAsync();
            }

            return status;
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
          /*  var internetConnection = Connectivity.NetworkAccess;
            if (internetConnection == NetworkAccess.Internet)
            { 
            }
            else
            {
                string ch = "Vérifier votre connexion internet";
                string ch1 = "";
                var pop = new MessageBox(ch, ch1);
                AppShell.Current.Navigation.PushPopupAsync(pop, true);
            }*/
        }
    }
}
