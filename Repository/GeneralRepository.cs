using API.Context;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class GeneralRepository<Entity> : IRepository<Entity, int> where Entity : class
    {
        MyContext myContext;
        public GeneralRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public int Create(Entity entity)
        {
            myContext.Set<Entity>().Add(entity);
            var data = myContext.SaveChanges();
            return data;
        }

        public int Delete(int id)
        {
            var data = GetById(id);
            myContext.Set<Entity>().Remove(data);
            var result = myContext.SaveChanges();
            return result;
        }

        public IEnumerable<Entity> Get()
        {
            return myContext.Set<Entity>().ToList();
        }

        public Entity GetById(int id)
        {
            return myContext.Set<Entity>().Find(id);
        }

        public int Update(Entity entity)
        {
            myContext.Entry(entity).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
    }
}
