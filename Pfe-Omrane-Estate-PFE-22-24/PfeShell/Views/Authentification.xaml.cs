using PfeShell.ViewModels;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Authentification : ContentPage
    {
        public static Guid licenseKey { get; set; }

        public static LoginResult user;
        PopupIndicator popup = new PopupIndicator();

        public Authentification( )
        {
            InitializeComponent();
            string lk = Preferences.Get("License", "");
            Guid Licence = new Guid(lk);
            licenseKey = Licence;
            code.Focus();
            this.BindingContext = this;
        }
        private async void Valider_Clicked(object sender, EventArgs e)
        {
            var internetConnection = Connectivity.NetworkAccess;
            if (internetConnection == NetworkAccess.Internet)
            {
                if (string.IsNullOrWhiteSpace(code.Text) || (string.IsNullOrWhiteSpace(pass.Text)))
                {
                    string ch = "Champ Vide";
                    string ch1 = "Insérer votre code et votre mot de passe";
                    var pop = new MessageBox(ch, ch1);
                    await AppShell.Current.Navigation.PushPopupAsync(pop, true);
                }
                else
                {
                    var test = false;
                    while (test == false)
                    {
                        Valider.IsBusy = true;
                        //await AppShell.Current.Navigation.PushPopupAsync(popup, true);
                        user = await testservice();
                        test = true;
                        Valider.IsBusy = false;
                    }

                }

                if (Remember.IsToggled)
                {
                    Preferences.Set("Code", user.User.Code);
                    Preferences.Set("Password", pass.Text.ToString());

                }
            }
            else
            {
                string ch = "Vérifier votre connexion internet";
                string ch1 = "";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);
            }
        }

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {

        }
        public async Task<LoginResult> testservice()
        {
            var c = new LoginResult();
                try
                 {   
                    var res = await App.yesService.LoginAsync(licenseKey, new YesS.User() { Code = code.Text.ToString(), Pass = pass.Text.ToString() });
                        if (res != null)
                         {
                            c = res;
                         }
                        await Shell.Current.GoToAsync($"///{nameof(ListeApplTelephonique)}");
                //await AppShell.Current.Navigation.PopPopupAsync(true);
            }

                 catch (Exception)
                     {
                //await AppShell.Current.Navigation.PopPopupAsync(true);
                string ch = "code ou mot de passe invalid";
                    string ch1 = "Veuillez vérifier votre code et votre mot de passe";
                    var pop = new MessageBox(ch, ch1);
                    await AppShell.Current.Navigation.PushPopupAsync(pop, true);
                     }
            
            return c;
        }
        protected override void OnAppearing()
        {
            base.OnAppearing();
            code.Text = Preferences.Get("Code", "");
            pass.Text = Preferences.Get("Password", "");

        }
    }
}