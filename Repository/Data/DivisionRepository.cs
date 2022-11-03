using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class DivisionRepository : IRepository<Division, int>
    {
        private MyContext myContext;
        public DivisionRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        //Get All
        public IEnumerable<Division> Get()
        {
            return myContext.Divisions.ToList();
        }
        //Get By Id
        public Division GetById(int id)
        {
            return myContext.Divisions.Find(id);
        }

        //Create
        public int Create(Division division)
        {
            myContext.Divisions.Add(division);
            var data = myContext.SaveChanges();
            return data;
        }
        //Update
        public int Update(Division division)
        {
            myContext.Entry(division).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
        //Delete
        public int Delete(int id)
        {
            var data = myContext.Divisions.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
