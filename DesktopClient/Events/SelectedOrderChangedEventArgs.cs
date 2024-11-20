using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DesktopClient.Events
{
    public class SelectedOrderChangedEventArgs : EventArgs
    {
        public OrderFullDataModel Order { get; set; }
    }
}
