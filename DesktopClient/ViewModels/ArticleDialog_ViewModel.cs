using DesktopClient.Dialogs;
using DesktopClient.General;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Models;
using DesktopClient.Services;
using System.Windows.Input;
using Microsoft.Win32;
using System.IO;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Documents;

namespace DesktopClient.ViewModels
{
    public class ArticleDialog_ViewModel : INotifyPropertyChanged
    {
        private ArticleDialog parentWnd;
        private IDesktopArticlesService service;
        private string title;
        private string text;
        private BitmapImage image;

        private RelayCommand saveCmd;

        public event PropertyChangedEventHandler PropertyChanged;

        public ArticleDialog_ViewModel(IDesktopArticlesService service)
        {
            this.service = service;
        }

        public int Id { get; set; }

        public ArticleDialog ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                parentWnd.photo.MouseLeftButtonDown += OnMouseBtnDown;
            }
        }

        public string ArticleTitle
        {
            get => title;
            set
            {
                title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArticleTitle)));
            }
        }

        public string ArticleText
        {
            get => text;
            set
            {
                text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArticleText)));
            }
        }

        public string FileName { get; set; }

        public string FilePath { get; set; }

        public string FileExt { get; set; }

        public BitmapImage ArticleImage
        {
            get => image;
            set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ArticleImage)));
            }
        }

        public void Edit(ArticleCard_ViewModel viewModel)
        {
            Id = viewModel.Id;
            ArticleTitle = viewModel.Title;
            ParentWnd.text.AppendText(viewModel.Text);
            ArticleImage = viewModel.Image;
            saveCmd = new RelayCommand(EditAction);
        }

        public RelayCommand Save_Cmd
        {
            get
            {
                return saveCmd ?? (saveCmd = new RelayCommand(obj =>
                {
                    CreateAction(obj);
                }));
            }
        }

        /// <summary>
        /// Отправляет в БД данные для добавления
        /// </summary>
        /// <param name="o"></param>
        private void CreateAction(object o)
        {
            RichTextBox rt = ParentWnd.text;
            TextRange range = new TextRange(rt.Document.ContentStart, rt.Document.ContentEnd);

            service.Create(new ArticleModel()
            {
                ArticleCaption = ArticleTitle,
                ArticleText = range.Text,
                ArticleImageFileName = FileName
            }, $"image/{FileExt}", File.OpenRead(FilePath), FileName);

            ParentWnd.DialogResult = true;
            ParentWnd.Close();
        }

        private void EditAction(object o)
        {
            TextRange range = new TextRange(ParentWnd.text.Document.ContentStart, ParentWnd.text.Document.ContentEnd);

            ArticleModel model = new ArticleModel()
            {
                Id = Id,
                ArticleCaption = ArticleTitle,
                ArticleText = range.Text
            };

            FileStream fs = !string.IsNullOrEmpty(FilePath) ? File.OpenRead(FilePath) : null;

            service.Update(model, $"image/{FileExt}", fs, FileName);

            ParentWnd.DialogResult = true;
            ParentWnd.Close();
        }

        private void OnMouseBtnDown(object sender, MouseButtonEventArgs args)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "(jpg, jpeg)|*.jpg;*.jpeg";
            if (dialog.ShowDialog().Value)
            {
                FilePath = dialog.FileName;
                FileName = dialog.SafeFileName;
                FileExt = Path.GetExtension(dialog.SafeFileName);
                //ParentWnd.photo.img.Source = CreateBitmap(FilePath);
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = File.OpenRead(FilePath);
                bitmap.EndInit();
                ArticleImage = bitmap;
            }
        }

        private BitmapImage CreateBitmap(string path)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.BeginInit();
            bitmap.StreamSource = File.OpenRead(path);
            bitmap.EndInit();
            return bitmap;
        }
    }
}
