using Rg.Plugins.Popup.Extensions;
using System;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public static Guid LicenseKey ;
       
        public Login()
        {
            InitializeComponent();
            txtcle.Text = "3e6dc500-5f71-4c14-818d-1e09c1e40001";
        }


        private async void Valider_Clicked(object sender, EventArgs e)
        {
            var internetConnection = Connectivity.NetworkAccess;
            if (internetConnection == NetworkAccess.Internet)
            {
                var test = false;
                while (test == false)
                {
                    Button.IsBusy = true;
                    //var popup = new PopupIndicator();
                    //await AppShell.Current.Navigation.PushPopupAsync(popup,true);
                    test = await HasLicenseAsync();
                    Button.IsBusy = false;
                }
                //await AppShell.Current.Navigation.PopPopupAsync(true);

                //await Shell.Current.GoToAsync($"//{nameof(Authentification)}");
            }
            else
            {
                string ch = "Vérifier votre connexion internet";
                string ch1 = "";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);
            }
        }

        public async Task<bool> HasLicenseAsync()
        {
            var licenseKey = txtcle.Text;

      
            bool verif = false;
         
            if (string.IsNullOrWhiteSpace(licenseKey)) 
            {
               string ch = "Champ Vide";
                string ch1 = "Insérer votre clé de licence";
                var pop = new MessageBox(ch, ch1);
              await  AppShell.Current.Navigation.PushPopupAsync(pop, true);
               // txtcle.Focus();
            }
            else
            { 
         
            try
                {
                    
                    LicenseKey = new Guid(licenseKey);
                    verif = await App.yesService.GetHasLicenseAsync(LicenseKey);
                if (verif == true)
                {
                    Preferences.Set("License Verified", verif);
                    Preferences.Set("License", txtcle.Text.ToString());
                        await Navigation.PushAsync(new Authentification());
                }
         
            }
           
            catch (Exception )
            {
                   string ch = "Licence invalid";
                    string ch1 = "Veuilliez vérifier votre Licence";
                    var pop = new MessageBox(ch, ch1);
                    await AppShell.Current.Navigation.PushPopupAsync(pop, true);
                //txtcle.Focus();
            }
            }
            return verif;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
           
                if (Preferences.Get("License Verified", false))
                {
                    await Navigation.PushAsync(new Authentification());
                }
        }

    }

    internal struct NewStruct
    {
        public object Item1;
        public object Item2;

        public NewStruct(object item1, object item2)
        {
            Item1 = item1;
            Item2 = item2;
        }

        public override bool Equals(object obj)
        {
            return obj is NewStruct other &&
                   System.Collections.Generic.EqualityComparer<object>.Default.Equals(Item1, other.Item1) &&
                   System.Collections.Generic.EqualityComparer<object>.Default.Equals(Item2, other.Item2);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Item1, Item2);
        }

        public void Deconstruct(out object item1, out object item2)
        {
            item1 = Item1;
            item2 = Item2;
        }

        public static implicit operator (object, object)(NewStruct value)
        {
            return (value.Item1, value.Item2);
        }

        public static implicit operator NewStruct((object, object) value)
        {
            return new NewStruct(value.Item1, value.Item2);
        }
    }
}