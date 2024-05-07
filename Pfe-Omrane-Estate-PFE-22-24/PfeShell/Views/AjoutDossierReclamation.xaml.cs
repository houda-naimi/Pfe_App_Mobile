using Newtonsoft.Json;
using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjoutDossierReclamation : ContentPage
    {
        public string[] vocation = { "Vente", "Location" };
        public string[] Origine = { "Mail", "Phone" };
        public AjoutDossierReclamation()
        {
            InitializeComponent();
            pickerOrigine.ItemsSource = Origine;
            pickerVocation.ItemsSource = vocation;
        }

        private async void Ajouter_Clicked(object sender, EventArgs e)
        {
            if (pickerOrigine.SelectedItem == null)
            {
                await DisplayAlert("Erreur", "Champ Titre Manquée", "OK");

            }
            if (pickerVocation.SelectedItem == null)
            {
                await DisplayAlert("Erreur", "Champ Titre Manquée", "OK");

            }

            if (Entite.Text is null)
            {
                await DisplayAlert("Erreur", "Champ Prénom Obligatoire", "OK");
            }

            if (Client.Text is null)
            {
                await DisplayAlert("Erreur", "Champ Nom Obligatoire", "OK");
            }

        }
    }
}