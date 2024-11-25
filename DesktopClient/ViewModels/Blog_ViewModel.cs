using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DesktopClient.UserControls;
using Services;
using Models;
using DesktopClient.Events;
using DesktopClient.General;
using System.Windows.Controls;
using DesktopClient.Dialogs;
using DesktopClient.Services;
using System.IO;
using System.Windows.Input;

namespace DesktopClient.ViewModels
{
    public class Blog_ViewModel
    {
        public event RequestingArticlesEventHandler RequestingArticle;

        /// <summary>
        /// Вызывается при необходимости обновить данные
        /// </summary>
        public event RequestingDataEventHandler RequestingData;

        /// <summary>
        /// Вызывается, когда получены новые данные
        /// </summary>
        public event ArticlesReceivedEventHandler DataReceived;

        private BlogControl parentWnd;
        private IDesktopArticlesService service;
        private ArticleModel[] articles;
        private bool viewMode = false;

        private RelayCommand addArticleCmd;

        public Blog_ViewModel(IDesktopArticlesService service)
        {
            this.service = service;
            RequestingData += OnRequestingData;
            DataReceived += OnDataReceived;
        }

        public Blog_ViewModel(IDesktopArticlesService service, bool viewMode) : this(service)
        {
            this.viewMode = viewMode;
        }

        /// <summary>
        /// Возвращает или задаёт ссылку на родительский элемент управления
        /// </summary>
        public BlogControl ParentWnd
        {
            get => parentWnd;
            set
            {
                parentWnd = value;
                if (viewMode) parentWnd.contantStackPanel.Children.Remove(parentWnd.addBtn);
                RequestingData?.Invoke(parentWnd, new RequestingDataEventArgs());
            }
        }

        public ArticleModel[] Articles
        {
            get => articles;
            set
            {
                articles = value;
                DataReceived?.Invoke(this, new ArticlesReceivedEventArgs(Articles));
            }
        }

        /// <summary>
        /// Инициирует обновление данных
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void OnRequestingData(object sender, RequestingDataEventArgs args)
        {
            Update();
        }


        private void OnDataReceived(object sender, ArticlesReceivedEventArgs args)
        {
            ShowData(args.Articles);
        }

        /// <summary>
        /// Обновляет данные в форме
        /// </summary>
        private void Update()
        {
            ArticleModel[] articles = service.GetAll();
            if (articles != null) Articles = articles;
        }


        private void ShowData(ArticleModel[] articles)
        {
            ParentWnd.articles.Children.Clear();

            foreach (ArticleModel temp in articles)
            {
                ArticleCard_ViewModel articleVm;

                if (viewMode)
                    articleVm = ArticleCard_ViewModel.CreateArticleCard(temp);
                //ParentWnd.articles.Children.Add(ArticleCard_ViewModel.CreateArticleCard(temp).ParentWnd);
                else
                    articleVm = ArticleCard_ViewModel.CreateArticleCard(temp, new RelayCommand(EditAction), new RelayCommand(DeleteAction));
                //ParentWnd.articles.Children.Add(ArticleCard_ViewModel.CreateArticleCard(temp, new RelayCommand(EditAction), new RelayCommand(DeleteAction)).ParentWnd);

                articleVm.ParentWnd.articleLnk.MouseLeftButtonDown += OnArticleCardClick;
                ParentWnd.articles.Children.Add(articleVm.ParentWnd);
            }
        }

        private void EditAction(object o)
        {
            ArticleViewCard viewCard = o as ArticleViewCard;
            ArticleCard_ViewModel viewModel = viewCard.DataContext as ArticleCard_ViewModel;

            ArticleDialog_ViewModel viewModel1 = new ArticleDialog_ViewModel(service);
            ArticleDialog dialog = new ArticleDialog(viewModel1);
            viewModel1.Edit(viewModel);
            if (dialog.ShowDialog().Value)
                RequestingData?.Invoke(this, new RequestingDataEventArgs());
        }

        private void DeleteAction(object o)
        {
            ArticleViewCard viewCard = o as ArticleViewCard;
            ArticleCard_ViewModel viewModel = viewCard.DataContext as ArticleCard_ViewModel;
            service.Delete(viewModel.Id);
            RequestingData?.Invoke(this, new RequestingDataEventArgs());
        }

        public RelayCommand AddArticle_Cmd
        {
            get
            {
                return addArticleCmd ?? (addArticleCmd = new RelayCommand(obj =>
                {
                    ArticleDialog_ViewModel viewModel = new ArticleDialog_ViewModel(service);
                    ArticleDialog dialog = new ArticleDialog(viewModel);
                    if (dialog.ShowDialog().Value)
                    {
                        RequestingData?.Invoke(this, new RequestingDataEventArgs());
                    }
                }));
            }
        }

        private void OnArticleCardClick(object sender, MouseButtonEventArgs args)
        {
            ArticleViewCard card = ((sender as StackPanel).Parent as Grid).Parent as ArticleViewCard;
            RequestingArticle?.Invoke(this, new RequestingArticlesEventArgs((card.DataContext as ArticleCard_ViewModel).Model));
        }
    }
}
