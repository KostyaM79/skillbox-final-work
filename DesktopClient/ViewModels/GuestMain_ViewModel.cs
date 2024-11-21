using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows;
using DesktopClient.General;
using DesktopClient.UserControls;
using System.Windows.Controls;
using Services;
using Models;

namespace DesktopClient.ViewModels
{
    public class GuestMain_ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private GuestMainControl parentWnd;

        private RelayCommand formVisibleCmd;
        private RelayCommand createOrderCmd;

        private IOrderService service;

        public GuestMain_ViewModel(IOrderService service)
        {
            this.service = service;
        }


        public GuestMainControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                parentWnd.lastnameTextbox.TextChanged += OnTextChange;
                parentWnd.firstnameTextbox.TextChanged += OnTextChange;
                parentWnd.emailTextbox.TextChanged += OnTextChange;
                parentWnd.messageTextbox.TextChanged += OnTextChange;
            }
        }

        private Visibility formVisible = Visibility.Hidden;

        public Visibility FormVisible
        {
            get => formVisible;
            set
            {
                formVisible = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(FormVisible)));
            }
        }

        public RelayCommand FormVisible_Cmd
        {
            get
            {
                return formVisibleCmd ?? (formVisibleCmd = new RelayCommand(obj =>
                {
                    FormVisible = Visibility.Visible;
                }));
            }
        }

        public RelayCommand CreateOrder_Cmd
        {
            get
            {
                return createOrderCmd ?? (createOrderCmd = new RelayCommand(obj =>
                {
                    if(service.Add(new OrderModel()
                    {
                        FirstName = parentWnd.firstnameTextbox.Text,
                        LastName = parentWnd.lastnameTextbox.Text,
                        Email = parentWnd.emailTextbox.Text,
                        Message = parentWnd.messageTextbox.Text
                    }))
                    {
                        parentWnd.firstnameTextbox.Text = string.Empty;
                        parentWnd.lastnameTextbox.Text = string.Empty;
                        parentWnd.emailTextbox.Text = string.Empty;
                        parentWnd.messageTextbox.Text = string.Empty;
                        FormVisible = Visibility.Hidden;
                    }
                }));
            }
        }


        private void OnTextChange(object sender, TextChangedEventArgs args)
        {
            if(!string.IsNullOrEmpty(parentWnd.firstnameTextbox.Text) &&
                !string.IsNullOrEmpty(parentWnd.lastnameTextbox.Text) &&
                !string.IsNullOrEmpty(parentWnd.emailTextbox.Text) &&
                !string.IsNullOrEmpty(parentWnd.messageTextbox.Text))
            {
                parentWnd.formBtn.IsEnabled = true;
            }
            else parentWnd.formBtn.IsEnabled = false;
        }
    }
}
