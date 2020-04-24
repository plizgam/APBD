using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cw3.Models;
using Cw3.Services;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;

namespace Cw3.Controllers
{
    [ApiController]
    [Route("api/students")]
    public class StudentsController : ControllerBase
    {
        public string ConnectionString = "Data source=db-mssql;Initial Catalog=s18536;Integrated Security=True";
        public readonly IStudentDbService _dbService;
        public IConfiguration _configuration;


        public StudentsController(IStudentDbService dbService, IConfiguration configuration)
        {
            _dbService = dbService;
            _configuration = configuration;
        }


        [HttpPost]
        public IActionResult Login(LoginRequestDto login)
        {

            //db check existing profile
            if (!_dbService.AccountExist(login))
                return StatusCode(401);

            var claims = new[]
           {
                new Claim(ClaimTypes.NameIdentifier, login.User + "0101"),
                new Claim(ClaimTypes.Name, login.User),
                new Claim(ClaimTypes.Role, "employee")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken
            (
                issuer: "Gakko",
                audience: "Students",
                claims: claims,
                expires: DateTime.Now.AddMinutes(10),
                signingCredentials: creds
            );




            return Ok(new
            {
                token = new JwtSecurityTokenHandler().WriteToken(token),
                refreshToken = Guid.NewGuid()
            });
        }

        [HttpPost("refresh-token/{token}")]
        public IActionResult RefreshToken(string refToken)
        {
            //check refresh token in db
            //save to database token



            return Ok();
        }

        [HttpPost]
        [Authorize(Roles="employee")]
        public IActionResult EnrollStudent(Student request)
        {
            return _dbService.EnrollStudent(request);
        }


        [HttpPost]
        [Authorize(Roles="employee")]
        public IActionResult PromoteStudent(EnrollmentRequest request)
        {
            return _dbService.PromoteStudents(request);
        }

        [HttpPost]
        public IActionResult RegisterAccount(LoginRequestDto request)
        {
            return _dbService.RegisterAccount(request);
        }



    }
}