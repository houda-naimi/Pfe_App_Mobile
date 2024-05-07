using Newtonsoft.Json;
using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AjouterReclamation : ContentPage
    {

        List<String> listCat = new List<String>();
        public AjouterReclamation()
        {
            InitializeComponent();
            GetCategorieRec();

        }

        Models.Reclamation2 _rec = new Reclamation2();
        public AjouterReclamation(Models.Reclamation2 rec)
        {
            InitializeComponent();

            Title = "Modification";
            getReclamation(rec.idRec);

            idclientTxt.Text = rec.nomClient.ToString();
            idclientTxt.IsReadOnly = true;
            dateRecTxt.Date = rec.dateRec;
            descriptionTxt.Text = rec.description;
            adresseTxt.Text = rec.adresse;
            GetCategorieRec();
            pickerCatg.SelectedItem = rec.catRec;

        }

        public async Task<List<String>> GetCategorieRec()
        {
            var client = new HttpClient();
            string json = await client.GetStringAsync(App.cheminCat);
            var res = JsonConvert.DeserializeObject<List<CategorieRec>>(json);
            foreach (var c in res) 
            {
                listCat.Add(c.code);
            }
            pickerCatg.ItemsSource = listCat;
            return listCat;
        }
        
        public async Task<Reclamation2> getReclamation(int id)
        {
            //Reclamation2 rec = new Reclamation2();
            var client = new HttpClient();
            string json = await client.GetStringAsync(App.chemin.ToString() + "/" + id );
            _rec = JsonConvert.DeserializeObject<Reclamation2>(json);
            return _rec;
        }

        public async void ajouterClient(ClientRec clt) 
        {
            var jsonclt = JsonConvert.SerializeObject(clt);
            var contentclt = new StringContent(jsonclt, Encoding.UTF8, "application/json");

            HttpClient clientclt = new HttpClient();

            var resultclt = await clientclt.PostAsync(App.cheminClient, contentclt); 

          /*  if (resultclt.StatusCode == HttpStatusCode.Created)
            {
                await DisplayAlert("Ajout", "Reclamation ajoutée avec succès", "ok");

                await Navigation.PushAsync(new AffichReclamation());
            }*/
        }


        private async void Enregister_Clicked(object sender, EventArgs e)
        {
            if (pickerCatg.SelectedItem == null)
            {
                await DisplayAlert("Erreur", "sélectionner catégorie", "ok");
            }
            if (string.IsNullOrWhiteSpace(idclientTxt.Text)) 
            {
                await DisplayAlert("Erreur", "champ nom obligatoire", "ok");
            }
            if (string.IsNullOrWhiteSpace(adresseTxt.Text))
            {
                await DisplayAlert("Erreur", "champ adresse obligatoire", "ok");
            }
            if (string.IsNullOrWhiteSpace(numtelf.Text))
            {
                await DisplayAlert("Erreur", "champ téléphone obligatoire", "ok");
            }
            if (string.IsNullOrWhiteSpace(descriptionTxt.Text))
            {
                await DisplayAlert("Erreur", "champ description obligatoire", "ok");
            }

            if (pickerCatg.SelectedItem!=null && string.IsNullOrWhiteSpace(numtelf.Text)!=true &&
                string.IsNullOrWhiteSpace(adresseTxt.Text) != true && string.IsNullOrWhiteSpace(descriptionTxt.Text) != true && string.IsNullOrWhiteSpace(idclientTxt.Text) !=true) { 
            ClientRec clt = new ClientRec();
            clt.nom = idclientTxt.Text;
            clt.numtel = numtelf.Text;
            ajouterClient(clt);
            
            if (_rec.idRec != 0)
            {
                EditReclamation();
            }
            else
            {
                var clientcat = new HttpClient();
                string jsoncat = await clientcat.GetStringAsync(App.cheminCat);
                var res = JsonConvert.DeserializeObject<List<CategorieRec>>(jsoncat);
                // var res = JsonConvert.DeserializeObject<List<Reclamation2>>(jsoncat);
                var clientClient = new HttpClient();
                string jsonClient = await clientcat.GetStringAsync(App.cheminClient);
                var resClient = JsonConvert.DeserializeObject<List<ClientRec>>(jsonClient);

                Reclamation2 rec = new Reclamation2();
                foreach (var itemCat in res) 
                {
                    if (pickerCatg.SelectedItem.ToString() == itemCat.code)
                    {
                         rec.idCat = itemCat.idCat ;
                        rec.catRec = itemCat.code ;

                    }
                }
                 foreach (var itemClt in resClient)
                 {
                     if (numtelf.Text.ToString().ToLower().Equals(itemClt.numtel.ToLower()))
                     {
                         rec.idclient = itemClt.idclient;
                         rec.nomClient = itemClt.nom;
                     }
                 }
                rec.dateRec = dateRecTxt.Date;
                rec.description = descriptionTxt.Text;
                rec.adresse = adresseTxt.Text;

                /*ClientRec c = new ClientRec();
                c.nom = idclientTxt.Text;
                c.numtel = numtelf.Text;
                rec.idclientNavigation.nom = c.nom;
                rec.idclientNavigation.numtel = c.numtel;*/


                //idRec = Convert.ToInt16(idRec.Text),
                //nomClient = nomClient.Text,
                //adresse = adresse.Text,
                //typeRec = TypeRec.Text,
                //descriptionRec = descriptionRec.Text,





                var json = JsonConvert.SerializeObject(rec);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpClient client = new HttpClient();

                var result = await client.PostAsync(App.chemin, content);

                if (result.StatusCode == HttpStatusCode.Created)
                {
                    await DisplayAlert("Ajout", "Reclamation ajoutée avec succès", "ok");

                    await Navigation.PushAsync(new AffichReclamation());
                }

                
            }
            }

        }



        async void EditReclamation()
        {
            //// _rec.idRec = Convert.ToInt16(idRec.Text);
            // _rec.dateRec = Convert.ToDateTime(nomClient.Text);
            // _rec.description = TypeRec.Text;
            // _rec.adresse = adresse.Text;
            // _rec.description = descriptionRec.Text;



            //_rec.idclient
            //-rec.nomClient
            var clientcat = new HttpClient();
            string jsoncat = await clientcat.GetStringAsync(App.cheminCat);
            var res = JsonConvert.DeserializeObject<List<CategorieRec>>(jsoncat);
            foreach (var itemCat in res)
            {
                if (pickerCatg.SelectedItem.ToString() == itemCat.code)
                {
                    _rec.idCat = itemCat.idCat;
                    _rec.catRec = itemCat.code;

                }
            }
            _rec.dateRec = dateRecTxt.Date;
            _rec.description = descriptionTxt.Text;
            _rec.adresse = adresseTxt.Text;


            var json = JsonConvert.SerializeObject(_rec);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            HttpClient client = new HttpClient();

            bool confirmation = await DisplayAlert("Modification", "Voulez-vous confirmer la modification", "oui", "non");

            if (confirmation == true)
            {
                var result = await client.PutAsync(App.chemin.ToString() + "/" + _rec.idRec, content);

                if (result.IsSuccessStatusCode)
                {
                    await DisplayAlert("Modification", "Reclamation modifié avec succès", "ok");

                    await Navigation.PushAsync(new AffichReclamation());
                }
            }
            else
            {
                return;
            }

        }


    }
}
