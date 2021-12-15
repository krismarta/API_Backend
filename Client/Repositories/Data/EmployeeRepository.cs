using API.Models;
using Client.Base.Urls;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Client.Repositories.Data
{
    public class EmployeeRepository : GeneralRepository<Employee, string>
    {
        private readonly Address address;
        private readonly string request;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly HttpClient httpClient;
        public EmployeeRepository(Address address, string request = "employees/") : base(address, request)
        {
            this.address = address;
            this.request = request;
            _contextAccessor = new HttpContextAccessor();
            httpClient = new HttpClient
            {
                BaseAddress = new Uri(address.Link)
            };
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", _contextAccessor.HttpContext.Session.GetString("JWToken"));
        }


        //public HttpStatusCode PostRegister(RegisterVM entity)
        //{
        //    StringContent content = new StringContent(JsonConvert.SerializeObject(entity), Encoding.UTF8, "application/json");
        //    string myContent = content.ReadAsStringAsync().Result;

        //    var result = httpClient.PostAsync(address.Link + request + "Register", content).Result;
        //    return result.StatusCode;
        //}

        //public async Task<List<RegisterVM>> GetRegister()
        //{
        //    //Employee entity = null;
        //    List<RegisterVM> entity = new List<RegisterVM>();


        //    using (var response = await httpClient.GetAsync(request + "getRegister"))
        //    {

        //        string apiResponse = await response.Content.ReadAsStringAsync();
        //        entity = JsonConvert.DeserializeObject<List<RegisterVM>>(apiResponse);

        //    }
        //    return entity;
        //}

        

    }
}
