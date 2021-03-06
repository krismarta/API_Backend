using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : Base.BasesController<Employee, EmployeeRepository, string>
    {
        private EmployeeRepository employeeRepository;
        public IConfiguration _configuration;
        private readonly MyContext context;
        public EmployeesController(EmployeeRepository employeeRepository, IConfiguration configuration, MyContext myContext) : base(employeeRepository)
        {
            this.employeeRepository = employeeRepository;
            this._configuration = configuration;
            context = myContext;

        }

        [HttpPost("Register")]
        public ActionResult Post(RegisterVM entity)
        {
            var result = employeeRepository.Register(entity);
            if (result == 1)
            {
                //return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });
                return Ok(result);
            }
            else if (result == 2)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Nik telah digunakan diakun lain" });
            }
            else if (result == 3)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Email telah digunakan diakun lain" });
            }
            else if (result == 4)
            {
                return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "Phone telah digunakan diakun lain" });
            }
            return Conflict(new { status = HttpStatusCode.Conflict, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });
        }

        
        [HttpGet("getRegister")]
        //[Authorize(Roles = "Director,Manager")]
        public ActionResult GetRegister()
        {
            var result = employeeRepository.GetRegister();
            // getuserrole (User.Identity.IsAuthenticated / true or false
            // get role User.IsInRole("namaRolenya")
            return Ok(result);
            //return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Fetch data real" });

        }

        [HttpGet("profile/{key}")]
        public ActionResult GetProfile(string key)
        {
            var result = employeeRepository.GetProfile(key);
            if (result == null)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, Message = "Data tidak ditemukan" });
            }

            //return Ok(new { status = HttpStatusCode.OK, result = result, Message = "Data berhasil ditampilkan" });
            return Ok(result);
        }
        
        [HttpPost("SignManager")]
        [Authorize(Roles = "Director,Manager")]
        public ActionResult SignManager(SignManagerVM signManagerVM)
        {
            var result = employeeRepository.SignManager(signManagerVM);
            if (result == 1)
            {
                //return Ok(new { status = HttpStatusCode.OK, result = result, Message = $"Role Telah diberikan ke {signManagerVM.nik}" });
                return Ok(result);
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, Message = "Gagal Memberikan Role" });
        }

        [HttpGet("TestCors")]
        public ActionResult TestCors()
        {
            return Ok("Test Cors Berhasil");
        }

        [HttpGet("countGender")]
        public ActionResult getCount()
        {
           
            var male = employeeRepository.getCountMale();
            var female = employeeRepository.getCountFemale();

            var series = new List<int>();
            var label = new List<string>();
            series.Add(male);
            series.Add(female);
            label.Add("Male");
            label.Add("Female");

            //var result = new { series = "["+male+","+female+"]", label = "[male,female]" };

            var result = new { series, label };

            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Total Gender" });
        }
    }
}
