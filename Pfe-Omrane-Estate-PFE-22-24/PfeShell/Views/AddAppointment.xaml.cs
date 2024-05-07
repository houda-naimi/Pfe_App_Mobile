using Newtonsoft.Json;
using PfeShell.Models;
using PfeShell.ViewModels;
using System;
using Syncfusion.SfCalendar.XForms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Syncfusion.XForms.Buttons;
using System.Globalization;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAppointment : ContentPage, INotifyPropertyChanged
    {
        CalendarInlineEvent app = new CalendarInlineEvent();
        private ObservableCollection<Color> colors;
        private object selectedItem;
        private Color selectedColor;
        public event PropertyChangedEventHandler PropertyChanged;
        public CalendarEventCollection CalendarInlineEvents { get; set; } = new CalendarEventCollection();
        public AddAppointment(DateTime c)
        {
            InitializeComponent();

            datePicker.MinimumDate = new DateTime(1970, 1, 1);
            datePicker.MaximumDate = new DateTime(2030, 1, 1);

            datePicker.Date = c;
            timePicker.Time = DateTime.Now.TimeOfDay;
            Colors = new ObservableCollection<Color>();
            Colors.Add(Color.Green);
            
            Colors.Add(Color.Red);

            this.BindingContext = this;
        }
        public AddAppointment(CalendarInlineEvent appointment)
        {
            InitializeComponent();
            app = appointment;
            datePicker.MinimumDate = new DateTime(1970, 1, 1);
            datePicker.MaximumDate = new DateTime(2030, 1, 1);
            title.Text = appointment.Subject;
            datePicker.Date = appointment.StartTime;
            timePicker.Time = appointment.StartTime.TimeOfDay;
            timePickerE.Time = appointment.EndTime.TimeOfDay;   
            Colors = new ObservableCollection<Color>();
            Colors.Add(Color.Green);
            Colors.Add(Color.Fuchsia);
            Colors.Add(Color.Red);

            this.BindingContext = this;
        }
        public ObservableCollection<Color> Colors
        {
            get
            {
                return colors;
            }
            set
            {
                colors = value;
                OnPropertyChanged();
            }
        }
        public object SelectedItem
        {
            get
            {
                return selectedItem;
            }
            set
            {
                selectedItem = value;
                OnPropertyChanged("SelectedItem");
            }
        }
        public Color SelectedColor
        {
            get
            {
                return selectedColor;
            }
            set
            {
                selectedColor = value;
                OnPropertyChanged("SelectedColor");
            }
        }

        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }


        async void Done_Clicked(object sender, EventArgs e)
        {

            AppointmentModel a = new AppointmentModel();
            if (string.IsNullOrWhiteSpace(title.Text))
            {
                await DisplayAlert("Error", "Please enter title", "OK");
                return;
            }
          var  verif = await App.eventTable.GetSpecificAction(app.Subject, app.StartTime.ToString());
            if (verif != null)
            {
                a.Id = verif.Id;
                a.Subject = title.Text;
                a.StartTime = datePicker.Date.Add(timePicker.Time).ToString();
                a.EndTime = datePicker.Date.Add(timePickerE.Time).ToString();
                a.Color = selectedColor.ToHex();
            }

            else
                
            {
                    a.Subject = title.Text;
                    a.StartTime = datePicker.Date.Add(timePicker.Time).ToString();
                    a.EndTime = datePicker.Date.Add(timePickerE.Time).ToString();
                    a.Color = selectedColor.ToHex();
            }

            await App.eventTable.AddEvent(a);


            await Navigation.PushAsync(new Agenda());
        }
    }
}
