namespace API.Repository.Interface
{
    public interface IRepository<Entity, Key> where Entity : class
    {
        public IEnumerable<Entity> Get();
        public Entity GetById(Key id);
        public int Create(Entity Enitity);
        public int Update(Entity Enitity);
        public int Delete(Key id);


    }
}
