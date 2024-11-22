using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.General;
using DesktopClient.UserControls;

namespace DesktopClient.ViewModels
{
    public class Contacts_VievModel
    {
        private RelayCommand editCmd;

        public static Contacts_VievModel CreateContectsControl()
        {
            Contacts_VievModel vievModel = new Contacts_VievModel();
            ContactsControl control = new ContactsControl();
            vievModel.ParentWnd = control;
            control.DataContext = vievModel;
            return vievModel;
        }

        public ContactsControl ParentWnd { get; set; }

        public RelayCommand Edit_Cmd
        {
            get
            {
                return editCmd ?? (editCmd = new RelayCommand(obj =>
                {
                    
                }));
            }
        }
    }
}
