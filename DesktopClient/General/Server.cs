﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Configuration;
using Models;
using System.IO;
using System.Net.Http.Headers;

namespace DesktopClient.General
{
    class Server
    {
        private static string _token;
        private string token;

        internal static void SetToken(string token)
        {
            _token = token;
        }

        internal static Server Create()
        {
            return new Server(_token);
        }

        public Server(string token)
        {
            this.token = token;
        }


        #region Методы для Orders
        
        internal OrdersListModel GetAllOrders()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Orders/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
            else return default;
        }

        public bool AddOrder(OrderModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.PostAsync($"api/Orders/Create", JsonContent.Create(model)).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        internal ModifyOrderModel GetOrder(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/Read/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
                return responseMessage.Content.ReadFromJsonAsync<ModifyOrderModel>().Result;
            else return default;
        }

        #endregion

        #region Методы для Projects
        public void AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            fileContent.Add(new StringContent(model.ProjectTitle), "ProjectTitle");
            fileContent.Add(new StringContent(model.ProjectDescr), "ProjectDescr");
            fileContent.Add(streamContent, name: "file", fileName: fileName);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync("api/Projects/Create", fileContent).Result;
        }

        public ProjectModel[] GetAllProjects()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Projects/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return responseMessage.Content.ReadFromJsonAsync<ProjectModel[]>().Result;
            else return default;
        }

        public async Task<ProjectModel[]> GetAllProjectsAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Projects/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return await responseMessage.Content.ReadFromJsonAsync<ProjectModel[]>();
            else return default;
        }

        public async Task<ProjectModel> GetProjectAsync(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Projects/Read/{id}").Result;
            if (responseMessage.IsSuccessStatusCode)
                return await responseMessage.Content.ReadFromJsonAsync<ProjectModel>();
            else return default;
        }

        public void UpdateProject(ProjectModel model, string contentType, Stream fileStream, string fileName)
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

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync("api/Projects/Edit", fileContent).Result;
        }

        public void DeleteProject(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Projects/Delete/{id}").Result;
        }
        #endregion

        #region Обновление данных
        internal void UpdateOrder(UpdateOrderModel model, string token)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync($"api/Orders/Update", JsonContent.Create(model)).Result;
        }

        #endregion


        internal string Login(LoginModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("auth-api-location"));
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Authenticate/Login", JsonContent.Create(model)).Result;

            if (responseMessage.IsSuccessStatusCode)
            {
                string token = responseMessage.Headers.GetValues("jwt").ToArray()[0];
                return token;
            }
            else return default;
        }

        public async Task<ServiceModel[]> GetAllServicesAsync()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Services/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return await responseMessage.Content.ReadFromJsonAsync<ServiceModel[]>();
            else return default;
        }

        public void AddService(ServiceModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync($"api/Services/Create", JsonContent.Create(model)).Result;
        }

        public void UpdateService(ServiceModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync($"api/Services/Update", JsonContent.Create(model)).Result;
        }

        public void DeleteService(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Services/Delete/{id}").Result;
        }

        public ArticleModel[] ReadAllArticles()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Articles/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return  responseMessage.Content.ReadFromJsonAsync<ArticleModel[]>().Result;
            else return default;
        }

        public void AddArticle(ArticleModel model, string contentType, Stream fileStream, string fileName)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            fileContent.Add(new StringContent(model.ArticleCaption), "ArticleTitle");
            fileContent.Add(new StringContent(model.ArticleText), "ArticleText");
            fileContent.Add(streamContent, name: "file", fileName: fileName);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync("api/Articles/Create", fileContent).Result;
        }

        public void DeleteArticle(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Articles/Delete/{id}").Result;
        }

        public void UpdateArticle(ArticleModel model, string contentType, Stream stream, string fileName)
        {
            StreamContent streamContent;

            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            if (stream != null)
            {
                streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                fileContent.Add(streamContent, name: "file", fileName: fileName);
            }

            fileContent.Add(new StringContent($"{model.Id}"), "Id");
            fileContent.Add(new StringContent(model.ArticleCaption), "ArticleCaption");
            fileContent.Add(new StringContent(model.ArticleText), "ArticleText");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Articles/Update", fileContent).Result;
        }

        public void AddSocial(SocialModel model, string contentType, Stream stream, string fileName)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(stream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue($"image/{contentType}");

            fileContent.Add(new StringContent(model.Link), "Link");
            fileContent.Add(streamContent, name: "file", fileName: fileName);

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.PostAsync("api/Contacts/Create", fileContent).Result;
        }

        public void DeleteSocial(int id)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            _ = httpClient.DeleteAsync($"api/Contacts/Delete/{id}").Result;
        }

        public SocialModel[] GetAllSocials()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Contacts/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return responseMessage.Content.ReadFromJsonAsync<SocialModel[]>().Result;
            else return default;
        }

        public void UpdateSocial(SocialModel model, string contentType, Stream stream, string fileName)
        {
            StreamContent streamContent;

            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            if (stream != null)
            {
                streamContent = new StreamContent(stream);
                streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
                fileContent.Add(streamContent, name: "file", fileName: fileName);
            }

            fileContent.Add(new StringContent($"{model.Id}"), "Id");
            fileContent.Add(new StringContent(model.Link), "Link");

            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["api-location"]);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Contacts/Update", fileContent).Result;
        }
    }
}
