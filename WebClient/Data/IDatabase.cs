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

        bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName, string token);

        void AddService(ServiceModel model, string token);
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

        bool EditProject(ProjectModel model, string contentType, Stream fileStream, string fileName, string token);

        void UpdateOrder(UpdateOrderModel model, string token);

        void DeleteProject(int id, string token);

        void UpdateService(ServiceModel model, string token);

        void DeleteService(int id, string token);

        void AddArticle(ArticleModel model, string contentType, Stream fileStream, string fileName, string token);

        ArticleModel[] GetAllArticles();

        void DeleteArticle(int id, string token);

        ArticleModel FindArticle(int id);

        void UpdateArticle(ArticleModel model, string contentType, Stream stream, string fileName, string token);
    }
}
