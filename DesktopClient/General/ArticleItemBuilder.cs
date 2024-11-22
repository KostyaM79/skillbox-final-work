using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace DesktopClient.General
{
    public class ArticleItemBuilder
    {
        private TextBlock titleDate = null;
        private Image image = null;
        private TextBlock titleText = null;
        private TextBlock fragment = null;
        private Button editBtn = null;
        private Button deleteBtn = null;
        private StackPanel btns = null;

        public ArticleItemBuilder AddDate(DateTime date)
        {
            titleDate = new TextBlock();
            titleDate.Text = date.ToString("dd.MM.yyyy");
            titleDate.FontSize = 16;
            titleDate.Foreground = new SolidColorBrush(Color.FromRgb(128, 128, 128));
            titleDate.Margin = new Thickness(5, 0, 0, 5);
            return this;
        }

        public ArticleItemBuilder AddImage(string path)
        {
            image = new Image();
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.UriSource = new Uri(path);
            bitmap.EndInit();
            image.Source = bitmap;
            image.Height = 200;
            image.Margin = new Thickness(0, 0, 0, 10);
            image.Stretch = Stretch.Fill;
            return this;
        }

        public ArticleItemBuilder AddTitle(string text)
        {
            titleText = new TextBlock();
            titleText.Text = text;
            titleText.FontSize = 20;
            titleText.FontWeight = FontWeights.Bold;
            titleText.Padding = new Thickness(5);
            return this;
        }

        public ArticleItemBuilder AddTextFragment(string text)
        {
            fragment = new TextBlock();
            fragment.Text = text.Trim();
            fragment.FontSize = 16;
            fragment.TextAlignment = TextAlignment.Justify;
            fragment.Padding = new Thickness(5);
            fragment.MaxWidth = 300;
            fragment.TextWrapping = TextWrapping.Wrap;
            return this;
        }

        public ArticleItemBuilder AddEditBtn(RelayCommand command, Stream iconFileStream, int articleId)
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
            editBtn.Tag = articleId;
            editBtn.Width = 24;
            editBtn.Height = 24;
            editBtn.Margin = new Thickness(0, 0, 10, 0);
            editBtn.VerticalAlignment = VerticalAlignment.Bottom;
            return this;
        }

        public ArticleItemBuilder AddDeleteBtn(RelayCommand command, Stream iconFileStream, int articleId)
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
            deleteBtn.Tag = articleId;
            deleteBtn.Width = 24;
            deleteBtn.Height = 24;
            deleteBtn.Margin = new Thickness(0, 0, 10, 0);
            deleteBtn.VerticalAlignment = VerticalAlignment.Bottom;
            return this;
        }

        public Border Build()
        {
            if (editBtn != null || deleteBtn != null)
            {
                btns = new StackPanel();
                btns.Orientation = Orientation.Horizontal;
                btns.Children.Add(editBtn);
                btns.Children.Add(deleteBtn);
                btns.VerticalAlignment = VerticalAlignment.Bottom;
            }

            Border card = new Border();
            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions[1].Height = new GridLength(26);

            Grid.SetRow(btns, 1);

            card.Width = 300;
            StackPanel content = new StackPanel();

            card.Margin = new Thickness(8, 5, 8, 10);
            content.Orientation = Orientation.Vertical;
            btns.Orientation = Orientation.Horizontal;

            card.Child = grid;
            content.Children.Add(titleDate);
            content.Children.Add(image);
            content.Children.Add(titleText);
            content.Children.Add(fragment);

            grid.Children.Add(content);
            grid.Children.Add(btns);

            ClearFields();

            return card;
        }

        private void ClearFields()
        {
            titleDate = null;
            image = null;
            titleText = null;
            fragment = null;
            editBtn = null;
            deleteBtn = null;
            btns = null;
        }
    }
}
