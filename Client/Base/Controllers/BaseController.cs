using Client.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Base.Controllers
{
    public class BaseController<TEntity, TRepository, TId> : Controller
        where TEntity : class
        where TRepository : IRepository<TEntity, TId>
    {
        private readonly TRepository repository;

        public BaseController(TRepository repository)
        {
            this.repository = repository;
        }

        [HttpGet]
        public async Task<JsonResult> GetAll()
        {
            var result = await repository.Get();
            return Json(result);
        }

        [HttpGet]
        public async Task<JsonResult> Get(TId id)
        {
            var result = await repository.Get(id);
            return Json(result);
        }

        [HttpPost]
        public JsonResult Post(TEntity entity)
        {
            var result = repository.Post(entity);
            return Json(result);

        }

        [HttpPut]
        public JsonResult Put(TId id, TEntity entity)
        {
            var result = repository.Put(id, entity);
            return Json(result);
        }

        [HttpDelete]
        public JsonResult Delete(TId id)
        {
            var result = repository.Delete(id);
            return Json(result);
        }
    }
}
