using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace DesktopClient.General
{
    public static class Buttons
    {
        public static Button GetEditButton(RelayCommand command, object tag)
        {
            FileStream fs = File.OpenRead("..\\..\\..\\icons\\edit.png");

            Image editIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = fs;
            iconBitmap.EndInit();
            editIconImg.Source = iconBitmap;

            Button editBtn = new Button();
            editBtn.Command = command;
            //editBtn.CommandParameter = editBtn;
            editBtn.Content = editIconImg;
            editBtn.Tag = tag;
            editBtn.Width = 24;
            editBtn.Height = 24;
            editBtn.Margin = new Thickness(0, 0, 10, 0);
            editBtn.VerticalAlignment = VerticalAlignment.Bottom;
            return editBtn;
        }

        public static Button GetDeleteButton(RelayCommand command, object tag)
        {
            FileStream fs = File.OpenRead("..\\..\\..\\icons\\delete.png");

            Button deleteBtn;
            Image deleteIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = fs;
            iconBitmap.EndInit();
            deleteIconImg.Source = iconBitmap;

            deleteBtn = new Button();
            deleteBtn.Command = command;
            //deleteBtn.CommandParameter = deleteBtn;
            deleteBtn.Content = deleteIconImg;
            deleteBtn.Tag = tag;
            deleteBtn.Width = 24;
            deleteBtn.Height = 24;
            deleteBtn.Margin = new Thickness(0, 0, 10, 0);
            deleteBtn.VerticalAlignment = VerticalAlignment.Bottom;
            return deleteBtn;
        }
    }
}
