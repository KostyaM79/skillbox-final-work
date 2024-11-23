using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.UserControls;

namespace DesktopClient.ViewModels
{
    public class SocialItem_ViewModel
    {
        public static SocialItem_ViewModel CreateSocialItem()
        {
            SocialItem_ViewModel viewModel = new SocialItem_ViewModel();
            SocialsItemControl control = new SocialsItemControl();
            control.DataContext = viewModel;
            viewModel.ParentWnd = control;
            return viewModel;
        }

        public SocialsItemControl ParentWnd { get; set; }


    }
}
