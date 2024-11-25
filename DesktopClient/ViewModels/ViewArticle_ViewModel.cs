using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;
using DesktopClient.UserControls;
using System.Windows.Media.Imaging;
using System.ComponentModel;

namespace DesktopClient.ViewModels
{
    public class ViewArticle_ViewModel
    {
        private ViewArticleControl parentWnd;
        private ArticleModel model;
        private BitmapImage image;

        public event PropertyChangedEventHandler PropertyChanged;

        public static ViewArticle_ViewModel Create(ArticleModel model)
        {
            ViewArticle_ViewModel viewModel = new ViewArticle_ViewModel(model);
            viewModel.ParentWnd = new ViewArticleControl();
            viewModel.ParentWnd.DataContext = viewModel;
            return viewModel;
        }

        public ViewArticle_ViewModel(ArticleModel model)
        {
            this.model = model;
        }

        public ViewArticleControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;

                Image = new BitmapImage();
                Image.BeginInit();
                Image.UriSource = new Uri(model.ArticleImageFileName);
                Image.EndInit();

                parentWnd.text.AppendText(model.ArticleText);
            }
        }

        public string Title => model.ArticleCaption;

        public string Text => model.ArticleText;

        public BitmapImage Image
        {
            get => image;
            set
            {
                image = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Image)));
            }
        }
    }
}
