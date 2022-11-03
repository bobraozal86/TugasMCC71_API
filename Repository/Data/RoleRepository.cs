using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class RoleRepository : IRepository<Role, int>
    {
        private MyContext myContext;
        public RoleRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Create(Role Enitity)
        {
            myContext.Roles.Add(Enitity);
            var data = myContext.SaveChanges();
            return data;
        }

        public int Delete(int id)
        {
            var data = myContext.Roles.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Role> Get()
        {
            return myContext.Roles.ToList();
        }

        public Role GetById(int id)
        {
            return myContext.Roles.Find(id);
        }

        public int Update(Role Enitity)
        {
            myContext.Entry(Enitity).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
    }
}
