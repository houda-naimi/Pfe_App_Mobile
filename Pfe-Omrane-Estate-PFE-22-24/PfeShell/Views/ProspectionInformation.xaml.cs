using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectionInformation : ContentPage
    {
        Guid modif = new Guid();

        public List<String> Tranche = new List<string>();
        public List<String> Kind = new List<string>();
        public List<String> Origin = new List<string>();

        YesS.Prospection prospectionAmodif = new YesS.Prospection();
        YesS.AddProspectionModel model = new YesS.AddProspectionModel();
        public ProspectionInformation(YesS.AddProspectionModel addprospectionmodel)
        {
           
            InitializeComponent();
         if (addprospectionmodel.Prospection == null)
            {
                pickerTranche.ItemsSource = GetTranche();
                pickerKind.ItemsSource = GetKind();
                pickerOrigin.ItemsSource = GetOrigin();
                model = addprospectionmodel;
            }
         else
            {
                prospectionAmodif = addprospectionmodel.Prospection;
                pickerTranche.ItemsSource = GetTranche();
                pickerTranche.SelectedItem = GetTrancheSpecific();
                pickerKind.ItemsSource = GetKind();
                pickerKind.SelectedItem = GetKindSpecific();
                pickerOrigin.ItemsSource = GetOrigin();
                pickerOrigin.SelectedItem = GetOriginSpecific();
                model = addprospectionmodel;
            }
        }
        

        public List<string> GetTranche()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.CfgTranches)
            {
                Tranche.Add(item.Description);
            }
            return Tranche;
        }
        public string GetTrancheSpecific()
        {
            string tranchename = "";
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.CfgTranches)
            {
                if(item.PKey == prospectionAmodif.StkHierarchyId) { tranchename = item.Description; break; }
            }
            return tranchename;
        }

        public List<string> GetKind()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.Kinds)
            {
                Kind.Add(item.Description);
            }
            return Kind;
        }
        public async Task<string> GetKindSpecific()
        {
            string kindname = "";
            var prospectionkindAmodif = new YesS.Prospection();
            prospectionkindAmodif = await GetSpecificProspection();
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.Kinds)
            {
                if (item.PKey == prospectionkindAmodif.ComProspectionKindId) { kindname = item.Description; break; }
            }
            return kindname;
        }


        public List<string> GetOrigin()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.Origins)
            {
                Origin.Add(item.Code);
            }
            return Origin;
        }
        public async Task<string> GetOriginSpecific()
        {
            string originname = "";
            var prospectionoriginAmodif = new YesS.Prospection();
            prospectionoriginAmodif = await GetSpecificProspection();
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.Origins)
            {
                if (item.PKey == prospectionoriginAmodif.ComProspectionOriginId) { originname = item.Description; break; }
            }
            return originname;
        }


        public async Task<Prospection> GetSpecificProspection()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
             //var res = await yesService0.GetProspectionListAsync(Authentification.licenseKey, prospectionAmodif.CfgProspectId,Authentification.user.Session);
            var res = await yesService0.GetProspectionsByProspectAsync(prospectionAmodif.CfgProspectId, Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res)
            {
                if (item.PKey == prospectionAmodif.PKey) 
                { 
                    prospectionAmodif = item;
                    break; 
                }
            }

            return prospectionAmodif;
        }








        private async void Retour_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private async void Ajouter_Clicked(object sender, EventArgs e)
        {
            if (pickerTranche.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une Tranche", "Ok");
            }
            if (pickerKind.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir degrés d'intéret", "Ok");
            }
            if (pickerOrigin.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une origine", "Ok");
            }
            if (pickerTranche.SelectedItem != null && pickerKind.SelectedItem != null && pickerOrigin.SelectedItem != null)
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
                var prospection = new YesS.Prospection();
                if (prospectionAmodif.PKey != modif)
                    {
                    foreach (var item in res.Result.CfgTranches)
                    {

                        if (item.Description == pickerTranche.SelectedItem.ToString())
                        {

                            prospectionAmodif.StkHierarchyId = item.PKey;

                        }
                    }
                    foreach (var item in res.Result.Kinds)
                    {
                        if (item.Description == pickerKind.SelectedItem.ToString())
                        {
                            prospectionAmodif.ComProspectionKindId = item.PKey;

                        }
                    }
                    foreach (var item in res.Result.Origins)
                    {

                        if (item.Code == pickerOrigin.SelectedItem.ToString())
                        {
                            prospectionAmodif.ComProspectionOriginId = item.PKey;
                        }
                    }
                    prospectionAmodif.DocDate = DateTime.Now;

                    model.Prospection = prospectionAmodif;

                    await yesService0.UpdateProspectionAsync(Authentification.licenseKey, Authentification.user.Session, model);
                    }
                    else
                        {
                             foreach (var item in res.Result.CfgTranches)
                             {

                                     if (item.Description == pickerTranche.SelectedItem.ToString())
                                     {
    
                                             prospection.StkHierarchyId = item.PKey;

                                     }
                             }                   

                            foreach (var item in res.Result.Kinds)
                            {
                                    if (item.Description == pickerKind.SelectedItem.ToString())
                                    {
                                    prospection.ComProspectionKindId = item.PKey;

                                    }
                            }
                            foreach (var item in res.Result.Origins)
                            {

                                if (item.Code == pickerOrigin.SelectedItem.ToString())
                                {
                                    prospection.ComProspectionOriginId = item.PKey;
                                }
                            }
                            prospection.DocDate = DateTime.Now;
                            model.Prospection = prospection;

                            await yesService0.AddProspectionAsync(Authentification.licenseKey, Authentification.user.Session, model);
                        }
               
                await Shell.Current.GoToAsync($"///{nameof(ListeProspects)}");
            }
        }
        private async void PlusDetails_Clicked(object sender, EventArgs e)
        {
            Guid TrancheId = new Guid();
            if (pickerTranche.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une Tranche", "Ok");
            }
            if (pickerKind.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir degrés d'intéret", "Ok");
            }
            if (pickerOrigin.SelectedItem == null)
            {
                await DisplayAlert("Error", "Choisir une origine", "Ok");
            }
            if (pickerTranche.SelectedItem != null && pickerKind.SelectedItem != null && pickerOrigin.SelectedItem != null)
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
                var prospection = new YesS.Prospection();

                if (prospectionAmodif.PKey != modif)
                {
                    foreach (var item in res.Result.CfgTranches)
                    {

                        if (item.Description == pickerTranche.SelectedItem.ToString())
                        {

                            prospectionAmodif.StkHierarchyId = item.PKey;

                        }
                    }
                    foreach (var item in res.Result.Kinds)
                    {
                        if (item.Description == pickerKind.SelectedItem.ToString())
                        {
                            prospectionAmodif.ComProspectionKindId = item.PKey;

                        }
                    }
                    foreach (var item in res.Result.Origins)
                    {

                        if (item.Code == pickerOrigin.SelectedItem.ToString())
                        {
                            prospectionAmodif.ComProspectionOriginId = item.PKey;
                        }
                    }
                    prospectionAmodif.DocDate = DateTime.Now;

                    model.Prospection = prospectionAmodif;
                    await Navigation.PushAsync(new MoreProspectionInformation(model, model.Prospection.StkHierarchyId));
                }

                else 
                {
                    foreach (var item in res.Result.Kinds)
                    {
                        if (item.Description == pickerKind.SelectedItem.ToString())
                        {
                            prospection.ComProspectionKindId = item.PKey;
                        }
                    }
                    foreach (var item in res.Result.Origins)
                    {

                        if (item.Code == pickerOrigin.SelectedItem.ToString())
                        {
                            prospection.ComProspectionOriginId = item.PKey;
                        }
                    }
                    foreach (var item in res.Result.CfgTranches)
                    {

                        if (item.Description == pickerTranche.SelectedItem.ToString())
                        {

                            TrancheId = item.PKey;
                            prospection.StkHierarchyId = item.PKey;
                        }
                    }
                    prospection.DocDate = DateTime.Now;
                    model.Prospection = prospection;
                    await Navigation.PushAsync(new MoreProspectionInformation(model, TrancheId));
                }


            }
                
            }
            
        }
}