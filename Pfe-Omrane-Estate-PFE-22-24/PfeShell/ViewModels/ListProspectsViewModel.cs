using MvvmHelpers;
using MvvmHelpers.Commands;
using PfeShell.Models;
using PfeShell.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.UI.Views;
using YesS;

namespace PfeShell.ViewModels
{
    public class ListProspectsViewModel : INotifyPropertyChanged
    {
        LayoutState _currentState = LayoutState.Loading;


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

        public static int l = 2;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
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

        private bool _isBusy1;
        public bool IsBusy1
        {
            get
            {
                return _isBusy1;
            }
            set
            {
                _isBusy1 = value;
                OnPropertyChanged("IsBusy1");
            }
        }
        public static int s=2;


        public ObservableCollection<YesS.Tier> ProspectsList = new ObservableCollection<YesS.Tier>();
        public ObservableCollection<YesS.Tier> prospectsList
        {
            get { return ProspectsList; }

            set
            {
                ProspectsList = value;
                OnPropertyChanged();
            }
        }
        public AsyncCommand load { get; set; }
       
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand LoadMoreCommand { get; set; }
        public ListProspectsViewModel()
        {
            LoadDataAsync();
            RefreshCommand = new AsyncCommand(refresh);
            LoadMoreCommand = new AsyncCommand(LoadMore);
        }

        

        public async Task<ICollection<Prospect>> GetSearchViewsAsync(int i)
        {


            ICollection<Prospect> c = new List<Prospect>();
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
               c = await yesService0.GetProspectsAsync(i, Authentification.licenseKey, Authentification.user.Session);
                
            }
            catch (Exception)
            {
                string ch = "";
                string ch1 = "Erreur lors de chargement de la page";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);
            }
            return c;
        }

        public async Task<ObservableCollection<YesS.Tier>> LoadDataAsync()
        {
          
            try
            {
                CurrentState = LayoutState.Loading;
                await System.Threading.Tasks.Task.Delay(2000);

                prospectsList.Clear();
                var cc = await GetSearchViewsAsync(1);
                foreach (var c in cc)
                    prospectsList.Add(new YesS.Tier()
                    {
                        Email = c.Email,
                        LastName = c.LastName,
                        Mobile = c.Mobile,
                        FirstName = c.FirstName,
                        AdmTierTitleId = c.AdmTierTitleId,
                    });
            }
            catch (Exception ex)
            {
            }
            CurrentState = LayoutState.None;
            return prospectsList;

        }
        public async Task<ObservableCollection<YesS.Tier>> LoadMoreDataAsync(int i)
        {
           
         
                try
                {
                    
                    var cc = await GetSearchViewsAsync(i);
                    foreach (var c in cc)
                        prospectsList.Add(new YesS.Tier()
                        {
                            Email = c.Email,
                            LastName = c.LastName,
                            Mobile = c.Mobile,
                            FirstName = c.FirstName,
                            AdmTierTitleId = c.AdmTierTitleId,
                        });
                }
                catch (Exception ex)
                {
                }
              
           
       
            return prospectsList;
        }

        async System.Threading.Tasks.Task refresh()
        {
            if (IsBusy == false) { return; }
            IsBusy = true;
            
            await LoadDataAsync();
            IsBusy = false;

        }

        async System.Threading.Tasks.Task LoadMore()
        {
            if(IsBusy1 == true) { return; }
            IsBusy1 = true;
            
            await LoadMoreDataAsync(l);
            l+=1;
            IsBusy1 = false;
           
        }

      public async Task<ObservableCollection<YesS.Tier>> Search(string MotRecherche)
        {   ObservableCollection<YesS.Tier> prospectsListSearch = new ObservableCollection<Tier>();
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var c = await yesService0.SearchTierAsync(MotRecherche, Authentification.licenseKey, Authentification.user.Session);
                //prospectsList.Clear();
                foreach (var cc in c)
                    prospectsListSearch.Add(new YesS.Tier()
                    {
                        Email = cc.Email,
                        LastName = cc.LastName,
                        Mobile = cc.Mobile,
                        FirstName = cc.FirstName,
                        AdmTierTitleId = cc.AdmTierTitleId,
                    });
            }
            catch (Exception)
            {
                string ch = "Aucune résultats trouvée";
                string ch1 = "";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);
                return prospectsList;

            }
            return prospectsListSearch;
        }
    }
}
