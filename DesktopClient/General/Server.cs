using System;
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

        internal OrdersListModel GetAllOrders()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Orders/ReadAll").Result;
            if (responseMessage.IsSuccessStatusCode)
                return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
            else return default;
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

        internal void UpdateOrder(UpdateOrderModel model)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri(ConfigurationManager.AppSettings.Get("api-location"));
            _ = httpClient.PostAsync($"api/Orders/Update", JsonContent.Create(model)).Result;
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
            _ = httpClient.PostAsync("api/Projects/Edit", fileContent).Result;
        }
    }
}
