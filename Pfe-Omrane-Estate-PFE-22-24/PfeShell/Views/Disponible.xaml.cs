using PfeShell.Models;
using PfeShell.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Disponible : ContentPage
    {
      



        public ObservableCollection<immeuble> Allimmeubles { get; set; }
        public Disponible()
        {
            InitializeComponent();
        }

        private async Task OpenAnimation(View view, uint length = 80)
        {
            view.RotationX = -90;
            view.IsVisible = true;
            view.Opacity = 0;
            _ = view.FadeTo(1, length);
            await view.RotateXTo(0, length);
        }

        private async void search_TextChanged(object sender, TextChangedEventArgs e)
        {
            Allimmeubles = new ObservableCollection<immeuble>();
            var MotRecherche = search.Text.ToString();
            DisponibleViewModel v = new DisponibleViewModel();
            Allimmeubles = await v.GetAvailable();
            var searchresult = Allimmeubles.Where(c => c.TrancheName.ToLower().Contains(MotRecherche.ToLower()));
            listDisponible.ItemsSource = searchresult;

        }

        private async void Categories_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var cat = ((CollectionView)sender).SelectedItem as Categories;
            if (cat != null)
            {
                await Navigation.PushAsync(new DisponibleDetails(cat));
            }
            ((CollectionView)sender).SelectedItem = null;
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");
        }

        
    }
}