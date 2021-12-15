using API.Models;
using API.ViewModel;
using Client.Base.Controllers;
using Client.Repositories.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class EmployeesController : BaseController<Employee, EmployeeRepository, string>
    {
        private readonly EmployeeRepository employeeRepository;
        public EmployeesController(EmployeeRepository repository) : base(repository)
        {
            this.employeeRepository = repository;
        }

        //[HttpGet]
        //public async Task<JsonResult> getRegister()
        //{
        //    var result = await employeeRepository.GetRegister();
        //    return Json(result);
        //}
        //[HttpPost]
        //public JsonResult PostRegister(RegisterVM entity)
        //{
        //    var result = employeeRepository.PostRegister(entity);
        //    return Json(result);
        //}

        //[HttpGet]
        //public async Task<JsonResult> Register(RegisterVM employee)
        //{
        //    var result = await employeeRepository.GetById(employee.NIK);
        //    return Json(result);
        //}

        public IActionResult index()
        {
            return View();
        }

    }
}
