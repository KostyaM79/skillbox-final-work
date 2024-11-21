using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.IO;
using System.Windows;
using System.Windows.Media;

namespace DesktopClient.General
{
    class ProjectControlBuilder
    {
        private Image image;
        private Button editBtn;
        private Button deleteBtn;
        private TextBlock title;
        private StackPanel btns;
        private Border titleBorder;
        private StackPanel content;
        private Border card;

        public ProjectControlBuilder AddImage(string url)
        {
            image = new Image();
            BitmapImage bitmap = new BitmapImage();

            bitmap.BeginInit();
            bitmap.UriSource = new Uri(url);
            bitmap.EndInit();
            image.Source = bitmap;
            return this;
        }

        public ProjectControlBuilder AddEditBtn(RelayCommand command, Stream iconFileStream, int projectId)
        {
            Image editIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = iconFileStream;
            iconBitmap.EndInit();
            editIconImg.Source = iconBitmap;

            editBtn = new Button();
            editBtn.Command = command;
            editBtn.CommandParameter = editBtn;
            editBtn.Content = editIconImg;
            editBtn.Tag = projectId;
            editBtn.Width = 24;
            editBtn.Height = 24;
            editBtn.Margin = new Thickness(0, 10, 10, 10);
            return this;
        }

        public ProjectControlBuilder AddDeleteBtn(RelayCommand command, Stream iconFileStream, int projectId)
        {
            Image deleteIconImg = new Image();
            BitmapImage iconBitmap = new BitmapImage();
            iconBitmap.BeginInit();
            iconBitmap.StreamSource = iconFileStream;
            iconBitmap.EndInit();
            deleteIconImg.Source = iconBitmap;

            deleteBtn = new Button();
            deleteBtn.Command = command;
            deleteBtn.CommandParameter = editBtn;
            deleteBtn.Content = deleteIconImg;
            deleteBtn.Tag = projectId;
            deleteBtn.Width = 24;
            deleteBtn.Height = 24;
            deleteBtn.Margin = new Thickness(0, 10, 10, 10);
            return this;
        }

        public ProjectControlBuilder AddTitle(string titleText)
        {
            title = new TextBlock();
            title.Text = titleText;
            title.FontSize = 16;
            return this;
        }

        public Border Build()
        {
            btns = new StackPanel();
            btns.Orientation = Orientation.Horizontal;

            titleBorder = new Border();
            titleBorder.Padding = new Thickness(5, 10, 5, 5);
            titleBorder.Background = new SolidColorBrush(Color.FromRgb(192, 192, 192));

            content = new StackPanel();
            content.Orientation = Orientation.Vertical;

            card = new Border();
            card.Width = 300;
            card.Margin = new Thickness(5, 0, 5, 10);

            btns.Children.Add(editBtn);
            btns.Children.Add(deleteBtn);

            titleBorder.Child = title;

            content.Children.Add(image);
            content.Children.Add(titleBorder);
            content.Children.Add(btns);

            card.Child = content;

            return card;
        }
    }
}
