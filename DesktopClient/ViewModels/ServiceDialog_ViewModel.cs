﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.Dialogs;
using Services;
using Models;
using DesktopClient.Services;
using DesktopClient.General;

namespace DesktopClient.ViewModels
{
    public class ServiceDialog_ViewModel
    {
        private RelayCommand okCmd;

        private IDesktopServicesService service;

        public ServiceDialog_ViewModel(IDesktopServicesService service)
        {
            this.service = service;
        }

        public ServiceDialog ParentWnd { get; set; }

        public string ServiceTitle { get; set; }

        public string ServiceDescr { get; set; }

        public RelayCommand Ok_Cmd
        {
            get
            {
                return okCmd ?? (okCmd = new RelayCommand(obj =>
                {
                    if (!string.IsNullOrEmpty(ServiceTitle) && !string.IsNullOrEmpty(ServiceDescr))
                    {
                        service.Add(new ServiceModel()
                        {
                            ServiceTitle = ServiceTitle,
                            ServiceDescr = ServiceDescr
                        });

                        ParentWnd.DialogResult = true;
                        ParentWnd.Close();
                    }
                }));
            }
        }
    }
}
