using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreerDossier : ContentPage
    {
        public CreerDossier()
        {
            InitializeComponent();
        }

        async void Button_Clicked(object sender, System.EventArgs e)
        {
            try
            {
                var contact = await Contacts.PickContactAsync();
                if (contact == null)
                    return;

                var info = new StringBuilder();
                info.AppendLine(contact.Id);
                info.AppendLine(contact.NamePrefix);
                info.AppendLine(contact.GivenName);
                info.AppendLine(contact.MiddleName);
                info.AppendLine(contact.FamilyName);
                info.AppendLine(contact.NameSuffix);
                info.AppendLine(contact.DisplayName);
                LabelInfo.Text = info.ToString();
            }
            catch (Exception)
            {

            }
        }

        async void Button_Clicked_1(object sender, System.EventArgs e)
        {
            try
            {
                var file = await FilePicker.PickAsync();

                if (file == null)
                    return;

                LabelInfoFile.Text = file.FileName;
            }
            catch (Exception)
            {

            }
        }
        async void Button_Clicked_2(object sender, System.EventArgs e)
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location == null)
                {
                    location = await Geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Medium,
                        Timeout = TimeSpan.FromSeconds(30),
                    });

                }
                if (location == null)
                {
                    LabelInfoLocation.Text = "no Geolocation";
                }

                else
                    LabelInfoLocation.Text = $"{location.Latitude} {location.Longitude}";
            }
            catch (Exception)
            {

            }
        }
    }
}