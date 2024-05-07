using MvvmHelpers;
using MvvmHelpers.Commands;
using PfeShell.Interfaces;
using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PfeShell.ViewModels
{
    public class ListeAppelTeleViewModel : INotifyPropertyChanged
    {
        public ObservableRangeCollection<Grouping<string, AppelTel>> ListeGroupRefresh { get; set; }

        public ObservableRangeCollection<Grouping<string, AppelTel>> ListeGroupLoad { get; set; } =
           new ObservableRangeCollection<Grouping<string, AppelTel>>();

        public AsyncCommand RefreshCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public bool isBusy;
        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged();
            }
        }
        public ObservableRangeCollection<Grouping<string, AppelTel>> ListeGroup;
        public ObservableRangeCollection<Grouping<string, AppelTel>> listeGroup
        {
            get { return ListeGroup; }

            set
            {
                ListeGroup = value;
                OnPropertyChanged();
            }
        }


        public ListeAppelTeleViewModel()
        {
            JournalAppel();
            RefreshCommand = new AsyncCommand(refresh);
        }

        public ObservableRangeCollection<Grouping<string,AppelTel>> JournalAppel()
        {
            listeGroup = new ObservableRangeCollection<Grouping<string,AppelTel>>();
            var contact =  DependencyService.Get<IJournalAppl>().ListeAppel();
            var group = from AppelTel in contact
                        group AppelTel by AppelTel.Date.ToString() into Group
                        select new Grouping<string, AppelTel>(Group.Key, Group);
            foreach (var g in group)
                listeGroup.Add(g);
            return listeGroup;
        }
        public ObservableRangeCollection<Grouping<string, AppelTel>> JournalAppelRefresh()
        {
            listeGroup = new ObservableRangeCollection<Grouping<string, AppelTel>>();
            var contact = DependencyService.Get<IJournalAppl>().ListeAppel();
            var group = from AppelTel in contact
                        group AppelTel by AppelTel.Date.ToString() into Group
                        select new Grouping<string, AppelTel>(Group.Key, Group);
            foreach (var g in group)
                listeGroup.Add(g);
            return listeGroup;
        }

        async Task refresh()
        {
            IsBusy = true;
            await Task.Delay(200);
            JournalAppelRefresh();
            IsBusy = false;
        }

    }
}
