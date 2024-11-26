using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.IO;
using Models;
using System.Net.Http.Headers;
using Exceptions;
using Microsoft.AspNetCore.Http;

namespace WebClient.Data
{
    public class Database : IDatabase
    {
        private readonly IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;

        public Database(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration;
            this.httpClientFactory = httpClientFactory;
        }

        #region Методы для Articles
        public void AddArticle(ArticleModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            fileContent.Add(new StringContent(model.ArticleCaption), "ArticleTitle");
            fileContent.Add(new StringContent(model.ArticleText), "ArticleText");
            fileContent.Add(streamContent, name: "file", fileName: fileName);

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Articles/Create", fileContent).Result;
        }

        public ArticleModel[] GetAllArticles()
        {
            HttpResponseMessage responseMessage = ReadAllAsync("Articles").Result;
            return responseMessage.Content.ReadFromJsonAsync<ArticleModel[]>().Result;
        }

        public void DeleteArticle(int id, string token)
        {
            _ = Delete("Articles", id, token).Result;
        }

        public ArticleModel FindArticle(int id)
        {
            HttpResponseMessage responseMessage = FindAsync("Articles", id).Result;
            return responseMessage.Content.ReadFromJsonAsync<ArticleModel>().Result;
        }

        public void UpdateArticle(ArticleModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            StreamContent streamContent;

            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            if (fileStream != null)
            {
                streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                fileContent.Add(streamContent, name: "file", fileName: fileName);
            }

            fileContent.Add(new StringContent($"{model.Id}"), "Id");
            fileContent.Add(new StringContent(model.ArticleCaption), "ArticleCaption");
            fileContent.Add(new StringContent(model.ArticleText), "ArticleText");

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Articles/Update", fileContent).Result;
        }
        #endregion

        #region Методы для Orders
        public bool AddOrder(OrderModel model)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Orders/Create", JsonContent.Create(model)).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public OrdersListModel GetAllOrders()
        {
            HttpResponseMessage responseMessage = ReadAllAsync("Orders").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
        }

        public OrdersListModel GetOrders(string filterName, int startOffset, int endOffset)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/Read/{filterName}/startOffset/{startOffset}/endOffset/{endOffset}").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
        }

        public ModifyOrderModel GetOrder(int id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/Read/{id}").Result;
            return responseMessage.Content.ReadFromJsonAsync<ModifyOrderModel>().Result;
        }

        public OrdersListModel GetOrdersByYesterday()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/ReadByYesterday").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
        }

        public OrdersListModel GetOrdersByWeek()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/ReadByWeek").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
        }


        public void UpdateOrder(UpdateOrderModel model, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync($"api/Orders/Update", JsonContent.Create(model)).Result;
            if (!responseMessage.IsSuccessStatusCode)
                throw new DatabaseServiceException((int)responseMessage.StatusCode, responseMessage.Content.ReadAsStringAsync().Result);
        }
        #endregion

        #region Методы для Services
        public void AddService(ServiceModel model, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Services/Create", JsonContent.Create(model)).Result;
            if (!responseMessage.IsSuccessStatusCode)
                throw new DatabaseServiceException((int)responseMessage.StatusCode, responseMessage.Content.ReadAsStringAsync().Result);
        }

        public ServiceModel GetService(int id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Services/Read/{id}").Result;
            return responseMessage.Content.ReadFromJsonAsync<ServiceModel>().Result;
        }

        public ServiceModel[] GetAllServices()
        {
            HttpResponseMessage responseMessage = ReadAllAsync("Services").Result;
            return responseMessage.Content.ReadFromJsonAsync<ServiceModel[]>().Result;
        }

        public void UpdateService(ServiceModel model, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync("api/Services/Update", JsonContent.Create(model)).Result;
        }

        public void DeleteService(int id, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Services/Delete/{id}").Result;
        }
        #endregion

        #region Методы для Projects
        public bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            fileContent.Add(new StringContent(model.ProjectTitle), "ProjectTitle");
            fileContent.Add(new StringContent(model.ProjectDescr), "ProjectDescr");
            fileContent.Add(streamContent, name: "file", fileName: fileName);

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Projects/Create", fileContent).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public ProjectModel GetProject(int id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Projects/Read/{id}").Result;
            return responseMessage.Content.ReadFromJsonAsync<ProjectModel>().Result;
        }

        public ProjectModel[] GetAllProjects()
        {
            HttpResponseMessage responseMessage = ReadAllAsync("Projects").Result;
            return responseMessage.Content.ReadFromJsonAsync<ProjectModel[]>().Result;
        }

        public bool EditProject(ProjectModel model, string contentType, Stream fileStream, string fileName, string token)
        {
            StreamContent streamContent;

            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            if (fileStream != null)
            {
                streamContent = new StreamContent(fileStream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
                fileContent.Add(streamContent, name: "file", fileName: "blog-1.jpg");
            }

            fileContent.Add(new StringContent($"{model.Id}"), "Id");
            fileContent.Add(new StringContent(model.ProjectTitle), "ProjectTitle");
            fileContent.Add(new StringContent(model.ProjectDescr), "ProjectDescr");

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Projects/Edit", fileContent).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public void DeleteProject(int id, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Projects/Delete/{id}").Result;
        }
        #endregion


        private Task<HttpResponseMessage> ReadAllAsync(string controllerName)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            return httpClient.GetAsync($"api/{controllerName}/ReadAll");
        }

        private Task<HttpResponseMessage> Delete(string controllerName, int entityId, string token)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return httpClient.DeleteAsync($"api/{controllerName}/Delete/{entityId}");
        }

        private Task<HttpResponseMessage> FindAsync(string controllerName, int entityId)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            return httpClient.GetAsync($"api/{controllerName}/Read/{entityId}");
        }

        public void UpdateSocials(string[] links, IFormFileCollection files, string token)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            foreach (IFormFile file in files)
            {
                StreamContent content = new StreamContent(file.OpenReadStream());
                content.Headers.ContentType = new MediaTypeHeaderValue(file.ContentType);
                fileContent.Add(content, name: "file", fileName: file.FileName);
            }

            for (int i = 0; i < links.Length; i++)
            {
                fileContent.Add(new StringContent(links[i]), $"Links[{i}]");
            }

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Contacts/Update", fileContent).Result;
        }

        public SocialModel[] GetAllSocials()
        {
            HttpResponseMessage responseMessage = ReadAllAsync("Contacts").Result;
            return responseMessage.Content.ReadFromJsonAsync<SocialModel[]>().Result;
        }
    }
}
