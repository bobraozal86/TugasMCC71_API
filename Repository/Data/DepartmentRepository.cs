using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class DepartmentRepository : IRepository<Department, int>
    {
        private MyContext myContext;
        public DepartmentRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }

        public int Create(Department Enitity)
        {
            myContext.Departments.Add(Enitity);
            var data = myContext.SaveChanges();
            return data;
        }

        public int Delete(int id)
        {
            var data = myContext.Departments.Find(id);
            if (data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }

        public IEnumerable<Department> Get()
        {
            return myContext.Departments.ToList();
        }

        public Department GetById(int id)
        {
            return myContext.Departments.Find(id);
        }

        public int Update(Department Enitity)
        {
            myContext.Entry(Enitity).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
    }
}
