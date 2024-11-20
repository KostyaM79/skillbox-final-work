using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Json;
using System.Configuration;
using Models;

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
    }
}
