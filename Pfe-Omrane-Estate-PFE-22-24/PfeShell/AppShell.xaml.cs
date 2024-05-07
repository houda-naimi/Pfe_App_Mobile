using System;
using Xamarin.Forms;
using PfeShell.Views;

namespace PfeShell
{
    public partial class AppShell : Xamarin.Forms.Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(ProspectInformation) , typeof(ProspectInformation));
            Routing.RegisterRoute(nameof(ProspectionInformation), typeof(ProspectionInformation));
            Routing.RegisterRoute(nameof(MoreProspectionInformation), typeof(MoreProspectionInformation));
            Routing.RegisterRoute(nameof(AddAppointment), typeof(AddAppointment));
            Routing.RegisterRoute(nameof(AffichReclamation), typeof(AffichReclamation));


        }

    }
}
