using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Maps : ContentPage
    {
        public Maps()
        {
            InitializeComponent();
            UpdateMap();
        }


        private async void UpdateMap()
        {
            var Loc = await Xamarin.Essentials.Geolocation.GetLocationAsync();
            myMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(Loc.Latitude, Loc.Longitude), Distance.FromKilometers(100)));
        }

    }
}