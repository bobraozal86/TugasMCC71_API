using API.Context;
using API.Models;
using API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace API.Repository.Data
{
    public class EmployeeRepository : IRepository<Employee, int>
    {
        private MyContext myContext;
        public EmployeeRepository(MyContext myContext)
        {
            this.myContext = myContext;
        }
        public IEnumerable<Employee> Get()
        {
            return myContext.Employees.ToList();
        }
        public Employee GetById(int id)
        {
            return myContext.Employees.Find(id);
        }
        public int Create(Employee employee)
        {
            myContext.Employees.Add(employee);
            var data = myContext.SaveChanges();
            return data;
        }
        public int Update(Employee employee)
        {
            myContext.Entry(employee).State = EntityState.Modified;
            var data = myContext.SaveChanges();
            return data;
        }
        public int Delete(int id)
        {
            var data = myContext.Employees.Find(id);
            if(data != null)
            {
                myContext.Remove(data);
                var result = myContext.SaveChanges();
                return result;
            }
            return 0;
        }
    }
}
