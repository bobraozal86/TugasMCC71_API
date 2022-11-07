using API.Context;
using API.Handlers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Repository.Data
{
    public class AccountRepository
    {
        private MyContext myContext;
       // public IConfiguration configuration;

        public AccountRepository(MyContext myContext)
        {
           // this.configuration = configuration;
            this.myContext = myContext;
        }
        public List<string> Login (string email, string password)
        {
            var data = myContext.Users
                .Include(x => x.Employee)
                .Include(x => x.Role)
                .SingleOrDefault(x => x.Employee.Email.Equals(email));
            //&& Hashing.ValidatePassword(password, data.Password) == true
            if (data != null && Hashing.ValidatePassword(password, data.Password) == true)
            {
                //var claims = new[]
                //{
                //    new Claim(JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]),
                //    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                //    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                //    new Claim("",data.Id.ToString()),
                //};
                List<string> result = new List<string>();
                //string[] result1 = new string[] { JwtRegisteredClaimNames.Sub, configuration["Jwt:Subject"]};
                //string[] result2 = new string[] { JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString() };
                //string[] result3 = new string[] { JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString() };
                //result.Add(result1.ToString());
                //result.Add(result2.ToString());
                //result.Add(result3.ToString());
                result.Add(data.Id.ToString());
                result.Add(data.Employee.Name);
                result.Add(data.Employee.Email);
                result.Add(data.Role.Name);
                return result;
            }
            return null;
        }
        public int Register(string name, string email, string birthDate, string password)
        {
            var checkMail = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email));
            if (checkMail != null)
            {
                return 0;
            }
            else
            {
                Employee employee = new Employee()
                {
                    Name = name,
                    Email = email,
                    Birth_Date = birthDate
                };
                myContext.Employees.Add(employee);
                var result = myContext.SaveChanges();

                if (result > 0)
                {
                    var id = myContext.Employees.SingleOrDefault(x => x.Email.Equals(email)).Employee_Id;
                    User user = new User()
                    {
                        Password = Hashing.HashPassword(password),
                        Role_Id = 1,
                        Employee_Id = id
                    };
                    myContext.Users.Add(user);
                    var resultUser = myContext.SaveChanges();
                    if (resultUser > 0)
                    {
                        return resultUser;
                    }

                }
            }
            return 0;
        }
        public int ChangePassword(string email, string password, string confirm)
        {
            var data = myContext.Users
               .Include(x => x.Employee)
               .SingleOrDefault(x => x.Employee.Email.Equals(email));
            if (data != null && Hashing.ValidatePassword(password, data.Password) == true)
            {
                data.Password = Hashing.HashPassword(confirm);
                myContext.Entry(data).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                {
                    return resultUser;
                }
            }
            return 0;
        }
        public int ForgotPassword(string email, string confirm)
        {
            var data = myContext.Users
               .Include(x => x.Employee)
               //.Include(x => x.Role)
               .AsNoTracking()
               .SingleOrDefault(x => x.Employee.Email.Equals(email));
            myContext.SaveChanges();
            if (data != null)
            {
                data.Password = Hashing.HashPassword(confirm);
                myContext.Entry(data).State = EntityState.Modified;
                var resultUser = myContext.SaveChanges();
                if (resultUser > 0)
                {
                    return resultUser;
                }
            }
            return 0;
        }
    }
}
