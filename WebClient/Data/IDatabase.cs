using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Models;

namespace WebClient.Data
{
    public interface IDatabase
    {
        #region Добавление данных
        bool AddOrder(OrderModel model);

        bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName);

        void AddService(ServiceModel model);
        #endregion

        OrdersListModel GetAllOrders();

        OrdersListModel GetOrders(string filterName, int startOffset, int endOffset);

        ServiceModel GetService(int id);

        OrdersListModel GetOrdersByYesterday();

        OrdersListModel GetOrdersByWeek();

        ModifyOrderModel GetOrder(int id);

        ProjectModel[] GetAllProjects();

        ProjectModel GetProject(int id);

        ServiceModel[] GetAllServices();

        bool EditProject(ProjectModel model, string contentType, Stream fileStream, string fileName);

        void UpdateOrder(UpdateOrderModel model);

        void DeleteProject(int id);

        void UpdateService(ServiceModel model);

        void DeleteService(int id);

        void AddArticle(ArticleModel model, string contentType, Stream fileStream, string fileName);

        ArticleModel[] GetAllArticles();

        void DeleteArticle(int id);

        ArticleModel FindArticle(int id);

        void UpdateArticle(ArticleModel model, string contentType, Stream stream, string fileName);
    }
}
