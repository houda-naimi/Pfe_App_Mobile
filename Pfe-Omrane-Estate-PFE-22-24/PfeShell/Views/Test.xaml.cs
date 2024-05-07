using PfeShell.Models;
using Plugin.Messaging;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Test : ContentPage, INotifyPropertyChanged
    {
        private ObservableCollection<EmployeeTest> employeeCollection;
        public ObservableCollection<EmployeeTest> EmployeeCollection
        {
            get { return employeeCollection; }
            set { employeeCollection = value; }
        }
        public Test()
        {
            InitializeComponent();
            employeeCollection = new ObservableCollection<EmployeeTest>();
            employeeCollection.Add(new EmployeeTest() {  Name = "John" });
            employeeCollection.Add(new EmployeeTest() {  Name = "Justin" });
            employeeCollection.Add(new EmployeeTest() {  Name = "Jerome" });
            employeeCollection.Add(new EmployeeTest() { Name = "Jessica" });
            employeeCollection.Add(new EmployeeTest() {  Name = "Victoria" });
            BindingContext = this;
           
        }
        
    }
}