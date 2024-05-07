using MvvmHelpers.Commands;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using PfeShell.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Messaging;
using System.Windows.Input;
using Xamarin.Forms.Core;
using Xamarin.CommunityToolkit.UI.Views;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using PfeShell.ViewModels;

namespace PfeShell.Views
{
    [DesignTimeVisible(false)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EcheancePage : ContentPage
    {
        public List<YesS.MobileUnPaidView> unpaidList = new List<YesS.MobileUnPaidView>();
        public ObservableCollection<YesS.MobileUnPaidView> unpaidListSearch;



        public EcheancePage()

        {
            InitializeComponent();
            
           

        }

      


       private async void SearchList_TextChanged_1(object sender, TextChangedEventArgs e)
        {
            unpaidListSearch = new ObservableCollection<YesS.MobileUnPaidView>();
            var MotRecherche = search.Text.ToString();
            EcheanceViewModel v = new EcheanceViewModel();
            unpaidListSearch = await v.LoadDataAsync();
            var searchresult = unpaidListSearch.Where(c => c.CfgTierDescription.ToLower().Contains(MotRecherche.ToLower()));
            listEcheance.ItemsSource = searchresult;
        }
      
        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var ech = (YesS.MobileUnPaidView)btn.BindingContext;


            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSms)
            {
                smsMessenger.SendSms(ech.CfgTierMobile, "Bonjour "+ech.AdmTierTitleCode +" "+ ech.CfgTierDescription+" "+"Veuillez Payer le montant "+ech.AmountRest);
            }

        }
        private async Task OpenAnimation(View view, uint length = 250)
        {
            view.RotationX = -90;
            view.IsVisible = true;
            view.Opacity = 0;
            _ = view.FadeTo(1, length);
            await view.RotateXTo(0, length);
        }

        private async Task CloseAnimation(View view, uint length = 250)
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

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");
        }
    }
}