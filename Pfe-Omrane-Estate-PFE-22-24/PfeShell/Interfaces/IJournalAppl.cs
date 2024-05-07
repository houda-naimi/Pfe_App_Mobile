using PfeShell.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace PfeShell.Interfaces
{
    public interface IJournalAppl
    {
        ObservableCollection<AppelTel> ListeAppel();
    }
}
