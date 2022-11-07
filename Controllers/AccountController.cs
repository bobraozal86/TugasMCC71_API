using API.Context;
using API.Handlers;
using API.Models;
using API.Repository.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using JwtRegisteredClaimNames = System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private AccountRepository accountRepository;
        public IConfiguration configuration;
        //private UserRepository userRepository;

        public AccountController(AccountRepository accountRepository, IConfiguration configuration)
        {
            this.accountRepository = accountRepository;
            this.configuration = configuration;
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(string email, string password)
        {
            try
            {
                var data = accountRepository.Login(email, password);
                if(data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Data Not Found"
                    });
                }
                else
                {
                    //int change = Convert.ToInt32(data[0]);
                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Id", data[0]),
                        new Claim("FullName", data[1]),
                        new Claim("Email",data[2]),
                        new Claim("Role", data[3])
                    };
                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                                      configuration["Jwt:Issuer"],
                                      configuration["Jwt:Audience"],
                                      claims,
                                      expires: DateTime.UtcNow.AddMinutes(10),
                                      signingCredentials: signIn);
                    return Ok(new JwtSecurityTokenHandler().WriteToken(token));
                    //return Ok(new
                    //{
                    //    StatusCode = 200,
                    //    Message = "Data Found",
                    //    Data = new 
                    //    {
                    //        Id = Convert.ToInt32(data[0]),
                    //        FullName = data[1],
                    //        Email = data[2],
                    //        Role = data[3]
                    //    }
                    //});
                }
            }
            catch(Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }
        [HttpPost]
        [Route("Register")]
        public ActionResult Register(string name, string email, string birthDate, string password)
        {
            try
            {
                var data = accountRepository.Register(name, email, birthDate, password);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Cant Regist data"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success To Register"

                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }

        }

        [HttpPut]
        [Route("ChangePassword")]
        public ActionResult ChangePassword(string email, string password, string confirm)
        {
            try
            {
                var data = accountRepository.ChangePassword(email, password, confirm);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Failed To Change Password"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success To Change Password"

                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("ForgotPassword")]
        public ActionResult ForgotPassword(string email, string confirm)
        {
            try
            {
                var data = accountRepository.ForgotPassword(email, confirm);
                if (data == null)
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Failed To Make New Password"
                    });
                }
                else
                {
                    return Ok(new
                    {
                        StatusCode = 200,
                        Message = "Success To Make New Password"

                    });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = ex.Message
                });
            }
        }

    }
}
