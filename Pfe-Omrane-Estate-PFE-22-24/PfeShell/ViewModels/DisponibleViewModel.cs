
using MvvmHelpers;
using MvvmHelpers.Commands;
using PfeShell.Models;
using PfeShell.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using YesS;
using Task = System.Threading.Tasks.Task;

namespace PfeShell.ViewModels
{
    public class DisponibleViewModel : INotifyPropertyChanged
    {
        LayoutState _currentState = LayoutState.Loading;

        private bool _isBusy;
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            set
            {
                _isBusy = value;
                OnPropertyChanged("IsBusy");
            }
        }
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public LayoutState CurrentState
        {
            set
            {
                if (_currentState != value)
                {
                    _currentState = value;
                    OnPropertyChanged(nameof(CurrentState));
                }
            }
            get => _currentState;
        }
        public ObservableCollection<immeuble> Detail;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<immeuble> detail
        {
            get { return Detail; }

            set
            {
                Detail = value;
                OnPropertyChanged();
            }
        }
        public AsyncCommand RefreshCommand { get; set; }
        public DisponibleViewModel()
        {
            RefreshCommand = new AsyncCommand(refresh);
            GetAvailable();
            
        }
        
        public async Task<ObservableCollection<immeuble>> GetAvailable()

        {
            detail = new ObservableCollection<immeuble>();
            immeuble immeuble = new immeuble();
            ObservableCollection<Categories> cat;
            

            try
            {
                CurrentState = LayoutState.Loading;
                await System.Threading.Tasks.Task.Delay(2000);
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = await yesService0.TrancheDetailsAsync(Authentification.user.Session, Authentification.licenseKey);
            
                //  yesService0.ItemsByCategoryAndTrancheAsync
                foreach (var x in res)
                {
                    immeuble = new immeuble();
                    cat = new ObservableCollection<Categories>();
                    immeuble.TrancheName = x.Description;
                    immeuble.TrancheGuid = x.PKey;
                    foreach (var y in x.Categories) 
                    {
                        cat.Add(new Categories
                        {
                            NameCategorie = y.Description,
                            ItemCount = y.Available,
                            CategorieGuid = y.PKey,
                            TrancheGuid = x.PKey
                    }) ;           
                    }
                    immeuble.categories = cat;
                    detail.Add(immeuble);
                }
            }
            catch (Exception)
            {
                string ch = "";
                string ch1 = "Erreur lors de chargement de la page";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);

            }
            CurrentState = LayoutState.None;

            return detail;
        }


        async System.Threading.Tasks.Task refresh()
        {
            if (IsBusy == false) { return; }
            IsBusy = true;
            await GetAvailable();
            IsBusy = false;

        }


    }
}
