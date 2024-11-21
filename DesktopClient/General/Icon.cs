using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Controls;

namespace DesktopClient.General
{
    public class Icon
    {
        public Image GetPenIcon()
        {
            Image editIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = File.OpenRead("..\\..\\..\\icons\\edit.png");
            iconBitmap.EndInit();
            editIconImg.Source = iconBitmap;
            return editIconImg;
        }

        public Image GetBasketIcon()
        {
            Image editIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = File.OpenRead("..\\..\\..\\icons\\delete.png");
            iconBitmap.EndInit();
            editIconImg.Source = iconBitmap;
            return editIconImg;
        }

        public Image GetUploadIcon()
        {
            Image editIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = File.OpenRead("..\\..\\..\\icons\\upload.png");
            iconBitmap.EndInit();
            editIconImg.Source = iconBitmap;
            return editIconImg;
        }
    }
}
