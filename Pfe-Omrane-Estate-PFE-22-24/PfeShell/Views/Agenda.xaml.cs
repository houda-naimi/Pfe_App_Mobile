using PfeShell.Models;
using PfeShell.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Syncfusion.SfCalendar.XForms;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Agenda : ContentPage
    {
        

        public Agenda()
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjY3NzEyQDMyMzAyZTMyMmUzMFY3RFBPNmxGZ1lINEt2a0ZHZlhna3ZZY1p1aGpYOE1wRmpzTU5hd3lGZzQ9");
            InitializeComponent();
           
        }

        private async void addevent_Clicked(object sender, EventArgs e)
        {
            //  popupLayout.Show();
            var AddAppointment = new AddAppointment(calendar.SelectedDate.Value);
            await Navigation.PushAsync(AddAppointment);
            // await Shell.Current.GoToAsync(nameof(AddAppointment));
           

        }



        public async void Calendar_InlineItemTapped(object sender, InlineItemTappedEventArgs e)
        {
            var l = await App.eventTable.GetEvents();
            var c = e.InlineEvent;
            var identifiant = 0;
            foreach (var x in l)
            {
                if (x.StartTime == c.StartTime.ToString() && x.EndTime == c.EndTime.ToString() && x.Subject == c.Subject && x.Color ==c.Color.ToHex())
                {
                    identifiant = x.Id; 
                    break;

                }
            }
             
        
            var appointment = new CalendarInlineEvent
            {
                Subject = c.Subject,
                StartTime = c.StartTime,
                EndTime = c.EndTime,
                Color = c.Color,
             
              
            };
            string d = c.StartTime.ToString("dd/MMM/yyyy");
            string st= c.StartTime.ToString("hh:mm");
            string et = c.EndTime.ToString("hh:mm");
            var pop = new PopupAgenda(appointment);
            await AppShell.Current.Navigation.PushPopupAsync(pop, true);
            

            //await DisplayAlert(c.Subject, c.StartTime.ToString("dd/MMM/yyyy hh:mm"), "ok");
        }
     
        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            await App.yesService.LogDisconnectAsync(Authentification.user.Session);
            await AppShell.Current.GoToAsync($"///{nameof(Authentification)}");
        }
      
    }
}