using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using PfeShell.Models;
using Syncfusion.SfCalendar.XForms;

using Xamarin.Forms;

namespace PfeShell.ViewModels
{
    public class CalendarViewModel : INotifyPropertyChanged
    {
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();
        public List<AppointmentModel> l = new List<AppointmentModel>();
        public CalendarInlineEvent action;
        public CalendarInlineEvent Action
        {
            get { return action; }

            set
            {
                action = value;
                OnPropertyChanged();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public CalendarViewModel()
        {
            AddAppointement();

            
        }
        


        public async void AddAppointement()
        {
            l = new List<AppointmentModel>();
            l = await App.eventTable.GetEvents();
            CalendarInlineEvents.Clear();
            foreach (var e in l)
            {
                Action = new CalendarInlineEvent();
                Action.StartTime = DateTime.Parse(e.StartTime.ToString());
                Action.EndTime = DateTime.Parse(e.EndTime.ToString());
                Action.Subject = e.Subject;
                Action.Color = Color.FromHex(e.Color);
                CalendarInlineEvents.Add(Action);
            }
           


        }
       


    }
}
