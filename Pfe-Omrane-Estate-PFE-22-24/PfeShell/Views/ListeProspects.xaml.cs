using PfeShell.Models;
using PfeShell.ViewModels;
using Plugin.Messaging;
using Syncfusion.Buttons.XForms.SfChip;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListeProspects : ContentPage
    {
        

        public MvvmHelpers.Commands.Command SendSMS { get; }

        

        public ListeProspects()
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

        private async Task CloseAnimation(View view, uint length = 80)
        {
            _ = view.FadeTo(0, length);
            await view.RotateXTo(-90, length);
            view.IsVisible = false;
        }
        public async void MainExpander_Tapped(object sender, EventArgs e)
        {
            var expander = sender as Expander;
            var detailsView = expander.FindByName<Grid>("DetailsView");
            if (expander.IsExpanded)
            {
                await OpenAnimation(detailsView);

            }
            else
            {
                await CloseAnimation(detailsView);
            }


        }



        prospectModel selectedProspect;
        public prospectModel SelectedProspect
        {
            get => selectedProspect;
            set => selectedProspect = value;

        }
        async Task Selected(Object args)
        {
            var prospect = args as prospectModel;
            if (prospect == null)
                return;

            SelectedProspect = null;
            await DisplayAlert(" ", prospect.Mobile, "ok");
        }
      

        private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ProspectInformation));
        }

        private async void search_TextChanged(object sender, TextChangedEventArgs e)
        {
                 var MotRecherche = search.Text.ToString();
                ListProspectsViewModel v = new ListProspectsViewModel();
                listprospects.ItemsSource = await v.Search(MotRecherche);
        }

        private void callAction(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var pro = (YesS.Tier)btn.BindingContext;
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(pro.Mobile);
            }

        }

        private void sendMsgAction(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var pro = (YesS.Tier)btn.BindingContext;
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms(pro.Mobile, "Bonjour veulliez payer");
            }
        }



        private async void btnInfo(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var pro = (YesS.Tier)btn.BindingContext;
            await Navigation.PushAsync(new ProspectDetails(pro));
        }

        private async void ImageButton_Clicked_1(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProspectInformation());
        }
        private async void ToolbarItem_Clicked(object sender, System.EventArgs e)
        {
             await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");

        }

       
    }
}