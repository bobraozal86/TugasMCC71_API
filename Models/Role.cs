using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace API.Models
{
    public class Role
    {
        public Role(int Role_Id, string Name)
        {
            this.Role_Id = Role_Id;
            this.Name = Name;
        }
        public Role()
        {

        }
        [Key]
        public int Role_Id { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public ICollection<User>? Users { get; set; }
    }
}
