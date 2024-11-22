using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.UserControls;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class AdditionPhoto_ViewModel
    {
        private AdditionPhotoControl parentWnd;

        public AdditionPhotoControl PhotoControl
        {
            get => parentWnd;
            set => parentWnd = value;
        }

        public double Width = 800;
    }
}
