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
using YesS;

namespace PfeShell.ViewModels
{
    public class DisponibleDetailsViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Item> listItems;
        public ObservableCollection<Item> ListItems
        {
            get { return listItems; }

            set
            {
                listItems = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }
        public immeuble trancheCat = new immeuble();
        public DisponibleDetailsViewModel(immeuble imm)
        {
            trancheCat = imm;
            GetAvailableDetails();
        }
        public DisponibleDetailsViewModel()
        {

        }
        public async Task<ObservableCollection<Item>> GetAvailableDetails()

        {
            ListItems = new ObservableCollection<Item>();


            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = await yesService0.TrancheDetailsAsync(Authentification.user.Session, Authentification.licenseKey);


                foreach (var y in trancheCat.categories)
                {
                    var res1 = await yesService0.ItemsByCategoryAndTrancheAsync(y.CategorieGuid, trancheCat.TrancheGuid, Authentification.licenseKey);
                    foreach (var item in res1)
                    {
                        var Type = new ObjectWithPKeyCodeAndDescription();
                        Item itemModel = new Item();
                        itemModel.Area = item.Area;
                        itemModel.SalePrice = item.SalePrice;
                        itemModel.Description = item.Description;
                        itemModel.ComSaleUnitCost = item.ComSaleUnitCost;
                        itemModel.ComSalePrice = item.ComSalePrice;
                        Type = item.Type;
                        itemModel.Type = Type;
                        ListItems.Add(itemModel);

                    }

                }

            }
            catch (Exception)
            {
                string ch = "";
                string ch1 = "Erreur lors de chargement de la page";
                var pop = new MessageBox(ch, ch1);
                await AppShell.Current.Navigation.PushPopupAsync(pop, true);
            }

            return ListItems;
        }




    }
}
