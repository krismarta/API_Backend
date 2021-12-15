using API.Models;
using API.ViewModel;
using Client.Base.Urls;
using Client.ViewModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class AccountRepository : GeneralRepository<Account, string>
    {
        private readonly Address address;
        private readonly string request;
        //private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public AccountRepository(Address address, string request = "accounts/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            //_contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
        }

        public async Task<LoginResponseVM> LoginEmployee(LoginResponseVM entity)
        {
            LoginResponseVM entitys = null;

            StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
            var result = httpClient.PostAsync(address.Link + request + "login", content).Result;
            string apiResponse = await result.Content.ReadAsStringAsync();
            entitys = JsonConvert.DeserializeObject<LoginResponseVM>(apiResponse);
            return entitys;
        }


    }
}
