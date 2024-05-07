using PfeShell.Models;
using PfeShell.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Syncfusion.SfCalendar.XForms;
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
    public partial class PopupAgenda : PopupPage
    {
        new CalendarInlineEvent appointment = new  CalendarInlineEvent();
        new AppointmentModel appointmentmodel = new AppointmentModel();
        public List<AppointmentModel> l = new List<AppointmentModel>();
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();
        public CalendarInlineEvent Action;
        public PopupAgenda( CalendarInlineEvent app)
        {
            appointment = app;
     
            InitializeComponent();
            titremsg.Text = app.Subject;
            date.Text = app.StartTime.ToString("dd/MMM/yyyy");
            StarTime.Text = app.StartTime.ToString("hh:mm");
            EndTime.Text = app.EndTime.ToString("hh:mm");
        }

        public async void Button_Clicked(object sender, EventArgs e)
        {   
            appointmentmodel = await App.eventTable.GetSpecificAction(appointment.Subject,appointment.StartTime.ToString());
            await App.eventTable.SupprimerItemAsync(appointmentmodel);
            await AppShell.Current.Navigation.PushAsync(new Agenda());
            await AppShell.Current.Navigation.PopPopupAsync(true);
        
        }

        private async void Button_Clicked_1(object sender, EventArgs e)
        {
          
            await AppShell.Current.Navigation.PushAsync(new AddAppointment(appointment));
            await AppShell.Current.Navigation.PopPopupAsync(true);

        }
    }
}