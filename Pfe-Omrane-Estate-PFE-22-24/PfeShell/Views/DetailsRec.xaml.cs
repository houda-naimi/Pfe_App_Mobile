using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PfeShell.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DetailsRec : ContentPage
    {
        public DetailsRec()
        {
            InitializeComponent();
        }
        Models.Reclamation2 _rec;
        public DetailsRec(Models.Reclamation2 rec)
        {
            InitializeComponent();

            Title = "Détails";
            _rec = rec;
            idRec.Text = rec.nomClient.ToString();
            nomClient.Text = rec.catRec.ToString();
            TypeRec.Text = rec.dateRec.ToString("ddd.dd/MMMM/yyyy");
            adresse.Text = rec.adresse;
            descriptionRec.Text = rec.description;
            idRec.Focus();

        }
    }
}