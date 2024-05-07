using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
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
    public partial class MessageBox : PopupPage
    {
        public MessageBox(string ch, string ch1)
        {
            InitializeComponent();
            titremsg.Text = ch;
            bodymsg.Text = ch1;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            AppShell.Current.Navigation.PopPopupAsync(true);
        }

       
    }
}