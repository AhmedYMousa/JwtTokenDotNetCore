using EcommApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace EcommApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext context;
        private readonly IConfiguration Configuration;
        public UsersController(AppDbContext context, IConfiguration Configuration)
        {
            this.context = context;
            this.Configuration = Configuration;
        }

        [HttpGet]
        public IEnumerable<User> Get()
        {
            return context.Users.ToList();
        }

        [HttpPost]
        public IActionResult Post(User user)
        {
            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }
        // "api/users/User/id")
        // [HttpGet("User/{id}")]
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest();
            }
        }
        
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var user = context.Users.Find(id);
            if (user != null)
            {
                context.Users.Remove(user);
                context.SaveChanges();
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("token")]
        public string GetToken(LoginViewModel login)
        {
            var user = context.Users.SingleOrDefault(x => x.Email == login.Email && x.Password == login.Password);
            if (user != null)
            {
                var key = Encoding.UTF8.GetBytes(Configuration["AppSettings:JWT_Key"].ToString());
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(new Claim[] {
                        new Claim("username",user.Username),
                        new Claim("role","Admin")
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                         SecurityAlgorithms.HmacSha256Signature)
                };
                var tokenHandler = new JwtSecurityTokenHandler();
                var securityToken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(securityToken);

                return "{token:" + token + "}";
            }
            else
            {
                return null;
            }

        }

    }
}