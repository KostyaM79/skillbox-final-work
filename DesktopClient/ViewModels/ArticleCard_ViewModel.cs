using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using DesktopClient.UserControls;
using DesktopClient.General;
using Models;
using System.Windows.Controls;

namespace DesktopClient.ViewModels
{
    public class ArticleCard_ViewModel
    {
        private ArticleModel model;

        public static ArticleCard_ViewModel CreateArticleCard(ArticleModel article, RelayCommand editAction = null, RelayCommand deleteAction = null)
        {
            ArticleCard_ViewModel viewModel = CreateArticleCard(article);
            FillData(viewModel, article, editAction, deleteAction);
            return viewModel;
        }

        private static ArticleCard_ViewModel CreateArticleCard(ArticleModel article)
        {
            ArticleViewCard control = new ArticleViewCard();
            ArticleCard_ViewModel viewModel = new ArticleCard_ViewModel(article);
            control.DataContext = viewModel;
            viewModel.ParentWnd = control;
            return viewModel;
        }

        public ArticleCard_ViewModel(ArticleModel model)
        {
            this.model = model;
        }

        private static void FillData(ArticleCard_ViewModel viewModel, ArticleModel article, RelayCommand editAction, RelayCommand deleteAction)
        {
            viewModel.Id = article.Id;

            viewModel.Date = article.ArticlePublishDate.ToString("dd.MM.yyyy г.");

            viewModel.Image = new BitmapImage();
            viewModel.Image.BeginInit();
            viewModel.Image.UriSource = new Uri(article.ArticleImageFileName);
            viewModel.Image.EndInit();

            viewModel.Title = article.ArticleCaption;

            viewModel.Text = article.ArticleText;

            if (editAction != null)
            {
                Button btn1 = Buttons.GetEditButton(editAction, article.Id);
                btn1.CommandParameter = viewModel.ParentWnd;
                viewModel.ParentWnd.btns.Children.Add(btn1);
            }

            if (deleteAction != null)
            {
                Button btn2 = Buttons.GetDeleteButton(deleteAction, article.Id);
                btn2.CommandParameter = viewModel.ParentWnd;
                viewModel.ParentWnd.btns.Children.Add(btn2);
            }
        }

        private ArticleViewCard parentWnd;

        public ArticleModel Model => model;

        public ArticleViewCard ParentWnd
        {
            get => parentWnd;
            set => parentWnd = value;
        }

        public int Id { get; set; }

        public string Date { get; set; }

        public BitmapImage Image { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }
    }
}
