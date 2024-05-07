
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MoreProspectionInformation : ContentPage, INotifyPropertyChanged
    {
        YesS.AddProspectionModel model = new YesS.AddProspectionModel();
        public List<string> budget = new List<string>();
        public List<string> ModePaiement = new List<string>();
        public List<string> Utilisation = new List<string>();
        public List<string> Article = new List<string>();
        public List<string> typecombo = new List<string>();
        public List<string> categcombo = new List<string>();
        public ObservableCollection<string> cats = new ObservableCollection<string>();
        private ObservableCollection<object> _selectedItem;
        private ObservableCollection<object> _selectedType;
        public event PropertyChangedEventHandler PropertyChanged;
        public void RaisePropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public ObservableCollection<object> SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                _selectedItem = value;
                RaisePropertyChanged("SelectedItem");
            }
        }
        public ObservableCollection<object> SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                RaisePropertyChanged("SelectedType");
            }
        }

        Guid? IdTranche = Guid.NewGuid();
        public MoreProspectionInformation(YesS.AddProspectionModel addprospectionmodel, Guid? TrancheId)
        {
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("NjY3NzEyQDMyMzAyZTMyMmUzMFY3RFBPNmxGZ1lINEt2a0ZHZlhna3ZZY1p1aGpYOE1wRmpzTU5hd3lGZzQ9");
            InitializeComponent();
            model = addprospectionmodel;
            comboBoxCat.DataSource = Getcategcombo(TrancheId);
            comboBoxTypes.DataSource = Gettypecombo(TrancheId);
            Budget.ItemsSource = Getbudget();
            modePaiement.ItemsSource = GetModePaiement();
            utilisation.ItemsSource = GetUtilisation();
            article.ItemsSource = GetArticle(TrancheId);
            IdTranche = TrancheId;
            BindingContext = this;
            
        }

        public List<string> Getbudget()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.ComBudgets)
            {
                budget.Add(item.Description);
            }
            return budget;
        }
        public List<string> GetModePaiement()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.ComPaymentMethods)
            {
                ModePaiement.Add(item.Description);
            }
            return ModePaiement;
        }
        public List<string> GetUtilisation()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.ComUtilizations)
            {
                Utilisation.Add(item.Description);
            }
            return Utilisation;
        }
        public List<string> GetArticle(Guid? trancheId)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            var tranche = yesService0.GetAvailableDetailsByTrancheAsync(Authentification.licenseKey, trancheId);
            foreach (var item in tranche.Result.Items)
            {
                Article.Add(item.Description);
            }

            return Article;
        }
        public List<string> Gettypecombo(Guid? trancheId)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            var tranche = yesService0.GetAvailableDetailsByTrancheAsync(Authentification.licenseKey, trancheId);
            
            foreach (var item in tranche.Result.Types)
            {
                typecombo.Add(item.Description);
            }
            return typecombo;
        }
        public List<string> Getcategcombo(Guid? trancheId)
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var tranche = yesService0.GetAvailableDetailsByTrancheAsync(Authentification.licenseKey, trancheId);

            foreach (var item in tranche.Result.Categories)
            {
                categcombo.Add(item.Description);
            }

            return categcombo;
        }

       
        private async void Retour_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void Ajouter_Clicked(object sender, EventArgs e)
        {
            if (Budget.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une Budget", "Ok");
            }
            if (modePaiement.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir mode de paiement", "Ok");
            }
            if (utilisation.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une utilisation", "Ok");
            }
            if (article.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir article", "Ok");
            }
            if (Budget.SelectedItem != null && article.SelectedItem != null
       && modePaiement.SelectedItem != null && utilisation.SelectedItem != null)
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
                var tranche = yesService0.GetAvailableDetailsByTrancheAsync(Authentification.licenseKey, IdTranche);
                foreach (var item in res.Result.ComBudgets)
                {
                    if (item.Description == Budget.SelectedItem.ToString())
                    {
                        //var ComBudget = new ComBudget();
                        model.Prospection.ComBudgetId = item.PKey;
                        //ComBudget.Description = item.Description;

                    }
                }
                foreach (var item in res.Result.ComPaymentMethods)
                {

                    if (item.Description == modePaiement.SelectedItem.ToString())
                    { 
                        //var ComPaymentMethod = new ComPaymentMethod();
                        model.Prospection.ComPaymentMethodId = item.PKey;
                        //ComPaymentMethod.Description = item.Description;
                        //model.Prospection.ComPaymentMethod = ComPaymentMethod;
                    }
                }
                foreach (var item in res.Result.ComUtilizations)
                {

                    if (item.Description == utilisation.SelectedItem.ToString())
                    {
                        //var ComUtilization = new ComUtilization();
                        model.Prospection.ComUtilizationId = item.PKey;
                        //ComUtilization.Description = item.Description;
                        //model.Prospection.ComUtilization = ComUtilization;
                    }
                }
                foreach (var item in tranche.Result.Items)
                {

                    if (item.Description == article.SelectedItem.ToString())
                    {
                        //var StkItem = new StkItem();
                        model.Prospection.StkItemId = item.PKey;
                       // StkItem.Description = item.Description;
                       // model.Prospection.StkItem = StkItem;
                    }
                }
                var Categories = new List<Guid>();
                foreach (var item in tranche.Result.Categories)
                {
                    foreach (var i in SelectedItem)
                    {
                        
                        if (i.ToString().Contains(item.Description))
                    {
                        
                        Categories.Add(item.PKey);
                        model.Categories = Categories;

                    }
                    }
                }
                var Types = new List<Guid>();
                foreach (var item in tranche.Result.Types)
                {
                    foreach (var i in SelectedType)
                    {
                        
                        if (i.ToString().Contains(item.Description))
                        {
                            
                            Types.Add(item.PKey);
                            model.Types = Types;
                        }
                    }
                }
               
            
                await yesService0.AddProspectionAsync(Authentification.licenseKey, Authentification.user.Session, model);

                await Shell.Current.GoToAsync($"///{nameof(ListeProspects)}");
            }
        }

    }
}