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

        public bool AddOrder(OrderModel model)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Orders/Create", JsonContent.Create(model)).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public OrdersListModel GetAllOrders()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Orders/ReadAll").Result;
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

        public OrdersListModel GetOrdersByToday()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/ReadByDate").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrdersListModel>().Result;
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


        public bool UpdateOrder(UpdateOrderModel model)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync($"api/Orders/Update", JsonContent.Create(model)).Result;
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
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Projects/ReadAll").Result;
            return responseMessage.Content.ReadFromJsonAsync<ProjectModel[]>().Result;
        }

        public bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            using MultipartFormDataContent fileContent = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");

            fileContent.Add(new StringContent(model.ProjectTitle), "ProjectTitle");
            fileContent.Add(new StringContent(model.ProjectDescr), "ProjectDescr");
            fileContent.Add(streamContent, name: "file", fileName: "blog-1.jpg");

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Projects/Create", fileContent).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public bool EditProject(ProjectModel model, string contentType, Stream fileStream, string fileName)
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
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Projects/Edit", fileContent).Result;
            return responseMessage.IsSuccessStatusCode;
        }

        public void DeleteProject(int id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            _ = httpClient.DeleteAsync($"api/Projects/Delete/{id}").Result;
        }
    }
}
