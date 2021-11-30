using API.Repository.Interface;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;

namespace API.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<Entity, Repository, Key> : ControllerBase
        where Entity : class
        where Repository : IRepository<Entity, Key>
    {
        private readonly Repository repository;
        public BasesController(Repository repository)
        {
            this.repository = repository;
        }


        [HttpPost]
        public ActionResult Post(Entity entity)
        {
            var result = repository.Insert(entity);
            if (result >= 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data berhasil ditambahkan" });
            }
            return Conflict(new { status = HttpStatusCode.Conflict, result, messageResult = "Sepertinya terjadi kesalahan, periksa kembali!" });
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = repository.Get();
            if (result.Count() == 0)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = "Sepertinya data masih kosong" });
            }
            return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Semua data berhasil ditampilkan" });
        }

        [HttpDelete("{Key}")]
        public ActionResult Delete(Key key)
        {
            var result = repository.Delete(key);
            if (result >= 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = $"Data {key} berhasil dihapus" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {key} tidak ditemukan." });
        }

        [HttpGet("{Key}")]
        public ActionResult Get(Key key)
        {
            var result = repository.Get(key);
            if (result != null)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Data ditemukan" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result, messageResult = $"Data {key} tidak ditemukan." });
        }

        [HttpPut]
        public ActionResult Put(Entity entity,Key key)
        {
            var result = repository.Update(entity,key);
            if (result >= 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result, messageResult = "Perubahan data Berhasil dilakukan" });
            }
            return BadRequest(new { status = HttpStatusCode.BadRequest, result, messageResult = "terjadi kesalahan pada Perubahan data." });
        }


    }
}
