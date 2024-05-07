using MvvmHelpers;
using MvvmHelpers.Commands;
using PfeShell.Interfaces;
using PfeShell.Models;
using PfeShell.ViewModels;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Command = MvvmHelpers.Commands.Command;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeApplTelephonique : ContentPage
    {
        ListeAppelTeleViewModel listeAppelTelViewModel = new ListeAppelTeleViewModel();
        AppelTel c = new AppelTel();
        ObservableRangeCollection<Grouping<string, AppelTel>> list = new ObservableRangeCollection<Grouping<string, AppelTel>>();

        

        public ListeApplTelephonique()
        {
            InitializeComponent();

        }

        private async void listContacts_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string tel = "";
            bool test = true;
            string nom = "";
            string prenom = "";
            int i = 0; 
            AppelTel selected = e.CurrentSelection.FirstOrDefault() as AppelTel;
            if (selected != null)
            {
                string name = selected.CallName;
                if (name == null)
                {
                    nom = "";
                    prenom = "";
                }
                else
                {
                    if (name.Contains(" "))
                    {
                        while ((i < name.Length) && test)
                        {
                            nom += name[i];

                            if (name[i + 1].Equals(' '))
                            {
                                test = false;
                            }
                            i++;
                        }
                        for (int j = (nom.Length) + 1; j < name.Length; j++)
                        {
                            prenom += name[j];
                        }
                    }
                    else
                    {
                        nom = name;
                        prenom = "";
                    }
                }
                tel = selected.CallNumber;
                await Navigation.PushAsync(new ProspectInformation(nom, tel, prenom));
            } 
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
            await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");

        }
    }
}