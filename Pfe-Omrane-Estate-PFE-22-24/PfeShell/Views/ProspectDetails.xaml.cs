using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectDetails : ContentPage,INotifyPropertyChanged
    {
        YesS.Tier tier= new YesS.Tier();
        public ObservableCollection<ProspectionByProspect> ListProspections;
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        public ObservableCollection<ProspectionByProspect> listProspections
        {
            get { return ListProspections; }

            set
            {
                ListProspections = value;
                OnPropertyChanged();
            }
        }
        public ProspectDetails(YesS.Tier prospectmodel)
        {
            InitializeComponent();
            tier = prospectmodel;
            //admTitle.Text = c.admti;
            //admTitle.Text = tier.Code;
            firstName.Text = tier.FirstName;
            lastName.Text = tier.LastName;
            mobile.Text = tier.Mobile;
            Email.Text = tier.Email;
            BindingContext = this;
            GetprospectionDetailsByProspect();

        }
        private async void modifier_Clicked(object sender, EventArgs e)
        {
            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = await yesService0.GetProspectByNumberAsync(Authentification.licenseKey, tier.Mobile);
                foreach (var item in res)
                {
                    tier.AdmTierTitleId = item.AdmTierTitleId;
                    tier.LastName = item.LastName;
                    tier.FirstName = item.FirstName;
                    tier.Mobile = item.Mobile;
                    tier.Email = item.Email;
                    tier.Code = item.AdmTierTitleCode;
                    tier.PKey = item.PKey;

                }
                //var pro = await yesService0.GetProspectionListAsync(Authentification.licenseKey, tier.PKey, Authentification.user.Session);
                var pro = await yesService0.GetProspectionsByProspectAsync(tier.PKey, Authentification.licenseKey, Authentification.user.Session);
            }
            catch (Exception ex)
            {

            }

            await Navigation.PushAsync(new ProspectInformation(tier));
        }
        public async Task<ObservableCollection<ProspectionByProspect>> GetprospectionDetailsByProspect()
        {   
            listProspections= new ObservableCollection<ProspectionByProspect>();
            //string categorieName = "Non Disponible";
            //string typeName = "Non Disponible";
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var prospectkey = await yesService0.GetProspectByNumberAsync(Authentification.licenseKey, tier.Mobile);
            var tranche = await yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            

            foreach (var itemprospectkey in prospectkey)
            {
                var res = await yesService0.GetProspectionsByProspectAsync( itemprospectkey.PKey, Authentification.licenseKey, Authentification.user.Session);
                
                var trancheid = new Guid();
                foreach (var itemprospection in res)
                { 
                    var Prospection = new ProspectionByProspect();
                    Prospection.ProspectionId = itemprospection.PKey;
                    foreach (var itemcfgtranches in tranche.CfgTranches)
                    {
                        if (itemprospection.StkHierarchyId == itemcfgtranches.PKey)
                        {
                            trancheid = (Guid)itemprospection.StkHierarchyId;
                             Prospection.TrancheName = itemcfgtranches.Description;
                             Prospection.DateProspection = itemprospection.DocDate;
                             Prospection.TrancheId = itemprospection.StkHierarchyId;
                        }
                    }
                   // var resType = await yesService0.GetAvailableDetailsByTrancheAsync(Authentification.licenseKey, trancheid);
                   /* foreach (var itemtype in res.Types)
                    {
                        foreach (var restype in resType.Types)
                        {
                            if (restype.PKey == itemtype.StkItemTypeId)
                            {
                                typeName = restype.Description;
                            }
                        }
                    }
                    foreach (var itemcategorie in res.Categories)
                    {
                        foreach (var rescat in resType.Categories)
                        {
                            if (rescat.PKey == itemcategorie.StkItemCategoryId)
                            {
                                categorieName = rescat.Description;
                            }
                        }
                    }
                    Prospection.CategorieName = categorieName;
                    Prospection.TypeName = typeName;*/
                    listProspections.Add(Prospection);
                }
            }

            ListProspection.ItemsSource = listProspections;
            return listProspections;
        }

        private async void ajouter_prospection(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ProspectionInformation(new YesS.AddProspectionModel() { Tier = tier}));
        }

       

        private async void action_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new Agenda());
        }

       /* private async void ImageButton_Clicked(object sender, EventArgs e)
        {
            var btn = (ImageButton)sender;
            var pro = (ProspectionByProspect)btn.BindingContext;
            YesS.AddProspectionModel prospection = new YesS.AddProspectionModel();
            Prospection prospection1 = new Prospection();
            prospection1.PKey = pro.ProspectionId;
            prospection1.StkHierarchyId = pro.TrancheId;
            prospection1.CfgProspectId = tier.PKey;
            prospection.Prospection = prospection1;
            prospection.Tier = tier;           
           await Navigation.PushAsync(new ProspectionInformation(prospection));
        }*/
        
    }
}