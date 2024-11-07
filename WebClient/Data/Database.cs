using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using Models;

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

        public OrderFullDataModel[] GetAllOrders()
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync("api/Orders/ReadAll").Result;
            return responseMessage.Content.ReadFromJsonAsync<OrderFullDataModel[]>().Result;
        }

        public ModifyOrderModel GetOrder(int id)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.GetAsync($"api/Orders/Read/{id}").Result;
            return responseMessage.Content.ReadFromJsonAsync<ModifyOrderModel>().Result;
        }

        public bool UpdateOrder(UpdateOrderModel model)
        {
            HttpClient httpClient = httpClientFactory.CreateClient();
            httpClient.BaseAddress = new Uri(configuration["ApiLocation"]);
            HttpResponseMessage responseMessage = httpClient.PostAsync($"api/Orders/Update", JsonContent.Create(model)).Result;
            return responseMessage.IsSuccessStatusCode;
        }
    }
}
