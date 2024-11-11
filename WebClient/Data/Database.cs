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

        public bool AddProject(ProjectModel model, string contentType, Stream fileStream, string fileName)
        {
            MultipartFormDataContent form = new MultipartFormDataContent();

            StreamContent streamContent = new StreamContent(fileStream);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);

            form.Add(new StringContent(model.ProjectTitle), "ProjectTitle");
            form.Add(new StringContent(model.ProjectDescr), "ProjectDescr");
            form.Add(streamContent, "File", fileName);

            //Dictionary<string, string> parameters = new Dictionary<string, string>();
            //HttpContent dictionaryItems = new FormUrlEncodedContent(parameters);
            //form.Add(dictionaryItems, "model");

            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync("api/Project/Create", form).Result;
            return responseMessage.IsSuccessStatusCode;
        }
    }
}
