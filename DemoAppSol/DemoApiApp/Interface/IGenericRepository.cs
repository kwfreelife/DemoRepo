namespace DemoApiApp.Interface
{
    public interface IGenericRepository<T> where T : class
    {
        IEnumerable<T> GetAll();
        T GetById(object id);
        
        void Save();
        void Add(T entity);
        void Update(T entity);
        void Delete(object id);
    }
}
