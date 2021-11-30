using API.Context;
using API.Models;
using API.Repository.Data;
using API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : Base.BasesController<Account, AccountRepository, string>
    {
        private AccountRepository accountrepository;
        public IConfiguration _configuration;
        private readonly MyContext context;

        public AccountsController(AccountRepository accountRepository,MyContext myContext ,IConfiguration configuration) : base(accountRepository)
        {
            this.accountrepository = accountRepository;
            this._configuration = configuration;
            this.context = myContext;
            
        }

        [HttpPost("login")]
        public ActionResult<LoginVM> GetLogin(LoginVM loginvm)
        {
            var result = accountrepository.Login(loginvm);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email atau nomor telepon salah" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password salah" });
            }
            else if (result == 1)
            {

                var results = accountrepository.GetProfile(loginvm);
                

                var getRole = from e in context.Set<Employee>()
                              where e.Email == loginvm.Email
                              join a in context.Set<Account>() on e.NIK equals a.NIK
                              join ar in context.Set<AccountRole>() on a.NIK equals ar.AccountNIK
                              join r in context.Set<Role>() on ar.RoleId equals r.RoleId
                              select new
                              {
                                  r.Name
                              };

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Email,loginvm.Email)
                    
                };
                foreach (var userRole in getRole)
                {
                    claims.Add(new Claim(ClaimTypes.Role, userRole.Name));
                }
                
                
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                var signin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                    _configuration["Jwt:Issuer"],
                    _configuration["Jwt:Audience"],
                    claims,
                    expires: DateTime.UtcNow.AddMinutes(10),
                    signingCredentials: signin
                    );
                var idtoken = new JwtSecurityTokenHandler().WriteToken(token);
                claims.Add(new Claim("TokenSecurity", idtoken.ToString()));

                return Ok(new {status = HttpStatusCode.OK,result = results,idtoken , message = "Berhasil login"});
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }


        [Authorize]
        [HttpGet("TestJWT")]
        public ActionResult TestJWT()
        {
            return Ok("Test JWT Berhasil");
        }

        [HttpPost("lupa")]
        public ActionResult ForgotPassword(ForgotVM forgotVM)
        {
            var result = accountrepository.ForgotPassword(forgotVM);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Password berhasil diubah, tetapi email tidak terkirim" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Periksa Email kamu" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }

        [HttpPost("changepass")]
        public ActionResult ChangePassword(ChangePasVM changePasVM)
        {
            var result = accountrepository.ChangePassword(changePasVM);
            if (result == 2)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Email tidak ditemukan" });
            }
            else if (result == 3)
            {
                return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = "Konfirmasi password tidak sesuai dengan new password" });
            }
            else if (result == 1)
            {
                return Ok(new { status = HttpStatusCode.OK, result = result, message = "Data Password kamu telah diperbarui" });
            }
            else if (result == 4)
            {
                return Ok(new { status = HttpStatusCode.NotFound, result = result, message = "Password lama tidak sesuai dengan database" });
            }
            return NotFound(new { status = HttpStatusCode.NotFound, result = result, message = $"Data tidak ditemukan" });
        }
    }
}
