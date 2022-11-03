using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class UserRepository : IRepository<User, int>
    {
        private MyContext myContext;
        public UserRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Create(User Enitity)
        {
            myContext.Users.Add(Enitity);
            var data = myContext.SaveChanges();
            return data;
        }

        public int Delete(int id)
        {
            var data = myContext.Users.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<User> Get()
        {
            return myContext.Users.ToList();
        }

        public User GetById(int id)
        {
            return myContext.Users.Find(id);
        }

        public int Update(User Enitity)
        {
            myContext.Entry(Enitity).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
    }
}
