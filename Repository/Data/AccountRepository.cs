using API.Context;
using API.Handlers;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class AccountRepository
    {
        private MyContext myContext;
        public AccountRepository(MyContext myContext)
        {
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
                List<string> result = new List<string>();
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
