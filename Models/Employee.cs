using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Employee
    {
        public Employee(int Employee_Id, string Name, string Email, string Birth_Date)
        {
            this.Employee_Id = Employee_Id;
            this.Name = Name;
            this.Email = Email;
            this.Birth_Date = Birth_Date;
        }
        public Employee()
        {

        }
        [Key]
        public int Employee_Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Birth_Date { get; set; }
    }
}
