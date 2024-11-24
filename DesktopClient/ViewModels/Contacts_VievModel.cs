using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.General;
using DesktopClient.UserControls;
using DesktopClient.Dialogs;
using DesktopClient.Services;

namespace DesktopClient.ViewModels
{
    public class Contacts_VievModel
    {
        private RelayCommand editCmd;

        public static Contacts_VievModel CreateContectsControl(IDesktopSocialsService service, bool viewMode = false)
        {
            Contacts_VievModel vievModel = new Contacts_VievModel();
            ContactsControl control = new ContactsControl();
            vievModel.ParentWnd = control;
            control.DataContext = vievModel;
            if (viewMode) control.contentGrid.Children.Remove(control.editBtn);
            return vievModel;
        }

        public ContactsControl ParentWnd { get; set; }

        public RelayCommand Edit_Cmd
        {
            get
            {
                return editCmd ?? (editCmd = new RelayCommand(obj =>
                {
                    
                    SocialsDialog dialog = new SocialsDialog();
                }));
            }
        }
    }
}
