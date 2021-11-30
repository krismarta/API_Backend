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
            var result = UniversityRepository.GetAllCount();

            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Total pegawai dalam kategori university" });
        }
    }
}
