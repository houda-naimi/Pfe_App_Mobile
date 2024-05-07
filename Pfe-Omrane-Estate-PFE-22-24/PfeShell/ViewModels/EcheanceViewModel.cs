using MvvmHelpers.Commands;
using PfeShell.Views;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Xamarin.CommunityToolkit.UI.Views;
using YesS;
using Task = System.Threading.Tasks.Task;

namespace PfeShell.ViewModels
{
    public class EcheanceViewModel :INotifyPropertyChanged
    {
        LayoutState _currentState = LayoutState.Loading;

        public static int l = 2;
        public event PropertyChangedEventHandler PropertyChanged;
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
        //public static int s = 2;
        public ObservableCollection<MobileUnPaidView> MobileUnPaidViews;
        public ObservableCollection<MobileUnPaidView> mobileUnPaidViews
        {
            get { return MobileUnPaidViews; }

            set
            {
                MobileUnPaidViews = value;
                OnPropertyChanged();
            }
        }
        public AsyncCommand load { get; set; }
        public AsyncCommand RefreshCommand { get; set; }
        public AsyncCommand LoadMoreCommand { get; set; }
        public EcheanceViewModel()
        {
            LoadDataAsync();
            RefreshCommand = new AsyncCommand(refresh);
            LoadMoreCommand = new AsyncCommand(LoadMore);
            
        }
        public async Task<ICollection<YesS.MobileUnPaidView>> GetSearchViewsAsync(int i)
        {


            ICollection<YesS.MobileUnPaidView> c = new List<YesS.MobileUnPaidView>();
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                 c = await yesService0.SearchUnpaidAsync(Authentification.licenseKey, Authentification.user.Session, "", i, "DocDate", true, 10);
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
        public async Task<ObservableCollection<YesS.MobileUnPaidView>> LoadDataAsync()
        {
            try
            {
                CurrentState = LayoutState.Loading;
                await System.Threading.Tasks.Task.Delay(2000);
                mobileUnPaidViews = new ObservableCollection<YesS.MobileUnPaidView>();
                var cc = await GetSearchViewsAsync(1);
                foreach (var c in cc)
                    mobileUnPaidViews.Add(new YesS.MobileUnPaidView()
                    {
                        DocDate = c.DocDate,
                        ComSettlementCategoryDescription = c.ComSettlementCategoryDescription,
                        AdmTierTitleCode = c.AdmTierTitleCode,
                        AmountRest=c.AmountRest,
                        CfgTrancheDescription=c.CfgTrancheDescription,
                        CfgTierDescription=c.CfgTierDescription,
                        ItemDescription=c.ItemDescription,
                        CfgCompanyDescription=c.CfgCompanyDescription,
                        CfgTierMobile=c.CfgTierMobile,

                    }) ;
            }
            catch (Exception ex)
            {
            }
            CurrentState = LayoutState.None;
            return mobileUnPaidViews;
        }
        public async Task<ObservableCollection<MobileUnPaidView>> LoadMoreDataAsync(int i)
        {
           
                try
                {
                
                var cc = await GetSearchViewsAsync(i);
                    foreach (var c in cc)
                        mobileUnPaidViews.Add(new MobileUnPaidView()
                        {
                            DocDate = c.DocDate,
                            ComSettlementCategoryDescription = c.ComSettlementCategoryDescription,
                            AdmTierTitleCode = c.AdmTierTitleCode,
                            AmountRest = c.AmountRest,
                            CfgTrancheDescription = c.CfgTrancheDescription,
                            CfgTierDescription = c.CfgTierDescription,
                            ItemDescription = c.ItemDescription,
                            CfgCompanyDescription = c.CfgCompanyDescription,
                            CfgTierMobile = c.CfgTierMobile,
                        });
                }
                catch (Exception ex)
                {
                }
            return mobileUnPaidViews;
        }
        async Task refresh()
        {
            if (IsBusy == false) { return; }
            IsBusy = true;

            await LoadDataAsync();
            IsBusy = false;

        }

        async Task LoadMore()
        {
            if (IsBusy1 == true) { return; }
            IsBusy1 = true;
            await LoadMoreDataAsync(l);
            l += 1;
            IsBusy1 = false;
        }
       /* public async Task<ObservableCollection<YesS.MobileUnPaidView>> Search(string MotRecherche)
        {
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var cc = await yesService0.SearchUnpaidAsync( Authentification.licenseKey, Authentification.user.Session, MotRecherche,null, MotRecherche,null,null);
                mobileUnPaidViews.Clear();
                foreach (var c in cc)
                    mobileUnPaidViews.Add(new MobileUnPaidView()
                    {
                        DocDate = c.DocDate,
                        ComSettlementCategoryDescription = c.ComSettlementCategoryDescription,
                        AdmTierTitleCode = c.AdmTierTitleCode,
                        AmountRest = c.AmountRest,
                        CfgTrancheDescription = c.CfgTrancheDescription,
                        CfgTierDescription = c.CfgTierDescription,
                        ItemDescription = c.ItemDescription,
                        CfgCompanyDescription = c.CfgCompanyDescription,
                        CfgTierMobile = c.CfgTierMobile,
                    });
            }
            catch (Exception)
            {


            }
            return mobileUnPaidViews;
        }*/

    }
}
