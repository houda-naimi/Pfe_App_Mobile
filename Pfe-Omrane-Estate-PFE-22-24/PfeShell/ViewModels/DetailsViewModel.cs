using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace PfeShell.ViewModels
{
   public class DetailsViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string name = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        private ObservableCollection<DossierReclamation> dossiers;
        public ObservableCollection<DossierReclamation> Dossiers
        {
            get { return dossiers; }
            set
            {
                dossiers = value;
                OnPropertyChanged();
            }
        }
        private DossierReclamation selectedDossier;
        public DossierReclamation SelectedDossier
        {
            get { return selectedDossier; }
            set
            {
                selectedDossier = value;
                OnPropertyChanged();
            }
        }
    }
}
