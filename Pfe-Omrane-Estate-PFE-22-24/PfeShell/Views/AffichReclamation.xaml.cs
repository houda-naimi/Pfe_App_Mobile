using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using PfeShell.Models;
using PfeShell.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AffichReclamation : ContentPage
    {

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }

        private bool _isBusy1;
        public bool IsBusy1
        {
            get
            {
                return _isBusy1;
            }
            set
            {
                _isBusy1 = value;
                OnPropertyChanged("IsBusy1");
            }
        }

        public ObservableCollection<Reclamation2> recList = new ObservableCollection<Reclamation2>();
        public ObservableCollection<Reclamation2> reclamationList = new ObservableCollection<Reclamation2>();
   

        public AsyncCommand RefreshCommand { get; set; }

        public AffichReclamation()
        {
            InitializeComponent();
            RefreshCommand = new AsyncCommand(refresh);

            //recList= await GetReclamations();
            // GetCategorieRec();
            GetReclamations();
            //RecListView.ItemsSource = recList;
            BindingContext = this;
        }


        public async Task<ObservableCollection<Reclamation2>> GetReclamations()
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync(App.chemin);
            var recList = JsonConvert.DeserializeObject<ObservableCollection<Reclamation2>>(json);
            ////
            var clientcat = new HttpClient();
            string jsoncat = await clientcat.GetStringAsync(App.cheminCat);
            var rescat = JsonConvert.DeserializeObject<List<CategorieRec>>(jsoncat); 
            ////
            var clientClient = new HttpClient();
            string jsonClient = await clientcat.GetStringAsync(App.cheminClient);
            var resClient = JsonConvert.DeserializeObject<List<ClientRec>>(jsonClient);
            ////
            reclamationList.Clear();
                foreach (var item in recList)
                {
                    Reclamation2 rec = new Reclamation2();
                    foreach (var c in rescat)
                    {
                        if (item.idCat == c.idCat)
                        {
                            rec.catRec = c.code;
                            rec.idCat = c.idCat;
                        }
                    }
                    foreach (var c in resClient)
                    {
                        if (item.idclient == c.idclient)
                        {
                            rec.nomClient = c.nom;
                            rec.idclient = c.idclient;
                        }
                    }
                    rec.adresse = item.adresse;
                    rec.dateRec = item.dateRec;
                    rec.description = item.description;
                    rec.idRec = item.idRec; 
                   
                reclamationList.Insert(0, rec);
                }
            
            ///////////
            RecListView.ItemsSource = reclamationList;
            return reclamationList;
        }

       /* private async Task GetReclamations()
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync(App.chemin);
            var listRec = JsonConvert.DeserializeObject<List<Reclamation2>>(json);

            RecListView.ItemsSource = listRec;

        }
       */






        private async void btnSupprimer_Clicked(object sender, EventArgs e)
        {
            var btnSupprimer = sender as MenuItem;
            var reclamation = btnSupprimer.CommandParameter as Reclamation2;
            var client = new HttpClient();
            var idR = reclamation.idRec;

            bool confirmation = await DisplayAlert("Suppression", "Voulez-vous confirmer la suppression", "oui", "non");

            if (confirmation == true)
            {
                var response = await client.DeleteAsync(App.chemin.ToString() + "/" + idR);

                if (response.IsSuccessStatusCode)
                {
                    await DisplayAlert("Alert", "Reclamation supprimé avec succés !!", "ok");
                    GetReclamations();
                }
            }
            else
            {
                return;
            }

        }

        async void btnEdit_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var reclamation = menuItem.CommandParameter as Reclamation2;
            await Navigation.PushAsync(new AjouterReclamation(reclamation));
        }

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AjouterReclamation());
        }



        async void btnDetails_Clicked(object sender, EventArgs e)
        {
            var btn = sender as ImageButton;
            var reclamation = btn.CommandParameter as Reclamation2;
            await Navigation.PushAsync(new DetailsRec(reclamation));


        }


        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");

        }


       /* private async void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            var MotRecherche = search.Text.ToString();

            var client = new HttpClient();
            string json = await client.GetStringAsync(App.chemin);
             
            Task<List<Reclamation2>> lstchr = new Task<List<Reclamation2>>();
            lstchr = GetReclamations();
            var filtre = listRec.Where(c => c.catRec.ToLower().Contains(MotRecherche) || c.nomClient.ToString().ToLower().Contains(MotRecherche));
            RecListView.ItemsSource = filtre;



        }*/


        async System.Threading.Tasks.Task refresh()
        {
            if (IsBusy == false) { return; }
            IsBusy = true;

            await GetReclamations();
            IsBusy = false;

        }






















    }
}