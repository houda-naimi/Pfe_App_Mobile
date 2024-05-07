using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsPage : ContentPage
    {
        private ObservableCollection<Reclamations> reclamations;
        public DetailsPage(string coddossier, string entite, DateTime datedossier, string client, string vocation, bool garantie, string origine)
        {
            InitializeComponent();
            reclamations = new ObservableCollection<Reclamations>();
            var Datet = DateTime.Today;
            for (int i = 0; i < 5; i++)
            {
                reclamations.Add(new Reclamations { codreclamation = i.ToString() + "1", datereclamation = Datet, categorie = "Aluminium", type = "Porte", statut = "Intervention" });

            }

            listReclamtion.ItemsSource = reclamations;

            dateDossier.Text = datedossier.ToString("dd, MMMM, yyyy");
            entiteLabel.Text = entite;
            codeLabel.Text = coddossier;
            clientLabel.Text = client;
            vocationLabel.Text = vocation;
            if (garantie == true)
                garantieLabel.Text = "Sous garantie";
            else
                garantieLabel.Text = "Hors garantie";
            origineLabel.Text = origine;

        }

        private async void Ajouter_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AjouterReclamation());
        }
    }
}