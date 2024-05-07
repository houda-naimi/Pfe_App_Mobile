using PfeShell.Models;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using YesS;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProspectInformation : ContentPage
    {
        public List<String> title = new List<string>();
  
        YesS.Tier TierAmodif = new YesS.Tier();
        Guid modif = new Guid();

        public ProspectInformation(string name,string numtel,string prenom)
        {
            InitializeComponent();
            Nom.Text = name;
            numtelf.Text = numtel;
            Prenom.Text = prenom;
            picker.ItemsSource = GetTitles();
        }
        public ProspectInformation(YesS.Tier tierAmodif)
        {

            InitializeComponent();
            picker.ItemsSource = GetTitles();
            picker.SelectedItem = tierAmodif.Code;
            Nom.Text = tierAmodif.FirstName;
            numtelf.Text = tierAmodif.Mobile;
            Prenom.Text = tierAmodif.LastName;
            email.Text = tierAmodif.Email;
            TierAmodif = tierAmodif;

        }
        public ProspectInformation()
        {
            InitializeComponent();
            picker.ItemsSource = GetTitles();
        }

        public List<string> GetTitles()
        {
            System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
            httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
            YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
            var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
            foreach (var item in res.Result.AdmTitles)
            {
                title.Add(item.Description);
            }
            return title;
        }


        private async void PlusDetail_Clicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null)
            {
                await DisplayAlert("Erreur", "Champ Titre Manquée", "OK");

            }

            if (string.IsNullOrWhiteSpace(Prenom.Text))
            {
                await DisplayAlert("Erreur", "Champ Prénom Obligatoire", "OK");
            }

            if (string.IsNullOrWhiteSpace(Nom.Text))
            {
                await DisplayAlert("Erreur", "Champ Nom Obligatoire", "OK");
            }

            /* if (numtelf.Text is null)
             {
                 await DisplayAlert("Erreur", "Num Téléphone Non Valide", "OK");
             }*/
            if (NumTelValidation.IsNotValid || string.IsNullOrWhiteSpace(numtelf.Text))
            {
                await DisplayAlert("Erreur", "Num Téléphone Non Valide", "OK");
            }

            /* if (email.Text is null)
             {
                 await DisplayAlert("Erreur", "Email Non Valide", "OK");
             }*/
            if (EmailValidation.IsNotValid || string.IsNullOrWhiteSpace(email.Text))
            {
                await DisplayAlert("Erreur", "Email Non Valide", "OK");
            }

            if (picker.SelectedItem != null && string.IsNullOrWhiteSpace(Prenom.Text) != true
                && string.IsNullOrWhiteSpace(Nom.Text) != true
       && NumTelValidation.IsValid && EmailValidation.IsValid && string.IsNullOrWhiteSpace(email.Text) != true && string.IsNullOrWhiteSpace(numtelf.Text) != true)
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                YesS.Tier cfgTier = new YesS.Tier();
                var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);
                foreach (var item in res.Result.AdmTitles)
                {
                    if (item.Description == picker.SelectedItem.ToString())
                    {
                        //var AdmTierTitle = new AdmTierTitle();
                        cfgTier.AdmTierTitleId = item.PKey;
                        //AdmTierTitle.Description = item.Description;
                        //cfgTier.AdmTierTitle = AdmTierTitle;
                    }
                }
                cfgTier.FirstName = Nom.Text.ToString();
                cfgTier.LastName = Prenom.Text.ToString();
                cfgTier.Email = email.Text.ToString();
                cfgTier.Mobile = numtelf.Text.ToString();
                await Navigation.PushAsync(new ProspectionInformation(new YesS.AddProspectionModel() { Tier = cfgTier }));
            }
        }

        private async void Ajouter_Clicked(object sender, EventArgs e)
        {
            if (picker.SelectedItem == null)
            {
                await DisplayAlert("Erreur", "Champ Titre Manquée", "OK");

            }

            if (string.IsNullOrWhiteSpace(Prenom.Text))
            {
                await DisplayAlert("Erreur", "Champ Prénom Obligatoire", "OK");
            }

            if (string.IsNullOrWhiteSpace(Nom.Text))
            {
                await DisplayAlert("Erreur", "Champ Nom Obligatoire", "OK");
            }

            /* if (numtelf.Text is null)
             {
                 await DisplayAlert("Erreur", "Num Téléphone Non Valide", "OK");
             }*/
            if (NumTelValidation.IsNotValid || string.IsNullOrWhiteSpace(numtelf.Text))
            {
                await DisplayAlert("Erreur", "Num Téléphone Non Valide", "OK");
            }

            /* if (email.Text is null)
             {
                 await DisplayAlert("Erreur", "Email Non Valide", "OK");
             }*/
            if (EmailValidation.IsNotValid || string.IsNullOrWhiteSpace(email.Text))
            {
                await DisplayAlert("Erreur", "Email Non Valide", "OK");
            }

            if (picker.SelectedItem != null && string.IsNullOrWhiteSpace(Prenom.Text) != true
                && string.IsNullOrWhiteSpace(Nom.Text) != true
       && NumTelValidation.IsValid && EmailValidation.IsValid && string.IsNullOrWhiteSpace(email.Text) != true && string.IsNullOrWhiteSpace(numtelf.Text) != true)
            {
                try
                {
                    System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                    httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                    YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                    var res = yesService0.GetFieldsAsync(Authentification.licenseKey, Authentification.user.Session);

                    if (TierAmodif.PKey != modif)
                    {

                        foreach (var item in res.Result.AdmTitles)
                        {
                            if (item.Description == picker.SelectedItem.ToString())
                            {
                                //var AdmTierTitle = new AdmTierTitle();
                                TierAmodif.AdmTierTitleId = item.PKey;
                                //AdmTierTitle.Description = item.Description;
                                //cfgTier.AdmTierTitle = AdmTierTitle;
                            }
                        }
                        TierAmodif.FirstName = Nom.Text.ToString();
                        TierAmodif.LastName = Prenom.Text.ToString();
                        TierAmodif.Email = email.Text.ToString();
                        TierAmodif.Mobile = numtelf.Text.ToString();
                        bool confirmation = await DisplayAlert("Modification", "Voulez-vous confirmer la modification", "oui","non");
                        
                        if (confirmation == true) 
                        {
                            await yesService0.UpdateProspectionAsync(Authentification.licenseKey, Authentification.user.Session, new YesS.AddProspectionModel() { Tier = TierAmodif });
                        }
                        else
                        {
                            return;
                        }
                    }


                    else
                    {
                        YesS.Tier cfgTier = new YesS.Tier();
                        foreach (var item in res.Result.AdmTitles)
                        {
                            if (item.Description == picker.SelectedItem.ToString())
                            {
                                //var AdmTierTitle = new AdmTierTitle();
                                cfgTier.AdmTierTitleId = item.PKey;
                                //AdmTierTitle.Description = item.Description;
                                //cfgTier.AdmTierTitle = AdmTierTitle;
                            }
                        }
                        cfgTier.FirstName = Nom.Text.ToString();
                        cfgTier.LastName = Prenom.Text.ToString();
                        cfgTier.Email = email.Text.ToString();
                        cfgTier.Mobile = numtelf.Text.ToString();
                        var tst= await yesService0.AddProspectionAsync(Authentification.licenseKey, Authentification.user.Session, new YesS.AddProspectionModel() { Tier = cfgTier });
                        if (tst != null) 
                        {
                            await DisplayAlert("", "Ajout prospect avec succcès", "Ok");
                        }
                    }
                    await Shell.Current.GoToAsync($"//{nameof(ListeProspects)}");
                }
                catch
                {
                    
                }
            }
        }

    }
}
