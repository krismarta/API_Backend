using API.Context;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UniversitiesController : Base.BasesController<University, UniversityRepository, int>
    {
        private UniversityRepository UniversityRepository;
        private readonly MyContext context;
        public UniversitiesController(UniversityRepository universityRepository,MyContext myContext ) : base(universityRepository)
        {
            this.UniversityRepository = universityRepository;
            context = myContext;
        }
        [HttpGet("getcount")]
        public ActionResult getCount()
        {
            var results = UniversityRepository.GetAllCount();
            var univ = new List<string>();
            var value = new List<int>(); ;
           
            foreach (var item in results)
            {
                univ.Add(Convert.ToString(item.GetType().GetProperty("data").GetValue(item, null)));

                value.Add(Convert.ToInt32(item.GetType().GetProperty("total").GetValue(item, null)));

            }



            //var data = new { data = value };
            var result = new { label = univ, series = value };
            return Ok(new { status = HttpStatusCode.OK,result, messageResult = "Total pegawai dalam kategori university" });
        }
    }
}
