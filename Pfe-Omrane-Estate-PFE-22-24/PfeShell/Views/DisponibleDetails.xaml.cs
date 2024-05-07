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
    public partial class DisponibleDetails : ContentPage
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

        public Categories trancheCat = new Categories();

        public DisponibleDetails()
        {
            InitializeComponent();
        }
        public DisponibleDetails(Categories cat)
        {
            InitializeComponent();
            trancheCat = cat;
            GetAvailable();
        }


        public async Task<ObservableCollection<Item>> GetAvailable()

        {
            var ListItems = new ObservableCollection<Item>();


            try
            {
                System.Net.Http.HttpClient httpClient = new System.Net.Http.HttpClient();
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + Authentification.user.Token);
                YesService yesService0 = new YesService(App.yesService.BaseUrl, httpClient);
                var res = await yesService0.TrancheDetailsAsync(Authentification.user.Session, Authentification.licenseKey);

                var res1 = await yesService0.ItemsByCategoryAndTrancheAsync(trancheCat.CategorieGuid, trancheCat.TrancheGuid, Authentification.licenseKey);
                foreach (var item in res1)
                {

                    Item itemModel = new Item();
                    ObjectWithPKeyCodeAndDescription Type = new ObjectWithPKeyCodeAndDescription();
                    itemModel.Type = Type;
                    itemModel.Area = item.Area;
                    itemModel.SalePrice = item.SalePrice;
                    itemModel.Description = item.Description;
                    itemModel.ComSaleUnitCost = item.ComSaleUnitCost;
                    itemModel.ComSalePrice = item.ComSalePrice;
                    Type.Description = item.Type.Description;
                    itemModel.Type = Type;
                    ListItems.Add(itemModel);

                }
            }
            catch (Exception)
            {

            }
            listItem.ItemsSource = ListItems;
            return ListItems;
        }

    }
}